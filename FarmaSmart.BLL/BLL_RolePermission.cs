using FarmaSmart.Models;
using FarmaSmartERP.DAL;
using System;
using System.Data;

namespace FarmaSmart.BLL
{
    public class BLL_RolePermission
    {
        #region Variables privadas
        private FarmaSmartContext ObjDB = null;
        #endregion

        #region Método index
        public void Index(ref M_RolePermission rp)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "RolePermissions",
                NombreSP = "[SP_RolePermissions_Index]",
                Scalar = false
            };
            Ejecutar(ref rp);
        }
        #endregion

        #region CRUD RolePermissions
        public void Create(ref M_RolePermission rp)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "RolePermissions",
                NombreSP = "[SP_RolePermissions_Create]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@RoleId", "4", rp.RoleId);
            ObjDB.Dt.Rows.Add(@"@PermissionId", "4", rp.PermissionId);
            Ejecutar(ref rp);
        }

        public void Read(ref M_RolePermission rp)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "RolePermissions",
                NombreSP = "[SP_RolePermissions_Read]",
                Scalar = false
            };

            ObjDB.Dt.Rows.Add(@"@RoleId", "4", rp.RoleId);
            ObjDB.Dt.Rows.Add(@"@PermissionId", "4", rp.PermissionId);
            Ejecutar(ref rp);
        }

        public void Delete(ref M_RolePermission rp)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "RolePermissions",
                NombreSP = "[SP_RolePermissions_Delete]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@RoleId", "4", rp.RoleId);
            ObjDB.Dt.Rows.Add(@"@PermissionId", "4", rp.PermissionId);
            Ejecutar(ref rp);
        }
        #endregion

        #region Métodos privados
        private void Ejecutar(ref M_RolePermission rp)
        {
            ObjDB.CRUD(ref ObjDB);

            if (ObjDB.MensajeErrorDB == null)
            {
                if (ObjDB.Scalar)
                {
                    rp.ValorScalar = ObjDB.ValorScalar;
                }
                else
                {
                    rp.DtResultados = ObjDB.Ds.Tables[0];
                    if (rp.DtResultados.Rows.Count == 1)
                    {
                        var item = rp.DtResultados.Rows[0];
                        rp.RoleId = Convert.ToInt32(item["RoleId"]);
                        rp.PermissionId = Convert.ToInt32(item["PermissionId"]);
                        rp.AssignedAt = Convert.ToDateTime(item["AssignedAt"]);
                    }
                }
            }
            else
            {
                rp.MensajeError = ObjDB.MensajeErrorDB;
            }
        }
        #endregion
    }
}