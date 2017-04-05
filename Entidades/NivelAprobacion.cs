using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class NivelAprobacion
    {

        public int codNivel { get; set; }
        public String desNivel { get; set; }
        public int activo { get; set; }
        public int tipoNivel { get; set; }
        public int orden { get; set; }
        /// <summary>
        /// Lista los usuarios que poseen este nivel de aprobaion
        /// </summary>
        public List<Usuario> usuariosAprobacion { get; set; }
        public NivelAprobacion() {
            usuariosAprobacion = new List<Usuario>();
        }

    }
}
