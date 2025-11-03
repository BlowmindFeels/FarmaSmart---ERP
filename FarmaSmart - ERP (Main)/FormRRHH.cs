using FarmaSmart.BLL;
using FarmaSmart.Models;
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
    public partial class FormRRHH : Form
    {

        private readonly BLL_User userBLL = new BLL_User();
        private M_User userModel = new M_User();

        public FormRRHH()
        {
            InitializeComponent();
            CargarListaUsuarios(); // carga al iniciar
        }

        private void CargarListaUsuarios()
        {
            // Reset del modelo
            userModel = new M_User();

            // Llamada al BLL (rellena userModel.DtResultados o userModel.MensajeError)
            userBLL.Index(ref userModel);

            if (!string.IsNullOrEmpty(userModel.MensajeError))
            {
                MessageBox.Show("Error al cargar usuarios: " + userModel.MensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Asignar DataTable al DataGridView
            DgvUsuarios.DataSource = null;
            DgvUsuarios.DataSource = userModel.DtResultados;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void FormEmpleado_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
