using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Sede
    {

        public int codSede { get; set; }
        public String desSede { get; set; }
        public List<Area> areas { get; set; }

        public Sede() {
            areas = new List<Area>();
        }

    }
}
