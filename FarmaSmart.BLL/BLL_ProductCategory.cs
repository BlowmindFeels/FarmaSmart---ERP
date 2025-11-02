using FarmaSmart.Models;
using FarmaSmartERP.DAL;
using System;
using System.Data;

namespace FarmaSmart.BLL
{
    public class BLL_ProductCategory
    {
        #region Variables privadas
        private FarmaSmartContext ObjDB = null;
        #endregion

        #region Método index
        public void Index(ref M_ProductCategory cat)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "ProductCategories",
                NombreSP = "[SP_ProductCategories_Index]",
                Scalar = false
            };
            Ejecutar(ref cat);
        }
        #endregion

        #region CRUD Categories
        public void Create(ref M_ProductCategory cat)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "ProductCategories",
                NombreSP = "[SP_ProductCategories_Create]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@Name", "17", cat.Name);
            ObjDB.Dt.Rows.Add(@"@Description", "17", cat.Description);
            ObjDB.Dt.Rows.Add(@"@IsActive", "1", cat.IsActive);

            Ejecutar(ref cat);
        }

        public void Read(ref M_ProductCategory cat)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "ProductCategories",
                NombreSP = "[SP_ProductCategories_Read]",
                Scalar = false
            };

            ObjDB.Dt.Rows.Add(@"@CategoryId", "4", cat.CategoryId);
            Ejecutar(ref cat);
        }

        public void Update(ref M_ProductCategory cat)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "ProductCategories",
                NombreSP = "[SP_ProductCategories_Update]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@CategoryId", "4", cat.CategoryId);
            ObjDB.Dt.Rows.Add(@"@Name", "17", cat.Name);
            ObjDB.Dt.Rows.Add(@"@Description", "17", cat.Description);
            ObjDB.Dt.Rows.Add(@"@IsActive", "1", cat.IsActive);

            Ejecutar(ref cat);
        }

        public void Delete(ref M_ProductCategory cat)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "ProductCategories",
                NombreSP = "[SP_ProductCategories_Delete]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@CategoryId", "4", cat.CategoryId);
            Ejecutar(ref cat);
        }
        #endregion

        #region Métodos privados
        private void Ejecutar(ref M_ProductCategory cat)
        {
            ObjDB.CRUD(ref ObjDB);

            if (ObjDB.MensajeErrorDB == null)
            {
                if (ObjDB.Scalar)
                {
                    cat.ValorScalar = ObjDB.ValorScalar;
                }
                else
                {
                    cat.DtResultados = ObjDB.Ds.Tables[0];
                    if (cat.DtResultados.Rows.Count == 1)
                    {
                        var item = cat.DtResultados.Rows[0];
                        cat.CategoryId = Convert.ToInt32(item["CategoryId"]);
                        cat.Name = item["Name"].ToString();
                        cat.Description = item["Description"].ToString();
                        cat.IsActive = Convert.ToBoolean(item["IsActive"]);
                        cat.CreatedAt = Convert.ToDateTime(item["CreatedAt"]);
                    }
                }
            }
            else
            {
                cat.MensajeError = ObjDB.MensajeErrorDB;
            }
        }
        #endregion
    }
}