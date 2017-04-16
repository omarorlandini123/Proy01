using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppV2.Models;
namespace AppV2.Controllers
{
    public class PresupuestoController : Controller
    {
        // GET: Presupuesto
        public ActionResult Index()
        {
            return View();
        }
       

        public ActionResult PorSede() {
            ViewModelPresupPorSede vista = new ViewModelPresupPorSede();
            ViewPresupuestoGeneral presupGeneral = new ViewPresupuestoGeneral();
            presupGeneral.anio="2015";
            presupGeneral.Desde = "01/01/2015";
            presupGeneral.Hasta = "31/01/2015";
            presupGeneral.fechaRegistro = "2/01/2015";
            presupGeneral.AprobadoPor = "COD_USUARIO";
            presupGeneral.idSede = "1";

            ViewPresupuestoGeneral presupGeneral2 = new ViewPresupuestoGeneral();
            presupGeneral2.anio = "2016";
            presupGeneral2.Desde = "01/01/2016";
            presupGeneral2.Hasta = "31/01/2016";
            presupGeneral2.fechaRegistro = "2/01/2016";
            presupGeneral2.AprobadoPor = "COD_USUARIO2";
            presupGeneral2.idSede = "1";

            vista.presupuestosPorAnio = new List<ViewPresupuestoGeneral>();
            vista.presupuestosPorAnio.Add(presupGeneral);
            vista.presupuestosPorAnio.Add(presupGeneral2);

            return View(vista);
        }

        public ActionResult PorArea(int idSede,int anio) {
            List<ViewPresupuestoArea> lista = new List<ViewPresupuestoArea>();

            ViewPresupuestoArea presupArea = new ViewPresupuestoArea();
            presupArea.codArea = "S-555";
            presupArea.nombreArea = "Sistemas";
            presupArea.desde = "01/01/2015";
            presupArea.hasta = "31/12/2015";
            presupArea.montoTotal = "45 670";
            presupArea.idArea = 1;
            presupArea.idSede = 1;

            ViewPresupuestoArea presupArea2 = new ViewPresupuestoArea();
            presupArea2.codArea = "S-556";
            presupArea2.nombreArea = "Contabilidad";
            presupArea2.desde = "01/01/2015";
            presupArea2.hasta = "31/12/2015";
            presupArea2.montoTotal = "47 879";
            presupArea2.idArea = 1;
            presupArea2.idSede = 1;

            lista.Add(presupArea);
            lista.Add(presupArea2);

            return PartialView(lista);
        }

        public ActionResult PresupArea(int id, int sede) {
            ViewModelPresupArea vista = new ViewModelPresupArea();

            List<ViewPresupuestoArea> lista = new List<ViewPresupuestoArea>();

            ViewPresupuestoArea presupArea = new ViewPresupuestoArea();
            presupArea.codArea = "S-555";
            presupArea.nombreArea = "Sistemas";
            presupArea.desde = "01/01/2015";
            presupArea.hasta = "31/12/2015";
            presupArea.fecReg = "05/01/2015";
            presupArea.usu_apro = "Juan Aguilar";
            presupArea.montoTotal = "45 670";
            presupArea.idArea = 1;
            presupArea.idSede = 1;
            presupArea.anio = 2015;

            ViewPresupuestoArea presupArea2 = new ViewPresupuestoArea();
            presupArea2.codArea = "S-556";
            presupArea2.nombreArea = "Sistemas";
            presupArea2.desde = "01/01/2016";
            presupArea2.hasta = "31/12/2016";
            presupArea2.fecReg = "07/02/2016";
            presupArea2.usu_apro = "Marcos Díaz";
            presupArea2.montoTotal = "47 879";

            presupArea2.idArea = 1;
            presupArea2.idSede = 1;
            presupArea2.anio = 2016;

            lista.Add(presupArea2);
            lista.Add(presupArea);

            vista.nombreArea = "Sistemas";
            vista.presupuestosArea = lista;
            return View(vista);
        }


        public ActionResult TiposPresupuesto() {
            List<ViewTipoPresupuesto> lista = new List<ViewTipoPresupuesto>();

            ViewTipoPresupuesto tipo1 = new ViewTipoPresupuesto();

            tipo1.codTipoPresup = "COD_235";
            tipo1.nomTipoPresup = "Gasto de Capital";
            tipo1.ultimaModif = "10/04/2017";

            tipo1.monto = "S/ 45 000";
            tipo1.desde = "En Edicion";
            tipo1.hasta = "En Edicion";

            ViewTipoPresupuesto tipo2 = new ViewTipoPresupuesto();

            tipo2.codTipoPresup = "COD_235";
            tipo2.nomTipoPresup = "Gasto de Funcionamiento";
            tipo2.ultimaModif = "05/04/2017";

            tipo2.monto = "S/ 48 589";
            tipo2.desde = "En Edicion";
            tipo2.hasta = "En Edicion";

            lista.Add(tipo1);
            lista.Add(tipo2);

            return PartialView(lista);

        }

        public ActionResult Versiones(int idArea, int idSede, int anio) {

            List<ViewPresupuestoArea> lista = new List<ViewPresupuestoArea>();
            ViewPresupuestoArea presup1 = new ViewPresupuestoArea();
            presup1.version = "3";
            presup1.codArea = "S-655";
            presup1.desde = "01/01/2016";
            presup1.hasta = "31/12/2016";
            presup1.fecReg = "05/01/2016";
            presup1.idPresupuesto = 45;
            presup1.idArea = 1;
            presup1.idSede = 1;
            presup1.anio = 2016;


            ViewPresupuestoArea presup2 = new ViewPresupuestoArea();
            presup2.version = "2";
            presup2.codArea = "S-656";
            presup2.desde = "01/01/2016";
            presup2.hasta = "31/12/2016";
            presup2.fecReg = "07/01/2016";
            presup2.idPresupuesto = 44;
            presup2.idArea = 1;
            presup2.idSede = 1;
            presup2.anio = 2016;

            ViewPresupuestoArea presup3 = new ViewPresupuestoArea();
            presup3.version = "1";
            presup3.codArea = "S-657";
            presup3.desde = "01/01/2016";
            presup3.hasta = "31/12/2016";
            presup3.fecReg = "09/01/2016";
            presup3.idPresupuesto = 43;
            presup3.idArea = 1;
            presup3.idSede = 1;
            presup3.anio = 2016;

            lista.Add(presup1);
            lista.Add(presup2);
            lista.Add(presup3);

            return PartialView(lista);

        }

        public ActionResult Edit(int idPresupuesto) {
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
            vista.Detalle=detalle1;


            return PartialView(vista);
        }

        public ActionResult Observaciones() {

            return PartialView();
        }


        public ActionResult Detalle(int idVersion)
        {
            ViewModelDetallePresup vista = new ViewModelDetallePresup();
            vista.nombrePresup= "Detalle de Presupuesto 2015 - III - Versión #2";
            List<ViewDetallePresup> listaDetalles = new List<ViewDetallePresup>();
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

            ViewDetallePresup detalle2 = new ViewDetallePresup();
            detalle2.idDetalle = 2;
            detalle2.codigoMaterial = "60734983";
            detalle2.descMaterial = "Viga de acero";
            detalle2.Clase = "Clase 1";
            detalle2.subClase = "Sub Clase 2";
            detalle2.precioUnit = 34.67;
            detalle2.cantidad = 2;
            detalle2.unidadMedida = "Metros";
            detalle2.criticidad = 2;
            detalle2.prioridad = 3;
            detalle2.largo = 5;
            detalle2.ancho = 3;
            detalle2.alto = 0;
            detalle2.mesSolicitud = new DateTime(2016, 01, 01);
            detalle2.mesEntrega = new DateTime(2016, 03, 01);

            listaDetalles.Add(detalle1);
            listaDetalles.Add(detalle2);

            vista.detalles = listaDetalles;

            return View(vista);
        }
    }
}
