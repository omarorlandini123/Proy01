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



        public Presupuesto getPresupuestosPorArea(int idPresupuesto,string usuario) {

            Presupuesto rpta = new Presupuesto();
            

            return rpta;
        }

        public bool AprobarPresupuesto(int id, string observacion, int estado, Usuario user)
        {
            return true;
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
            rpta.idPresupuesto = 1;
            
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

        public List<Aprobacion> AprobacionesPresupuesto(int idPresupuesto)
        {
            Conexion con = new Conexion();
            Procedimiento proc = new Procedimiento() { nombre = "APROB_PRESUP" };
            proc.parametros.Add(new Parametro("VAR_ID_PRESUP", idPresupuesto, OracleDbType.Int32, Parametro.tipoIN));
            DataTable dt = con.EjecutarProcedimiento(proc);
            List<Aprobacion> listaRpta = new List<Aprobacion>();
            if (dt != null)
            {
                foreach (DataRow fila in dt.Rows)
                {
                    Aprobacion aprob = new Aprobacion();
                    try
                    {
                        aprob.idAprobacion = int.Parse(fila["ID_APROB_PRESUP"].ToString());
                    }
                    catch (Exception s)
                    {
                        Console.WriteLine("Error en AprobacionesPresupuesto campo idAprobacion ==> " + s.Message);
                    }

                    try
                    {
                        aprob.orden = int.Parse(fila["ORDEN"].ToString());
                    }
                    catch (Exception s)
                    {
                        Console.WriteLine("Error en AprobacionesPresupuesto campo orden ==> " + s.Message);
                    }

                    try
                    {
                        aprob.usuarioApro = new Usuario();
                        aprob.usuarioApro.usuario = fila["USR_APROB"].ToString();
                    }
                    catch (Exception s)
                    {
                        Console.WriteLine("Error en AprobacionesPresupuesto campo usuarioApro ==> " + s.Message);
                    }

                    try
                    {
                        aprob.estado = (Aprobacion.estados)int.Parse(fila["ESTADO"].ToString());
                    }
                    catch (Exception s)
                    {
                        Console.WriteLine("Error en AprobacionesPresupuesto campo estado ==> " + s.Message);
                    }

                    try
                    {
                        aprob.FechaApro = DateTime.Parse(fila["FEC_APROB"].ToString());
                    }
                    catch (Exception s)
                    {
                        Console.WriteLine("Error en AprobacionesPresupuesto campo estado ==> " + s.Message);
                        aprob.FechaApro = new DateTime(01, 01, 01);
                    }

                    try
                    {
                        aprob.Observacion = fila["OBS"].ToString();
                    }
                    catch (Exception s)
                    {
                        Console.WriteLine("Error en AprobacionesPresupuesto campo estado ==> " + s.Message);
                    }



                }
            }

           
            return listaRpta;
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


        public Sede getPresupuestosPorSede(int idSede) {

            Sede sede = null;
            Conexion con = new Conexion();
            Procedimiento proc = new Procedimiento() { nombre = "GET_PRESUP_POR_SEDE" };
            proc.parametros.Add(new Parametro("VAR_ID_SEDE", idSede, OracleDbType.Int32, Parametro.tipoIN));
            DataTable dt = con.EjecutarProcedimiento(proc);

            bool rpta = false;
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    sede = new Sede();
                    sede.codSede = 1;
                    sede.desSede = "Lima";
                    sede.presupuestos = new List<Presupuesto>();
                    foreach (DataRow fila in dt.Rows)
                    {
                        try
                        {
                            Presupuesto presup = new Presupuesto();
                            presup.estadoActual = (Aprobacion.estados)int.Parse(fila["EST_ACTUAL"].ToString());
                            presup.nombrePresupuesto = fila["NOMB_PRESUP"].ToString();
                            presup.idPresupuesto = int.Parse(fila["ID_PRESUPUESTO"].ToString());
                            presup.monto = double.Parse(fila["MONTO"].ToString());
                            presup.fechaReg = (DateTime)fila["FECHA_REG"];
                            presup.fechaValIni = (DateTime)fila["FECHA_VAL_INI"];
                            presup.fechaValFin = (DateTime)fila["FECHA_VAL_FIN"];
                            presup.UltModifFec = (DateTime)fila["ULT_MODIF_FEC"];
                            Usuario userUltModif = new Usuario();
                            userUltModif.usuario = fila["ULT_MODIF_USER"].ToString();
                            presup.UltModifUser = userUltModif;
                            sede.presupuestos.Add(presup);
                        }
                        catch (Exception s) {
                            Console.WriteLine("Error En getPresupuestosPorSede ==> " + s.Message);
                        }
                    }
                }
            }
            return sede;

        }


    }
}
