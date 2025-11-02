using FarmaSmart.Models;
using FarmaSmartERP.DAL;
using System;
using System.Data;

namespace FarmaSmart.BLL
{
    public class BLL_User
    {
        #region Variables privadas
        private FarmaSmartContext ObjDB = null;
        #endregion

        #region Método index
        public void Index(ref M_User user)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Users",
                NombreSP = "[SP_Users_Index]",
                Scalar = false
            };
            Ejecutar(ref user);
        }
        #endregion

        #region CRUD Users
        public void Create(ref M_User user)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Users",
                NombreSP = "[SP_Users_Create]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@UserName", "17", user.UserName);
            ObjDB.Dt.Rows.Add(@"@Email", "17", user.Email);
            ObjDB.Dt.Rows.Add(@"@PasswordHash", "17", user.PasswordHash);
            ObjDB.Dt.Rows.Add(@"@EmployeeId", "4", user.EmployeeId);
            ObjDB.Dt.Rows.Add(@"@IsActive", "1", user.IsActive);

            Ejecutar(ref user);
        }

        public void Read(ref M_User user)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Users",
                NombreSP = "[SP_Users_Read]",
                Scalar = false
            };

            ObjDB.Dt.Rows.Add(@"@UserId", "4", user.UserId);
            Ejecutar(ref user);
        }

        public void Update(ref M_User user)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Users",
                NombreSP = "[SP_Users_Update]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@UserId", "4", user.UserId);
            ObjDB.Dt.Rows.Add(@"@UserName", "17", user.UserName);
            ObjDB.Dt.Rows.Add(@"@Email", "17", user.Email);
            ObjDB.Dt.Rows.Add(@"@PasswordHash", "17", user.PasswordHash);
            ObjDB.Dt.Rows.Add(@"@EmployeeId", "4", user.EmployeeId);
            ObjDB.Dt.Rows.Add(@"@IsActive", "1", user.IsActive);

            Ejecutar(ref user);
        }

        public void Delete(ref M_User user)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Users",
                NombreSP = "[SP_Users_Delete]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@UserId", "4", user.UserId);
            Ejecutar(ref user);
        }
        #endregion

        #region Métodos privados
        private void Ejecutar(ref M_User user)
        {
            ObjDB.CRUD(ref ObjDB);

            if (ObjDB.MensajeErrorDB == null)
            {
                if (ObjDB.Scalar)
                {
                    user.ValorScalar = ObjDB.ValorScalar;
                }
                else
                {
                    user.DtResultados = ObjDB.Ds.Tables[0];
                    if (user.DtResultados.Rows.Count == 1)
                    {
                        var item = user.DtResultados.Rows[0];
                        user.UserId = Convert.ToInt32(item["UserId"]);
                        user.UserName = item["UserName"].ToString();
                        user.Email = item["Email"].ToString();
                        user.PasswordHash = item["PasswordHash"].ToString();
                        user.EmployeeId = item["EmployeeId"] == DBNull.Value ? (int?)null : Convert.ToInt32(item["EmployeeId"]);
                        user.IsActive = Convert.ToBoolean(item["IsActive"]);
                        user.CreatedAt = Convert.ToDateTime(item["CreatedAt"]);
                        user.UpdatedAt = item["UpdatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(item["UpdatedAt"]);
                        user.LastLoginAt = item["LastLoginAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(item["LastLoginAt"]);
                    }
                }
            }
            else
            {
                user.MensajeError = ObjDB.MensajeErrorDB;
            }
        }
        #endregion
    }
}