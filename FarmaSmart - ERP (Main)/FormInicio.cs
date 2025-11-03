using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FarmaSmart___ERP__Main_
{
    public partial class FormInicio : Form
    {

        private Form formularioActivo = null;

        public FormInicio()
        {
            InitializeComponent();
        }

        

        private void AbrirFormularioHijo(Form formHijo)
        {
            if (formularioActivo != null)
                formularioActivo.Close();

            formularioActivo = formHijo;
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill;
            panel4.Controls.Add(formHijo);
            panel4.Tag = formHijo;
            formHijo.BringToFront();
            formHijo.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormRRHH());
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
    }
}