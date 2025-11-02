using FarmaSmart.Models;
using FarmaSmartERP.DAL;
using System;
using System.Data;

namespace FarmaSmart.BLL
{
    public class BLL_Supplier
    {
        #region Variables privadas
        private FarmaSmartContext ObjDB = null;
        #endregion

        #region Método index
        public void Index(ref M_Supplier supplier)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Suppliers",
                NombreSP = "[SP_Suppliers_Index]",
                Scalar = false
            };
            Ejecutar(ref supplier);
        }
        #endregion

        #region CRUD Suppliers
        public void Create(ref M_Supplier supplier)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Suppliers",
                NombreSP = "[SP_Suppliers_Create]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@TaxId", "17", supplier.TaxId);
            ObjDB.Dt.Rows.Add(@"@CompanyName", "17", supplier.CompanyName);
            ObjDB.Dt.Rows.Add(@"@ContactName", "17", supplier.ContactName);
            ObjDB.Dt.Rows.Add(@"@Phone", "17", supplier.Phone);
            ObjDB.Dt.Rows.Add(@"@Email", "17", supplier.Email);
            ObjDB.Dt.Rows.Add(@"@Address", "17", supplier.Address);
            ObjDB.Dt.Rows.Add(@"@IsActive", "1", supplier.IsActive);

            Ejecutar(ref supplier);
        }

        public void Read(ref M_Supplier supplier)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Suppliers",
                NombreSP = "[SP_Suppliers_Read]",
                Scalar = false
            };

            ObjDB.Dt.Rows.Add(@"@SupplierId", "4", supplier.SupplierId);
            Ejecutar(ref supplier);
        }

        public void Update(ref M_Supplier supplier)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Suppliers",
                NombreSP = "[SP_Suppliers_Update]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@SupplierId", "4", supplier.SupplierId);
            ObjDB.Dt.Rows.Add(@"@TaxId", "17", supplier.TaxId);
            ObjDB.Dt.Rows.Add(@"@CompanyName", "17", supplier.CompanyName);
            ObjDB.Dt.Rows.Add(@"@ContactName", "17", supplier.ContactName);
            ObjDB.Dt.Rows.Add(@"@Phone", "17", supplier.Phone);
            ObjDB.Dt.Rows.Add(@"@Email", "17", supplier.Email);
            ObjDB.Dt.Rows.Add(@"@Address", "17", supplier.Address);
            ObjDB.Dt.Rows.Add(@"@IsActive", "1", supplier.IsActive);

            Ejecutar(ref supplier);
        }

        public void Delete(ref M_Supplier supplier)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Suppliers",
                NombreSP = "[SP_Suppliers_Delete]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@SupplierId", "4", supplier.SupplierId);
            Ejecutar(ref supplier);
        }
        #endregion

        #region Métodos privados
        private void Ejecutar(ref M_Supplier supplier)
        {
            ObjDB.CRUD(ref ObjDB);

            if (ObjDB.MensajeErrorDB == null)
            {
                if (ObjDB.Scalar)
                {
                    supplier.ValorScalar = ObjDB.ValorScalar;
                }
                else
                {
                    supplier.DtResultados = ObjDB.Ds.Tables[0];
                    if (supplier.DtResultados.Rows.Count == 1)
                    {
                        var item = supplier.DtResultados.Rows[0];
                        supplier.SupplierId = Convert.ToInt32(item["SupplierId"]);
                        supplier.TaxId = item["TaxId"].ToString();
                        supplier.CompanyName = item["CompanyName"].ToString();
                        supplier.ContactName = item["ContactName"].ToString();
                        supplier.Phone = item["Phone"].ToString();
                        supplier.Email = item["Email"].ToString();
                        supplier.Address = item["Address"].ToString();
                        supplier.IsActive = Convert.ToBoolean(item["IsActive"]);
                        supplier.CreatedAt = Convert.ToDateTime(item["CreatedAt"]);
                        supplier.UpdatedAt = item["UpdatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(item["UpdatedAt"]);
                    }
                }
            }
            else
            {
                supplier.MensajeError = ObjDB.MensajeErrorDB;
            }
        }
        #endregion
    }
}