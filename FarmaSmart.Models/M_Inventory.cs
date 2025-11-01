using System;
using System.Data;

namespace FarmaSmart.Models
{
    public class M_Inventory
    {
        #region Atributos privados
        private int _inventoryId;
        private int _productId;
        private string _warehouse;
        private decimal _quantity;
        private DateTime _lastUpdatedAt;

        // Atributos de manejo de la BD
        private string _mensajeError;
        private string _valorScalar;
        private DataTable _dtResultados;
        #endregion

        #region Atributos públicos
        public int InventoryId { get => _inventoryId; set => _inventoryId = value; }
        public int ProductId { get => _productId; set => _productId = value; }
        public string Warehouse { get => _warehouse; set => _warehouse = value; }
        public decimal Quantity { get => _quantity; set => _quantity = value; }
        public DateTime LastUpdatedAt { get => _lastUpdatedAt; set => _lastUpdatedAt = value; }
        public string MensajeError { get => _mensajeError; set => _mensajeError = value; }
        public string ValorScalar { get => _valorScalar; set => _valorScalar = value; }
        public DataTable DtResultados { get => _dtResultados; set => _dtResultados = value; }
        #endregion
    }
}