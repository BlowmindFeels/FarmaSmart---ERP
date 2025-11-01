using System;
using System.Data;

namespace FarmaSmart.Models
{
    public class M_StockMovement
    {
        #region Atributos privados
        private int _movementId;
        private int _productId;
        private string _warehouse;
        private decimal _quantity;
        private string _movementType;
        private string _reference;
        private int? _performedByUserId;
        private DateTime _createdAt;

        // Atributos de manejo de la BD
        private string _mensajeError;
        private string _valorScalar;
        private DataTable _dtResultados;
        #endregion

        #region Atributos públicos
        public int MovementId { get => _movementId; set => _movementId = value; }
        public int ProductId { get => _productId; set => _productId = value; }
        public string Warehouse { get => _warehouse; set => _warehouse = value; }
        public decimal Quantity { get => _quantity; set => _quantity = value; }
        public string MovementType { get => _movementType; set => _movementType = value; }
        public string Reference { get => _reference; set => _reference = value; }
        public int? PerformedByUserId { get => _performedByUserId; set => _performedByUserId = value; }
        public DateTime CreatedAt { get => _createdAt; set => _createdAt = value; }
        public string MensajeError { get => _mensajeError; set => _mensajeError = value; }
        public string ValorScalar { get => _valorScalar; set => _valorScalar = value; }
        public DataTable DtResultados { get => _dtResultados; set => _dtResultados = value; }
        #endregion
    }
}