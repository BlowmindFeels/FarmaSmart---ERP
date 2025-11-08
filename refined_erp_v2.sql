CREATE DATABASE refined_erp_v2;
GO

USE refined_erp_v2;
GO
/******************************************
  SCRIPT SQL COMPLETO REFINADO PARA ERP - SQL Server (VERSIÓN V2)
  - Diseño: Usuarios (solo para empleados) vinculados 1:1 a Empleados.
  - Se crean todas las tablas primero, luego se agregan las relaciones (FK) con ALTER TABLE.
  - Se evitan columnas confusas; NO hay UserId en Customers ni Suppliers.
  - Incluye seeds mínimos para Roles y Permissions.
  - Pensado para importarse en una base de datos nueva en SQL Server.
******************************************/

/* =========================
   1) CREACIÓN DE TABLAS
   ========================= */

-- Roles
CREATE TABLE dbo.Roles (
    RoleId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(255) NULL,
    IsActive BIT NOT NULL DEFAULT (1),
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

-- Permissions
CREATE TABLE dbo.Permissions (
    PermissionId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Code NVARCHAR(100) NOT NULL UNIQUE, -- ej: ManageProducts, ViewInventory
    Description NVARCHAR(255) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

-- RolePermissions (join table N:N, FK agregado más abajo)
CREATE TABLE dbo.RolePermissions (
    RoleId INT NOT NULL,
    PermissionId INT NOT NULL,
    AssignedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_RolePermissions PRIMARY KEY (RoleId, PermissionId)
);

-- Employees (maestro de personal)
CREATE TABLE dbo.Employees (
    EmployeeId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    DocumentNumber NVARCHAR(50) NULL,
    Position NVARCHAR(100) NULL,
    Phone NVARCHAR(50) NULL,
    Email NVARCHAR(255) NULL,
    IsActive BIT NOT NULL DEFAULT (1),
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NULL
);

-- Usuarios (autenticación) - vinculados opcionalmente a EmployeeId (FK más abajo)
CREATE TABLE dbo.Users (
    UserId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    UserName NVARCHAR(150) NOT NULL UNIQUE,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(512) NULL, -- si usas Identity, Identity gestiona esto
    EmployeeId INT NULL, -- vinculacion a Employees (1:1 si se crea índice único después)
    IsActive BIT NOT NULL DEFAULT (1),
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NULL,
    LastLoginAt DATETIME2 NULL
);

-- UserRoles (join table N:N, FK agregado más abajo)
CREATE TABLE dbo.UserRoles (
    UserId INT NOT NULL,
    RoleId INT NOT NULL,
    AssignedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_UserRoles PRIMARY KEY (UserId, RoleId)
);

-- Clientes (no pueden loguear en esta versión)
CREATE TABLE dbo.Customers (
    CustomerId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    TaxId NVARCHAR(50) NULL,
    CompanyName NVARCHAR(200) NOT NULL,
    ContactName NVARCHAR(150) NULL,
    Phone NVARCHAR(50) NULL,
    Email NVARCHAR(255) NULL,
    Address NVARCHAR(400) NULL,
    IsActive BIT NOT NULL DEFAULT (1),
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NULL
);

-- Proveedores (no pueden loguear en esta versión)
CREATE TABLE dbo.Suppliers (
    SupplierId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    TaxId NVARCHAR(50) NULL,
    CompanyName NVARCHAR(200) NOT NULL,
    ContactName NVARCHAR(150) NULL,
    Phone NVARCHAR(50) NULL,
    Email NVARCHAR(255) NULL,
    Address NVARCHAR(400) NULL,
    IsActive BIT NOT NULL DEFAULT (1),
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NULL
);

-- Categorías de producto
CREATE TABLE dbo.ProductCategories (
    CategoryId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name NVARCHAR(150) NOT NULL UNIQUE,
    Description NVARCHAR(400) NULL,
    IsActive BIT NOT NULL DEFAULT (1),
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

-- Productos
CREATE TABLE dbo.Products (
    ProductId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    SKU NVARCHAR(100) NULL UNIQUE,
    Name NVARCHAR(255) NOT NULL,
    CategoryId INT NULL, -- FK agregado más abajo
    Description NVARCHAR(1000) NULL,
    Price DECIMAL(18,4) NOT NULL DEFAULT (0),
    ReorderLevel INT NULL,
    IsActive BIT NOT NULL DEFAULT (1),
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NULL
);

-- Inventario por producto y bodega (warehouse)
CREATE TABLE dbo.Inventory (
    InventoryId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ProductId INT NOT NULL, -- FK agregado más abajo
    Warehouse NVARCHAR(100) NOT NULL DEFAULT ('MAIN'),
    Quantity DECIMAL(18,4) NOT NULL DEFAULT (0),
    LastUpdatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
    -- Unique index ProductId+Warehouse se creará más abajo
);

-- Movimientos de stock (registro de cambios en inventario)
CREATE TABLE dbo.StockMovements (
    MovementId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ProductId INT NOT NULL, -- FK agregado más abajo
    Warehouse NVARCHAR(100) NOT NULL,
    Quantity DECIMAL(18,4) NOT NULL,
    MovementType NVARCHAR(50) NOT NULL, -- 'IN','OUT','ADJUST'
    Reference NVARCHAR(200) NULL,
    PerformedByUserId INT NULL, -- FK agregado más abajo
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

-- =========================
-- 2) RELACIONES (FOREIGN KEYS)
-- =========================

-- RolePermissions -> Roles, Permissions
ALTER TABLE dbo.RolePermissions
    ADD CONSTRAINT FK_RolePermissions_Roles FOREIGN KEY (RoleId)
        REFERENCES dbo.Roles(RoleId);

ALTER TABLE dbo.RolePermissions
    ADD CONSTRAINT FK_RolePermissions_Permissions FOREIGN KEY (PermissionId)
        REFERENCES dbo.Permissions(PermissionId);

-- UserRoles -> Users, Roles
ALTER TABLE dbo.UserRoles
    ADD CONSTRAINT FK_UserRoles_Users FOREIGN KEY (UserId)
        REFERENCES dbo.Users(UserId);

ALTER TABLE dbo.UserRoles
    ADD CONSTRAINT FK_UserRoles_Roles FOREIGN KEY (RoleId)
        REFERENCES dbo.Roles(RoleId);

-- Users.EmployeeId -> Employees.EmployeeId (1:1 lógica por índice único)
ALTER TABLE dbo.Users
    ADD CONSTRAINT FK_Users_Employees FOREIGN KEY (EmployeeId)
        REFERENCES dbo.Employees(EmployeeId);

-- Products.CategoryId -> ProductCategories.CategoryId
ALTER TABLE dbo.Products
    ADD CONSTRAINT FK_Products_Categories FOREIGN KEY (CategoryId)
        REFERENCES dbo.ProductCategories(CategoryId);

-- Inventory.ProductId -> Products.ProductId
ALTER TABLE dbo.Inventory
    ADD CONSTRAINT FK_Inventory_Products FOREIGN KEY (ProductId)
        REFERENCES dbo.Products(ProductId);

-- StockMovements.ProductId -> Products.ProductId
ALTER TABLE dbo.StockMovements
    ADD CONSTRAINT FK_StockMovements_Products FOREIGN KEY (ProductId)
        REFERENCES dbo.Products(ProductId);

-- StockMovements.PerformedByUserId -> Users.UserId
ALTER TABLE dbo.StockMovements
    ADD CONSTRAINT FK_StockMovements_Users FOREIGN KEY (PerformedByUserId)
        REFERENCES dbo.Users(UserId);

-- =========================
-- 3) ÍNDICES / RESTRICCIONES ÚTILES
-- =========================

-- Forzar 1:1 entre Users y Employees: un empleado puede tener a lo sumo 1 usuario
-- Creamos un índice único filtrado sobre EmployeeId (permite NULL)
CREATE UNIQUE INDEX UQ_Users_EmployeeId ON dbo.Users(EmployeeId) WHERE EmployeeId IS NOT NULL;

-- Único por producto+bodega para evitar duplicados en Inventory
CREATE UNIQUE INDEX UQ_Inventory_Product_Warehouse ON dbo.Inventory(ProductId, Warehouse);

-- Índices comunes
CREATE INDEX IX_Users_Email ON dbo.Users(Email);
CREATE INDEX IX_Employees_LastName_FirstName ON dbo.Employees(LastName, FirstName);
CREATE INDEX IX_Products_Name ON dbo.Products(Name);
CREATE INDEX IX_Customers_CompanyName ON dbo.Customers(CompanyName);

-- =========================
-- 4) SEEDS MÍNIMOS (Roles y Permissions)
-- =========================

-- Roles
IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Name = 'Admin')
    INSERT INTO dbo.Roles (Name, Description) VALUES ('Admin', 'Administrador del sistema');

IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Name = 'Bodeguero')
    INSERT INTO dbo.Roles (Name, Description) VALUES ('Bodeguero', 'Gestiona inventario');

IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Name = 'Vendedor')
    INSERT INTO dbo.Roles (Name, Description) VALUES ('Vendedor', 'Gestiona clientes y ventas');

IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Name = 'Comprador')
    INSERT INTO dbo.Roles (Name, Description) VALUES ('Comprador', 'Gestiona compras y proveedores');

-- Permissions
IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Code = 'ManageUsers')
    INSERT INTO dbo.Permissions (Code, Description) VALUES ('ManageUsers', 'Crear/editar/eliminar usuarios');

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Code = 'ManageProducts')
    INSERT INTO dbo.Permissions (Code, Description) VALUES ('ManageProducts', 'Crear/editar/eliminar productos');

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Code = 'ViewInventory')
    INSERT INTO dbo.Permissions (Code, Description) VALUES ('ViewInventory', 'Ver inventario');

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Code = 'ManageSuppliers')
    INSERT INTO dbo.Permissions (Code, Description) VALUES ('ManageSuppliers', 'CRUD proveedores');

-- Asignar todos los permisos al rol Admin (si no existen las relaciones)
INSERT INTO dbo.RolePermissions (RoleId, PermissionId)
SELECT r.RoleId, p.PermissionId
FROM dbo.Roles r
CROSS JOIN dbo.Permissions p
WHERE r.Name = 'Admin'
  AND NOT EXISTS (
      SELECT 1 FROM dbo.RolePermissions rp
      WHERE rp.RoleId = r.RoleId AND rp.PermissionId = p.PermissionId
  );

-- =========================
-- 5) EJEMPLO DE FLUJO (INSERTS) - USO
-- =========================

/*
Flujo recomendado:
1) Crear empleado (maestro)
2) Crear usuario vinculando EmployeeId (si necesita acceso)
3) Asignar roles al usuario

Ejemplo:
*/

-- Crear empleado (solo especificamos las columnas necesarias, las demás tienen DEFAULT)
INSERT INTO dbo.Employees (FirstName, LastName, DocumentNumber, Position, Phone, Email)
VALUES ('Admin', 'Sistema', 'N/A', 'Administrador', '+000', 'admin@empresa.local');

DECLARE @AdminEmployeeId INT = SCOPE_IDENTITY();

-- Crear usuario vinculado al empleado (la contraseña debe ser hasheada por la app)
INSERT INTO dbo.Users (UserName, Email, PasswordHash, EmployeeId)
VALUES ('admin', 'admin@empresa.local', 'HASH_PLACEHOLDER', @AdminEmployeeId);
-- Nota: IsActive y CreatedAt tienen valores por defecto

DECLARE @AdminUserId INT = SCOPE_IDENTITY();

-- Asignar rol Admin al usuario
INSERT INTO dbo.UserRoles (UserId, RoleId)
SELECT @AdminUserId, r.RoleId FROM dbo.Roles r WHERE r.Name = 'Admin';

PRINT 'Usuario administrador creado exitosamente';
PRINT 'EmployeeId: ' + CAST(@AdminEmployeeId AS NVARCHAR(10));
PRINT 'UserId: ' + CAST(@AdminUserId AS NVARCHAR(10));


----------------------------------------
-- CUSTOMERS
----------------------------------------
USE refined_erp_v2
GO


CREATE PROCEDURE [dbo].[SP_Customers_Index]
AS
BEGIN

SELECT [CustomerId]
      ,[TaxId]
      ,[CompanyName]
      ,[ContactName]
      ,[Phone]
      ,[Email]
      ,[Address]
      ,[IsActive]
      ,[CreatedAt]
      ,[UpdatedAt]
  FROM [dbo].[Customers]

END



GO
CREATE PROCEDURE [dbo].[SP_Customers_Create]
(
@TaxId nvarchar(50)
,@CompanyName nvarchar(200)
,@ContactName nvarchar(150)
,@Phone nvarchar(50)
,@Email nvarchar(255)
,@Address nvarchar(400)
,@IsActive bit
,@CreatedAt datetime2(7)
,@UpdatedAt datetime2(7)
)
AS
BEGIN

INSERT INTO [dbo].[Customers]
           ([TaxId]
           ,[CompanyName]
           ,[ContactName]
           ,[Phone]
           ,[Email]
           ,[Address]
           ,[IsActive]
           ,[CreatedAt]
           ,[UpdatedAt])
     VALUES
           (@TaxId
           ,@CompanyName
           ,@ContactName
           ,@Phone
           ,@Email
           ,@Address
           ,@IsActive
           ,@CreatedAt
           ,@UpdatedAt)

SELECT SCOPE_IDENTITY()

END
GO



CREATE PROCEDURE [dbo].[SP_Customers_Read]
(
@CustomerId int
)
AS
BEGIN

SELECT [CustomerId]
      ,[TaxId]
      ,[CompanyName]
      ,[ContactName]
      ,[Phone]
      ,[Email]
      ,[Address]
      ,[IsActive]
      ,[CreatedAt]
      ,[UpdatedAt]
  FROM [dbo].[Customers] WHERE CustomerId = @CustomerId

END
GO



CREATE PROCEDURE [dbo].[SP_Customers_Update]
(
@CustomerId int
,@TaxId nvarchar(50)
,@CompanyName nvarchar(200)
,@ContactName nvarchar(150)
,@Phone nvarchar(50)
,@Email nvarchar(255)
,@Address nvarchar(400)
,@IsActive bit
,@CreatedAt datetime2(7)
,@UpdatedAt datetime2(7)
)
AS
BEGIN

UPDATE [dbo].[Customers]
   SET [TaxId] = @TaxId
      ,[CompanyName] = @CompanyName
      ,[ContactName] = @ContactName
      ,[Phone] = @Phone
      ,[Email] = @Email
      ,[Address] = @Address
      ,[IsActive] = @IsActive
      ,[CreatedAt] = @CreatedAt
      ,[UpdatedAt] = @UpdatedAt
 WHERE CustomerId = @CustomerId

SELECT SCOPE_IDENTITY()

END
GO


CREATE PROCEDURE [dbo].[SP_Customers_Delete]
(
@CustomerId int
)
AS
BEGIN

DELETE
  FROM [dbo].[Customers] WHERE CustomerId = @CustomerId
  
  SELECT 1
END
GO


----------------------------------------
-- ROLES
----------------------------------------
CREATE PROCEDURE [dbo].[SP_Roles_Index]
AS
BEGIN
    SELECT [RoleId]
          ,[Name]
          ,[Description]
          ,[IsActive]
          ,[CreatedAt]
      FROM [dbo].[Roles]
END
GO

CREATE PROCEDURE [dbo].[SP_Roles_Create]
(
    @Name NVARCHAR(100),
    @Description NVARCHAR(255),
    @IsActive BIT,
    @CreatedAt DATETIME2(7)
)
AS
BEGIN
    INSERT INTO [dbo].[Roles]
           ([Name]
           ,[Description]
           ,[IsActive]
           ,[CreatedAt])
     VALUES
           (@Name
           ,@Description
           ,@IsActive
           ,@CreatedAt)

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_Roles_Read]
(
    @RoleId INT
)
AS
BEGIN
    SELECT [RoleId]
          ,[Name]
          ,[Description]
          ,[IsActive]
          ,[CreatedAt]
      FROM [dbo].[Roles]
      WHERE RoleId = @RoleId
END
GO

CREATE PROCEDURE [dbo].[SP_Roles_Update]
(
    @RoleId INT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(255),
    @IsActive BIT,
    @CreatedAt DATETIME2(7)
)
AS
BEGIN
    UPDATE [dbo].[Roles]
       SET [Name] = @Name,
           [Description] = @Description,
           [IsActive] = @IsActive,
           [CreatedAt] = @CreatedAt
     WHERE RoleId = @RoleId

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_Roles_Delete]
(
    @RoleId INT
)
AS
BEGIN
    DELETE FROM [dbo].[Roles] WHERE RoleId = @RoleId
    SELECT 1
END
GO

----------------------------------------
-- PERMISSIONS
----------------------------------------
CREATE PROCEDURE [dbo].[SP_Permissions_Index]
AS
BEGIN
    SELECT [PermissionId]
          ,[Code]
          ,[Description]
          ,[CreatedAt]
      FROM [dbo].[Permissions]
END
GO

CREATE PROCEDURE [dbo].[SP_Permissions_Create]
(
    @Code NVARCHAR(100),
    @Description NVARCHAR(255),
    @CreatedAt DATETIME2(7)
)
AS
BEGIN
    INSERT INTO [dbo].[Permissions]
           ([Code]
           ,[Description]
           ,[CreatedAt])
     VALUES
           (@Code
           ,@Description
           ,@CreatedAt)

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_Permissions_Read]
(
    @PermissionId INT
)
AS
BEGIN
    SELECT [PermissionId]
          ,[Code]
          ,[Description]
          ,[CreatedAt]
      FROM [dbo].[Permissions]
      WHERE PermissionId = @PermissionId
END
GO

CREATE PROCEDURE [dbo].[SP_Permissions_Update]
(
    @PermissionId INT,
    @Code NVARCHAR(100),
    @Description NVARCHAR(255),
    @CreatedAt DATETIME2(7)
)
AS
BEGIN
    UPDATE [dbo].[Permissions]
       SET [Code] = @Code,
           [Description] = @Description,
           [CreatedAt] = @CreatedAt
     WHERE PermissionId = @PermissionId

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_Permissions_Delete]
(
    @PermissionId INT
)
AS
BEGIN
    DELETE FROM [dbo].[Permissions]
    WHERE PermissionId = @PermissionId
    SELECT 1
END
GO

----------------------------------------
-- ROLEPERMISSIONS
----------------------------------------
CREATE PROCEDURE [dbo].[SP_RolePermissions_Index]
AS
BEGIN
    SELECT [RoleId]
          ,[PermissionId]
          ,[AssignedAt]
      FROM [dbo].[RolePermissions]
END
GO

CREATE PROCEDURE [dbo].[SP_RolePermissions_Create]
(
    @RoleId INT,
    @PermissionId INT,
    @AssignedAt DATETIME2(7)
)
AS
BEGIN
    INSERT INTO [dbo].[RolePermissions]
           ([RoleId]
           ,[PermissionId]
           ,[AssignedAt])
     VALUES
           (@RoleId
           ,@PermissionId
           ,@AssignedAt)

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_RolePermissions_Read]
(
    @RoleId INT,
    @PermissionId INT
)
AS
BEGIN
    SELECT [RoleId]
          ,[PermissionId]
          ,[AssignedAt]
      FROM [dbo].[RolePermissions]
      WHERE RoleId = @RoleId AND PermissionId = @PermissionId
END
GO

CREATE PROCEDURE [dbo].[SP_RolePermissions_Update]
(
    @RoleId INT,
    @PermissionId INT,
    @AssignedAt DATETIME2(7)
)
AS
BEGIN
    UPDATE [dbo].[RolePermissions]
       SET [AssignedAt] = @AssignedAt
     WHERE RoleId = @RoleId AND PermissionId = @PermissionId

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_RolePermissions_Delete]
(
    @RoleId INT,
    @PermissionId INT
)
AS
BEGIN
    DELETE FROM [dbo].[RolePermissions]
    WHERE RoleId = @RoleId AND PermissionId = @PermissionId
    SELECT 1
END
GO

----------------------------------------
-- EMPLOYEES
----------------------------------------
CREATE PROCEDURE [dbo].[SP_Employees_Index]
AS
BEGIN
    SELECT [EmployeeId], [FirstName], [LastName], [DocumentNumber],
           [Position], [Phone], [Email], [IsActive],
           [CreatedAt], [UpdatedAt]
    FROM [dbo].[Employees]
END
GO

CREATE PROCEDURE [dbo].[SP_Employees_Create]
(
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @DocumentNumber NVARCHAR(50),
    @Position NVARCHAR(100),
    @Phone NVARCHAR(50),
    @Email NVARCHAR(255),
    @IsActive BIT,
    @CreatedAt DATETIME2(7),
    @UpdatedAt DATETIME2(7)
)
AS
BEGIN
    INSERT INTO [dbo].[Employees]
           ([FirstName],[LastName],[DocumentNumber],[Position],
            [Phone],[Email],[IsActive],[CreatedAt],[UpdatedAt])
     VALUES
           (@FirstName,@LastName,@DocumentNumber,@Position,
            @Phone,@Email,@IsActive,@CreatedAt,@UpdatedAt)

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_Employees_Read]
(
    @EmployeeId INT
)
AS
BEGIN
    SELECT [EmployeeId], [FirstName], [LastName], [DocumentNumber],
           [Position], [Phone], [Email], [IsActive],
           [CreatedAt], [UpdatedAt]
    FROM [dbo].[Employees]
    WHERE EmployeeId = @EmployeeId
END
GO

CREATE PROCEDURE [dbo].[SP_Employees_Update]
(
    @EmployeeId INT,
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @DocumentNumber NVARCHAR(50),
    @Position NVARCHAR(100),
    @Phone NVARCHAR(50),
    @Email NVARCHAR(255),
    @IsActive BIT,
    @CreatedAt DATETIME2(7),
    @UpdatedAt DATETIME2(7)
)
AS
BEGIN
    UPDATE [dbo].[Employees]
       SET [FirstName] = @FirstName,
           [LastName] = @LastName,
           [DocumentNumber] = @DocumentNumber,
           [Position] = @Position,
           [Phone] = @Phone,
           [Email] = @Email,
           [IsActive] = @IsActive,
           [CreatedAt] = @CreatedAt,
           [UpdatedAt] = @UpdatedAt
     WHERE EmployeeId = @EmployeeId

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_Employees_Delete]
(
    @EmployeeId INT
)
AS
BEGIN
    DELETE FROM [dbo].[Employees]
    WHERE EmployeeId = @EmployeeId
    SELECT 1
END
GO

----------------------------------------
-- USERS
----------------------------------------
CREATE PROCEDURE [dbo].[SP_Users_Index]
AS
BEGIN
    SELECT [UserId]
          ,[UserName]
          ,[Email]
          ,[PasswordHash]
          ,[EmployeeId]
          ,[IsActive]
          ,[CreatedAt]
          ,[UpdatedAt]
          ,[LastLoginAt]
      FROM [dbo].[Users]
END
GO

CREATE PROCEDURE [dbo].[SP_Users_Create]
(
    @UserName NVARCHAR(150),
    @Email NVARCHAR(255),
    @PasswordHash NVARCHAR(512),
    @EmployeeId INT,
    @IsActive BIT,
    @CreatedAt DATETIME2(7),
    @UpdatedAt DATETIME2(7),
    @LastLoginAt DATETIME2(7)
)
AS
BEGIN
    INSERT INTO [dbo].[Users]
           ([UserName]
           ,[Email]
           ,[PasswordHash]
           ,[EmployeeId]
           ,[IsActive]
           ,[CreatedAt]
           ,[UpdatedAt]
           ,[LastLoginAt])
     VALUES
           (@UserName
           ,@Email
           ,@PasswordHash
           ,@EmployeeId
           ,@IsActive
           ,@CreatedAt
           ,@UpdatedAt
           ,@LastLoginAt)

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_Users_Read]
(
    @UserId INT
)
AS
BEGIN
    SELECT [UserId]
          ,[UserName]
          ,[Email]
          ,[PasswordHash]
          ,[EmployeeId]
          ,[IsActive]
          ,[CreatedAt]
          ,[UpdatedAt]
          ,[LastLoginAt]
      FROM [dbo].[Users]
      WHERE UserId = @UserId
END
GO

CREATE PROCEDURE [dbo].[SP_Users_Update]
(
    @UserId INT,
    @UserName NVARCHAR(150),
    @Email NVARCHAR(255),
    @PasswordHash NVARCHAR(512),
    @EmployeeId INT,
    @IsActive BIT,
    @CreatedAt DATETIME2(7),
    @UpdatedAt DATETIME2(7),
    @LastLoginAt DATETIME2(7)
)
AS
BEGIN
    UPDATE [dbo].[Users]
       SET [UserName] = @UserName,
           [Email] = @Email,
           [PasswordHash] = @PasswordHash,
           [EmployeeId] = @EmployeeId,
           [IsActive] = @IsActive,
           [CreatedAt] = @CreatedAt,
           [UpdatedAt] = @UpdatedAt,
           [LastLoginAt] = @LastLoginAt
     WHERE UserId = @UserId

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_Users_Delete]
(
    @UserId INT
)
AS
BEGIN
    DELETE FROM [dbo].[Users] WHERE UserId = @UserId
    SELECT 1
END
GO

----------------------------------------
-- USERROLES
----------------------------------------
CREATE PROCEDURE [dbo].[SP_UserRoles_Index]
AS
BEGIN
    SELECT [UserId]
          ,[RoleId]
          ,[AssignedAt]
      FROM [dbo].[UserRoles]
END
GO

CREATE PROCEDURE [dbo].[SP_UserRoles_Create]
(
    @UserId INT,
    @RoleId INT,
    @AssignedAt DATETIME2(7)
)
AS
BEGIN
    INSERT INTO [dbo].[UserRoles]
           ([UserId]
           ,[RoleId]
           ,[AssignedAt])
     VALUES
           (@UserId
           ,@RoleId
           ,@AssignedAt)

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_UserRoles_Read]
(
    @UserId INT,
    @RoleId INT
)
AS
BEGIN
    SELECT [UserId]
          ,[RoleId]
          ,[AssignedAt]
      FROM [dbo].[UserRoles]
      WHERE UserId = @UserId AND RoleId = @RoleId
END
GO

CREATE PROCEDURE [dbo].[SP_UserRoles_Update]
(
    @UserId INT,
    @RoleId INT,
    @AssignedAt DATETIME2(7)
)
AS
BEGIN
    UPDATE [dbo].[UserRoles]
       SET [AssignedAt] = @AssignedAt
     WHERE UserId = @UserId AND RoleId = @RoleId

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_UserRoles_Delete]
(
    @UserId INT,
    @RoleId INT
)
AS
BEGIN
    DELETE FROM [dbo].[UserRoles]
    WHERE UserId = @UserId AND RoleId = @RoleId
    SELECT 1
END
GO


----------------------------------------
-- SUPPLIERS
----------------------------------------
CREATE PROCEDURE [dbo].[SP_Suppliers_Index]
AS
BEGIN
    SELECT [SupplierId], [TaxId], [CompanyName], [ContactName],
           [Phone], [Email], [Address], [IsActive],
           [CreatedAt], [UpdatedAt]
    FROM [dbo].[Suppliers]
END
GO

CREATE PROCEDURE [dbo].[SP_Suppliers_Create]
(
    @TaxId NVARCHAR(50),
    @CompanyName NVARCHAR(200),
    @ContactName NVARCHAR(150),
    @Phone NVARCHAR(50),
    @Email NVARCHAR(255),
    @Address NVARCHAR(400),
    @IsActive BIT,
    @CreatedAt DATETIME2(7),
    @UpdatedAt DATETIME2(7)
)
AS
BEGIN
    INSERT INTO [dbo].[Suppliers]
           ([TaxId],[CompanyName],[ContactName],[Phone],[Email],[Address],
            [IsActive],[CreatedAt],[UpdatedAt])
     VALUES
           (@TaxId,@CompanyName,@ContactName,@Phone,@Email,@Address,
            @IsActive,@CreatedAt,@UpdatedAt)

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_Suppliers_Read]
(
    @SupplierId INT
)
AS
BEGIN
    SELECT [SupplierId], [TaxId], [CompanyName], [ContactName],
           [Phone], [Email], [Address], [IsActive],
           [CreatedAt], [UpdatedAt]
    FROM [dbo].[Suppliers]
    WHERE SupplierId = @SupplierId
END
GO

CREATE PROCEDURE [dbo].[SP_Suppliers_Update]
(
    @SupplierId INT,
    @TaxId NVARCHAR(50),
    @CompanyName NVARCHAR(200),
    @ContactName NVARCHAR(150),
    @Phone NVARCHAR(50),
    @Email NVARCHAR(255),
    @Address NVARCHAR(400),
    @IsActive BIT,
    @CreatedAt DATETIME2(7),
    @UpdatedAt DATETIME2(7)
)
AS
BEGIN
    UPDATE [dbo].[Suppliers]
       SET [TaxId] = @TaxId,
           [CompanyName] = @CompanyName,
           [ContactName] = @ContactName,
           [Phone] = @Phone,
           [Email] = @Email,
           [Address] = @Address,
           [IsActive] = @IsActive,
           [CreatedAt] = @CreatedAt,
           [UpdatedAt] = @UpdatedAt
     WHERE SupplierId = @SupplierId

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_Suppliers_Delete]
(
    @SupplierId INT
)
AS
BEGIN
    DELETE FROM [dbo].[Suppliers]
    WHERE SupplierId = @SupplierId
    SELECT 1
END
GO


----------------------------------------
-- PRODUCTCATEGORIES
----------------------------------------
CREATE PROCEDURE [dbo].[SP_ProductCategories_Index]
AS
BEGIN
    SELECT [CategoryId]
          ,[Name]
          ,[Description]
          ,[IsActive]
          ,[CreatedAt]
      FROM [dbo].[ProductCategories]
END
GO

CREATE PROCEDURE [dbo].[SP_ProductCategories_Create]
(
    @Name NVARCHAR(150),
    @Description NVARCHAR(400),
    @IsActive BIT,
    @CreatedAt DATETIME2(7)
)
AS
BEGIN
    INSERT INTO [dbo].[ProductCategories]
           ([Name]
           ,[Description]
           ,[IsActive]
           ,[CreatedAt])
     VALUES
           (@Name
           ,@Description
           ,@IsActive
           ,@CreatedAt)

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_ProductCategories_Read]
(
    @CategoryId INT
)
AS
BEGIN
    SELECT [CategoryId]
          ,[Name]
          ,[Description]
          ,[IsActive]
          ,[CreatedAt]
      FROM [dbo].[ProductCategories]
      WHERE CategoryId = @CategoryId
END
GO

CREATE PROCEDURE [dbo].[SP_ProductCategories_Update]
(
    @CategoryId INT,
    @Name NVARCHAR(150),
    @Description NVARCHAR(400),
    @IsActive BIT,
    @CreatedAt DATETIME2(7)
)
AS
BEGIN
    UPDATE [dbo].[ProductCategories]
       SET [Name] = @Name,
           [Description] = @Description,
           [IsActive] = @IsActive,
           [CreatedAt] = @CreatedAt
     WHERE CategoryId = @CategoryId

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_ProductCategories_Delete]
(
    @CategoryId INT
)
AS
BEGIN
    DELETE FROM [dbo].[ProductCategories] WHERE CategoryId = @CategoryId
    SELECT 1
END
GO

----------------------------------------
-- PRODUCTS
----------------------------------------
CREATE PROCEDURE [dbo].[SP_Products_Index]
AS
BEGIN
    SELECT [ProductId]
          ,[SKU]
          ,[Name]
          ,[CategoryId]
          ,[Description]
          ,[Price]
          ,[ReorderLevel]
          ,[IsActive]
          ,[CreatedAt]
          ,[UpdatedAt]
      FROM [dbo].[Products]
END
GO

CREATE PROCEDURE [dbo].[SP_Products_Create]
(
    @SKU NVARCHAR(100),
    @Name NVARCHAR(255),
    @CategoryId INT,
    @Description NVARCHAR(1000),
    @Price DECIMAL(18,4),
    @ReorderLevel INT,
    @IsActive BIT,
    @CreatedAt DATETIME2(7),
    @UpdatedAt DATETIME2(7)
)
AS
BEGIN
    INSERT INTO [dbo].[Products]
           ([SKU]
           ,[Name]
           ,[CategoryId]
           ,[Description]
           ,[Price]
           ,[ReorderLevel]
           ,[IsActive]
           ,[CreatedAt]
           ,[UpdatedAt])
     VALUES
           (@SKU
           ,@Name
           ,@CategoryId
           ,@Description
           ,@Price
           ,@ReorderLevel
           ,@IsActive
           ,@CreatedAt
           ,@UpdatedAt)

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_Products_Read]
(
    @ProductId INT
)
AS
BEGIN
    SELECT [ProductId]
          ,[SKU]
          ,[Name]
          ,[CategoryId]
          ,[Description]
          ,[Price]
          ,[ReorderLevel]
          ,[IsActive]
          ,[CreatedAt]
          ,[UpdatedAt]
      FROM [dbo].[Products]
      WHERE ProductId = @ProductId
END
GO

CREATE PROCEDURE [dbo].[SP_Products_Update]
(
    @ProductId INT,
    @SKU NVARCHAR(100),
    @Name NVARCHAR(255),
    @CategoryId INT,
    @Description NVARCHAR(1000),
    @Price DECIMAL(18,4),
    @ReorderLevel INT,
    @IsActive BIT,
    @CreatedAt DATETIME2(7),
    @UpdatedAt DATETIME2(7)
)
AS
BEGIN
    UPDATE [dbo].[Products]
       SET [SKU] = @SKU,
           [Name] = @Name,
           [CategoryId] = @CategoryId,
           [Description] = @Description,
           [Price] = @Price,
           [ReorderLevel] = @ReorderLevel,
           [IsActive] = @IsActive,
           [CreatedAt] = @CreatedAt,
           [UpdatedAt] = @UpdatedAt
     WHERE ProductId = @ProductId

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_Products_Delete]
(
    @ProductId INT
)
AS
BEGIN
    DELETE FROM [dbo].[Products] WHERE ProductId = @ProductId
    SELECT 1
END
GO

----------------------------------------
-- INVENTORY
----------------------------------------
CREATE PROCEDURE [dbo].[SP_Inventory_Index]
AS
BEGIN
    SELECT [InventoryId]
          ,[ProductId]
          ,[Warehouse]
          ,[Quantity]
          ,[LastUpdatedAt]
      FROM [dbo].[Inventory]
END
GO

CREATE PROCEDURE [dbo].[SP_Inventory_Create]
(
    @ProductId INT,
    @Warehouse NVARCHAR(100),
    @Quantity DECIMAL(18,4),
    @LastUpdatedAt DATETIME2(7)
)
AS
BEGIN
    INSERT INTO [dbo].[Inventory]
           ([ProductId]
           ,[Warehouse]
           ,[Quantity]
           ,[LastUpdatedAt])
     VALUES
           (@ProductId
           ,@Warehouse
           ,@Quantity
           ,@LastUpdatedAt)

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_Inventory_Read]
(
    @InventoryId INT
)
AS
BEGIN
    SELECT [InventoryId]
          ,[ProductId]
          ,[Warehouse]
          ,[Quantity]
          ,[LastUpdatedAt]
      FROM [dbo].[Inventory]
      WHERE InventoryId = @InventoryId
END
GO

CREATE PROCEDURE [dbo].[SP_Inventory_Update]
(
    @InventoryId INT,
    @ProductId INT,
    @Warehouse NVARCHAR(100),
    @Quantity DECIMAL(18,4),
    @LastUpdatedAt DATETIME2(7)
)
AS
BEGIN
    UPDATE [dbo].[Inventory]
       SET [ProductId] = @ProductId,
           [Warehouse] = @Warehouse,
           [Quantity] = @Quantity,
           [LastUpdatedAt] = @LastUpdatedAt
     WHERE InventoryId = @InventoryId

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_Inventory_Delete]
(
    @InventoryId INT
)
AS
BEGIN
    DELETE FROM [dbo].[Inventory] WHERE InventoryId = @InventoryId
    SELECT 1
END
GO

----------------------------------------
-- STOCKMOVEMENTS
----------------------------------------
CREATE PROCEDURE [dbo].[SP_StockMovements_Index]
AS
BEGIN
    SELECT [MovementId]
          ,[ProductId]
          ,[Warehouse]
          ,[Quantity]
          ,[MovementType]
          ,[Reference]
          ,[PerformedByUserId]
          ,[CreatedAt]
      FROM [dbo].[StockMovements]
END
GO

CREATE PROCEDURE [dbo].[SP_StockMovements_Create]
(
    @ProductId INT,
    @Warehouse NVARCHAR(100),
    @Quantity DECIMAL(18,4),
    @MovementType NVARCHAR(50),
    @Reference NVARCHAR(200),
    @PerformedByUserId INT,
    @CreatedAt DATETIME2(7)
)
AS
BEGIN
    INSERT INTO [dbo].[StockMovements]
           ([ProductId]
           ,[Warehouse]
           ,[Quantity]
           ,[MovementType]
           ,[Reference]
           ,[PerformedByUserId]
           ,[CreatedAt])
     VALUES
           (@ProductId
           ,@Warehouse
           ,@Quantity
           ,@MovementType
           ,@Reference
           ,@PerformedByUserId
           ,@CreatedAt)

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_StockMovements_Read]
(
    @MovementId INT
)
AS
BEGIN
    SELECT [MovementId]
          ,[ProductId]
          ,[Warehouse]
          ,[Quantity]
          ,[MovementType]
          ,[Reference]
          ,[PerformedByUserId]
          ,[CreatedAt]
      FROM [dbo].[StockMovements]
      WHERE MovementId = @MovementId
END
GO

CREATE PROCEDURE [dbo].[SP_StockMovements_Update]
(
    @MovementId INT,
    @ProductId INT,
    @Warehouse NVARCHAR(100),
    @Quantity DECIMAL(18,4),
    @MovementType NVARCHAR(50),
    @Reference NVARCHAR(200),
    @PerformedByUserId INT,
    @CreatedAt DATETIME2(7)
)
AS
BEGIN
    UPDATE [dbo].[StockMovements]
       SET [ProductId] = @ProductId,
           [Warehouse] = @Warehouse,
           [Quantity] = @Quantity,
           [MovementType] = @MovementType,
           [Reference] = @Reference,
           [PerformedByUserId] = @PerformedByUserId,
           [CreatedAt] = @CreatedAt
     WHERE MovementId = @MovementId

    SELECT SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE [dbo].[SP_StockMovements_Delete]
(
    @MovementId INT
)
AS
BEGIN
    DELETE FROM [dbo].[StockMovements] WHERE MovementId = @MovementId
    SELECT 1
END
GO


-- =========================
-- FIN DEL SCRIPT
-- =========================
 

 SELECT * FROM dbo.Employees;