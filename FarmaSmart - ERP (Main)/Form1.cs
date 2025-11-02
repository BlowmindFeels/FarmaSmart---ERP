using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FarmaSmart___ERP__Main_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var ctx = new FarmaSmartERP.DAL.FarmaSmartContext();
            string error;
            if (ctx.ProbarConexion(out error))
            {
                MessageBox.Show("Conexión OK", "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(error, "Error conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
