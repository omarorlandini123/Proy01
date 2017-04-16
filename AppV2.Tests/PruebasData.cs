using System.Data;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using Oracle.DataAccess.Client;
using System.Configuration;
namespace AppV2.Tests
{
    [TestClass]
    public class PruebasData
    {
        [TestMethod]
        public OracleDataReader TestConexionBasica()
        {
            Conexion con = new Conexion("192.168.1.10","orcl","SIMAUSR","1234");
            Assert.IsNotNull(con);
            OracleDataReader reader = con.Ejecutar("select 1");
            Assert.IsNotNull(reader);
            return reader;
        }

        
    }
}
