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



        public Presupuesto getPresupuestosPorArea(int idPresupuesto, string usuario) {

            Presupuesto rpta = new Presupuesto();
            rpta.presupuestosArea = new List<PresupuestoArea>();
            rpta = getPresupuesto(idPresupuesto);
            rpta.TiposPresupuestos = getPresupuestosTipos(idPresupuesto);
            if (rpta.TiposPresupuestos != null)
            {
                foreach (PresupuestoTipo presup in rpta.TiposPresupuestos)
                {
                    presup.versiones = getVersiones(presup.idPresupuestoTipo);
                }
            }

            rpta.presupuestosArea = getPresupuestosArea(idPresupuesto,usuario);
            return rpta;
        }

        public PresupuestoTipo getPresupuestoTipo(int idPresupuestoTipo) {

            List<PresupuestoTipo> listaRpta = null;

            Conexion con = new Conexion();
            Procedimiento proc = new Procedimiento() { nombre = "GET_PRESUPUESTO_TIPO" };
            proc.parametros.Add(new Parametro("VAR_id_presupuesto_tipo", idPresupuestoTipo, OracleDbType.Int32, Parametro.tipoIN));
            DataTable dt = con.EjecutarProcedimiento(proc);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    listaRpta = new List<PresupuestoTipo>();
                    foreach (DataRow fila in dt.Rows)
                    {
                        try
                        {
                            PresupuestoTipo presupTipo = new PresupuestoTipo();
                            presupTipo.idPresupuestoTipo = int.Parse(fila["ID_PRESUPUESTO_TIPO"].ToString());
                            presupTipo.FechaReg = (DateTime)fila["PRE_T_FECHA_REG"];
                            presupTipo.UltModifReg = (DateTime)fila["PRE_T_ULT_MODIF_FEC"];
                            presupTipo.fechaValIni = (DateTime)fila["PRE_T_FECHA_VAL_INI"];
                            presupTipo.fechaValFin = (DateTime)fila["PRE_T_FECHA_VAL_FIN"];
                            presupTipo.estadoActual = (Aprobacion.estados)int.Parse(fila["PRE_T_EST_ACTUAL"].ToString());
                            presupTipo.tipoPresupuesto = new TipoPresupuesto();
                            presupTipo.tipoPresupuesto.idTipoPresupuesto = int.Parse(fila["ID_TIPO_PRESUP"].ToString());
                            presupTipo.tipoPresupuesto.nombrePresupuesto = fila["NOMBRE"].ToString();
                            presupTipo.monto = double.Parse(fila["monto"].ToString());
                            DAOAcceso daoAcceso = new DAOAcceso();
                            presupTipo.UltModifUser = daoAcceso.getUsuario(fila["PRE_T_MODIF_USR"].ToString());
                            presupTipo.presupuesto = getPresupuesto(int.Parse(fila["id_presupuesto"].ToString()));
                            listaRpta.Add(presupTipo);


                        }
                        catch (Exception s)
                        {
                            Console.WriteLine("Error En getPresupuestosPorSede ==> " + s.Message);
                        }
                    }
                    return listaRpta.FirstOrDefault();
                }
            }
            return null;

        }

        public Entidades.Version getDetalleDeVersion(int id)
        {
            Entidades.Version ver = null;
            Conexion con = new Conexion();
            Procedimiento proc = new Procedimiento() { nombre = "GET_VERSION" };
            proc.parametros.Add(new Parametro("VAR_ID_VERSION", id, OracleDbType.Int32, Parametro.tipoIN));
            DataTable dt = con.EjecutarProcedimiento(proc);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ver = new Entidades.Version();
                    foreach (DataRow fila in dt.Rows)
                    {
                        try
                        {
                            DAOArea daoArea = new DAOArea();
                            DAOUsuario daoUsuario = new DAOUsuario();

                            ver.idVersion = int.Parse(fila["id_version"].ToString());
                            ver.numeroVersion = int.Parse(fila["nro"].ToString());
                            ver.presupuestoTipo = getPresupuestoTipo(int.Parse(fila["id_presupuesto_tipo"].ToString()));                            
                            ver.area = daoArea.getArea(int.Parse(fila["id_area"].ToString()));
                            ver.fechaReg = (DateTime)fila["v_fecha_reg"];                           
                            ver.usuarioReg = daoUsuario.getUsuario(fila["v_usuario_reg"].ToString());
                            ver.UltModifFec = (DateTime)fila["v_ult_modif_fec"];
                            ver.fechaValIni = (DateTime)fila["v_fecha_val_ini"];
                            ver.fechaValFin = (DateTime)fila["v_fecha_val_fin"];
                            ver.estadoActual = (Aprobacion.estados)int.Parse(fila["est_actual"].ToString());
                            ver.detalles = DetallesDeVersion(ver.idVersion);

                        }
                        catch (Exception s)
                        {
                            Console.WriteLine("Error En getPresupuestosPorSede ==> " + s.Message);
                        }
                    }
                }
            }
            
            return ver;
        }

        public List<DetalleVersion> DetallesDeVersion(int idVersion) {

            Conexion con = new Conexion();
            Procedimiento proc = new Procedimiento() { nombre = "GET_DETALLE_VERSION" };
            proc.parametros.Add(new Parametro("VAR_ID_VERSION", idVersion, OracleDbType.Int32, Parametro.tipoIN));
            DataTable dt = con.EjecutarProcedimiento(proc);

            List<DetalleVersion> listaRpta = null;

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    listaRpta = new List<DetalleVersion>();
                   
                    foreach (DataRow fila in dt.Rows)
                    {
                        try
                        {
                            DetalleVersion det = new DetalleVersion();

                            DAOArea daoArea = new DAOArea();
                            DAOUsuario daoUsuario = new DAOUsuario();
                            DAOMaterial daoMaterial = new DAOMaterial();

                            det.idDetalle = int.Parse(fila["ID_DETALLE"].ToString());
                            det.mat = daoMaterial.getMaterial(fila["COD_MATERIAL"].ToString());
                            det.cantidadSoli = double.Parse(fila["CANT_SOLIC"].ToString());
                            det.precioSoli = double.Parse(fila["PRECIO_UNI_SOLIC"].ToString());
                            det.criticidad = int.Parse(fila["CRITICIDAD"].ToString());
                            det.totalSoli = double.Parse(fila["TOTAL_SOLIC"].ToString());
                            det.tipo = int.Parse(fila["TIPO"].ToString());
                            det.largo = double.Parse(fila["LARGO"].ToString());
                            det.ancho = double.Parse(fila["ANCHO"].ToString());
                            det.alto = double.Parse(fila["ALTO"].ToString());
                            det.sustento = fila["SUSTENTO"].ToString();
                            det.uniSoli = (fila["UNID_SOLI"].ToString());
                            det.FechaReg = (DateTime)fila["DET_V_FEC_REG"];
                            det.FechaUltModif = (DateTime)fila["DET_V_ULT_FEC"];
                            det.UsuarioReg = daoUsuario.getUsuario(fila["DET_V_USR_REG"].ToString());
                            det.UsuarioUltModif = daoUsuario.getUsuario(fila["DET_V_ULT_USR"].ToString());
                            listaRpta.Add(det);
                        }
                        catch (Exception s)
                        {
                            Console.WriteLine("Error En DetallesDeVersion ==> " + s.Message);
                        }
                    }
                }
            }

            return listaRpta;

        }

        public List<PresupuestoArea> getPresupuestosArea(int idPresupuesto,string usuario)
        {
            List<PresupuestoArea> listaRpta = null;

            Conexion con = new Conexion();
            Procedimiento proc = new Procedimiento() { nombre = "GET_PRESUPUESTO_AREAS" };
            proc.parametros.Add(new Parametro("VAR_ID_PRESUP", idPresupuesto, OracleDbType.Int32, Parametro.tipoIN));
            DataTable dt = con.EjecutarProcedimiento(proc);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    listaRpta = new List<PresupuestoArea>();
                    foreach (DataRow fila in dt.Rows)
                    {
                        try
                        {
                            PresupuestoArea presup = new PresupuestoArea();
                            presup.monto = double.Parse(fila["MONTO"].ToString());
                            presup.UltModifFec =(DateTime) fila["ULTMODIF"];
                            presup.fechaValIni = (DateTime)fila["FECHAVALINI"];
                            presup.fechaValFin = (DateTime)fila["FECHAVALFIN"];
                            DAOArea daoarea = new DAOArea();
                            presup.area = daoarea.getArea(int.Parse(fila["ID_AREA"].ToString()));

                            listaRpta.Add(presup);
                        }
                        catch (Exception s)
                        {
                            Console.WriteLine("Error En getPresupuestosPorSede ==> " + s.Message);
                        }
                    }
                }
            }
            return listaRpta;

        }

        public PresupuestoTipo getVersiones(int idPresupTipo, int idArea)
        {
            PresupuestoTipo presupTipo = new PresupuestoTipo();
            presupTipo.versiones = new List<Entidades.Version>();

            Conexion con = new Conexion();
            Procedimiento proc = new Procedimiento() { nombre = "GET_VERSIONES" };
            proc.parametros.Add(new Parametro("VAR_ID_PRESUPUESTO_TIPO", idPresupTipo, OracleDbType.Int32, Parametro.tipoIN));
            proc.parametros.Add(new Parametro("VAR_ID_AREA", idArea, OracleDbType.Int32, Parametro.tipoIN));
            DataTable dt = con.EjecutarProcedimiento(proc);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow fila in dt.Rows)
                    {
                        try
                        {
                            
                            presupTipo.idPresupuestoTipo = int.Parse(fila["ID_PRESUPUESTO_TIPO"].ToString());
                            presupTipo.FechaReg = (DateTime)fila["PRE_T_FECHA_REG"];
                            presupTipo.UltModifReg = (DateTime)fila["PRE_T_ULT_MODIF_FEC"];
                            presupTipo.fechaValIni = (DateTime)fila["PRE_T_FECHA_VAL_INI"];
                            presupTipo.fechaValFin = (DateTime)fila["PRE_T_FECHA_VAL_FIN"];
                            presupTipo.estadoActual = (Aprobacion.estados)int.Parse(fila["PRE_T_EST_ACTUAL"].ToString());                            

                            DAOAcceso daoAcceso = new DAOAcceso();
                            presupTipo.UltModifUser = daoAcceso.getUsuario(fila["PRE_T_MODIF_USR"].ToString());

                            Entidades.Version vers = new Entidades.Version();
                            vers.idVersion = int.Parse(fila["id_version"].ToString());
                            vers.numeroVersion = int.Parse(fila["nro"].ToString());

                            DAOArea daoArea = new DAOArea();
                            vers.area = daoArea.getArea(int.Parse(fila["id_area"].ToString()));

                            vers.fechaReg = (DateTime)fila["v_fecha_reg"];
                            vers.usuarioReg = daoAcceso.getUsuario(fila["v_usuario_reg"].ToString());
                            vers.UltModifFec = (DateTime)fila["v_ult_modif_fec"];
                            vers.fechaValIni = (DateTime) fila["v_fecha_val_ini"];
                            vers.estadoActual = (Aprobacion.estados)int.Parse(fila["est_actual"].ToString());
                            vers.monto = double.Parse(fila["MONTO"].ToString());
                            presupTipo.versiones.Add(vers);


                        }
                        catch (Exception s)
                        {
                            Console.WriteLine("Error En getPresupuestosPorSede ==> " + s.Message);
                        }
                    }
                }
            }

            return presupTipo;
        }

        public List<PresupuestoTipo> getPresupuestosTipos(int idPresupuesto) {
            List<PresupuestoTipo> listaRpta = null;

            Conexion con = new Conexion();
            Procedimiento proc = new Procedimiento() { nombre = "GET_PRESUPUESTOS_TIPO" };
            proc.parametros.Add(new Parametro("VAR_ID_PRESUPUESTO", idPresupuesto, OracleDbType.Int32, Parametro.tipoIN));
            DataTable dt = con.EjecutarProcedimiento(proc);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    listaRpta = new List<PresupuestoTipo>();
                    foreach (DataRow fila in dt.Rows)
                    {
                        try
                        {
                            PresupuestoTipo presupTipo = new PresupuestoTipo();
                            presupTipo.idPresupuestoTipo = int.Parse(fila["ID_PRESUPUESTO_TIPO"].ToString());
                            presupTipo.FechaReg = (DateTime)fila["PRE_T_FECHA_REG"];
                            presupTipo.UltModifReg = (DateTime)fila["PRE_T_ULT_MODIF_FEC"];
                            presupTipo.fechaValIni = (DateTime)fila["PRE_T_FECHA_VAL_INI"];
                            presupTipo.fechaValFin = (DateTime)fila["PRE_T_FECHA_VAL_FIN"];
                            presupTipo.estadoActual = (Aprobacion.estados)int.Parse(fila["PRE_T_EST_ACTUAL"].ToString());
                            presupTipo.tipoPresupuesto = new TipoPresupuesto();
                            presupTipo.tipoPresupuesto.idTipoPresupuesto = int.Parse(fila["ID_TIPO_PRESUP"].ToString());
                            presupTipo.tipoPresupuesto.nombrePresupuesto = fila["NOMBRE"].ToString();
                            presupTipo.monto = double.Parse(fila["monto"].ToString());
                            DAOAcceso daoAcceso = new DAOAcceso();
                            presupTipo.UltModifUser = daoAcceso.getUsuario(fila["PRE_T_MODIF_USR"].ToString());
                            listaRpta.Add(presupTipo);


                        }
                        catch (Exception s)
                        {
                            Console.WriteLine("Error En getPresupuestosPorSede ==> " + s.Message);
                        }
                    }
                }
            }
            return listaRpta;
        }


        public List<Entidades.Version> getVersiones(int idPresupuestoTipo)
        {
            List<Entidades.Version> listaRpta = null;

            Conexion con = new Conexion();
            Procedimiento proc = new Procedimiento() { nombre = "GET_VERSIONES_PRESUPUESTO_TIPO" };
            proc.parametros.Add(new Parametro("VAR_ID_PRESUP_TIPO", idPresupuestoTipo, OracleDbType.Int32, Parametro.tipoIN));
            DataTable dt = con.EjecutarProcedimiento(proc);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    listaRpta = new List<Entidades.Version>();
                    foreach (DataRow fila in dt.Rows)
                    {
                        try
                        {
                            Entidades.Version vers = new Entidades.Version();
                            vers.idVersion = int.Parse(fila["ID_VERSION"].ToString());
                            vers.numeroVersion = int.Parse(fila["NRO"].ToString());
                            vers.fechaReg = (DateTime)fila["v_fecha_reg"];
                            vers.fechaValIni = (DateTime)fila["v_fecha_val_ini"];
                            vers.fechaValFin = (DateTime)fila["v_fecha_val_fin"];
                            vers.UltModifFec = (DateTime)fila["v_ult_modif_fec"];
                            vers.estadoActual = (Aprobacion.estados)int.Parse(fila["est_actual"].ToString());
                            
                            DAOAcceso daoAcceso = new DAOAcceso();
                            vers.usuarioReg = daoAcceso.getUsuario(fila["v_usuario_reg"].ToString());
                           
                            DAOArea daoArea = new DAOArea();
                            vers.area=daoArea.getArea(int.Parse(fila["id_area"].ToString()));

                            listaRpta.Add(vers);
                        }
                        catch (Exception s)
                        {
                            Console.WriteLine("Error En getPresupuestosPorSede ==> " + s.Message);
                        }
                    }
                }
            }
            return listaRpta;
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
            Presupuesto presup = null;
          
            Conexion con = new Conexion();
            Procedimiento proc = new Procedimiento() { nombre = "GET_PRESUPUESTO" };
            proc.parametros.Add(new Parametro("VAR_ID_PRESUPUESTO", codPresupuesto, OracleDbType.Int32, Parametro.tipoIN));
            DataTable dt = con.EjecutarProcedimiento(proc);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow fila in dt.Rows)
                    {
                        try
                        {
                            presup = new Presupuesto();
                            presup.estadoActual = (Aprobacion.estados)int.Parse(fila["EST_ACTUAL"].ToString());
                            presup.nombrePresupuesto = fila["NOMB_PRESUP"].ToString();
                            presup.idPresupuesto = int.Parse(fila["ID_PRESUPUESTO"].ToString());
                            presup.fechaReg = (DateTime)fila["FECHA_REG"];
                            presup.fechaValIni = (DateTime)fila["FECHA_VAL_INI"];
                            presup.fechaValFin = (DateTime)fila["FECHA_VAL_FIN"];
                            presup.UltModifFec = (DateTime)fila["ULT_MODIF_FEC"];
                            try
                            {
                                DAOAcceso daoacceso = new DAOAcceso();
                                presup.UltModifUser = daoacceso.getUsuario(fila["ULT_MODIF_USER"].ToString());
                            }
                            catch (Exception ex) {
                                Console.WriteLine("Error En consiguiendo usuario para getPresupuestosPorSede  ==> " + ex.Message);
                            }
                        }
                        catch (Exception s)
                        {
                            Console.WriteLine("Error En getPresupuestosPorSede ==> " + s.Message);
                        }
                    }
                }
            }
            return presup;
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
        public List<Presupuesto> getPresupuestosPorSedeLista(int idSede)
        {

            List<Presupuesto>  listaRpta= new List<Presupuesto> ();
            Conexion con = new Conexion();
            Procedimiento proc = new Procedimiento() { nombre = "GET_PRESUP_POR_SEDE" };
            proc.parametros.Add(new Parametro("VAR_ID_SEDE", idSede, OracleDbType.Int32, Parametro.tipoIN));
            DataTable dt = con.EjecutarProcedimiento(proc);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
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
                            listaRpta.Add(presup);
                        }
                        catch (Exception s)
                        {
                            Console.WriteLine("Error En getPresupuestosPorSedeLista ==> " + s.Message);
                        }
                    }
                }
            }
            return listaRpta;

        }

    }
}
