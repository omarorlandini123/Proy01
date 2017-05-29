using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Data;
using System.Data;

namespace DataAccess
{
    public class DAOAcceso
    {

        public Usuario Login(string usuario, string password)
        {
            Usuario userRpta = null;

            try
            {
                AutenticarService.Autenticar aut = new AutenticarService.Autenticar();

                AutenticarService.RespuestaBE rpta = aut.LogOn(usuario, password, false);
                if (rpta.IdUsuario > 0)
                {
                    DataTable st = aut.GetOptionsByProfile(1, rpta.IdUsuario);
                }
                else {
                   rpta = aut.LogOn(usuario, password, true);

                }
                if (rpta != null)
                {
                    DataTable rptaperfil = rpta.MiPerfil;

                    userRpta = new Usuario();
                    userRpta.perfiles = new List<Perfil>();
                    foreach (DataRow fila in rptaperfil.Rows)
                    {
                        userRpta.idUsuario = fila["IdUsuario"].ToString();
                        userRpta.usuario = fila["Login"].ToString();
                        userRpta.Nombres = fila["ApellidosyNombres"].ToString();
                        userRpta.numeroPersonal = fila["NroPersonal"].ToString();
                        userRpta.area = new Area();
                        userRpta.area.codArea = fila["idArea"].ToString();
                        userRpta.area.desArea = fila["NombreArea"].ToString();
                        userRpta.area.sede = new Sede();
                        userRpta.area.sede.codSede = int.Parse(fila["idCentroOperativo"].ToString());
                        userRpta.area.sede.desSede = fila["NombreCentroOperativo"].ToString();
                        userRpta.centroCosto = new CentroCosto();
                        userRpta.centroCosto.idCentro = int.Parse(fila["idCentroCosto"].ToString());
                        userRpta.centroCosto.nroCentro = fila["NroCentroCosto"].ToString();
                        userRpta.centroCosto.nombre = fila["NombreCentroCosto"].ToString();
                        userRpta.grupoCentroCosto = new GrupoCentroCosto();
                        userRpta.grupoCentroCosto.idGrupoCentro = int.Parse(fila["idGrupoCentroCosto"].ToString());
                        userRpta.grupoCentroCosto.nombre = fila["NombreGrupoCentroCosto"].ToString();
                        Perfil perfil = new Perfil();
                        perfil.idPerfil = int.Parse(fila["IdPerfil"].ToString());
                        perfil.nombre = fila["Perfil"].ToString();
                        userRpta.perfiles.Add(perfil);


                    }
                }
            }
            catch (Exception s) {

            }
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
