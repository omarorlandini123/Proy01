using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppV2.Models;
namespace AppV2.Controllers
{
    public class LoginController : Controller
    {
        

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ViewModelCuentaUsuario cuenta)
        {
            
            return RedirectToAction("PorSede","Presupuesto");
        }

    }
}