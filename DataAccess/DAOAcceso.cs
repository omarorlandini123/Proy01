using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Data;
namespace DataAccess
{
    public class DAOAcceso
    {

        public Usuario Login(string usuario, string password)
        {
            Usuario userRpta = null;

            //*********** DATA DE PRUEBA **********

            userRpta = new Usuario();
            userRpta.idUsuario = "420";
            userRpta.usuario = "consultor3";
            userRpta.password = "1234";
            userRpta.area = new Area();
            userRpta.area.codArea = "4";
            userRpta.area.desArea = "Sistemas";
            userRpta.area.sede = new Sede();
            userRpta.area.sede.codSede= 1;
            userRpta.area.sede.desSede = "Lima";
            userRpta.Nombres = "Juan Miguel";
            userRpta.ApellidoMaterno = "Paredes";
            userRpta.ApellidoPaterno = "Diaz";
            //userRpta.nivelesAprobacion;

            //*************************************

            return userRpta;
        }

        public Usuario getUsuario(string usuario)
        {
            Usuario userRpta = null;

            //*********** DATA DE PRUEBA **********

            userRpta = new Usuario();
            userRpta.idUsuario = "420";
            userRpta.usuario = "consultor3";
            userRpta.password = "1234";
            userRpta.area = new Area();
            userRpta.area.codArea = "4";
            userRpta.area.desArea = "Sistemas";
            userRpta.area.sede = new Sede();
            userRpta.area.sede.codSede = 1;
            userRpta.area.sede.desSede = "Lima";
            userRpta.Nombres = "Juan Miguel";
            userRpta.ApellidoMaterno = "Paredes";
            userRpta.ApellidoPaterno = "Diaz";
            //userRpta.nivelesAprobacion;

            //*************************************

            return userRpta;
        }
    }
}
