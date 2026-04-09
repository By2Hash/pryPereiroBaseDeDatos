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
            string cadenaConexion = null;

            // Proveedores a intentar según la extensión
            var providersToTry = new List<string>();
            if (extension == ".mdb")
            {
                // Jet primera opción para .mdb
                providersToTry.Add("Microsoft.Jet.OLEDB.4.0");
                // ACE también puede abrir .mdb si está instalado
                providersToTry.Add("Microsoft.ACE.OLEDB.16.0");
                providersToTry.Add("Microsoft.ACE.OLEDB.12.0");
            }
            else if (extension == ".accdb")
            {
                // ACE para .accdb
                providersToTry.Add("Microsoft.ACE.OLEDB.16.0");
                providersToTry.Add("Microsoft.ACE.OLEDB.12.0");
            }
            else
            {
                MessageBox.Show("Tipo de archivo no soportado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Buscar un proveedor que funcione
            cadenaConexion = TryFindWorkingConnectionString(rutaArchivo, providersToTry);

            if (string.IsNullOrEmpty(cadenaConexion))
            {
                MessageBox.Show("No se encontró un proveedor OLEDB disponible para abrir el archivo.\nAsegúrese de tener instalado el proveedor ACE/Jet correspondiente (32/64 bits según su aplicación).", "Proveedor no disponible", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        if (string.Equals(tableType, "TABLE", StringComparison.OrdinalIgnoreCase) || string.Equals(tableType, "VIEW", StringComparison.OrdinalIgnoreCase))
                        {
                            var tableName = row["TABLE_NAME"]?.ToString();
                            if (!string.IsNullOrEmpty(tableName) && !tableName.StartsWith("MSys"))
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

        // Intenta abrir la base de datos con cada proveedor y devuelve la cadena de conexión válida o null
        private string TryFindWorkingConnectionString(string rutaArchivo, List<string> providers)
        {
            foreach (var provider in providers)
            {
                var cs = $"Provider={provider};Data Source={rutaArchivo};";
                try
                {
                    using (var cnn = new OleDbConnection(cs))
                    {
                        cnn.Open();
                        cnn.Close();
                        return cs; // proveedor válido
                    }
                }
                catch
                {
                    // Ignorar y probar el siguiente proveedor
                }
            }
            return null;
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
