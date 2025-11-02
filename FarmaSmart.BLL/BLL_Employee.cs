using FarmaSmart.Models;
using FarmaSmartERP.DAL;
using System;
using System.Data;

namespace FarmaSmart.BLL
{
    public class BLL_Employee
    {
        #region Variables privadas
        private FarmaSmartContext ObjDB = null;
        #endregion

        #region Método index
        public void Index(ref M_Employee employee)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Employees",
                NombreSP = "[SP_Employees_Index]",
                Scalar = false
            };
            Ejecutar(ref employee);
        }
        #endregion

        #region CRUD Employees
        public void Create(ref M_Employee employee)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Employees",
                NombreSP = "[SP_Employees_Create]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@FirstName", "17", employee.FirstName);
            ObjDB.Dt.Rows.Add(@"@LastName", "17", employee.LastName);
            ObjDB.Dt.Rows.Add(@"@DocumentNumber", "17", employee.DocumentNumber);
            ObjDB.Dt.Rows.Add(@"@Position", "17", employee.Position);
            ObjDB.Dt.Rows.Add(@"@Phone", "17", employee.Phone);
            ObjDB.Dt.Rows.Add(@"@Email", "17", employee.Email);
            ObjDB.Dt.Rows.Add(@"@IsActive", "1", employee.IsActive);

            Ejecutar(ref employee);
        }

        public void Read(ref M_Employee employee)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Employees",
                NombreSP = "[SP_Employees_Read]",
                Scalar = false
            };

            ObjDB.Dt.Rows.Add(@"@EmployeeId", "4", employee.EmployeeId);
            Ejecutar(ref employee);
        }

        public void Update(ref M_Employee employee)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Employees",
                NombreSP = "[SP_Employees_Update]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@EmployeeId", "4", employee.EmployeeId);
            ObjDB.Dt.Rows.Add(@"@FirstName", "17", employee.FirstName);
            ObjDB.Dt.Rows.Add(@"@LastName", "17", employee.LastName);
            ObjDB.Dt.Rows.Add(@"@DocumentNumber", "17", employee.DocumentNumber);
            ObjDB.Dt.Rows.Add(@"@Position", "17", employee.Position);
            ObjDB.Dt.Rows.Add(@"@Phone", "17", employee.Phone);
            ObjDB.Dt.Rows.Add(@"@Email", "17", employee.Email);
            ObjDB.Dt.Rows.Add(@"@IsActive", "1", employee.IsActive);

            Ejecutar(ref employee);
        }

        public void Delete(ref M_Employee employee)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Employees",
                NombreSP = "[SP_Employees_Delete]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@EmployeeId", "4", employee.EmployeeId);
            Ejecutar(ref employee);
        }
        #endregion

        #region Métodos privados
        private void Ejecutar(ref M_Employee employee)
        {
            ObjDB.CRUD(ref ObjDB);

            if (ObjDB.MensajeErrorDB == null)
            {
                if (ObjDB.Scalar)
                {
                    employee.ValorScalar = ObjDB.ValorScalar;
                }
                else
                {
                    employee.DtResultados = ObjDB.Ds.Tables[0];
                    if (employee.DtResultados.Rows.Count == 1)
                    {
                        var item = employee.DtResultados.Rows[0];
                        employee.EmployeeId = Convert.ToInt32(item["EmployeeId"]);
                        employee.FirstName = item["FirstName"].ToString();
                        employee.LastName = item["LastName"].ToString();
                        employee.DocumentNumber = item["DocumentNumber"].ToString();
                        employee.Position = item["Position"].ToString();
                        employee.Phone = item["Phone"].ToString();
                        employee.Email = item["Email"].ToString();
                        employee.IsActive = Convert.ToBoolean(item["IsActive"]);
                        employee.CreatedAt = Convert.ToDateTime(item["CreatedAt"]);
                        employee.UpdatedAt = item["UpdatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(item["UpdatedAt"]);
                    }
                }
            }
            else
            {
                employee.MensajeError = ObjDB.MensajeErrorDB;
            }
        }
        #endregion
    }
}