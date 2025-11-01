using System;
using System.Data;

namespace FarmaSmart.Models
{
    public class M_Product
    {
        #region Atributos privados
        private int _productId;
        private string _sku;
        private string _name;
        private int? _categoryId;
        private string _description;
        private decimal _price;
        private int? _reorderLevel;
        private bool _isActive;
        private DateTime _createdAt;
        private DateTime? _updatedAt;

        // Atributos de manejo de la BD
        private string _mensajeError;
        private string _valorScalar;
        private DataTable _dtResultados;
        #endregion

        #region Atributos públicos
        public int ProductId { get => _productId; set => _productId = value; }
        public string SKU { get => _sku; set => _sku = value; }
        public string Name { get => _name; set => _name = value; }
        public int? CategoryId { get => _categoryId; set => _categoryId = value; }
        public string Description { get => _description; set => _description = value; }
        public decimal Price { get => _price; set => _price = value; }
        public int? ReorderLevel { get => _reorderLevel; set => _reorderLevel = value; }
        public bool IsActive { get => _isActive; set => _isActive = value; }
        public DateTime CreatedAt { get => _createdAt; set => _createdAt = value; }
        public DateTime? UpdatedAt { get => _updatedAt; set => _updatedAt = value; }
        public string MensajeError { get => _mensajeError; set => _mensajeError = value; }
        public string ValorScalar { get => _valorScalar; set => _valorScalar = value; }
        public DataTable DtResultados { get => _dtResultados; set => _dtResultados = value; }
        #endregion
    }
}