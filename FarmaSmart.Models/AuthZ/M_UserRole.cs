using System;
using System.Data;

namespace FarmaSmart.Models
{
    public class M_UserRole
    {
        #region Atributos privados
        private int _userId;
        private int _roleId;
        private DateTime _assignedAt;

        // Atributos de manejo de la BD
        private string _mensajeError;
        private string _valorScalar;
        private DataTable _dtResultados;
        #endregion

        #region Atributos públicos
        public int UserId { get => _userId; set => _userId = value; }
        public int RoleId { get => _roleId; set => _roleId = value; }
        public DateTime AssignedAt { get => _assignedAt; set => _assignedAt = value; }
        public string MensajeError { get => _mensajeError; set => _mensajeError = value; }
        public string ValorScalar { get => _valorScalar; set => _valorScalar = value; }
        public DataTable DtResultados { get => _dtResultados; set => _dtResultados = value; }
        #endregion
    }
}