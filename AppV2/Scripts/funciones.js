function msgLogOut() {
    $('#idContenidoModal').html(EsperaModal());
    $.get("../Login/Logout", { idUsuario: 0 })
      .done(function (data) {
          $('#idContenidoModal').fadeOut(500, function () {
              $('#idContenidoModal').html(data).fadeIn(500);
          });
      })
    .fail(function (data) {
        $('#idContenidoModal').fadeOut(500, function () {
            $('#idContenidoModal').html(EsperaModalFAIL()).fadeIn(500);
        });
    });

}

function msgAprobar(idPresupuesto) {

    $('#idContenidoModal').html(EsperaModal());
    $.get("../Presupuesto/AprobarPresup", { id: idPresupuesto })
      .done(function (data) {
          $('#idContenidoModal').fadeOut(500, function () {
              $('#idContenidoModal').html(data).fadeIn(500);
          });
      })
    .fail(function (data) {
        $('#idContenidoModal').fadeOut(500, function () {
            $('#idContenidoModal').html(EsperaModalFAIL()).fadeIn(500);
        });
    });

}

function verAreasDisponibles(idPresup) {

    $('#PresupPorArea').html(getImgEspera());
    $.post("../Presupuesto/PorArea", { idPresupuesto: idPresup })
    .done(function (data) {
        $('#PresupPorArea').fadeOut(1000, function () {
            $('#PresupPorArea').html(data).fadeIn(1000);
        });
        
    })
    .fail(function (data) {
        $('#PresupPorArea').fadeOut(1000, function () {
            $('#PresupPorArea').html(getErrorMesaje()).fadeIn(1000);
        });
        
    });


}

function AprobarPresupuesto(idPresupuesto) {

    $('#idContenidoModal').html(EsperaModal());

    $.post("../Presupuesto/AprobarPresup", {
        id: idPresupuesto, observacion: $('#observacionNuevoEstado').val(), estado: $('#nuevoEstado').val()
    })
    .done(function (data) {
        if (data) {
            $('#idContenidoModal').fadeOut(500, function () {
                $('#idContenidoModal').html(EsperaModalPERS('Se cambiado el estado con éxito')).fadeIn(500);
            });
        } else {
            $('#idContenidoModal').fadeOut(500, function () {
                $('#idContenidoModal').html(EsperaModalPERS('No se ha podido cambiar el estado')).fadeIn(500);
            });
        }
    })
        .fail(function (data) {
        $('#idContenidoModal').fadeOut(500, function () {
            $('#idContenidoModal').html(EsperaModalFAIL()).fadeIn(500);
        });
    });



}
function LogOut() {
    $.post("../Login/Logout", { idUsuario: 0 })
    .done(function (data) {
        location.href = "../Login";
    });
    
}
function mostrarPresupuestosPorSede() {
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


function ListarTipos(idPresupuesto) {
    $(AreaSelect).hide();
    $.get("/Presupuesto/TiposPresupuesto/" + idPresupuesto, {})
      .done(function (data) {
          $('#TiposPresup_' + idPresupuesto).fadeOut(500, function () {
              $('#TiposPresup_' + idPresupuesto).show();
              $('#TiposPresup_' + idPresupuesto).html(data).fadeIn(500);
          });
      });
    AreaSelect = '#TiposPresup_' + idPresupuesto;
}

function listarVersiones(idPresupTipo) {
    $.get("/Presupuesto/Versiones/" + idPresupTipo, { idArea: VAR_AREA_SEL, })
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


function showDataObservaciones(codDetalle,data) {
    $('#DETALLEDIV_' + codDetalle).hide();
    $('#DETALLEDIV_' + codDetalle).html(data);
    $('#DETALLE_' + codDetalle).show();
    $('#DETALLEDIV_' + codDetalle).fadeOut(500, function () {
        $('#DETALLEDIV_' + codDetalle).html(data).fadeIn(500);
    });
}

function EsperaModal() {
    var retstring='<div class="modal-content">';
    retstring=retstring+'<div class="modal-body">';
    retstring=retstring+'<div class="form-group">';
    retstring=retstring+'<center>';
    retstring = retstring + getImgEspera();
    retstring = retstring + '</center><br/><center><h4>Espere Por Favor</h4></center></div></div></div>';
    return retstring;
}

function EsperaModalPERS(mensage) {

    var retstring = '<div class="modal-content">';
    retstring = retstring + '<div class="modal-body">';
    retstring = retstring + '<div class="form-group">';
    retstring = retstring + '<center>';
    retstring = retstring + '<h3>' + mensage + '</h3>';
    retstring = retstring + '</center></div></div></div>';
    return retstring;

}

function EsperaModalFAIL() {

    var retstring = '<div class="modal-content">';
    retstring = retstring + '<div class="modal-body">';
    retstring = retstring + '<div class="form-group">';
    retstring = retstring + '<center>';
    retstring = retstring + '<h3>Hubo un error solicitando los datos</h3>';
    retstring = retstring + '</center></div></div></div>';
    return retstring;
}

function getImgEspera() {

    return '<div class="row" style="height: 120px;"><div class="cssload-box-loading"></div></div>';
}

function getErrorMesaje() {
    return '<div class="row" style="height: 120px;"><center><h5>Hubo un error al consultar la informacion </h5></center></div>';
}

function ExpandirObservaciones(codDetalle){
    $(TRSELECT).hide();
    $(DIVSEl).html('');

    showDataObservaciones(codDetalle, getImgEspera());

    $.get("/Presupuesto/Observaciones", { idDetalle: codDetalle })
      .done(function (data) {
          showDataObservaciones(codDetalle,data);
      })
        .fail(function (data) {
            showDataObservaciones(codDetalle, getErrorMesaje());
        })
    ;
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

    function MensajeObservarDetalle(mensaje) {

        $('#contObser').html(mensaje);
        $('#alertaObser').fadeIn(2000, function () {
            $('#alertaObser').fadeOut(2000);
        });
    }

    function ObservarDetalle(idObservarDetalle) {

        if ($('#commentObservacion').val() == "") {
            MensajeObservarDetalle('Observación no puede estar en blanco');
        } else {


            $('#btnObsGuardar').fadeOut(500, function () {
                $('#btnEspere').fadeIn(500);

                $.post("/Presupuesto/ObservarDetalle", { idDetalle: idObservarDetalle, observacion: $('#commentObservacion').val() })
                      .done(function (data) {
                          if (data) {
                              $('#btnEspere').fadeOut(500, function () {
                                  $('#btnObsAceptar').fadeIn(500);
                              });
                              MensajeObservarDetalle('Se guardó correctamente');
                          } else {
                              $('#btnEspere').fadeOut(500, function () {

                                  $('#btnObsGuardar').fadeIn(500);
                              });
                              MensajeObservarDetalle('Hubo un error al guardar, intente de nuevo');
                          }

                      })
                       .fail(function (data) {
                           MensajeObservarDetalle('Hubo un error con el servidor');
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

