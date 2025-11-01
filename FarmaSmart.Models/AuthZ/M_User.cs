using System;
using System.Data;

namespace FarmaSmart.Models
{
    public class M_User
    {
        #region Atributos privados
        private int _userId;
        private string _userName;
        private string _email;
        private string _passwordHash;
        private int? _employeeId;
        private bool _isActive;
        private DateTime _createdAt;
        private DateTime? _updatedAt;
        private DateTime? _lastLoginAt;

        // Atributos de manejo de la BD
        private string _mensajeError;
        private string _valorScalar;
        private DataTable _dtResultados;
        #endregion

        #region Atributos públicos
        public int UserId { get => _userId; set => _userId = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public string Email { get => _email; set => _email = value; }
        public string PasswordHash { get => _passwordHash; set => _passwordHash = value; }
        public int? EmployeeId { get => _employeeId; set => _employeeId = value; }
        public bool IsActive { get => _isActive; set => _isActive = value; }
        public DateTime CreatedAt { get => _createdAt; set => _createdAt = value; }
        public DateTime? UpdatedAt { get => _updatedAt; set => _updatedAt = value; }
        public DateTime? LastLoginAt { get => _lastLoginAt; set => _lastLoginAt = value; }
        public string MensajeError { get => _mensajeError; set => _mensajeError = value; }
        public string ValorScalar { get => _valorScalar; set => _valorScalar = value; }
        public DataTable DtResultados { get => _dtResultados; set => _dtResultados = value; }
        #endregion
    }
}