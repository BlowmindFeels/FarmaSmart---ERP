using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FarmaSmart___ERP__Main_
{
    public partial class FormInicio : Form
    {

        private static IconMenuItem menuactivo = null;
        private static Form formularioactivo = null;
        private static IconButton botonActivo = null;


        public FormInicio()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void contenedor_Paint(object sender, PaintEventArgs e)
        {

        }



        private void abrirformulario(IconMenuItem menu, Form formulario)
        {

            if (menuactivo != null)
            {
                menuactivo.BackColor = Color.FromArgb(0, 120, 215); 
                menuactivo.ForeColor = Color.White; 
            }

            menuactivo = menu;
            menu.BackColor = Color.SkyBlue;
            menu.BackColor = Color.SkyBlue; // azul claro activo


            if (formularioactivo != null)
            {
                formularioactivo.Close();
            }

            formularioactivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            contenedor.Controls.Add(formulario);
            formulario.Show();
        }

        private void iconRRHH_Click(object sender, EventArgs e)
        {
 
            
            abrirformulario((IconMenuItem)sender, new FormRRHH());
        }

        private void iconCRM_Click(object sender, EventArgs e)
        {

            abrirformulario((IconMenuItem)sender, new FormCRM());
        }

        private void iconInventario_Click(object sender, EventArgs e)
        {


            abrirformulario((IconMenuItem)sender, new FormInventario());
        }

        private void iconProveedores_Click(object sender, EventArgs e)
        {

            abrirformulario((IconMenuItem)sender, new FormProveedores());
        }
    }
}