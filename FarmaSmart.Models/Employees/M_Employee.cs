using System;
using System.Data;

namespace FarmaSmart.Models
{
    public class M_Employee
    {
        #region Atributos privados
        private int _employeeId;
        private string _firstName;
        private string _lastName;
        private string _documentNumber;
        private string _position;
        private string _phone;
        private string _email;
        private bool _isActive;
        private DateTime _createdAt;
        private DateTime? _updatedAt;

        // Atributos de manejo de la BD
        private string _mensajeError;
        private string _valorScalar;
        private DataTable _dtResultados;
        #endregion

        #region Atributos públicos
        public int EmployeeId { get => _employeeId; set => _employeeId = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public string DocumentNumber { get => _documentNumber; set => _documentNumber = value; }
        public string Position { get => _position; set => _position = value; }
        public string Phone { get => _phone; set => _phone = value; }
        public string Email { get => _email; set => _email = value; }
        public bool IsActive { get => _isActive; set => _isActive = value; }
        public DateTime CreatedAt { get => _createdAt; set => _createdAt = value; }
        public DateTime? UpdatedAt { get => _updatedAt; set => _updatedAt = value; }
        public string MensajeError { get => _mensajeError; set => _mensajeError = value; }
        public string ValorScalar { get => _valorScalar; set => _valorScalar = value; }
        public DataTable DtResultados { get => _dtResultados; set => _dtResultados = value; }
        #endregion
    }
}