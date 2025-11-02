using System;
using System.Data;

namespace FarmaSmart.Models
{
    public class M_Permission
    {
        #region Atributos privados
        private int _permissionId;
        private string _code;
        private string _description;
        private DateTime _createdAt;

        // Atributos de manejo de la BD
        private string _mensajeError;
        private string _valorScalar;
        private DataTable _dtResultados;
        #endregion

        #region Atributos públicos
        public int PermissionId { get => _permissionId; set => _permissionId = value; }
        public string Code { get => _code; set => _code = value; }
        public string Description { get => _description; set => _description = value; }
        public DateTime CreatedAt { get => _createdAt; set => _createdAt = value; }
        public string MensajeError { get => _mensajeError; set => _mensajeError = value; }
        public string ValorScalar { get => _valorScalar; set => _valorScalar = value; }
        public DataTable DtResultados { get => _dtResultados; set => _dtResultados = value; }
        #endregion
    }
}