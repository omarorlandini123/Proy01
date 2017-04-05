using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using DataAccess;
namespace LogicAccess
{
   public class LogicMaterial
    {
        public List<Material> getMateriales(Clase clase, SubClase subClase, String desc)
        {
            DAOMaterial dao = new DAOMaterial();
            return dao.getMateriales(clase,subClase,desc);
        }
    }
}
