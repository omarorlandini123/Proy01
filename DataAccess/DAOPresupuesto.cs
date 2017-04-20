using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Data;
using System.Data;
using Oracle.DataAccess.Client;
namespace DataAccess
{
    public class DAOPresupuesto
    {
        /// <summary>
        /// Lista los presupuestos generales de cada sede
        /// </summary>
        /// <param name="sede">La sede de la cual se quiere obtener el presupuesto (Solo es necesario el código de Sede)</param>
        /// <returns></returns>
        public List<Presupuesto> getPresupuestosPorSede(Sede sede) {
            List<Presupuesto> listaRpta = new List<Presupuesto>();

            return listaRpta;
        }


        /// <summary>
        /// Obtiene los presupuestos generales de las áreas
        /// </summary>
        /// <param name="sede">Sede de la cual se necesita el área</param>
        /// <param name="anio">Año del cual se quieren los presupuestos</param>
        /// <returns></returns>
        public List<Presupuesto> getPresupuestosPorArea(Sede sede, int anio) {
            List<Presupuesto> listaRpta = new List<Presupuesto>();

            return listaRpta;
        }

        /// <summary>
        /// Obtiene las versiones de los presupuestos del área 
        /// </summary>
        /// <param name="sede">Sede de la cual se pide las versiones </param>
        /// <param name="anio">Año de las versiones</param>
        /// <param name="area">El área del cual se requieren las versiones</param>
        /// <returns></returns>
        public List<Presupuesto> getPresupuestosPorVersion(Sede sede, int anio, Area area) {
            List<Presupuesto> listaRpta = new List<Presupuesto>();

            return listaRpta;
        }


        /// <summary>
        /// Obtiene un presupuesto por código en específico
        /// </summary>
        /// <param name="codPresupuesto">El código del presupuesto</param>
        /// <returns></returns>
        public Presupuesto getPresupuesto(int codPresupuesto) {
            Presupuesto rpta = new Presupuesto();

            return rpta;
        }

  

        /// <summary>
        /// Permite enviar el borrador de presupuesto al siguiente Nivel 
        /// </summary>
        /// <param name="presup">Presupuesto que se quiere llevar al siguiente nivel</param>
        /// <returns></returns>
        public Boolean enviarPresupuestoAprobacion(Presupuesto presup) {
            return true;
        }

        /// <summary>
        /// Rechaza el presupuesto en el nivel Actual y genera una nueva version del mismo
        /// </summary>
        /// <param name="presup">presupuesto a Rechazar</param>
        /// <returns></returns>
        public Boolean rechazarPresupuesto(Presupuesto presup) {
            return true;
        }

        public DetalleVersion getObservacionesDetalle(int idDetalle) {

            Conexion con = new Conexion();
            Procedimiento proc = new Procedimiento() { nombre = "GET_OBSERVACIONES" };
            proc.parametros.Add(new Parametro("VAR_ID_DETALLE", idDetalle, OracleDbType.Int32, Parametro.tipoIN));
            DataTable dt =con.EjecutarProcedimiento(proc);
            DetalleVersion detalle=null;
            

            if (dt != null)
            {
                detalle = new DetalleVersion();
                detalle.observaciones = new List<Observacion>();

                foreach (DataRow fila in dt.Rows)
                {
                    
                    try
                    {
                        detalle.idDetalle = int.Parse(fila["ID_DETALLE"].ToString());
                    }
                    catch (Exception s)
                    {
                        Console.WriteLine("Error en getObservacionesDetalle ==> " + s.Message);
                        detalle.idDetalle = 0;
                    }

                    try
                    {
                        detalle.mat = new Material();
                        detalle.mat.codProducto = fila["COD_MATERIAL"].ToString();
                        detalle.mat.desc = "Conectar Al Web Service de Materiales";
                    }
                    catch (Exception s)
                    {
                        Console.WriteLine("Error en getObservacionesDetalle ==> " + s.Message);
                        detalle.mat = new Material();
                        detalle.mat.codProducto = "-1";
                        detalle.mat.desc = "Error en Web Service";
                    }

                    Observacion obs = new Observacion();
                    obs.detalle = detalle;

                    try
                    {
                        obs.idObservacion = int.Parse(fila["ID_OBSERVACIONES"].ToString());
                    }
                    catch (Exception s)
                    {
                        Console.WriteLine("Error en getObservacionesDetalle ==> " + s.Message);
                        obs.idObservacion = 0;
                    }
                    try
                    {
                        obs.observacion = fila["OBSERVACION"].ToString();
                    }
                    catch (Exception s)
                    {
                        Console.WriteLine("Error en getObservacionesDetalle ==> " + s.Message);
                        obs.observacion = "No se Obtuvo observación";
                    }

                    try
                    {
                        obs.usuarioReg = new Usuario();
                        obs.usuarioReg.idUsuario = fila["OBS_USR_REG"].ToString();
                        obs.usuarioReg.Nombres = "Conectar Web Service Usuarios";
                        obs.usuarioReg.ApellidoPaterno = "Conectar Web Service Usuarios";
                        obs.usuarioReg.ApellidoMaterno = "Conectar Web Service Usuarios";
                    }
                    catch (Exception s)
                    {
                        Console.WriteLine("Error en getObservacionesDetalle ==> " + s.Message);
                        obs.usuarioReg = new Usuario();
                        obs.usuarioReg.idUsuario = "0";
                        obs.usuarioReg.Nombres = "No se pudo obtener nombre";
                        obs.usuarioReg.ApellidoPaterno = "";
                        obs.usuarioReg.ApellidoMaterno = "";
                    }
                    try
                    {
                        obs.observacionRes = fila["OBS_RES"].ToString();
                    }
                    catch (Exception s)
                    {
                        Console.WriteLine("Error en getObservacionesDetalle ==> " + s.Message);
                        obs.observacionRes = "No se Obtuvo resolución de observación";
                    }
                    try
                    {
                        obs.usuarioRes = new Usuario();
                        obs.usuarioRes.idUsuario = fila["OBS_USR_RES"].ToString();
                        obs.usuarioRes.Nombres = "Conectar Web Service Usuarios";
                        obs.usuarioRes.ApellidoPaterno = "Conectar Web Service Usuarios";
                        obs.usuarioRes.ApellidoMaterno = "Conectar Web Service Usuarios";
                    }
                    catch (Exception s)
                    {
                        Console.WriteLine("Error en getObservacionesDetalle ==> " + s.Message);
                        obs.usuarioRes = new Usuario();
                        obs.usuarioRes.idUsuario = "0";
                        obs.usuarioRes.Nombres = "No se pudo obtener nombre";
                        obs.usuarioRes.ApellidoPaterno = "";
                        obs.usuarioRes.ApellidoMaterno = "";
                    }
                    detalle.observaciones.Add(obs);
                }
                
            }
            return detalle;
        }





        /// <summary>
        /// Registra una observación acerca del producto agregado en el presupuesto
        /// </summary>
        /// <param name="idDetalle"></param>
        /// <param name="observacion"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool ObservarDetalle(int idDetalle, string observacion, string usuario)
        {
            /*
             Falta agregar el VAR_USUREG en el procedimiento OBSERVAR_DETALLE
            */
            Conexion con = new Conexion();
            Procedimiento proc = new Procedimiento() { nombre = "OBSERVAR_DETALLE" };
            proc.parametros.Add(new Parametro("VAR_ID_DETALLE", idDetalle, OracleDbType.Int32, Parametro.tipoIN));
            proc.parametros.Add(new Parametro("VAR_OBSERVACION", observacion, OracleDbType.Varchar2, Parametro.tipoIN));
            proc.parametros.Add(new Parametro("VAR_USEREG", observacion, OracleDbType.Varchar2, Parametro.tipoIN));
            DataTable dt = con.EjecutarProcedimiento(proc);

            bool rpta = false; 
            if (dt != null)
            {                
                foreach (DataRow fila in dt.Rows)
                {
                    try
                    {
                        rpta = fila["INSERTO_OBS"].ToString().Contains("1");
                    }
                    catch (Exception s)
                    {
                        Console.WriteLine("Error En ObservarDetalle ==> " + s.Message);
                        rpta = false;
                    }                                       
                }
            }
            return rpta;
        }

        /// <summary>
        /// Registra el comentario de la resolución de observación
        /// </summary>
        /// <param name="idObservacion"></param>
        /// <param name="observacion"></param>
        /// <returns>True, si se ha registrado la resolución </returns>
        public bool ResolverObservacion(int idObservacion, string observacion,string usuario)
        {
            /*
              Falta agregar el VAR_USUREG en el procedimiento RESOLVER_OBS_DET
             */
            Conexion con = new Conexion();
            Procedimiento proc = new Procedimiento() { nombre = "RESOLVER_OBS_DET" };
            proc.parametros.Add(new Parametro("VAR_ID_OBS", idObservacion, OracleDbType.Int32, Parametro.tipoIN));
            proc.parametros.Add(new Parametro("VAR_OBSERVACION", observacion, OracleDbType.Varchar2, Parametro.tipoIN));
            proc.parametros.Add(new Parametro("VAR_USUREG", usuario, OracleDbType.Varchar2, Parametro.tipoIN));
            DataTable dt = con.EjecutarProcedimiento(proc);

            bool rpta = false;
            if (dt != null)
            {
                foreach (DataRow fila in dt.Rows)
                {
                    try
                    {
                        rpta = fila["ACT_OBS"].ToString().Contains("1");
                    }
                    catch (Exception s)
                    {
                        Console.WriteLine("Error En Resolver Observacion ==> " + s.Message);
                        rpta = false;
                    }
                }
            }
            return rpta;
        }


        public List<Presupuesto> getPresupuestos(int idSede) {
            List<Presupuesto> listaRpta = new List<Presupuesto>();
            Presupuesto presup1 = new Presupuesto();
            presup1.idPresupuesto = 1;
            presup1.nombrePresupuesto = "Presupuesto 2017 - I";
            presup1.fechaValIni = new DateTime(2017,1,1);
            presup1.fechaValFin = new DateTime(2017, 12, 31);
            presup1.fechaReg = new DateTime(2016, 12, 1);
            presup1.estadoActual = Aprobacion.estados.aprobado;
            presup1.usuarioReg = new Usuario() { idUsuario = "consultor3" };
            return listaRpta;

        }


    }
}
