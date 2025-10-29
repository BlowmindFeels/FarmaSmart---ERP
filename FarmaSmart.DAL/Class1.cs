using FarmaSmart.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace FarmaSmartERP.DAL
{
    public class FarmaSmartContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=TU_SERVIDOR;Database=FarmaSmartERP_DB;Trusted_Connection=True;");
        }
    }
}
