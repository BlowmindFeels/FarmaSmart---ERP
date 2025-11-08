using System;
using System.Data;
using FarmaSmart.Models;
using FarmaSmart.DAL;
using FarmaSmartERP.DAL;

namespace FarmaSmart.BLL
{
    public class BLL_Role
    {
        #region Variables privadas

        private FarmaSmartContext ObjDB = null;

        #endregion

        #region Método index

        public void Index(ref M_Role role)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Roles",
                NombreSP = "[SP_Roles_Index]",
                Scalar = false
            };
            Ejecutar(ref role);
        }

        #endregion

        #region CRUD Roles

        public void Create(ref M_Role role)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Roles",
                NombreSP = "[SP_Roles_Create]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@Name", "17", role.Name);
            ObjDB.Dt.Rows.Add(@"@Description", "17", role.Description);
            ObjDB.Dt.Rows.Add(@"@IsActive", "1", role.IsActive);

            Ejecutar(ref role);
        }

        public void Read(ref M_Role role)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Roles",
                NombreSP = "[SP_Roles_Read]",
                Scalar = false
            };

            ObjDB.Dt.Rows.Add(@"@RoleId", "4", role.RoleId);

            Ejecutar(ref role);
        }

        public void Update(ref M_Role role)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Roles",
                NombreSP = "[SP_Roles_Update]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@RoleId", "4", role.RoleId);
            ObjDB.Dt.Rows.Add(@"@Name", "17", role.Name);
            ObjDB.Dt.Rows.Add(@"@Description", "17", role.Description);
            ObjDB.Dt.Rows.Add(@"@IsActive", "1", role.IsActive);

            Ejecutar(ref role);
        }

        public void Delete(ref M_Role role)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Roles",
                NombreSP = "[SP_Roles_Delete]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@RoleId", "4", role.RoleId);

            Ejecutar(ref role);
        }

        #endregion

        #region Métodos privados

        private void Ejecutar(ref M_Role role)
        {
            ObjDB.CRUD(ref ObjDB);

            if (ObjDB.MensajeErrorDB == null)
            {
                if (ObjDB.Scalar)
                {
                    role.ValorScalar = ObjDB.ValorScalar;
                }
                else
                {
                    role.DtResultados = ObjDB.Ds.Tables[0];
                    if (role.DtResultados.Rows.Count == 1)
                    {
                        foreach (DataRow item in role.DtResultados.Rows)
                        {
                            role.RoleId = Convert.ToInt32(item["RoleId"].ToString());
                            role.Name = item["Name"].ToString();
                            role.Description = item["Description"].ToString();
                            role.IsActive = Convert.ToBoolean(item["IsActive"]);
                            role.CreatedAt = Convert.ToDateTime(item["CreatedAt"]);
                        }
                    }
                }
            }
            else
            {
                role.MensajeError = ObjDB.MensajeErrorDB;
            }
        }

        #endregion
    }
}