using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Oracle.DataAcces;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
namespace Data
{
    public class Conexion
    {

        private string conectionString;

        public Conexion() {

            try
            {
                conectionString = ConfigurationManager.ConnectionStrings["ORACLESTR"].ConnectionString;

            }
            catch (Exception es)
            {
                Console.WriteLine("No hay cadena de conexíon");
            }
        }

        public Conexion(string ip,string schema, string user, string pass)
        {           
                conectionString = "Data Source="+ ip + "/"+ schema + ";User ID="+user+";Password="+pass;
        }

        public OracleDataReader Ejecutar(String Query) {

            OracleDataReader reader = null;
            try
            {
                OracleConnection sqlConnection1 = new OracleConnection(conectionString);
                OracleCommand cmd = new OracleCommand();

                cmd.CommandText = Query;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();

                reader = cmd.ExecuteReader();

                sqlConnection1.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error ejecutando consulta ==> "+e.Message);
            }
                        
            return reader;

        }

        public OracleDataReader EjecutarProcedimiento(Procedimiento proc) {

            OracleDataReader reader;

            OracleConnection conn = new OracleConnection(conectionString);
            
            conn.Open();

            OracleCommand command = new OracleCommand(proc.nombre, conn);
            command.CommandType = CommandType.StoredProcedure;

            foreach (Parametro param in proc.parametros) {
                command.Parameters.Add(param.nombreVariable, param.contenido);
            }
            reader=command.ExecuteReader();
            conn.Close();            

            return reader;

        }

    }
}
