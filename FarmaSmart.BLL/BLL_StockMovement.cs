using FarmaSmart.Models;
using FarmaSmartERP.DAL;
using System;
using System.Data;

namespace FarmaSmart.BLL
{
    public class BLL_StockMovement
    {
        #region Variables privadas
        private FarmaSmartContext ObjDB = null;
        #endregion

        #region Método index
        public void Index(ref M_StockMovement sm)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "StockMovements",
                NombreSP = "[SP_StockMovements_Index]",
                Scalar = false
            };
            Ejecutar(ref sm);
        }
        #endregion

        #region CRUD StockMovements
        public void Create(ref M_StockMovement sm)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "StockMovements",
                NombreSP = "[SP_StockMovements_Create]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@ProductId", "4", sm.ProductId);
            ObjDB.Dt.Rows.Add(@"@Warehouse", "17", sm.Warehouse);
            ObjDB.Dt.Rows.Add(@"@Quantity", "6", sm.Quantity);
            ObjDB.Dt.Rows.Add(@"@MovementType", "17", sm.MovementType);
            ObjDB.Dt.Rows.Add(@"@Reference", "17", sm.Reference);
            ObjDB.Dt.Rows.Add(@"@PerformedByUserId", "4", sm.PerformedByUserId);

            Ejecutar(ref sm);
        }

        public void Read(ref M_StockMovement sm)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "StockMovements",
                NombreSP = "[SP_StockMovements_Read]",
                Scalar = false
            };

            ObjDB.Dt.Rows.Add(@"@MovementId", "4", sm.MovementId);
            Ejecutar(ref sm);
        }

        public void Update(ref M_StockMovement sm)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "StockMovements",
                NombreSP = "[SP_StockMovements_Update]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@MovementId", "4", sm.MovementId);
            ObjDB.Dt.Rows.Add(@"@ProductId", "4", sm.ProductId);
            ObjDB.Dt.Rows.Add(@"@Warehouse", "17", sm.Warehouse);
            ObjDB.Dt.Rows.Add(@"@Quantity", "6", sm.Quantity);
            ObjDB.Dt.Rows.Add(@"@MovementType", "17", sm.MovementType);
            ObjDB.Dt.Rows.Add(@"@Reference", "17", sm.Reference);
            ObjDB.Dt.Rows.Add(@"@PerformedByUserId", "4", sm.PerformedByUserId);

            Ejecutar(ref sm);
        }

        public void Delete(ref M_StockMovement sm)
        {
            ObjDB = new FarmaSmartContext()
            {
                NombreTabla = "StockMovements",
                NombreSP = "[SP_StockMovements_Delete]",
                Scalar = true
            };

            ObjDB.Dt.Rows.Add(@"@MovementId", "4", sm.MovementId);
            Ejecutar(ref sm);
        }
        #endregion

        #region Métodos privados
        private void Ejecutar(ref M_StockMovement sm)
        {
            ObjDB.CRUD(ref ObjDB);

            if (ObjDB.MensajeErrorDB == null)
            {
                if (ObjDB.Scalar)
                {
                    sm.ValorScalar = ObjDB.ValorScalar;
                }
                else
                {
                    sm.DtResultados = ObjDB.Ds.Tables[0];
                    if (sm.DtResultados.Rows.Count == 1)
                    {
                        var item = sm.DtResultados.Rows[0];
                        sm.MovementId = Convert.ToInt32(item["MovementId"]);
                        sm.ProductId = Convert.ToInt32(item["ProductId"]);
                        sm.Warehouse = item["Warehouse"].ToString();
                        sm.Quantity = Convert.ToDecimal(item["Quantity"]);
                        sm.MovementType = item["MovementType"].ToString();
                        sm.Reference = item["Reference"].ToString();
                        sm.PerformedByUserId = item["PerformedByUserId"] == DBNull.Value ? (int?)null : Convert.ToInt32(item["PerformedByUserId"]);
                        sm.CreatedAt = Convert.ToDateTime(item["CreatedAt"]);
                    }
                }
            }
            else
            {
                sm.MensajeError = ObjDB.MensajeErrorDB;
            }
        }
        #endregion
    }
}