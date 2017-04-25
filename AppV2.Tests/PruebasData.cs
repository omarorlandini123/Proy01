using System.Data;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using System.Configuration;
namespace AppV2.Tests
{
    [TestClass]
    public class PruebasData
    {
        [TestMethod]
        public void TestConexionBasica()
        {
            Conexion con = new Conexion("192.168.237.10","orcl","SIMAUSR","1234");
            Assert.IsNotNull(con);
            DataTable reader = con.Ejecutar("select table_name from user_tables order by table_name");
            Assert.IsNotNull(reader);
        }

        
    }
}
