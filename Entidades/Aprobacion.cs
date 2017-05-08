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
            Aprobado=1,
            Desaprobado,
            Inactivo,
            Activo,
            Edicion,
            NoVerficiado
        }
        public int idAprobacion { get; set; }
        public NivelAprobacion nivel { get; set; }
        public estados estado { get; set; }
        public Usuario usuarioApro { get; set; }
        public int orden { get; set; }
        public DateTime FechaApro { get; set; }
        public Usuario usuarioReg { get; set; }
        public DateTime FechaReg { get; set; }
        public string Observacion { get; set; }
        public Presupuesto presupAplica { get; set; }
        public DetallePresupuesto tipoPresup { get; set; }
        public Version version { get; set; }
        public DetalleVersion detalleVersion { get; set; }
        public bool listoParaAprobar { get; set; }

    }
}
