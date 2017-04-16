using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class MesEntSoli
    {

        public enum Meses
        {
            Enero=1, Febrero, Marzo,
            Abril, Mayo, Junio,
            Julio,Agosto,Setiembre,Octubre,
            Noviembre,Diciembre
        }
       

        public int idMesEntSoli { get; set; }
        public DetalleVersion detalle { get; set; }
        public int Mes { get; set; }
        public DateTime fechaReg { get; set; }
        public Usuario usuarioReg { get; set; }

    }

    
}
