using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using DataAccess;
namespace LogicAccess
{
    public class LogicAcceso
    {
        public Usuario Login(string usuario,string password)
        {
            DAOAcceso dao = new DAOAcceso();
            return dao.Login(usuario,password);
        }
    }
}
