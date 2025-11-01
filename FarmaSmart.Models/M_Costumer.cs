using System;
using System.Data;

namespace FarmaSmart.Models
{
    public class M_Customer
    {
        #region Atributos privados
        private int _customerId;
        private string _taxId;
        private string _companyName;
        private string _contactName;
        private string _phone;
        private string _email;
        private string _address;
        private bool _isActive;
        private DateTime _createdAt;
        private DateTime? _updatedAt;

        // Atributos de manejo de la BD
        private string _mensajeError;
        private string _valorScalar;
        private DataTable _dtResultados;
        #endregion

        #region Atributos públicos
        public int CustomerId { get => _customerId; set => _customerId = value; }
        public string TaxId { get => _taxId; set => _taxId = value; }
        public string CompanyName { get => _companyName; set => _companyName = value; }
        public string ContactName { get => _contactName; set => _contactName = value; }
        public string Phone { get => _phone; set => _phone = value; }
        public string Email { get => _email; set => _email = value; }
        public string Address { get => _address; set => _address = value; }
        public bool IsActive { get => _isActive; set => _isActive = value; }
        public DateTime CreatedAt { get => _createdAt; set => _createdAt = value; }
        public DateTime? UpdatedAt { get => _updatedAt; set => _updatedAt = value; }
        public string MensajeError { get => _mensajeError; set => _mensajeError = value; }
        public string ValorScalar { get => _valorScalar; set => _valorScalar = value; }
        public DataTable DtResultados { get => _dtResultados; set => _dtResultados = value; }
        #endregion
    }
}