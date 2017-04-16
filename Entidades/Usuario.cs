using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public String usuario { get; set; }
        public String Nombres { get; set; }
        public String ApellidoPaterno { get; set; }
        public String ApellidoMaterno { get; set; }
        /// <summary>
        /// Representa el área a la cual pertenece el usuario
        /// </summary>
        public Area area { get; set; } 
        /// <summary>
        /// lista los presupuestos que ha aperturado el usuario
        /// </summary>
        public List<Presupuesto> presupAper { get; set; }
        /// <summary>
        /// Lista los presupuestos que ha aprobado el usuario 
        /// </summary>
        public List<Presupuesto> presupAprob { get; set; }
        /// <summary>
        /// Lista los niveles de aprobacion que tiene el usuario
        /// </summary>
        public List<NivelAprobacion> nivelesAprobacion { get; set; }
        /// <summary>
        /// List los detalles de presupuesto que han sido creados por el usuario
        /// </summary>
      
        public Usuario() {
            presupAper = new List<Presupuesto>();
            presupAprob = new List<Presupuesto>();
            nivelesAprobacion = new List<NivelAprobacion>();
            detallesCreados = new List<DetallePresupuesto>();
        }

    }
}
