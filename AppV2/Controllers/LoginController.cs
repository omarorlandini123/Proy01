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
            Session["usuario"] = null;
            if (Session["malInicio"] != null)
            {
                if ((bool)Session["malInicio"])
                {
                    ViewBag.mensaje = "No se han podido validar sus credenciales";
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario cuenta)
        {
            LogicAcceso logic = new LogicAcceso();
            Session.Timeout = 1440;
            Session["usuario"] =logic.Login(cuenta.usuario,cuenta.password);
            if (Session["usuario"] == null)
            {
                Session["malInicio"] = true;
                return RedirectToAction("Index", "Login");
            }
            else {
                return RedirectToAction("PorSede", "Presupuesto");
            }
          
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

        public ActionResult Configuracion()
        {
            if (Session["usuario"] != null)
            {
                return View();

            }
            else {
                return RedirectToAction("Index", "Login");
            }

        }

    }
}