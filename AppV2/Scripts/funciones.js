function mostrarPresupuestosPorSede(){
  var jqxhr = $.post( "Presupuesto/PorSede", function(data) {
    $('#contenido-principal').html(data);
  });

}
function listarPorArea(sede,anio) {
    $.get("PorArea", { idSede: sede, anio: anio })
      .done(function (data) {
          $('#PresupPorArea').fadeOut(500, function () {
              $('#PresupPorArea').html(data).fadeIn(500);
          });
      });

}

var AreaSelect = '';
var TRSELECT = '';


function ListarTipos(codArea) {
    $(AreaSelect).hide();
    $.get("/Presupuesto/TiposPresupuesto", {  })
      .done(function (data) {
          $('#TiposPresup_' + codArea).fadeOut(500, function () {
              $('#TiposPresup_' + codArea).show();
              $('#TiposPresup_' + codArea).html(data).fadeIn(500);
          });
      });
    AreaSelect = '#TiposPresup_' + codArea;
}

function listarVersiones(area, sede, anio) {
    $.get("/Presupuesto/Versiones", { idArea: area, idSede: sede, anio: anio })
      .done(function (data) {
          $('#VersionesArea').fadeOut(500, function () {
              $('#VersionesArea').html(data).fadeIn(500);
          });
      });
}

function ExpandirDetalle(codDetalle) {
    $(TRSELECT).hide();
    $.get("/Presupuesto/Edit", { idPresupuesto: codDetalle})
      .done(function (data) {
          $('#DETALLEDIV_' + codDetalle).hide();
          $('#DETALLEDIV_' + codDetalle).html(data);
          $('#DETALLE_' + codDetalle).show();
          $('#DETALLEDIV_' + codDetalle).fadeOut(500, function () {
              $('#DETALLEDIV_' + codDetalle).html(data).fadeIn(500);
          });
      });
    TRSELECT = '#DETALLE_' + codDetalle;
}


function ExpandirObservaciones(codDetalle) {
    $(TRSELECT).hide();
    $.get("/Presupuesto/Observaciones", { idPresupuesto: codDetalle })
      .done(function (data) {
          $('#DETALLEDIV_' + codDetalle).hide();
          $('#DETALLEDIV_' + codDetalle).html(data);
          $('#DETALLE_' + codDetalle).show();
          $('#DETALLEDIV_' + codDetalle).fadeOut(500, function () {
              $('#DETALLEDIV_' + codDetalle).html(data).fadeIn(500);
          });
      });
    TRSELECT = '#DETALLE_' + codDetalle;

}

function OcultarDetalle(codDetalle) {
    $('#DETALLE_' + codDetalle).hide();
}

$(document).ready(function () {
    $('[data-toggle="popover"]').popover();
});