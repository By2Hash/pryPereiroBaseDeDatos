using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace pryPereiroBaseDeDatos
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
            this.Load += frmPrincipal_Load;
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            cmbDatabases.Items.Clear();
            cmbTablas.Items.Clear();
            cmbDatabases.Text = string.Empty;
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos de Access|*.mdb;*.accdb|Todos los archivos|*.*";
                ofd.Title = "Seleccione archivo de base de datos Access";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var ruta = ofd.FileName;
                    cmbDatabases.Items.Add(ruta);
                    cmbDatabases.Text = ruta;

                    
                    LoadTablesIntoCombo(ruta);
                }
            }
        }

        private void LoadTablesIntoCombo(string rutaArchivo)
        {
            cmbTablas.Items.Clear();

            var extension = Path.GetExtension(rutaArchivo).ToLower();
            var cadenaConexion = string.Empty;

            if (extension == ".mdb")
            {
                cadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + rutaArchivo + ";";
            }
            else if (extension == ".accdb")
            {
                cadenaConexion = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaArchivo + ";";
            }
            else
            {
                MessageBox.Show("Tipo de archivo no soportado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var cnn = new OleDbConnection(cadenaConexion))
                {
                    cnn.Open();

                    var schema = cnn.GetSchema("Tables");

                    foreach (DataRow row in schema.Rows)
                    {
                        var tableType = row["TABLE_TYPE"]?.ToString();
                      
                        if (string.Equals(tableType, "TABLE", StringComparison.OrdinalIgnoreCase))
                        {
                            var tableName = row["TABLE_NAME"]?.ToString();
                            if (!string.IsNullOrEmpty(tableName))
                            {
                                cmbTablas.Items.Add(tableName);
                            }
                        }
                    }

                    if (cmbTablas.Items.Count > 0)
                    {
                        cmbTablas.SelectedIndex = 0;
                    }

                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener tablas:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            var ruta = cmbDatabases.Text?.Trim();
            var tabla = cmbTablas.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(ruta) || !File.Exists(ruta))
            {
                MessageBox.Show("Seleccione un archivo de base de datos válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(tabla))
            {
                MessageBox.Show("Seleccione una tabla para cargar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var conexion = new CConexion();
            var ok = conexion.MostrarEnGrilla(ruta, tabla, dgvDatos);
            if (!ok)
            {
                MessageBox.Show(conexion.ObtenerError(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
