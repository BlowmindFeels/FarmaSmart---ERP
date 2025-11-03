namespace FarmaSmart___ERP__Main_
{
    partial class FormInicio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel4 = new System.Windows.Forms.Panel();
            this.contenedor = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.iconRRHH = new FontAwesome.Sharp.IconMenuItem();
            this.iconCRM = new FontAwesome.Sharp.IconMenuItem();
            this.iconInventario = new FontAwesome.Sharp.IconMenuItem();
            this.iconProveedores = new FontAwesome.Sharp.IconMenuItem();
            this.panel4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.contenedor);
            this.panel4.Controls.Add(this.menuStrip1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1086, 636);
            this.panel4.TabIndex = 1;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // contenedor
            // 
            this.contenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedor.Location = new System.Drawing.Point(149, 0);
            this.contenedor.Name = "contenedor";
            this.contenedor.Size = new System.Drawing.Size(937, 636);
            this.contenedor.TabIndex = 2;
            this.contenedor.Paint += new System.Windows.Forms.PaintEventHandler(this.contenedor_Paint);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Highlight;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iconRRHH,
            this.iconCRM,
            this.iconInventario,
            this.iconProveedores});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(149, 636);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // iconRRHH
            // 
            this.iconRRHH.AutoSize = false;
            this.iconRRHH.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.iconRRHH.IconChar = FontAwesome.Sharp.IconChar.IdCardAlt;
            this.iconRRHH.IconColor = System.Drawing.Color.White;
            this.iconRRHH.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconRRHH.IconSize = 60;
            this.iconRRHH.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconRRHH.Margin = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.iconRRHH.Name = "iconRRHH";
            this.iconRRHH.Size = new System.Drawing.Size(143, 95);
            this.iconRRHH.Text = "RRHH";
            this.iconRRHH.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconRRHH.Click += new System.EventHandler(this.iconRRHH_Click);
            // 
            // iconCRM
            // 
            this.iconCRM.AutoSize = false;
            this.iconCRM.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.iconCRM.IconChar = FontAwesome.Sharp.IconChar.IdBadge;
            this.iconCRM.IconColor = System.Drawing.Color.White;
            this.iconCRM.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconCRM.IconSize = 60;
            this.iconCRM.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconCRM.Margin = new System.Windows.Forms.Padding(0, 50, 0, 0);
            this.iconCRM.Name = "iconCRM";
            this.iconCRM.Size = new System.Drawing.Size(143, 95);
            this.iconCRM.Text = "CRM";
            this.iconCRM.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconCRM.Click += new System.EventHandler(this.iconCRM_Click);
            // 
            // iconInventario
            // 
            this.iconInventario.AutoSize = false;
            this.iconInventario.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.iconInventario.IconChar = FontAwesome.Sharp.IconChar.DollyFlatbed;
            this.iconInventario.IconColor = System.Drawing.Color.White;
            this.iconInventario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconInventario.IconSize = 60;
            this.iconInventario.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconInventario.Margin = new System.Windows.Forms.Padding(0, 50, 0, 0);
            this.iconInventario.Name = "iconInventario";
            this.iconInventario.Size = new System.Drawing.Size(141, 95);
            this.iconInventario.Text = "Inventario";
            this.iconInventario.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconInventario.Click += new System.EventHandler(this.iconInventario_Click);
            // 
            // iconProveedores
            // 
            this.iconProveedores.AutoSize = false;
            this.iconProveedores.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.iconProveedores.IconChar = FontAwesome.Sharp.IconChar.ContactBook;
            this.iconProveedores.IconColor = System.Drawing.Color.White;
            this.iconProveedores.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconProveedores.IconSize = 60;
            this.iconProveedores.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconProveedores.Margin = new System.Windows.Forms.Padding(0, 50, 0, 0);
            this.iconProveedores.Name = "iconProveedores";
            this.iconProveedores.Size = new System.Drawing.Size(141, 95);
            this.iconProveedores.Text = "Proveedores";
            this.iconProveedores.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconProveedores.Click += new System.EventHandler(this.iconProveedores_Click);
            // 
            // FormInicio
            // 
            this.ClientSize = new System.Drawing.Size(1086, 636);
            this.Controls.Add(this.panel4);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormInicio";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private FontAwesome.Sharp.IconMenuItem iconRRHH;
        private FontAwesome.Sharp.IconMenuItem iconCRM;
        private FontAwesome.Sharp.IconMenuItem iconInventario;
        private FontAwesome.Sharp.IconMenuItem iconProveedores;
        private System.Windows.Forms.Panel contenedor;
    }
}