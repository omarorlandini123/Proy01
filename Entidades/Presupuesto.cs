using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Presupuesto
    {

        public int codPresupuesto { get; set; }
        public String desPresupuesto { get; set; }
        public int version { get; set; }
        public int anioPresupuesto { get; set; }
        public DateTime fecInicio { get; set; }
        public DateTime fecFin { get; set; }
        public DateTime fecApertura { get; set; }
        /// <summary>
        /// Lista las aprobaciones que tiene el presupuesto 
        /// </summary>
        public List<Aprobacion> aprobacionesPresup { get; set; }
        /// <summary>
        /// El usuario que aperturó el presupuesto
        /// </summary>
        public Usuario usuarioApe { get; set; }
        /// <summary>
        /// Área a la cual pertence el presupuesto
        /// </summary>
        public Area area { get; set; }
        /// <summary>
        /// Los detalles de este presupuesto
        /// </summary>
        public List<DetallePresupuesto> detalles { get; set; }
        public Presupuesto() {
            aprobacionesPresup = new List<Aprobacion>();
            detalles = new List<DetallePresupuesto>();
        }

    }
}
