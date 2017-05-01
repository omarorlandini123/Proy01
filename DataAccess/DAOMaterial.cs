using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace DataAccess
{
    public class DAOMaterial
    {

        public List<Material> getMateriales(Clase clase, SubClase subClase, String desc) {
            List<Material> listaRpta = new List<Material>();

            return listaRpta;
        }

        public Material getMaterial(string codMaterial)
        {
            Material mat = new Material();
            mat.codProducto = codMaterial;
            mat.desc = "Sin conexion a Materiales";
            return mat;
        }

    }
}
