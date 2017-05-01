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
    public class PresupuestoController : Controller
    {
        // GET: Presupuesto
        public ActionResult Index()
        {
            return View();
        }

        #region "Acciones"

        public ActionResult PorSede() {
            LogicPresupuesto logic = new LogicPresupuesto();
            return View(logic.getPresupuestosPorSede(((Usuario)Session["usuario"]).area.sede.codSede));
        }

        public ActionResult Detalle(int id)
        {
            LogicPresupuesto logic = new LogicPresupuesto();
            Entidades.Version version = logic.getDetalleDeVersion(id);
            return View(version);
        }

        public ActionResult PresupArea(int id)
        {
            LogicPresupuesto logic = new LogicPresupuesto();
            LogicArea logArea = new LogicArea();
            Area area = logArea.getArea(id);
            area.presupuestos = logic.getPresupuestosPorSedeLista(((Usuario)Session["usuario"]).area.sede.codSede);
            return View(area);
        }

     

        #endregion

        #region "Vistas parciales"

        public ActionResult Observaciones(int idDetalle)
        {
            LogicPresupuesto logicPresup = new LogicPresupuesto();
            return PartialView(logicPresup.getObservacionesDetalle(idDetalle));
        }

        public ActionResult Edit(int idPresupuesto)
        {
            ViewModelEditPresup vista = new ViewModelEditPresup();
            vista.nombrePresup = "Presupuesto " + 2016 + " #" + 3;

            ViewDetallePresup detalle1 = new ViewDetallePresup();
            detalle1.idDetalle = 1;
            detalle1.codigoMaterial = "60734984";
            detalle1.descMaterial = "Plancha de acero";
            detalle1.Clase = "Clase 1";
            detalle1.subClase = "Sub Clase 2";
            detalle1.precioUnit = 18.93;
            detalle1.cantidad = 4;
            detalle1.unidadMedida = "Metros";
            detalle1.criticidad = 3;
            detalle1.prioridad = 4;
            detalle1.largo = 4;
            detalle1.ancho = 2;
            detalle1.alto = 0;
            detalle1.mesSolicitud = new DateTime(2016, 01, 01);
            detalle1.mesEntrega = new DateTime(2016, 03, 01);
            vista.Detalle = detalle1;


            return PartialView(vista);
        }

        public ActionResult Versiones(int id, int idArea)
        {

            LogicPresupuesto logic = new LogicPresupuesto();
            PresupuestoTipo presTip=logic.getVersiones(id, idArea);

            return PartialView(presTip);

        }

        public ActionResult TiposPresupuesto(int id)
        {
            LogicPresupuesto logic = new LogicPresupuesto();
            Presupuesto presup = logic.getPresupuesto(id);
            presup.TiposPresupuestos = logic.getPresupuestoTipos(id);
            return PartialView(presup);

        }

        public ActionResult PorArea(int idPresupuesto) {
            LogicPresupuesto logicpresup = new LogicPresupuesto();
            Usuario user = (Usuario)Session["usuario"];
            Presupuesto rpta= logicpresup.getPresupuestosPorArea(idPresupuesto, user.usuario);
            return PartialView(rpta);
        }

        #endregion

        #region "Modales"

        public ActionResult AprobarPresup(int id) {
            LogicPresupuesto logic = new LogicPresupuesto();
            return PartialView(logic.AprobacionesPresupuesto(id));
        }

        #endregion

        #region "JSon POST"

        [HttpPost]
        public JsonResult AprobarPresup(int id, string observacion,int estado) {
            LogicPresupuesto logic = new LogicPresupuesto();
            Usuario user = (Usuario)Session["usuario"];
            bool rpta = logic.AprobarPresupuesto(id, observacion, estado, user);
            return Json(rpta, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult ObservarDetalle(int idDetalle, string observacion)
        {
            LogicPresupuesto logicPresup = new LogicPresupuesto();
            return Json(logicPresup.ObservarDetalle(idDetalle, observacion), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult ResolverObservacion(int idObservacion, string observacion)
        {
            LogicPresupuesto logicPresup = new LogicPresupuesto();
            return Json(logicPresup.ResolverObservacion(idObservacion, observacion), JsonRequestBehavior.DenyGet);
        }
        #endregion

        

    }
}
