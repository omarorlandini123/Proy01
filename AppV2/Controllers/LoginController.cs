using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppV2.Models;
using LogicAccess;
using Entidades;
namespace AppV2.Controllers
{
    public class LoginController : Controller
    {


        // GET: Login
        public ActionResult Index()
        {
            Session["idUsuario"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario cuenta)
        {
            LogicAcceso logic = new LogicAcceso();
            Session["usuario"] =logic.Login(cuenta.usuario,cuenta.password);           
            
            return RedirectToAction("PorSede","Presupuesto");
        }

        public ActionResult Logout()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Logout(int idUsuario)
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

    }
}