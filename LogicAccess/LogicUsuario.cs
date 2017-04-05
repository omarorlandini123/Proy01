using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Entidades;
namespace LogicAccess
{
    public class LogicUsuario
    {

        

        public Usuario Login(String usuario, String password)
        {

            DAOUsuario dao = new DAOUsuario();
            return dao.Login(usuario,password);
        }

    }
}
