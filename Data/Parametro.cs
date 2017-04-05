using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Parametro
    {

        public const int tipoIN = 0;
        public const int tipoOUT = 1;
        public const int tipoINOUT = 2;

        public String nombreVariable { get; set; }
        public Object  contenido { get; set; }
        public int SQLDBTYPE { get; set; }
        /// <summary>
        /// Especifica el tipo de variable, si es IN, OUT, IN_OUT
        /// </summary>
        public int tipo { get; set; }

    }
}
