using System;
using System.Data;

namespace FarmaSmart.Models
{
    public class M_ProductCategory
    {
        #region Atributos privados
        private int _categoryId;
        private string _name;
        private string _description;
        private bool _isActive;
        private DateTime _createdAt;

        // Atributos de manejo de la BD
        private string _mensajeError;
        private string _valorScalar;
        private DataTable _dtResultados;
        #endregion

        #region Atributos públicos
        public int CategoryId { get => _categoryId; set => _categoryId = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public bool IsActive { get => _isActive; set => _isActive = value; }
        public DateTime CreatedAt { get => _createdAt; set => _createdAt = value; }
        public string MensajeError { get => _mensajeError; set => _mensajeError = value; }
        public string ValorScalar { get => _valorScalar; set => _valorScalar = value; }
        public DataTable DtResultados { get => _dtResultados; set => _dtResultados = value; }
        #endregion
    }
}