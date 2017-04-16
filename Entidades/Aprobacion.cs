using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Aprobacion
    {

        public enum estados {
            aprobado=1,
            desaprobado,
            noVerficiado
        }
        public int idAprobacion { get; set; }
        public NivelAprobacion nivel { get; set; }
        public int estado { get; set; }
        public Usuario usuarioReg { get; set; }
        public DateTime FechaReg { get; set; }
        public string Observacion { get; set; }
        public Presupuesto presupAplica { get; set; }
        public PresupuestoTipo tipoPresup { get; set; }
        public Version version { get; set; }
        public DetalleVersion detalleVersion { get; set; }


    }
}
