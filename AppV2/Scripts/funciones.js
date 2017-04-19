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
var DIVSEl = '';


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
    $(DIVSEl).html('');
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
    DIVSEl = '#DETALLEDIV_' + codDetalle;
}


function ExpandirObservaciones(codDetalle) {
    $(TRSELECT).hide();
    $(DIVSEl).html('');
    $.get("/Presupuesto/Observaciones", { idDetalle: codDetalle })
      .done(function (data) {
          $('#DETALLEDIV_' + codDetalle).hide();
          $('#DETALLEDIV_' + codDetalle).html(data);
          $('#DETALLE_' + codDetalle).show();
          $('#DETALLEDIV_' + codDetalle).fadeOut(500, function () {
              $('#DETALLEDIV_' + codDetalle).html(data).fadeIn(500);
          });
      });
    TRSELECT = '#DETALLE_' + codDetalle;
    DIVSEl = '#DETALLEDIV_' + codDetalle;
}

function OcultarDetalle(codDetalle) {
    $('#DETALLE_' + codDetalle).hide();
}



var idObservacionSel = 0;
function CambiarIdResolverObs(codDetalle,desDetalle) {
    idObservacionSel = codDetalle;
    $('#ObservacionInicial').html(desDetalle);
    $('#resObservacion').val('');
}


var idObservarDetalle = 0;
function CambiarIdObservarDetalle(codDetalle, desDetalle) {
    $('#NombreItem').html(desDetalle);
    idObservarDetalle = codDetalle;    
    $('#btnObsAceptar').hide();
    $('#btnObsGuardar').show();
}

function ObservarDetalle() {

    if ($('#commentObservacion').val() == "") {
        $('#alertaObser').fadeIn(2000, function () {
            $('#alertaObser').fadeOut(2000);
        });

    } else {


        $('#btnObsGuardar').fadeOut(500, function () {
            $('#btnEspere').fadeIn(500);

            $.post("/Presupuesto/ObservarDetalle", { idDetalle: idObservarDetalle, observacion: $('#commentObservacion').val() })
                  .done(function (data) {
                      if (data) {
                          $('#btnEspere').fadeOut(500, function () {
                              $('#btnObsAceptar').fadeIn(500);
                          });

                      } else {
                          $('#btnEspere').fadeOut(500, function () {

                              $('#btnObsGuardar').fadeIn(500);
                          });
                      }

                  });
        });

    }
    
}


function MostrarResolucionObs(obser, Resobs) {
    $('#ObservacionResAnt').html(obser);
    $('#ObserResolucion').html(Resobs);
}

function ResolverObservacion() {
    $('#btnAceptarRes').fadeOut(500, function () {
        $('#btnEspereRes').fadeIn(500);

        $.post("/Presupuesto/ResolverObservacion", { idObservacion: idObservacionSel, observacion: $('#resObservacion').val() })
              .done(function (data) {
                  if (data) {
                      $('#btnEspereRes').fadeOut(500, function () {
                          $('#btnObsAceptarRes').fadeIn(500);
                      });

                  } else {
                      $('#btnEspereRes').fadeOut(500, function () {

                          $('#btnAceptarRes').fadeIn(500);
                      });
                  }

              });
    });
}
$(document).ready(function () {
    $('[data-toggle="popover"]').popover();
});

