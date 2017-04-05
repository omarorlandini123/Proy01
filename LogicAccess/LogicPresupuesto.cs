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

        /// <summary>
        /// Obtiene los presupuestos generales de las áreas
        /// </summary>
        /// <param name="sede">Sede de la cual se necesita el área</param>
        /// <param name="anio">Año del cual se quieren los presupuestos</param>
        /// <returns></returns>
        public List<Presupuesto> getPresupuestosPorArea(Sede sede, int anio)
        {
            DAOPresupuesto dao = new DAOPresupuesto();
            return dao.getPresupuestosPorArea(sede,anio);
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

        /// <summary>
        /// Obtiene el detalle de presupuesto 
        /// </summary>
        /// <param name="presup">Presupuesto del cual se quiere el detalle</param>
        /// <returns></returns>
        public DetallePresupuesto getDetallePresupuesto(Presupuesto presup)
        {
            DAOPresupuesto dao = new DAOPresupuesto();
            return dao.getDetallePresupuesto(presup);
        }

        /// <summary>
        /// Permite realizar una observacion a un Detalle de presupuesto
        /// </summary>
        /// <param name="detPresup">El detalle que se desea Observar. Solo es necesario el codigo y el texto de observacion</param>
        /// <returns></returns>
        public Boolean observarDetallePresupuesto(DetallePresupuesto detPresup)
        {
            DAOPresupuesto dao = new DAOPresupuesto();
            return dao.observarDetallePresupuesto(detPresup);
        }

        /// <summary>
        /// Permite editar el detalle de presupuesto 
        /// </summary>
        /// <param name="detPresup">El nuevo detalle de presupuesto, debe tener obligatoriamente el codigo</param>
        /// <returns></returns>
        public Boolean editarDetallePresupuesto(DetallePresupuesto detPresup)
        {
            DAOPresupuesto dao = new DAOPresupuesto();
            return dao.editarDetallePresupuesto(detPresup);
        }

        /// <summary>
        /// Permite insertar un nuevo detalle al presupuesto
        /// </summary>
        /// <param name="detPresup">El detalle de presupuesto a agregar. No se considera el código</param>
        /// <returns></returns>
        public Boolean insertarDetallePresupuesto(DetallePresupuesto detPresup)
        {

            DAOPresupuesto dao = new DAOPresupuesto();
            return dao.insertarDetallePresupuesto(detPresup);

        }

        /// <summary>
        /// Permite eliminar un detalle de presupuesto específico
        /// </summary>
        /// <param name="detPresup">El detalle que desea eliminar. Solo es necesario el código</param>
        /// <returns></returns>
        public Boolean eliminarDetallePresupuesto(DetallePresupuesto detPresup)
        {
            DAOPresupuesto dao = new DAOPresupuesto();
            return dao.eliminarDetallePresupuesto(detPresup);
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

    }
}
