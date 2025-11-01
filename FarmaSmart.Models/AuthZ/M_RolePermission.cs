using System;
using System.Data;

namespace FarmaSmart.Models
{
    public class M_RolePermission
    {
        #region Atributos privados
        private int _roleId;
        private int _permissionId;
        private DateTime _assignedAt;

        // Atributos de manejo de la BD
        private string _mensajeError;
        private string _valorScalar;
        private DataTable _dtResultados;
        #endregion

        #region Atributos públicos
        public int RoleId { get => _roleId; set => _roleId = value; }
        public int PermissionId { get => _permissionId; set => _permissionId = value; }
        public DateTime AssignedAt { get => _assignedAt; set => _assignedAt = value; }
        public string MensajeError { get => _mensajeError; set => _mensajeError = value; }
        public string ValorScalar { get => _valorScalar; set => _valorScalar = value; }
        public DataTable DtResultados { get => _dtResultados; set => _dtResultados = value; }
        #endregion
    }
}