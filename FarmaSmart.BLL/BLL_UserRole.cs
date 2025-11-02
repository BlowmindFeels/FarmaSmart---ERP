using FarmaSmart.Models;
using FarmaSmartERP.DAL;
using System;
using System.Data;

namespace FarmaSmart.BLL
{
    public class BLL_UserRole
    {
        #region Variables privadas
        private FarmaSmartContext ObjDB = null;
        #endregion

        #region Método index
        public void Index(ref M_UserRole ur)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "UserRoles",
                NombreSP = "[SP_UserRoles_Index]",
                Scalar = false
            };
            Ejecutar(ref ur);
        }
        #endregion

        #region CRUD UserRoles
        public void Create(ref M_UserRole ur)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "UserRoles",
                NombreSP = "[SP_UserRoles_Create]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@UserId", "4", ur.UserId);
            ObjDB.Dt.Rows.Add(@"@RoleId", "4", ur.RoleId);
            Ejecutar(ref ur);
        }

        public void Read(ref M_UserRole ur)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "UserRoles",
                NombreSP = "[SP_UserRoles_Read]",
                Scalar = false
            };

            ObjDB.Dt.Rows.Add(@"@UserId", "4", ur.UserId);
            ObjDB.Dt.Rows.Add(@"@RoleId", "4", ur.RoleId);
            Ejecutar(ref ur);
        }

        public void Delete(ref M_UserRole ur)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "UserRoles",
                NombreSP = "[SP_UserRoles_Delete]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@UserId", "4", ur.UserId);
            ObjDB.Dt.Rows.Add(@"@RoleId", "4", ur.RoleId);
            Ejecutar(ref ur);
        }
        #endregion

        #region Métodos privados
        private void Ejecutar(ref M_UserRole ur)
        {
            ObjDB.CRUD(ref ObjDB);

            if (ObjDB.MensajeErrorDB == null)
            {
                if (ObjDB.Scalar)
                {
                    ur.ValorScalar = ObjDB.ValorScalar;
                }
                else
                {
                    ur.DtResultados = ObjDB.Ds.Tables[0];
                    if (ur.DtResultados.Rows.Count == 1)
                    {
                        var item = ur.DtResultados.Rows[0];
                        ur.UserId = Convert.ToInt32(item["UserId"]);
                        ur.RoleId = Convert.ToInt32(item["RoleId"]);
                        ur.AssignedAt = Convert.ToDateTime(item["AssignedAt"]);
                    }
                }
            }
            else
            {
                ur.MensajeError = ObjDB.MensajeErrorDB;
            }
        }
        #endregion
    }
}