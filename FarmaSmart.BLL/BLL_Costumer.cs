using FarmaSmart.Models;
using FarmaSmartERP.DAL;
using System;
using System.Data;

namespace FarmaSmart.BLL
{
    public class BLL_Customer
    {
        #region Variables privadas
        private FarmaSmartContext ObjDB = null;
        #endregion

        #region Método index
        public void Index(ref M_Customer customer)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Customers",
                NombreSP = "[SP_Customers_Index]",
                Scalar = false
            };
            Ejecutar(ref customer);
        }
        #endregion

        #region CRUD Customers
        public void Create(ref M_Customer customer)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Customers",
                NombreSP = "[SP_Customers_Create]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@TaxId", "17", customer.TaxId);
            ObjDB.Dt.Rows.Add(@"@CompanyName", "17", customer.CompanyName);
            ObjDB.Dt.Rows.Add(@"@ContactName", "17", customer.ContactName);
            ObjDB.Dt.Rows.Add(@"@Phone", "17", customer.Phone);
            ObjDB.Dt.Rows.Add(@"@Email", "17", customer.Email);
            ObjDB.Dt.Rows.Add(@"@Address", "17", customer.Address);
            ObjDB.Dt.Rows.Add(@"@IsActive", "1", customer.IsActive);

            Ejecutar(ref customer);
        }

        public void Read(ref M_Customer customer)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Customers",
                NombreSP = "[SP_Customers_Read]",
                Scalar = false
            };

            ObjDB.Dt.Rows.Add(@"@CustomerId", "4", customer.CustomerId);
            Ejecutar(ref customer);
        }

        public void Update(ref M_Customer customer)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Customers",
                NombreSP = "[SP_Customers_Update]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@CustomerId", "4", customer.CustomerId);
            ObjDB.Dt.Rows.Add(@"@TaxId", "17", customer.TaxId);
            ObjDB.Dt.Rows.Add(@"@CompanyName", "17", customer.CompanyName);
            ObjDB.Dt.Rows.Add(@"@ContactName", "17", customer.ContactName);
            ObjDB.Dt.Rows.Add(@"@Phone", "17", customer.Phone);
            ObjDB.Dt.Rows.Add(@"@Email", "17", customer.Email);
            ObjDB.Dt.Rows.Add(@"@Address", "17", customer.Address);
            ObjDB.Dt.Rows.Add(@"@IsActive", "1", customer.IsActive);

            Ejecutar(ref customer);
        }

        public void Delete(ref M_Customer customer)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Customers",
                NombreSP = "[SP_Customers_Delete]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@CustomerId", "4", customer.CustomerId);
            Ejecutar(ref customer);
        }
        #endregion

        #region Métodos privados
        private void Ejecutar(ref M_Customer customer)
        {
            ObjDB.CRUD(ref ObjDB);

            if (ObjDB.MensajeErrorDB == null)
            {
                if (ObjDB.Scalar)
                {
                    customer.ValorScalar = ObjDB.ValorScalar;
                }
                else
                {
                    customer.DtResultados = ObjDB.Ds.Tables[0];
                    if (customer.DtResultados.Rows.Count == 1)
                    {
                        var item = customer.DtResultados.Rows[0];
                        customer.CustomerId = Convert.ToInt32(item["CustomerId"]);
                        customer.TaxId = item["TaxId"].ToString();
                        customer.CompanyName = item["CompanyName"].ToString();
                        customer.ContactName = item["ContactName"].ToString();
                        customer.Phone = item["Phone"].ToString();
                        customer.Email = item["Email"].ToString();
                        customer.Address = item["Address"].ToString();
                        customer.IsActive = Convert.ToBoolean(item["IsActive"]);
                        customer.CreatedAt = Convert.ToDateTime(item["CreatedAt"]);
                        customer.UpdatedAt = item["UpdatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(item["UpdatedAt"]);
                    }
                }
            }
            else
            {
                customer.MensajeError = ObjDB.MensajeErrorDB;
            }
        }
        #endregion
    }
}