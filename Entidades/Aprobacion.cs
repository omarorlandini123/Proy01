using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Aprobacion
    {

        public int tipoAprobacion { get; set; }
        public String observacion { get; set; }
        /// <summary>
        /// El presupuesto al cual aplica esta aprobación
        /// </summary>
        public Presupuesto presupAprob { get; set; }
        /// <summary>
        /// El detalle al cual aplica esta aprobación
        /// </summary>
        public DetallePresupuesto DetalleAprobado { get; set; }
        /// <summary>
        /// El nivel de aprobacion al cual corresponte esta aprobación
        /// </summary>
        public NivelAprobacion nivelAprobacion { get; set; }

    }
}
