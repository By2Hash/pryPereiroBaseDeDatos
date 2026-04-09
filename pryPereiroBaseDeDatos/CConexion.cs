using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace pryPereiroBaseDeDatos
{
    internal class CConexion
    {
        private OleDbConnection CNN;
        private DataSet DS;
        private string ERROR = "";

        public CConexion()
        {
            CNN = new OleDbConnection();
            DS = new DataSet();
        }


        public bool MostrarEnGrilla(string rutaArchivo, string nombreTabla, DataGridView grilla)
        {
            bool resultado = false;
            string extension = Path.GetExtension(rutaArchivo).ToLower();
            string cadenaConexion = "";

            
            if (extension == ".mdb")
            {
                cadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + rutaArchivo;
            }
            else if (extension == ".accdb")
            {
               
                cadenaConexion = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + rutaArchivo;
            }

            CNN.ConnectionString = cadenaConexion;

            try
            {
               
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = CNN;
                cmd.CommandType = CommandType.TableDirect;
                cmd.CommandText = nombreTabla;

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                DS.Clear(); 
                da.Fill(DS, nombreTabla);

                grilla.DataSource = DS.Tables[nombreTabla];

                resultado = true;
            }
            catch (Exception ex)
            {
                ERROR = ex.Message; 
            }

            return resultado;
        }

        public string ObtenerError()
        {
            return ERROR; 
        }
    }
}
