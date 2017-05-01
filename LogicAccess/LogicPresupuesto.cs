using Entidades;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicAccess
{
    public class LogicPresupuesto
    {
        /// <summary>
        /// Lista los presupuestos generales de cada sede
        /// </summary>
        /// <param name="sede">La sede de la cual se quiere obtener el presupuesto (Solo es necesario el código de Sede)</param>
        /// <returns></returns>
        public List<Presupuesto> getPresupuestosPorSede(Sede sede)
        {
            DAOPresupuesto dao = new DAOPresupuesto();
            return  dao.getPresupuestosPorSede(sede);
        }

        public Entidades.Version getDetalleDeVersion(int id)
        {
            DAOPresupuesto dao = new DAOPresupuesto();
            return dao.getDetalleDeVersion(id);
        }

        /// <summary>
        /// Obtiene los presupuestos generales de las áreas
        /// </summary>
        /// <param name="sede">Sede de la cual se necesita el área</param>
        /// <param name="anio">Año del cual se quieren los presupuestos</param>
        /// <returns></returns>
        public Presupuesto getPresupuestosPorArea(int idPresupuesto,string usuario)
        {
            DAOPresupuesto dao = new DAOPresupuesto();
            Presupuesto pre=dao.getPresupuestosPorArea(idPresupuesto,usuario);          
           
            return pre;
        }

        public bool AprobarPresupuesto(int id, string observacion, int estado, Usuario user)
        {
            DAOPresupuesto dao = new DAOPresupuesto();
            return dao.AprobarPresupuesto(id,observacion,estado,user);
        }

        /// <summary>
        /// Obtiene las versiones de los presupuestos del área 
        /// </summary>
        /// <param name="sede">Sede de la cual se pide las versiones </param>
        /// <param name="anio">Año de las versiones</param>
        /// <param name="area">El área del cual se requieren las versiones</param>
        /// <returns></returns>
        public List<Presupuesto> getPresupuestosPorVersion(Sede sede, int anio, Area area)
        {
            DAOPresupuesto dao = new DAOPresupuesto();
            return dao.getPresupuestosPorVersion(sede,anio,area);
        }

        /// <summary>
        /// Obtiene un presupuesto por código en específico
        /// </summary>
        /// <param name="codPresupuesto">El código del presupuesto</param>
        /// <returns></returns>
        public Presupuesto getPresupuesto(int codPresupuesto)
        {
            DAOPresupuesto dao = new DAOPresupuesto();
            return dao.getPresupuesto(codPresupuesto);
        }

        public PresupuestoTipo getVersiones(int idPresupTipo, int idArea)
        {
            DAOPresupuesto dao = new DAOPresupuesto();
            return dao.getVersiones(idPresupTipo,idArea);
        }


        /// <summary>
        /// Permite enviar el borrador de presupuesto al siguiente Nivel 
        /// </summary>
        /// <param name="presup">Presupuesto que se quiere llevar al siguiente nivel</param>
        /// <returns></returns>
        public Boolean enviarPresupuestoAprobacion(Presupuesto presup)
        {
            DAOPresupuesto dao = new DAOPresupuesto();
            return dao.enviarPresupuestoAprobacion(presup);
        }

        /// <summary>
        /// Rechaza el presupuesto en el nivel Actual y genera una nueva version del mismo
        /// </summary>
        /// <param name="presup">presupuesto a Rechazar</param>
        /// <returns></returns>
        public Boolean rechazarPresupuesto(Presupuesto presup)
        {
            DAOPresupuesto dao = new DAOPresupuesto();
            return dao.rechazarPresupuesto(presup);
        }

        public DetalleVersion getObservacionesDetalle(int idDetalle)
        {
            DAOPresupuesto dao = new DAOPresupuesto();
            return dao.getObservacionesDetalle(idDetalle);
        }
        public bool ObservarDetalle(int idDetalle, string observacion) {
            DAOPresupuesto dao = new DAOPresupuesto();
            return dao.ObservarDetalle(idDetalle,observacion,"");
        }

        public List<PresupuestoTipo> getPresupuestoTipos(int id)
        {
            DAOPresupuesto dao = new DAOPresupuesto();
            return dao.getPresupuestosTipos(id);
        }

        public bool ResolverObservacion(int idObservacion, string observacion) {
            DAOPresupuesto dao = new DAOPresupuesto();
            return dao.ResolverObservacion(idObservacion, observacion,"");
        }

        public Sede getPresupuestosPorSede(int idSede) {

            DAOPresupuesto dao = new DAOPresupuesto();
            return dao.getPresupuestosPorSede(idSede);
        }
        public List<Presupuesto> getPresupuestosPorSedeLista(int idSede) {

            DAOPresupuesto dao = new DAOPresupuesto();
            return dao.getPresupuestosPorSedeLista(idSede);
        }

        public Presupuesto AprobacionesPresupuesto(int idPresupuesto) {
            DAOPresupuesto dao = new DAOPresupuesto();
            Presupuesto presup = dao.getPresupuesto(idPresupuesto);
            presup.aprobaciones=dao.AprobacionesPresupuesto(idPresupuesto);
            return presup;

        }


    }
}
