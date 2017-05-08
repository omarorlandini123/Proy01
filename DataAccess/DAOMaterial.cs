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

        public List<Material> getPrueba() {

            List<Material> materiales = new List<Material>();
            Material mat1 = new Material();
            mat1.codProducto = "0102451234";
            mat1.desc = "Producto numero 1";
            mat1.precioUnit = 15.7;
            mat1.unidad = "Unid";
            mat1.subClase = new SubClase() { codSubClase = "02", desSubClase = "SubClase1" };
            mat1.subClase.clase = new Clase() { codClase = "01", desClase = "Clase 1" };

            Material mat2 = new Material();
            mat2.codProducto = "0102455002";
            mat2.desc = "Producto numero 2";
            mat2.precioUnit = 30.0;
            mat2.unidad = "Kg";
            mat2.subClase = new SubClase() { codSubClase = "02", desSubClase = "SubClase1" };
            mat2.subClase.clase = new Clase() { codClase = "01", desClase = "Clase 1" };

            Material mat3 = new Material();
            mat3.codProducto = "0102456778";
            mat3.desc = "Producto numero 3";
            mat3.precioUnit = 30.0;
            mat3.unidad = "Litros";
            mat3.subClase = new SubClase() { codSubClase = "03", desSubClase = "SubClase2" };
            mat3.subClase.clase = new Clase() { codClase = "02", desClase = "Clase 2" };

            materiales.Add(mat1);
            materiales.Add(mat2);
            materiales.Add(mat3);
            return materiales;
        }

        public List<Material> getMateriales(string cond) {
            List<Material> listaRpta = new List<Material>();
            try
            {
                foreach (Material mat in getPrueba())
                {
                    if (mat.codProducto.ToUpper().StartsWith(cond.ToUpper()) || mat.desc.Contains(cond))
                    {
                        listaRpta.Add(mat);
                    }
                }
            }
            catch (Exception s) {

            }
            return listaRpta;
        }

        public Material getMaterial(string codMaterial)
        {
           
            foreach (Material mat in getPrueba())
            {
                if (mat.codProducto.Equals(codMaterial))
                {
                    return mat;
                }
            }

            return new Material() {codProducto=codMaterial};
            
        }

    }
}
