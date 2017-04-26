using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
namespace DataAccess
{
    public class DAOArea
    {

        public Area getArea(int codArea)
        {

            Area area = new Area();
            area.codArea = "1";
            area.desArea = "Sistemas";
            return area;
        }

        public List<Area> getAreas(int idSede) {
            List<Area> listaRpta = new List<Area>();
            return listaRpta;
        }


    }
}
