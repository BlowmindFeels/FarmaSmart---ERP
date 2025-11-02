using FarmaSmart.Models;
using FarmaSmartERP.DAL;
using System;
using System.Data;

namespace FarmaSmart.BLL
{
    public class BLL_Inventory
    {
        #region Variables privadas
        private FarmaSmartContext ObjDB = null;
        #endregion

        #region Método index
        public void Index(ref M_Inventory inv)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Inventory",
                NombreSP = "[SP_Inventory_Index]",
                Scalar = false
            };
            Ejecutar(ref inv);
        }
        #endregion

        #region CRUD Inventory
        public void Create(ref M_Inventory inv)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Inventory",
                NombreSP = "[SP_Inventory_Create]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@ProductId", "4", inv.ProductId);
            ObjDB.Dt.Rows.Add(@"@Warehouse", "17", inv.Warehouse);
            ObjDB.Dt.Rows.Add(@"@Quantity", "6", inv.Quantity);

            Ejecutar(ref inv);
        }

        public void Read(ref M_Inventory inv)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Inventory",
                NombreSP = "[SP_Inventory_Read]",
                Scalar = false
            };

            ObjDB.Dt.Rows.Add(@"@InventoryId", "4", inv.InventoryId);
            Ejecutar(ref inv);
        }

        public void Update(ref M_Inventory inv)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Inventory",
                NombreSP = "[SP_Inventory_Update]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@InventoryId", "4", inv.InventoryId);
            ObjDB.Dt.Rows.Add(@"@ProductId", "4", inv.ProductId);
            ObjDB.Dt.Rows.Add(@"@Warehouse", "17", inv.Warehouse);
            ObjDB.Dt.Rows.Add(@"@Quantity", "6", inv.Quantity);

            Ejecutar(ref inv);
        }

        public void Delete(ref M_Inventory inv)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "Inventory",
                NombreSP = "[SP_Inventory_Delete]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@InventoryId", "4", inv.InventoryId);
            Ejecutar(ref inv);
        }
        #endregion

        #region Métodos privados
        private void Ejecutar(ref M_Inventory inv)
        {
            ObjDB.CRUD(ref ObjDB);

            if (ObjDB.MensajeErrorDB == null)
            {
                if (ObjDB.Scalar)
                {
                    inv.ValorScalar = ObjDB.ValorScalar;
                }
                else
                {
                    inv.DtResultados = ObjDB.Ds.Tables[0];
                    if (inv.DtResultados.Rows.Count == 1)
                    {
                        var item = inv.DtResultados.Rows[0];
                        inv.InventoryId = Convert.ToInt32(item["InventoryId"]);
                        inv.ProductId = Convert.ToInt32(item["ProductId"]);
                        inv.Warehouse = item["Warehouse"].ToString();
                        inv.Quantity = Convert.ToDecimal(item["Quantity"]);
                        inv.LastUpdatedAt = Convert.ToDateTime(item["LastUpdatedAt"]);
                    }
                }
            }
            else
            {
                inv.MensajeError = ObjDB.MensajeErrorDB;
            }
        }
        #endregion
    }
}