using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class NivelAprobacion
    {

        public int idNivel { get; set; }
        public string nombre { get; set; }
        public int orden { get; set; }
        public DateTime vigenciaIni { get; set; }
        public DateTime vigenciaFin { get; set; }
        public DateTime fechaReg { get; set; }
        public Usuario usuarioReg { get; set; }

    }
}
