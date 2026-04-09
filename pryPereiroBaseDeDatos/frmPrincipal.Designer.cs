namespace pryPereiroBaseDeDatos
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbDatabases = new System.Windows.Forms.ComboBox();
            this.lblBase = new System.Windows.Forms.Label();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.btnExaminar = new System.Windows.Forms.Button();
            this.lblTabla = new System.Windows.Forms.Label();
            this.cmbTablas = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbDatabases
            // 
            this.cmbDatabases.FormattingEnabled = true;
            this.cmbDatabases.Location = new System.Drawing.Point(52, 15);
            this.cmbDatabases.Name = "cmbDatabases";
            this.cmbDatabases.Size = new System.Drawing.Size(350, 21);
            this.cmbDatabases.TabIndex = 0;
            // 
            // lblBase
            // 
            this.lblBase.AutoSize = true;
            this.lblBase.Location = new System.Drawing.Point(12, 18);
            this.lblBase.Name = "lblBase";
            this.lblBase.Size = new System.Drawing.Size(34, 13);
            this.lblBase.TabIndex = 1;
            this.lblBase.Text = "Base:";
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(672, 12);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(75, 25);
            this.btnConsultar.TabIndex = 2;
            this.btnConsultar.Text = "Cargar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // dgvDatos
            // 
            this.dgvDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Location = new System.Drawing.Point(12, 50);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.Size = new System.Drawing.Size(740, 274);
            this.dgvDatos.TabIndex = 3;
            // 
            // btnExaminar
            // 
            this.btnExaminar.Location = new System.Drawing.Point(408, 12);
            this.btnExaminar.Name = "btnExaminar";
            this.btnExaminar.Size = new System.Drawing.Size(75, 25);
            this.btnExaminar.TabIndex = 4;
            this.btnExaminar.Text = "Examinar...";
            this.btnExaminar.UseVisualStyleBackColor = true;
            this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);
            // 
            // lblTabla
            // 
            this.lblTabla.AutoSize = true;
            this.lblTabla.Location = new System.Drawing.Point(492, 18);
            this.lblTabla.Name = "lblTabla";
            this.lblTabla.Size = new System.Drawing.Size(37, 13);
            this.lblTabla.TabIndex = 5;
            this.lblTabla.Text = "Tabla:";
            // 
            // cmbTablas
            // 
            this.cmbTablas.FormattingEnabled = true;
            this.cmbTablas.Location = new System.Drawing.Point(535, 15);
            this.cmbTablas.Name = "cmbTablas";
            this.cmbTablas.Size = new System.Drawing.Size(126, 21);
            this.cmbTablas.TabIndex = 6;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 336);
            this.Controls.Add(this.cmbTablas);
            this.Controls.Add(this.lblTabla);
            this.Controls.Add(this.btnExaminar);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.lblBase);
            this.Controls.Add(this.cmbDatabases);
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Base de Datos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDatabases;
        private System.Windows.Forms.Label lblBase;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.Button btnExaminar;
        private System.Windows.Forms.Label lblTabla;
        private System.Windows.Forms.ComboBox cmbTablas;

       
    }
}

