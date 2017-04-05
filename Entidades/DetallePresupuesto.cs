using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DetallePresupuesto
    {

        public int codPresupuesto { get; set; }
        public Double cantidadSoli { get; set; }
        public Double  precioUniSoli { get; set; }
        public int criticidad { get; set; }
        public int prioridad { get; set; }
        public String sustento { get; set; }
        public Double largo { get; set; }
        public Double  ancho { get; set; }
        public Double alto { get; set; }
        public DateTime fechaSoli { get; set; }
        public DateTime fechaEntrega { get; set; }
        /// <summary>
        /// Lista las aprobaciones que tiene este detalle de presupuesto
        /// </summary>
        public List<Aprobacion> aprobacionesDet { get; set; }
        /// <summary>
        /// Presupuesto al cual pertenece este detalle
        /// </summary>
        public Presupuesto presupuesto { get; set; }
        /// <summary>
        /// El usuario que creó el detalle de presupuesto
        /// </summary>
        public Usuario usuarioCreacion { get; set; }
        /// <summary>
        /// EL material solicitado en el detalle
        /// </summary>
        public Material materialSoli { get; set; }
        /// <summary>
        /// La unidad en la que se solicita el material
        /// </summary>
        public Unidad unidadSoli { get; set; }
        public DetallePresupuesto() {
            aprobacionesDet = new List<Aprobacion>();
        }

    }
}
