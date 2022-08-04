//Importa libreria de funciones comunes


$.holdReady(true);
$.getScript("./scripts/Common.js", function () {
    $.holdReady(false);
});

/**
 * Utilidad: Clase que define el comportamiento 
 * de la pagina operacionesinternacionales.aspx
 * 
 * Autor: Cristian Martinez Alvarez
 * Fecha: Febrero 04 de 2021
 */
window.onload = function () {
    //$('#page').hide();
}
$(document).ready(function () {
    var acceso = autenticarModulo("07");
    if (acceso.Codigo != "0000") {
       //redirecciona a pagina de error por permisos
       window.location.href = "NoAutorizado.aspx?codigo=" + acceso.Codigo + "&descripcion=" + acceso.Descripcion
        //$('#page').show();
    } else {
        $('#page').show();
    }

    $("input, textarea").on("keypress", function () {
        $input = $(this);
        setTimeout(function () {
            $input.val($input.val().toUpperCase());
        }, 50);
    });


    $("#divPanelInscripcion").hide();
    $("#divResultadosBusqueda").hide();
    $("#divResultadosDetallesAutorizaciones").hide();

    $("#panelInformacion").hide();
    $("#panelPrincipal2").hide();


    $("#btnGuardar").hide();

    $("#btnModificar").hide();
    
    ListarDDLTpDocumento("dllTipoDocumentoB");

    $("#dllTipoDocumentoU").prop("disabled", true);
    $("#dllOperacionesInt").prop("enable", true);

    $("#dllOficina").prop("disabled", true);


    ListarDDLTpDocumento("dllTipoDocumentoU");
    ListarOperacionesInter("dllOperacionesInt");
    ListarDDLTpDocumento("dllTipoDocumentoNuevo");
    

});


//FUNCIONES
function ListarOficina(ddlOficina) {

    var svc = "./Servicios/WS_Utils.asmx/";//Pruebas
    var params = JSON.stringify({ numero: "100veces" });
    var metodo = "Lista_Oficina";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));

    var filasHtml = "";
    var cant = 1;
    $.each(objJson, function (key, value) {
        $('#' + ddlOficina).append("<option style='width:10px!important;' value='" + value.CODIGOOFICINA + "'>" + value.CODIGOOFICINA + " - " + value.DESCROFICINA + "</option>");
    });
}




function ListarDDLTpDocumento(ddltipodoc) {

    var svc = "./Servicios/WS_Utils.asmx/";//Pruebas
    var params = JSON.stringify({ numero: "100veces" });
    var metodo = "Lista_TipoIdentificacion";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));

    var filasHtml = "";
    var cant = 1;
    $.each(objJson, function (key, value) {
        $('#' + ddltipodoc).append("<option value='" + value.IdTipoIdentificacion + "'>" + value.ValorTipoIdentificacion + "</option>");
    });

    //ListaEntidades();


}



function ListarOperacionesInter(ddlopeint) {

    var svc = "./Servicios/WS_Utils.asmx/";//Pruebas
    var params = JSON.stringify({ numero: "100veces" });
    var metodo = "Lista_OperacInter";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));

    var filasHtml = "";
    var cant = 1;
    $.each(objJson, function (key, value) {
        $('#' + ddlopeint).append("<option value='" + value.IdOperacionesInt + "'>" + value.ValorOperacionesInt + "</option>");
    });


}




function ListarDivisasFieldChange(ddlopeint) {




}


function AV_ES_VALIDO_CUPO_PN_OPR_INT(opr, valor) {

    var usuario = getUsuario();
    objEliminar = {
        Modulo: "Probando",
        usuarioAplicacion: usuario,
        tipoDocumento: value.tipoDocumentoCod,
        numeroDocumento: value.numeroDocumento,
        tipoPersona: 0,
        nombreRazonSocial: "",
        primerNombre: value.primerNombre,
        segundoNombre: value.segundoNombre,
        primerApellido: value.primerApellido,
        segundoApellido: value.segundoApellido,
        partyID: value.PartyID
    }

    var svc = "./Servicios/WS_Eliminacion.asmx/";
    var metodo = "EliminarCliente";
    var params = JSON.stringify({ cliente: objEliminar });
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
    if (objJson.Codigo != 9999) {

        mensaje(objJson.Descripcion);
        //inicializaVariables();

    } else {
        var str1 = objJson.ErrorLog;
        var str2 = "CLIENTE NO EXISTE";
        if (str1.indexOf(str2) != -1) {
            mensajeErrorGeneral("CLIENTE NO EXISTE EN EL ARCHIVO 640");
        } else {
            mensajeErrorGeneral(objJson.Descripcion);

        }
    }

}

var ClienteTPBusqueda;


function BuscarCliente() {
    ClienteTPBusqueda = {
        TIPODOCUMENTO: $("#dllTipoDocumentoB option:selected").text(),
        NRODOCUMENTO: $("#txtNumeroDocumento").val(),
        PRIMERNOMBRE: $("#txtPrimerNombre").val(),
        SEGUNDONOMBRE: $("#txtSegundoNombre").val(),
        PRIMERAPELLIDO: $("#txtPrimerApellido").val(),
        SEGUNDOAPLELLIDO: $("#txtSegundoApellido").val()
        // ESTADOCLIENTE: $("#dllEstado ").val(),
        // USUARIOAPLICACION: "OFICINA_ASIGNADA"
    }

    var svc = "./Servicios/WS_Cliente.asmx/";//Pruebas
    var params = JSON.stringify({ valor: ClienteTPBusqueda });
    var metodo = "BuscarClienteTP_FIDEL";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
    var filasHtml = "";
    var filasUsuario = "";
    var cant = 1;
    if (jQuery.isEmptyObject(objJson.ListaClientesTP)) {
        mensajeErrorGeneral("No se obtuvieron registros.");
        mensaje(objJson.Descripcion);
        $("#tblResultadoBusqueda").html(filasHtml);
    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaClientesTP[0].NRODOCUMENTO != "") {
                $('#tblResultadoBusqueda').show();



                $.each(objJson.ListaClientesTP, function (key, value) {


                    objClientes = {
                        TIPODOCUMENTO: value.TIPODOCUMENTO,
                        FECHAULTIMAACTUALIZACION: value.FECHAULTIMAACTUALIZACION
                    }

                    if (value.TIPODOCUMENTO == 'C') {
                        var TIPODOCUMENTO = "CC";
                    }
                    if (value.TIPODOCUMENTO == 'T') {
                        var TIPODOCUMENTO = "TI";
                    }
                    if (value.TIPODOCUMENTO == 'R') {
                        var TIPODOCUMENTO = "RC";
                    }
                    if (value.TIPODOCUMENTO == 'P') {
                        var TIPODOCUMENTO = "PS";
                    }
                    if (value.TIPODOCUMENTO == 'E') {
                        var TIPODOCUMENTO = "CE";
                    }
                    if (value.TIPODOCUMENTO == 'N') {
                        var TIPODOCUMENTO = "NIT";
                    }



                    filasHtml += "<tr onclick='mostrarDatosCompletos(" + JSON.stringify(value) + ")'>";
                    filasHtml += "<td><a>" + cant + "</td>";
                    filasHtml += "<td><a>" + TIPODOCUMENTO + "  " + value.NRODOCUMENTO + "</a></td>";
                    filasHtml += "<td><a>" + value.PRIMERNOMBRE + " " + value.SEGUNDONOMBRE + " " + value.PRIMERAPELLIDO + " " + value.SEGUNDOAPLELLIDO + "</a></td>";
                    filasHtml += "<td><a>" + value.ESTADOCLIENTE + "</td>";
                    filasHtml += "</tr>";



                    if (objClientes.TIPODOCUMENTO != 'N') {


                        filasUsuario += "<div><h3>PERSONA NATURAL <h3></div>"; //Al hacer click sobre la fila se va a otro panel


                    }
                    else {


                        filasUsuario += "<div><h3>PERSONA JURIDICA <h3></div>"; //Al hacer click sobre la fila se va a otro panel


                    }

                    $("#tipo_cliente").append(filasUsuario);



                    var dialog = bootbox.dialog({
                        message: '<p class="text-center mb-0"><i class="fa fa-spin fa-cog"></i>Buscando, por favor espere...</p>',
                        closeButton: false
                    });

                    // do something in the background


                    setTimeout(function () {

                        dialog.modal('hide');
                        cantRegistros = 0;

                        objDivBuscar = {


                            NRODOCUMENTO: $('#txtNumeroDocumento').val(),
                            TIPODOCUMENTO: $('#dllTipoDocumentoB').val()
                        }

                        ListarOperacionesInterDetall(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO, 'tblResultadoBusqueda2');


                        $('#divResultadosBusqueda2').show(); //TABLA 3 MUESTRA LOS DIVS
                        $('#divResultadosDetalle').hide(); //Oculta las TABLA 2 y TABLA 3 
                        $('#btnNuevoGeneral').show();  // BOTON NUEVO REGISTRO

                        $('#panelPrincipal').hide();// BOTONES DE Buscar y Nuevo registro como panel principal



                        ListaEntidades();
                        var TipoDocumento = $('#dllTipoDocumentoU ').val();
                        ListarOperacionesInt(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO, 'tableOperaciones');
                        OperacionesMesaDineroVenta(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO);
                        OperacionesMesaDineroCompra(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO);
                        $("#divResultadosDetalle").show();
                        ListarTBLGeneral(objDivBuscar, "tblResultadoBusqueda2"); //ListarTBLGeneral(1020,tblResultadoBusqueda2) Muestra los datos de la tabla 1
                        ListaPaises();
                        ValidarListaBloqueo(objDivBuscar.NRODOCUMENTO);

                        var year = objJson.ListaClientesTP[0].FECHAULTIMAACTUALIZACION.substring(0, 4);
                        var month = objJson.ListaClientesTP[0].FECHAULTIMAACTUALIZACION.substring(4, 6);
                        var day = objJson.ListaClientesTP[0].FECHAULTIMAACTUALIZACION.substring(6, 8);
                        var Fecha = year + '/' + month + '/' + day;

                        var nuevaFecha = new Date(Fecha);
                        AV_INFO_ACTUALIZADA(nuevaFecha);
                        ListarTBLEntidades(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO, "tblEntidadesCliente");
                        ListarCargoAutoriza(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO);
                        ListarTBLDetalle(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO, "tblResultadoBusquedaDetalle");

                    }, 5000);


                    cant++;
                });
            }

        } else {
            mensajeErrorGeneral(objJson.Descripcion);
        }
        $("#tblResultadoBusqueda").html(filasHtml);
        $("#dllOficina").prop("disabled", true);
    }
}







var DetalleAutorizacion;
function BuscarDetalleAutoriz() {
    DetalleAutorizacion = {
        TIPODOCUMENTO: $("#dllTipoDocumentoB option:selected").text(),
        NRODOCUMENTO: $("#txtNumeroDocumento").val()

    }

    var svc = "./Servicios/WS_Cliente.asmx/";//Pruebas
    var params = JSON.stringify({ valor: DetalleAutorizacion });
    var metodo = "BuscarDetallesAutorizacion";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
    var filasHtml = "";
    var cant = 1;
    if (jQuery.isEmptyObject(objJson.ListaClientesTP)) {
        //mensajeErrorGeneral("No se obtuvieron registros.");
        mensaje(objJson.Descripcion);
        $("#tblResultadoBusquedaDetallesAut").html(filasHtml);
    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaClientesTP[0].NRODOCUMENTO != "") {
                $('#tblResultadoBusquedaDetallesAut').show();

                $.each(objJson.ListaClientesTP, function (key, value) {
                    filasHtml += "<tr onclick='mostrarDatosCompletos(" + JSON.stringify(value) + ")'>";
                    filasHtml += "<td><a>" + cant + "</td>";
                    filasHtml += "<td><a>" + value.SEQNBR + "</a></td>";
                    filasHtml += "<td><a>" + value.AUTORIZADIVISA + "</td>";
                    filasHtml += "<td><a>" + value.TIPOVIGENCIA + "</td>";
                    filasHtml += "<td><a>" + value.SOPORTEDIVISA + "</td>";
                    filasHtml += "<td><a>" + value.DATEEND + "</td>";
                    filasHtml += "<td><a>" + value.MONTOPROMVENT + "</td>";
                    filasHtml += "<td><a>" + value.MONTOPROMEDIO + "</td>";
                    filasHtml += "<td><a>" + value.ROWADDEDDTTM + "</td>";
                    filasHtml += "<td><a>" + value.ROWADDEDOPPRID + "</td>";
                    filasHtml += "<td><a>" + value.ROWLASTMANDTTM + "</td>";
                    filasHtml += "<td><a>" + value.ROWLASTMANOPPRID + "</td>";
                    filasHtml += "<td><a>" + value.BLOQUEODIVISAS + "</td>";

                    //filasHtml += "<td><a>" + value.FECHAULTIMAACTUALIZACION + "</td>";
                    //filasHtml += "<td><a>" + value.FECHACREACION + "</td>";
                    filasHtml += "</tr>";
                    cant++;
                });
            }
        } else {
            mensajeErrorGeneral(objJson.Descripcion);
        }
        $("#tblResultadoBusquedaDetallesAut").html(filasHtml);
        $("#dllOficina").prop("disabled", true);
    }
}



function AV_INFO_ACTUALIZADA(valorGeneral) {


    var params = JSON.stringify({ valor: valorGeneral });
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var metodo = "Av_info_actualizada";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));


    //console.log(objJson);
    var filasHtml = "";
    var cant = 1;
    if (jQuery.isEmptyObject(objJson.ListaClientesTP)) {
        //mensajeErrorGeneral("Sin alertas");
        // $("#" + tblListadoOpInt).html(filasHtml);
    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaClientesTP[0].VALUE_NBR != "") {
                $.each(objJson.ListaClientesTP, function (key, value) {

                    objTBLValidarCupon = {
                        VALUE_NBR: value.VALUE_NBR,
                        AUTORIZADIVISAS: value.AUTORIZADIVISAS

                    }


                    if (objTBLValidarCupon.AUTORIZADIVISAS == false) {


                        filasHtml += "<div><img src='./style/atencion.png' alt='' border=0  ></img><span style='font-weight:bold;'>La información del cliente tiene más de un año sin actualizarse, por tanto no se puede adicionar topes de divisas. </span></div>"; //Al hacer click sobre la fila se va a otro panel
                        filasHtml += "<div><img src='./style/atencion.png' alt='' border=0  ></img><span style='font-weight:bold;'> La información del cliente esta desactualizada.  </span></div>"; //Al hacer click sobre la fila se va a otro panel
                        filasHtml += "<br/>"; //Al hacer click sobre la fila se va a otro panel

                        $("#dllOperacionesInt2").val("N");

                        $('#divResultadosDetalle').hide();
                        $('#btnNuevaFila').hide();

                        $('#divPanelInformacion11').hide();

                        $('#AprobacionVigenteDiv').hide();
                        $('#AprobacionCargoDiv').hide();

                        $('#dllOperacionesInt2').prop('disabled', true);

                       // $("#dllOperacionesInt2").val("N");

                        var valor = $('#txtNumeroDocumento').val();  //Si hay un valor en BUSCAR ejemplo 1020 se va a la funcion ListarTBLGeneral
                        var TipoDocumento = $('#dllTipoDocumentoU').val();  //Si hay un valor en BUSCAR ejemplo 1020 se va a la funcion ListarTBLGeneral


                       // console.log("LINEA 3520");

                        ListarTBLEntidades(valor, TipoDocumento, "tblEntidadesCliente");



                    }

                    else {


                        $('#btnNuevaFila').show();

                    }



                    $("#info_actualizada").append(filasHtml);



                    cant++;
                });
            }
        } else {

            mensajeErrorGeneral(objJson.Descripcion);

        }

    }
    if (objJson.Codigo == 9999) {
        mensajeErrorGeneral(objJson.Descripcion);
        throw new Error(objJson.Descripcion);
    }


}







function formatoFecha(fecha) {
    if (fecha == "0") {
        return "----------";
    } else {
        return fecha.substr(0, 4) + "/" + fecha.substr(4, 2) + "/" + fecha.substr(6, 2);
    }
}
function mostrarDatosCompletos(value) {
    $("#divPanelInformacion").show();
    $("#divPanelPrincipal").show();

    $("#btnModificar").show();
    $("#btnGuardar").hide();
    $("#panelInformacion :input").prop("readonly", true);
    $("#panelPrincipal2 :input").prop("readonly", true);

    $("#btnModificar").prop("readonly", false);
    $("#divTableResultadosBusqueda").removeClass("col-lg-12");
    $("#divTableResultadosBusqueda").addClass("col-lg-6");

    $("#divTableResultadosDetallesAut").removeClass("col-lg-12");
    $("#divTableResultadosDetallesAut").addClass("col-lg-6");

    $("#divPanelInformacion").css("margin-left", "0");
    $("#divPanelPrincipal").css("margin-left", "0");

    switch (value.TIPODOCUMENTO) {
        case "C":
            value.TIPODOCUMENTO = "CC";
            break;
        case "E":
            value.TIPODOCUMENTO = "CE";
            break;
        case "T":
            value.TIPODOCUMENTO = "TI";
            break;
        case "R":
            value.TIPODOCUMENTO = "RC";
            break;
        case "N":
            value.TIPODOCUMENTO = "NIT";
            break;
    }
    $("#dllTipoDocumentoU option:selected").text(value.TIPODOCUMENTO);
    $("#dllOperacionesInt option:selected").text(value.OPERACIONES_INTER);
   

    $("#txtRazonSocial").val(value.RAZON_SOCIAL);

    
    $("#txtDocumentoU").val(value.NRODOCUMENTO);
    $("#txtPrimerNombreU").val(value.PRIMERNOMBRE);
    $("#txtSegundoNombreU").val(value.SEGUNDONOMBRE);
    $("#txtPrimerApellidoU").val(value.PRIMERAPELLIDO);
    $("#txtSegundoApellidoU").val(value.SEGUNDOAPLELLIDO);
    $("#txtEstadoU").val(value.ESTADOCLIENTE);
    $("#dllOficina option:selected").text(value.CODOFICINA);
    //$("#txtOficinaU").val(value.CODOFICINA);
    $("#txtSegmentoU").val(value.CODSEGMENTO);
    //$("#txtFechaUltimaActualizaU").val(formatoFecha(value.FECHAULTIMAACTUALIZACION));
    //$("#txtFechaInscripcionU").val(formatoFecha(value.FECHACREACION));
    $("#txtOperacionesInt").val(value.NRODOCUMENTO);

}
