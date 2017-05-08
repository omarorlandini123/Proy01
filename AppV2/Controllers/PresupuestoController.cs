using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppV2.Models;
using LogicAccess;
using Entidades;
using System.IO;

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



        public ActionResult Detalle(int id, int idTipo)
        {
            LogicPresupuesto logic = new LogicPresupuesto();
            Entidades.Version version = logic.getVersionDetallada(id, idTipo);
            ViewBag.idTipo = idTipo;
            switch (idTipo)
            {
                case 1:
                    ViewBag.TipoDetalles = "Materiales y Suministros";
                    break;
                case 2:
                    ViewBag.TipoDetalles = "Servicios";
                    break;
            }

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


        public ActionResult VerArchivos(int idDetalle)
        {
            LogicPresupuesto logic = new LogicPresupuesto();
            return PartialView(logic.getArchivosDetalle(idDetalle));
        }

        public ActionResult EliminarArchivo(int idArchivo, int idDetalle) {

            LogicPresupuesto logic = new LogicPresupuesto();
            ViewBag.idArchivo = idArchivo;
            ViewBag.idDetalle = idDetalle;
            return PartialView(logic.EliminarArchivo(idArchivo));

        }

        public FileResult DescargarArchivo(int idArchivo) {

            LogicPresupuesto logic = new LogicPresupuesto();
            Archivo arc = logic.getArchivo(idArchivo);
            var FileVirtualPath = "~/App_Data/uploads/" + Path.GetFileName(arc.ruta);
            return File(FileVirtualPath, "application/force-download", arc.nombre);

        }

        public ActionResult SubirArchivo(int idDetalle)
        {
            LogicPresupuesto logic = new LogicPresupuesto();
            ViewBag.idDetalle = int.Parse(Request.Form["idDetalle"].ToString());

            string path = "";
            string filename = "";
            string typeFile = "";
            string nombreAleatorio = GenerarCodigo();



            foreach (string upload in Request.Files)
            {
                if (Request.Files[upload].FileName != "")
                {
                    path = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/uploads/";
                    filename = Path.GetFileName(Request.Files[upload].FileName);
                    typeFile = Path.GetExtension(Request.Files[upload].FileName);
                    Request.Files[upload].SaveAs(Path.Combine(path, nombreAleatorio + typeFile));
                }
            }


            return PartialView(logic.subirArchivoaDetalle(idDetalle, filename, typeFile, Path.Combine(path, nombreAleatorio + typeFile), ((Usuario)Session["usuario"]).usuario));
        }

        [HttpPost]
        public ActionResult ActualizarDetalle() {

            DetalleVersion detVersion = new DetalleVersion();
            detVersion.version = new Entidades.Version();
            detVersion.version.idVersion = int.Parse(Request.Form["idVersion"].ToString());
            detVersion.tipo = int.Parse(Request.Form["idTipo"].ToString());
            detVersion.cantidadSoli = double.Parse(Request.Form["cantidad"].ToString());
            detVersion.criticidad = int.Parse(Request.Form["criticidad"].ToString());
            detVersion.prioridad = new Prioridad() { idPrioridad = int.Parse(Request.Form["prioridad"].ToString()) };
            detVersion.largo = double.Parse(Request.Form["largo"].ToString());
            detVersion.ancho = double.Parse(Request.Form["ancho"].ToString());
            detVersion.alto = double.Parse(Request.Form["alto"].ToString());
            detVersion.sustento = Request.Form["sustento"].ToString();
            detVersion.messoli = Request.Form["messoli"].ToString();
            detVersion.mesent = Request.Form["mesent"].ToString();
            detVersion.precioSoli = double.Parse(Request.Form["preciosoli"].ToString());
            detVersion.uniSoli = Request.Form["uniSoli"].ToString();
            detVersion.mat = new Material();
            detVersion.mat.codProducto = Request.Form["codMaterial"].ToString();
            detVersion.mat.desc = Request.Form["nomMaterial"].ToString();
            detVersion.UsuarioReg = (Usuario)Session["usuario"];
            detVersion.idDetalle = int.Parse(Request.Form["idDetalle"].ToString());

            LogicPresupuesto logic = new LogicPresupuesto();
            int rpta = logic.actualizarDetalleVersion(detVersion);
            return PartialView(rpta);
        }


        public ActionResult NuevoDetalle()
        {
            string path = "";
            string filename = "";
            string typeFile = "";
            string nombreAleatorio = GenerarCodigo();



            foreach (string upload in Request.Files)
            {
                if (Request.Files[upload].FileName != "")
                {
                    path = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/uploads/";
                    filename = Path.GetFileName(Request.Files[upload].FileName);
                    typeFile = Path.GetExtension(Request.Files[upload].FileName);
                    Request.Files[upload].SaveAs(Path.Combine(path, nombreAleatorio + typeFile));
                }
            }

            DetalleVersion detVersion = new DetalleVersion();
            detVersion.version = new Entidades.Version();
            detVersion.version.idVersion = int.Parse(Request.Form["idVersion"].ToString());
            detVersion.tipo = int.Parse(Request.Form["idTipo"].ToString());
            detVersion.cantidadSoli = double.Parse(Request.Form["cantidad"].ToString());
            detVersion.criticidad = int.Parse(Request.Form["criticidad"].ToString());
            detVersion.prioridad = new Prioridad() { idPrioridad = int.Parse(Request.Form["prioridad"].ToString()) };
            detVersion.largo = double.Parse(Request.Form["largo"].ToString());
            detVersion.ancho = double.Parse(Request.Form["ancho"].ToString());
            detVersion.alto = double.Parse(Request.Form["alto"].ToString());
            detVersion.sustento = Request.Form["sustento"].ToString();
            detVersion.messoli = Request.Form["messoli"].ToString();
            detVersion.mesent = Request.Form["mesent"].ToString();
            detVersion.precioSoli = double.Parse(Request.Form["preciosoli"].ToString());
            detVersion.uniSoli = Request.Form["uniSoli"].ToString();
            detVersion.mat = new Material();
            detVersion.mat.codProducto = Request.Form["codMaterial"].ToString();
            detVersion.mat.desc = Request.Form["nomMaterial"].ToString();
            detVersion.archivosSustento = new List<Archivo>();
            detVersion.archivosSustento.Add(new Archivo() { ruta = Path.Combine(path, nombreAleatorio + typeFile), nombre = filename, tipo = typeFile });
            detVersion.UsuarioReg = (Usuario)Session["usuario"];

            LogicPresupuesto logic = new LogicPresupuesto();
            int rpta = logic.agregarDetalleVersion(detVersion);
            return PartialView(rpta);
        }


        #endregion

        #region "Vistas parciales"

        public ActionResult Observaciones(int idDetalle)
        {
            LogicPresupuesto logicPresup = new LogicPresupuesto();
            return PartialView(logicPresup.getObservacionesDetalle(idDetalle));
        }

        public ActionResult MostrarObservarDetalle(int idDetalle)
        {
            LogicPresupuesto logicPresup = new LogicPresupuesto();

            return PartialView(logicPresup.DetalleDeVersion(idDetalle));

        }

        public ActionResult MostrarCrearPresup(int idSede)
        {
            LogicPresupuesto logicPresup = new LogicPresupuesto();
            ViewBag.sedes = logicPresup.getSedes();
            return PartialView();
        }


        public ActionResult CrearPresup(string nombre, int idSede,int mesDesde, int anioDesde,int mesHasta,int anioHasta)
        {
            LogicPresupuesto logicPresup = new LogicPresupuesto();
            
            return PartialView(logicPresup.CrearPresup(nombre,idSede,((Usuario)Session["usuario"]).usuario,mesDesde,anioDesde,mesHasta,anioHasta));
        }

        public ActionResult MostrarResolucionObs(int idObservacion)
        {
            LogicPresupuesto logicPresup = new LogicPresupuesto();
            return PartialView(logicPresup.getObservacion(idObservacion));

        }

        public ActionResult MostrarEliminarDetalle(int idDetalle) {

            LogicPresupuesto logicPresup = new LogicPresupuesto();            
            return PartialView(logicPresup.DetalleDeVersion(idDetalle));
        }

        public ActionResult EliminarDetalle(int idDetalle) {

            LogicPresupuesto logicPresup = new LogicPresupuesto();
            return PartialView(logicPresup.EliminarDetalle(idDetalle));
        }


        public ActionResult MostrarResolverObservacion(int idObservacion)
        {
            LogicPresupuesto logicPresup = new LogicPresupuesto();

            return PartialView(logicPresup.getObservacion(idObservacion));

        }

        public ActionResult DetallesVersion(string cond,int idVersion,int idTipo)
        {
            LogicPresupuesto logicPresup = new LogicPresupuesto();
            return PartialView(logicPresup.DetallesDeVersion(cond,idVersion,idTipo));
        }


        public ActionResult Edit(int id)
        {
            LogicPresupuesto logic = new LogicPresupuesto();
            ViewBag.prioridades = (List<Prioridad>)logic.getPrioridades();
            return PartialView(logic.DetalleDeVersion(id));
        }


       

        public ActionResult Nuevo()
        {
            LogicPresupuesto logic = new LogicPresupuesto();
            ViewBag.prioridades = (List<Prioridad>)logic.getPrioridades();
            return PartialView();
        }

        public ActionResult Versiones(int id, int idArea)
        {

            LogicPresupuesto logic = new LogicPresupuesto();
            DetallePresupuesto presTip=logic.getVersiones(id, idArea);

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



        public ActionResult PorTipo(int idPresupuesto)
        {
            LogicPresupuesto logicPresup = new LogicPresupuesto();
            Presupuesto det = logicPresup.getPresupuestosPorTipo(idPresupuesto);
            return PartialView(det);
        }

        public ActionResult getMateriales(string cond)
        {
            LogicMaterial logicMaterial = new LogicMaterial();
            return PartialView(logicMaterial.getMateriales(cond));

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
            return Json(logicPresup.ObservarDetalle(idDetalle, observacion,((Usuario)Session["usuario"]).usuario), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult ResolverObservacion(int idObservacion, string observacion)
        {
            LogicPresupuesto logicPresup = new LogicPresupuesto();
            return Json(logicPresup.ResolverObservacion(idObservacion, observacion, ((Usuario)Session["usuario"]).usuario), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult getMaterial(string cond)
        {
            LogicMaterial logicMaterial = new LogicMaterial();
            return Json(logicMaterial.getMaterial(cond), JsonRequestBehavior.DenyGet);

        }



        #endregion

        private string GenerarCodigo()
        {
            Random obj = new Random();
            string sCadena = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int longitud = sCadena.Length;
            char cletra;
            int nlongitud = 20;
            string sNuevacadena = string.Empty;
            for (int i = 0; i < nlongitud; i++)
            {
                cletra = sCadena[obj.Next(nlongitud)];
                sNuevacadena += cletra.ToString();
            }
            return sNuevacadena;
        }

    }
}
