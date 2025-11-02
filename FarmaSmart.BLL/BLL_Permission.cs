using FarmaSmart.Models;
using FarmaSmartERP.DAL;
using System;
using System.Data;

namespace FarmaSmart.BLL
{
    public class BLL_Permission
    {
        #region Variables privadas
        private FarmaSmartContext ObjDB = null;
        #endregion

        #region Método index
        public void Index(ref M_Permission permission)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Permissions",
                NombreSP = "[SP_Permissions_Index]",
                Scalar = false
            };
            Ejecutar(ref permission);
        }
        #endregion

        #region CRUD Permissions
        public void Create(ref M_Permission permission)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Permissions",
                NombreSP = "[SP_Permissions_Create]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@Code", "17", permission.Code);
            ObjDB.Dt.Rows.Add(@"@Description", "17", permission.Description);

            Ejecutar(ref permission);
        }

        public void Read(ref M_Permission permission)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Permissions",
                NombreSP = "[SP_Permissions_Read]",
                Scalar = false
            };

            ObjDB.Dt.Rows.Add(@"@PermissionId", "4", permission.PermissionId);
            Ejecutar(ref permission);
        }

        public void Update(ref M_Permission permission)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Permissions",
                NombreSP = "[SP_Permissions_Update]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@PermissionId", "4", permission.PermissionId);
            ObjDB.Dt.Rows.Add(@"@Code", "17", permission.Code);
            ObjDB.Dt.Rows.Add(@"@Description", "17", permission.Description);
            Ejecutar(ref permission);
        }

        public void Delete(ref M_Permission permission)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Permissions",
                NombreSP = "[SP_Permissions_Delete]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@PermissionId", "4", permission.PermissionId);
            Ejecutar(ref permission);
        }
        #endregion

        #region Métodos privados
        private void Ejecutar(ref M_Permission permission)
        {
            ObjDB.CRUD(ref ObjDB);

            if (ObjDB.MensajeErrorDB == null)
            {
                if (ObjDB.Scalar)
                {
                    permission.ValorScalar = ObjDB.ValorScalar;
                }
                else
                {
                    permission.DtResultados = ObjDB.Ds.Tables[0];
                    if (permission.DtResultados.Rows.Count == 1)
                    {
                        var item = permission.DtResultados.Rows[0];
                        permission.PermissionId = Convert.ToInt32(item["PermissionId"]);
                        permission.Code = item["Code"].ToString();
                        permission.Description = item["Description"].ToString();
                        permission.CreatedAt = Convert.ToDateTime(item["CreatedAt"]);
                    }
                }
            }
            else
            {
                permission.MensajeError = ObjDB.MensajeErrorDB;
            }
        }
        #endregion
    }
}