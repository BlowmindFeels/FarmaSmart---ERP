using FarmaSmart.Models;
using FarmaSmartERP.DAL;
using System;
using System.Data;

namespace FarmaSmart.BLL
{
    public class BLL_Product
    {
        #region Variables privadas
        private FarmaSmartContext ObjDB = null;
        #endregion

        #region Método index
        public void Index(ref M_Product product)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Products",
                NombreSP = "[SP_Products_Index]",
                Scalar = false
            };
            Ejecutar(ref product);
        }
        #endregion

        #region CRUD Products
        public void Create(ref M_Product product)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Products",
                NombreSP = "[SP_Products_Create]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@SKU", "17", product.SKU);
            ObjDB.Dt.Rows.Add(@"@Name", "17", product.Name);
            ObjDB.Dt.Rows.Add(@"@CategoryId", "4", product.CategoryId);
            ObjDB.Dt.Rows.Add(@"@Description", "17", product.Description);
            ObjDB.Dt.Rows.Add(@"@Price", "6", product.Price);
            ObjDB.Dt.Rows.Add(@"@ReorderLevel", "4", product.ReorderLevel);
            ObjDB.Dt.Rows.Add(@"@IsActive", "1", product.IsActive);

            Ejecutar(ref product);
        }

        public void Read(ref M_Product product)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Products",
                NombreSP = "[SP_Products_Read]",
                Scalar = false
            };

            ObjDB.Dt.Rows.Add(@"@ProductId", "4", product.ProductId);
            Ejecutar(ref product);
        }

        public void Update(ref M_Product product)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Products",
                NombreSP = "[SP_Products_Update]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@ProductId", "4", product.ProductId);
            ObjDB.Dt.Rows.Add(@"@SKU", "17", product.SKU);
            ObjDB.Dt.Rows.Add(@"@Name", "17", product.Name);
            ObjDB.Dt.Rows.Add(@"@CategoryId", "4", product.CategoryId);
            ObjDB.Dt.Rows.Add(@"@Description", "17", product.Description);
            ObjDB.Dt.Rows.Add(@"@Price", "6", product.Price);
            ObjDB.Dt.Rows.Add(@"@ReorderLevel", "4", product.ReorderLevel);
            ObjDB.Dt.Rows.Add(@"@IsActive", "1", product.IsActive);

            Ejecutar(ref product);
        }

        public void Delete(ref M_Product product)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Products",
                NombreSP = "[SP_Products_Delete]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@ProductId", "4", product.ProductId);
            Ejecutar(ref product);
        }
        #endregion

        #region Métodos privados
        private void Ejecutar(ref M_Product product)
        {
            ObjDB.CRUD(ref ObjDB);

            if (ObjDB.MensajeErrorDB == null)
            {
                if (ObjDB.Scalar)
                {
                    product.ValorScalar = ObjDB.ValorScalar;
                }
                else
                {
                    product.DtResultados = ObjDB.Ds.Tables[0];
                    if (product.DtResultados.Rows.Count == 1)
                    {
                        var item = product.DtResultados.Rows[0];
                        product.ProductId = Convert.ToInt32(item["ProductId"]);
                        product.SKU = item["SKU"].ToString();
                        product.Name = item["Name"].ToString();
                        product.CategoryId = item["CategoryId"] == DBNull.Value ? (int?)null : Convert.ToInt32(item["CategoryId"]);
                        product.Description = item["Description"].ToString();
                        product.Price = Convert.ToDecimal(item["Price"]);
                        product.ReorderLevel = item["ReorderLevel"] == DBNull.Value ? (int?)null : Convert.ToInt32(item["ReorderLevel"]);
                        product.IsActive = Convert.ToBoolean(item["IsActive"]);
                        product.CreatedAt = Convert.ToDateTime(item["CreatedAt"]);
                        product.UpdatedAt = item["UpdatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(item["UpdatedAt"]);
                    }
                }
            }
            else
            {
                product.MensajeError = ObjDB.MensajeErrorDB;
            }
        }
        #endregion
    }
}