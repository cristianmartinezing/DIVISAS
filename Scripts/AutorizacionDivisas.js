
$.holdReady(true);
$.getScript("./scripts/Common.js", function () {
    $.holdReady(false);
});

/**
* Utilidad: Clase que define el comportamiento 
 * de la pagina  autorizacionesdivisas.aspx
* 
 * Autor: Cristian Jordano Martinez Alvarez
* Fecha: Septiembre de 2021
*/
window.onload = function () {
    $('#page').show();


}
$(document).ready(function () {

    $("input, textarea").on("keypress", function () {
        $input = $(this);
        setTimeout(function () {
            $input.val($input.val().toUpperCase());
        }, 50);
    })

    $('#divResultadosDetalle').hide();  //Muestra la tabla 2 y 3 
    $('#divResultadosBusqueda').hide(); //Muestra los encabezados de la tabla 1, sin haberle dado BUSCAR
    $('#panelPrincipal').show(); //Muestra configuracion de autorizaciones,los botones BUSCAR y  NUEVO REGISTRO(PANEL PRINCIPAL)
    ListarDDLTpDocumento("dllTipoDocumentoB");

    
    var acceso = autenticarModulo("07");
    if (acceso.Codigo != "0000") {
        //redirecciona a pagina de error por permisos
        window.location.href = "NoAutorizado.aspx?codigo=" + acceso.Codigo + "&descripcion=" + acceso.Descripcion
        //$('#page').show();
    } else {
        $('#page').show();
    }




    //Al dar click en el boton BUSCAR 
    $('#btnBuscarPalabraClave').click(function () {


        var dialog = bootbox.dialog({
            message: '<p class="text-center mb-0"><i class="fa fa-spin fa-cog"></i>Buscando, por favor espere...</p>',
            closeButton: false
        });




        setTimeout(function () {


            dialog.modal('hide');


           


            var valor = $('#txtPalabraClave').val();  //Si hay un valor en BUSCAR ejemplo 1020 se va a la funcion ListarTBLGeneral
            var tipoDocumento = $('#dllTipoDocumentoB').val();  //Si hay un valor en BUSCAR ejemplo 1020 se va a la funcion ListarTBLGeneral




            objDivBuscar = {


                NRODOCUMENTO: $('#txtPalabraClave').val(),
                TIPODOCUMENTO: $('#dllTipoDocumentoB').val()
            }


            if (getCookie("usuarioNR") == "") {
                alert("Iniciar sesión, por favor");


            }
            else {



                ObtenerUsuario(getCookie("usuarioNR"), "C");

                ListarTBLDetalle(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO, "tblResultadoBusquedaDetalle"); //Muestra los datos de la tabla 3, usando la funcion ListarTBLDetalle


            }

        }, 3000);



       
    });




    //Ejecucion boton nueva Fila
    $("#btnNuevaFila").click(function () {

        nuevaFila();

    });


    $('#btnNuevoGeneral').click(function () {
        cantRegistros = 0;
        $('#divResultadosDetalle').show();

        $('#divResultadosBusqueda').hide();
        cantidad = 0;
        ListaObjetosDetalles = [];
        $('#tblResultadoBusquedaDetalle').empty();
        $('#tblResultadoBusquedaDetalleExtendido').empty();
        $('#txtPalabraClave').prop('disabled', false);
        $('#txtPalabraClave').val("");
        $('#txtTitulo').val("");
        $('#txtValorNumerico').val("");
        $('#txtFecha').val("");
        $('#txtDescr100').val("");
        $('#txtValue').val("");
        $('#txtValueDisplay').val("");
        $('#txtSqlQuery').val("");
        $('#btnNuevoGeneral').hide();
        $('#txtModificado').val("");

        $('#txtMantenimiento').val("");

    });


    //Guardar aprobaciones de divisas
    $('#btnGuardar22').click(function () {
        $('#btnNuevoGeneral').show();
        //        validaArrayCamposParam("campo_Parametros", true);  //Valida los campos de fecha
        if ($('#txtPalabraClave').val() == "") {    //Si no hay numero de cedula genera mensaje de error
            mensajeErrorGeneral("NUMERO DE DOCUMENTO requerido");
        } else {

          
            cantRegistros = 0;
            guardarAprobacion();  //Ejcuta la funcion guardar registro
            $('#txtPalabraClave').prop('disabled', true);


          
        }

    });
    $('#btnVolverBusqueda').click(function () {

        $('#divResultadosDetalle').hide();
        $('#divResultadosBusqueda').hide();
        $('#panelPrincipal').show();
        $('#btnNuevoGeneral').show();

    });
});
//funcion que valida los campos del formulario
function validaArrayCamposParam(campos, boolLista) {

    var contador = 0;
    var contadorFinal = 0;
    $('input[name^="' + campos + '"]').each(function () {

        contador = validarCampo(this, $(this).data("validador"));
        contadorFinal = contadorFinal + contador;
    });
    if (contadorFinal == 0) {

    } else {
        inicializaVariables();
    }
    //$('.alertasMsj').remove();
    //$('.alertasMsj').text("");
}



//Muestras las divisas pendientes por aprobar
function ListarTBLGeneralPendientes(objDivBuscar, tblListado) {


    var params = JSON.stringify({ valor: objDivBuscar });
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var metodo = "TraeDivisasPendiente";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));


    var filasHtml = "";
    var cant = 1;
    if (jQuery.isEmptyObject(objJson.ListaParametros)) {

        $('#panelPrincipal').show();
        mensajeErrorGeneral("No se obtuvieron registros.");
       
    } else {

        $('#panelPrincipal').hide();


        if (objJson.Codigo != 9999) {
            if (objJson.ListaParametros[0].NRODOCUMENTO != "") {
                $.each(objJson.ListaParametros, function (key, value) {

                    objTBLGeneral = {
                        NRODOCUMENTO: value.NRODOCUMENTO,
                        SEQNBR: value.SEQNBR,
                        AUTORIZADIVISA: value.AUTORIZADIVISA,
                        TIPOVIGENCIA: value.TIPOVIGENCIA,
                        SOPORTEDIVISA: value.SOPORTEDIVISA,
                        DATEEND: value.DATEEND,
                        MONTOPROMVENT: value.MONTOPROMVENT,
                        MONTOPROMEDIO: value.MONTOPROMEDIO,
                        ROWADDEDDTTM: value.ROWADDEDDTTM,
                        ROWADDEDOPPRID: value.ROWADDEDOPPRID,
                        ROWLASTMANDTTM: value.ROWLASTMANDTTM,
                        ROWLASTMANOPPRID: value.ROWLASTMANOPPRID,
                        BLOQUEODIVISAS: value.BLOQUEODIVISAS,
                        AA_AUTORIZACION_DIV: value.AA_AUTORIZACION_DIV,
                        AA_ESTADO_AUTORIZA: value.AA_ESTADO_AUTORIZA,
                        TIPODOCUMENTO: value.TIPODOCUMENTO

                    }
                    filasHtml += "<tr onclick='Actualizar(" + JSON.stringify(objTBLGeneral) + ")'>"; //Al hacer click sobre la fila se va a otro panel
                    filasHtml += "<td><a>" + cant + "</td>";
                    filasHtml += "<td><a>" + value.SEQNBR + "</a></td>";
                    filasHtml += "<td><a>" + value.AUTORIZADIVISA + "</a></td>";
                    filasHtml += "<td><a>" + value.TIPOVIGENCIA + "</td>";
                    filasHtml += "<td><a>" + value.SOPORTEDIVISA + "</td>";
                    filasHtml += "<td><a>" + fechaNormalizada(value.DATEEND) + "</td>";
                    filasHtml += "<td><a>" + value.MONTOPROMVENT + "</td>";
                    filasHtml += "<td><a>" + value.MONTOPROMEDIO + "</td>";
                    filasHtml += "<td><a>" + fechaNormalizada(value.ROWADDEDDTTM) + "</td>";
                    filasHtml += "<td><a>" + value.ROWADDEDOPPRID + "</td>";
                    filasHtml += "<td><a>" + value.ROWLASTMANDTTM + "</td>";
                    filasHtml += "<td><a>" + value.ROWLASTMANOPPRID + "</td>";
                    filasHtml += "<td><a>" + value.BLOQUEODIVISAS + "</td>";

                    Actualizar(objTBLGeneral);
                   
                    filasHtml += "</tr>";
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





//Funcion que muestra la tabla detalles de divisas con toda la información
var objTBLDetalleNuevo;
function ListarTBLDetalle(valorDetalle, valorTipoDocumento, tblListado) {


    var params = JSON.stringify({ valor: valorDetalle, valorTipoDoc: valorTipoDocumento });
    var metodo = "TraerOperacionesPendientes";

    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
   // console.log(objJson);
    var filasHtml = "";
    var filasHtmlExtendido = "";
    var cant = 1;
    if (jQuery.isEmptyObject(objJson.ListaParametrosDetalle)) {
        mensajeErrorGeneral("No hay Divisas por aprobar.");
        $('#panelPrincipal').hide();

        $("#" + tblListado).html(filasHtml);
    } else {

                $('#panelPrincipal').hide();


        if (objJson.Codigo != 9999) {
            if (objJson.ListaParametrosDetalle[0].NRODOCUMENTO != "") {

                $.each(objJson.ListaParametrosDetalle, function (key, value) {

                    var nuevoValor = value.MONTOPROMVENT.replace(',', '.');

                    (function (old) {
                        var dec = 0.12.toLocaleString().charAt(1),
                            tho = dec === "," ? "." : ".";

                        if (1000 .toLocaleString() !== "1,000.00") {
                            Number.prototype.toLocaleString = function () {
                                var neg = this < 0,
                                    f = this.toFixed(2).slice(+neg);
                                // console.log(neg, f);
                                return (neg ? "-" : "")
                                    + f.slice(0, -3).replace(/(?=(?!^)(?:\d{3})+(?!\d))/g, tho)
                                    + dec + f.slice(-2);
                            }
                        }
                    })(Number.prototype.toLocaleString);

                    var MONTOPROMVENT = parseFloat(value.MONTOPROMVENT);
                    var MONTOPROMEDIO = parseFloat(value.MONTOPROMEDIO);


                    var signo = "$";
                 

                    filasHtml += "<tr id='tr" + cant + "' >";
                    filasHtml += "<td>" + cant + "</td>";
                    filasHtml += "<td><input type='hidden' class='form-control inputAvvillas' style='width:20px' autocomplete='off' type='text' id='txtSeqNum" + cant + "' value='" + value.SEQNBR + "'  maxlength='38' ><a>" + value.SEQNBR + "</a>   </input></td>";

                    filasHtml += "<td>" + value.AUTORIZADIVISA + "</td>";

                    filasHtml += "<td>" + value.TIPOVIGENCIA + "</td>";

                    filasHtml += "<td>" + value.SOPORTEDIVISA + "</td>";

                    filasHtml += "<td><input type='hidden' class='form-control inputAvvillas' style='width:20px' autocomplete='off' type='text' id='txtDescr" + cant + "' value='" + value.DATEEND + "'  maxlength='38' ><a>" + fechaNormalizada(value.DATEEND) + "</a>   </input></td>";



                    filasHtml += "<td><input class='form-control inputAvvillas' autocomplete='off' type='text' id='txtDescrlong" + cant + "'  disabled='disabled' value='" + signo.concat(MONTOPROMVENT .toLocaleString())+ "'    onchange='ActualizaFilas(" + JSON.stringify(value) + "," + JSON.stringify(cant) + ")'/></td>";

                    filasHtml += "<td><input class='form-control inputAvvillas' autocomplete='off' type='text'   disabled='disabled' id='txtNbr" + cant + "' value='" + signo.concat(MONTOPROMEDIO .toLocaleString()) + "' onchange='ActualizaFilas(" + JSON.stringify(value) + "," + JSON.stringify(cant) + ")'/></td>";

                    filasHtml += "<td><input type='hidden' class='form-control inputAvvillas' autocomplete='off' type='text' id='txtEstado" + cant + "' value='" + value.AA_ESTADO_AUTORIZA + "'  maxlength='38' />" + value.AA_ESTADO_AUTORIZA + "</td>";


                    filasHtml += "<td>" + fechaNormalizada(value.ROWADDEDDTTM) + "</td>";


                    filasHtml += "<td ><input type='radio' id='approve" + cant + "' name='dec" + cant + "' value='SI' onclick='ShowHideDiv()'></td>";

                    filasHtml += "<td ><input type='radio' id='deny" + cant + "' name='dec" + cant + "' value='NO' onclick='ShowHideDiv()'> <label for='deny" + cant + "'></td>";


                    filasHtml += "</tr>";


                    filasHtmlExtendido += "<tr onchange='ActualizaFilas(" + JSON.stringify(value) + "," + JSON.stringify(cant) + ")'>";
                    filasHtmlExtendido += "<td><a>" + value.SEQNBR + "</a></td>";
                     filasHtmlExtendido += "<td><input class='form-control inputAvvillas' disabled='disabled' autocomplete='off' type='text'    id='DESCR254_1" + cant + "' value='" + value.ROWADDEDOPPRID + "' onchange='ActualizaFilas(" + JSON.stringify(value) + "," + JSON.stringify(cant) + ")'/></td>";

                    filasHtmlExtendido += "<td><input class='form-control ' disabled='disabled'  type='text' autocomplete='off'  name='campo_Parametros' data-validador='fecha'  data-nombre='Fecha_campo' data-mensaje='El campo solo admite fecha en formato (AAAA/MM/DD)' id='DESCR254_2" + cant + "' value='" + value.ROWLASTMANDTTM + "' maxlength='10' onchange='ActualizaFilas(" + JSON.stringify(value) + "," + JSON.stringify(cant) + ")'/></td>";
                     filasHtmlExtendido += "<td><input class='form-control inputAvvillas' disabled='disabled' autocomplete='off' type='text' id='DESCR254_3" + cant + "' value='" + value.ROWLASTMANOPPRID + "'  maxlength='254' onchange='ActualizaFilas(" + JSON.stringify(value) + "," + JSON.stringify(cant) + ")'/></td>";
                    


                    filasHtmlExtendido += "</tr>";

                    cant++;
                });
            }

        } else {


            mensajeErrorGeneral(objJson.Descripcion);

        }
    }

    $('#datatableBody').append(filasHtml);
    $("#" + tblListado).html(filasHtml);
    $("#" + tblListado).show();
    $("#tblResultadoBusquedaDetalleExtendido").html(filasHtmlExtendido);



    $("#divResultadosDetalle").show();



    $("#myTable").DataTable({

        "destroy": true,
        "processing": true,
        "filter": true,
        "lengthChange": true,
        "lengthMenu": [[10, 20, 30, -1], [10, 20, 30, 'Todos']],

        "language": {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            }
        }


    });


    for (i = 1; i <= cant; i++) {
        
        $('#txtSeqNum' + i).prop('disabled', true);
    }
    cantidad = cant - 1;

}






//Funcion actualizar que al hacer click sobre alguna fila de la tabla 1 , se dirige a otra vista
//Viene el objeto de la tabla con sus datos
function Actualizar(objeto) {
    ListaObjetosDetalles = [];


    $('#dllTipoDocumentoB').val(objeto.TIPODOCUMENTO),
        $('#txtPalabraClave').val(objeto.NRODOCUMENTO);
    $('#txtID').val(objeto.SEQNBR);
    $('#txtTitulo').val(objeto.AUTORIZADIVISA);
    $('#txtValorNumerico').val(objeto.TIPOVIGENCIA);
    $('#txtFecha').val(fechaNormalizada(objeto.DATEEND));
    $('#txtDescr100').val(objeto.SOPORTEDIVISA);
    $('#txtValue').val(objeto.MONTOPROMVENT);
    $('#txtValueDisplay').val(objeto.MONTOPROMEDIO);//Tope Compra
    $('#txtSqlQuery').val(objeto.ROWADDEDDTTM); //Fecha/Hora Introducción:
    $('#txtAñadido').val(objeto.ROWADDEDOPPRID); //Añadido Por:
    $('#txtModificado').val(objeto.ROWLASTMANDTTM); //Ultima modificación:
    $('#txtMantenimiento').val(fechaNormalizada(objeto.ROWLASTMANOPPRID));
    $('#txtBloqueo').val(objeto.BLOQUEODIVISAS); //Bloqueo Divisas:

    ListarTBLDetalle(objeto.NRODOCUMENTO, objeto.TIPODOCUMENTO, "tblResultadoBusquedaDetalle"); //Muestra los datos de la tabla 3, usando la funcion ListarTBLDetalle
    $('#divResultadosBusqueda').hide(); // se oculta la tabla 1

}
var ListaObjetosDetalles = [];



//Funcion para saber si existe el SEQ NBR En el array
function existeSeqNuminArray(seqNum) {
    var pos = 9999;
    if (ListaObjetosDetalles.length > 0) {
        for (i = 0; i < ListaObjetosDetalles.length; i++) {
            if (ListaObjetosDetalles[i].SEQNBR == seqNum) {
                pos = i;
                return pos;
            } else {
                pos = 9999;
            }
        }
    } else {
        pos = 9999;
    }
    //mensaje(pos);
    return pos;
}






function fechaNormalizada(fecha) {
    var fechaf;

    if (fecha != null) {
        fecha = fecha.split(" ");
        fechaf = $.trim(fecha[0].substr(0, 10));
        var result = fechaf.split("/");


        if (result[0] < 10) {
            result[0] = "0" + result[0];
        } else {
            result[0] = result[0];
        }
        if (result[1] < 10 && result[1].substr(0, 1) != "0") {
            result[1] = "0" + result[1];
        } else {
            result[1] = result[1];
        }
        var fechaFinal = result[0] + "/" + result[1] + "/" + result[2];
        // console.log(fechaFinal);
        return fechaFinal;
    } else {
        return "01/01/1900";
    }

}





$(document).ready(function () {
    //$("#myTable").DataTable();
    $("#approveAll").on("click", function () {
        $('input[value="SI"]').prop("checked", true);
    });
    $("#denyAll").on("click", function () {
        $('input[value="reject"]').prop("checked", true);
    });
    $('input[type="radio"]').on("click", updateCount);
});



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


function guardarAprobacion() {

    var totalSI = $('table input[value="SI"]').length;
    var countApprove = $('table input[value="SI"]:checked').length;
    var Negadas = $('table input[value="NO"]:checked').length;
    var count = $('table input[value="SI"]:checked');
    var totalNO = $('table input[value="NO"]').length;
    var sumaaa = totalSI;
    var countDeny = $('table input[type="radio"]').length;
    var sumaTotal = countDeny / 2;
    var NegadasAprobadas = Negadas + countApprove;

    if (NegadasAprobadas != 0) {

        var i = 1;

        for (i; i <= sumaTotal; i++) {
            if (document.getElementById('approve' + i).checked) {
                rate_value = document.getElementById('approve' + i).value;

                objTBLGeneral = {
                    AA_AUTORIZACION_DIV: $('#approve' + i).val(),
                    SEQNBR: $('#txtSeqNum' + i).val(),
                    NRODOCUMENTO: $('#txtPalabraClave').val(),
                    DATEEND: $('#txtDescr' + i).val(),
                    AUTORIZADIVISA: $('#txtDescOther' + i).val(),
                    AA_ESTADO_AUTORIZA: $('#txtEstado' + i).val(),
                    TIPODOCUMENTO: $('#dllTipoDocumentoB').val(),
                    TOPEVENTA: $('#txtDescrlong' + i).val().replace(/ [$.]/g, ''),
                    TOPECOMPRA: $('#txtNbr' + i).val().replace(/[$.]/g, ''),
                    ROWLASTMANOPPRID: getCookie("usuarioNR")

                }


                var TopeCompra = objTBLGeneral.TOPECOMPRA.replace(/[$.]/g, '');
                var TopeVenta = objTBLGeneral.TOPEVENTA.replace(/[$.]/g, '');

                var fechaActualObj = new Date();
                var month = fechaActualObj.getMonth() + 1;
                var day = fechaActualObj.getDate();
                var year = fechaActualObj.getFullYear();
                nuevaFecha = year + "/" + month + "/" + day;
                var d = new Date(nuevaFecha);
                var n = d.getTime();//Obtener el numero de milisegundos de la fecha actual
                var fecha = (objTBLGeneral.DATEEND).split('/');    //fecha final
                var fechaNew = fecha[2] + "/" + fecha[1] + "/" + fecha[0];
                var s = new Date(fechaNew);
                var l = s.getTime();


                if ((objTBLGeneral.AA_AUTORIZACION_DIV == "SI" || objTBLGeneral.AA_AUTORIZACION_DIV == true) && n < l) {

                    objTBLGeneral.AA_ESTADO_AUTORIZA = "Vigente";
                }


                if ((objTBLGeneral.AA_AUTORIZACION_DIV == "SI" || objTBLGeneral.AA_AUTORIZACION_DIV == true) && n > l) {
                    objTBLGeneral.AA_ESTADO_AUTORIZA = "Vencido";


                }

                if ((objTBLGeneral.AA_AUTORIZACION_DIV == "NO" || objTBLGeneral.AA_AUTORIZACION_DIV == false) && n >= l) {
                    objTBLGeneral.AA_ESTADO_AUTORIZA = "No Autorizado";
                }


                AprobacionTope(TopeCompra, TopeVenta, objUsuarioSesion.JOBCODE);



                if (objAutorizacionTope.puedeAutorizar == "N" || objAutorizacionTope.puedeAutorizar2 == "N") {

                 

                    alert("No tienes permiso para aprobar esta divisa No:" + objTBLGeneral.SEQNBR);

                } else {
                    ListaObjetosDetalles.push(objTBLGeneral);
                    guardarAutorizacionDiv(ListaObjetosDetalles);

                }
               

            }


            // NO APROBADAS

            if (document.getElementById('deny' + i).checked) {
                rate_value = document.getElementById('deny' + i).value;

                objTBLGeneral = {
                    AA_AUTORIZACION_DIV: $('#deny' + i).val(),
                    SEQNBR: $('#txtSeqNum' + i).val(),
                    NRODOCUMENTO: $('#txtPalabraClave').val(),
                    DATEEND: $('#txtDescr' + i).val(),
                    AUTORIZADIVISA: $('#txtDescOther' + i).val(),
                    AA_ESTADO_AUTORIZA: $('#txtEstado' + i).val(),
                    TIPODOCUMENTO: $('#dllTipoDocumentoB').val(),
                    TOPEVENTA: $('#txtDescrlong' + i).val(),
                    TOPECOMPRA: $('#txtNbr' + i).val(),
                    ROWLASTMANOPPRID: '109874616066'


                }

                var TopeCompra = objTBLGeneral.TOPECOMPRA.replace(/[$.]/g, '');
                var TopeVenta = objTBLGeneral.TOPEVENTA.replace(/[$.]/g, '');
               
                var fechaActualObj = new Date();
                var month = fechaActualObj.getMonth() + 1;
                var day = fechaActualObj.getDate();
                var year = fechaActualObj.getFullYear();
                nuevaFecha = year + "/" + month + "/" + day;
                var d = new Date(nuevaFecha);
                var n = d.getTime();//Obtener el numero de milisegundos de la fecha actual
                var fecha = (objTBLGeneral.DATEEND).split('/');    //fecha final
                var fechaNew = fecha[2] + "/" + fecha[1] + "/" + fecha[0];
                var s = new Date(fechaNew);
                var l = s.getTime();


                if ((objTBLGeneral.AA_AUTORIZACION_DIV == "SI" || objTBLGeneral.AA_AUTORIZACION_DIV == true) && n < l) {
                    objTBLGeneral.AA_ESTADO_AUTORIZA = "Vigente";
                }


                if ((objTBLGeneral.AA_AUTORIZACION_DIV == "SI" || objTBLGeneral.AA_AUTORIZACION_DIV == true) && n > l) {
                    objTBLGeneral.AA_ESTADO_AUTORIZA = "Vencido";
                }

                if ((objTBLGeneral.AA_AUTORIZACION_DIV == "NO" || objTBLGeneral.AA_AUTORIZACION_DIV == false) || n >= l) {
                    objTBLGeneral.AA_ESTADO_AUTORIZA = "No Autorizado";
                }



                AprobacionTope(TopeCompra, TopeVenta, objUsuarioSesion.JOBCODE);



                if (objAutorizacionTope.puedeAutorizar == "N" || objAutorizacionTope.puedeAutorizar2 == "N") {

                  

                    alert("No tienes permiso para no autorizar esta divisa No:" + objTBLGeneral.SEQNBR);

                } else {
                    ListaObjetosDetalles.push(objTBLGeneral);
                    guardarAutorizacionDiv(ListaObjetosDetalles);

                }

            }

        }// end for
      
        $("#myTable").dataTable().fnDestroy();
        ListarTBLDetalle(objTBLGeneral.NRODOCUMENTO, objTBLGeneral.TIPODOCUMENTO, "tblResultadoBusquedaDetalle");
       
    }


}



function CambiarEstado(arg) {



    var FechaActual = new Date();
    function formatDate(FechaActual) {

        var formatted_date = FechaActual.getDate() + "/" + (FechaActual.getMonth() + 1) + "/" + FechaActual.getFullYear();


        return formatted_date;
    }

    var FechaHoy = formatDate(FechaActual);




    if ((arg.AA_AUTORIZACION_DIV = "SI" || arg.AA_AUTORIZACION_DIV == true) && FechaHoy <= objTBLGeneral.DATEEND) {

        objTBLGeneral.AA_ESTADO_AUTORIZA = "Vigente";


    }


    if ((objTBLGeneral.AA_AUTORIZACION_DIV = "SI" || objTBLGeneral.AA_AUTORIZACION_DIV == true) && FechaHoy > objTBLGeneral.DATEEND) {

        objTBLGeneral.AA_ESTADO_AUTORIZA = "Vencido";


    }

    if ((objTBLGeneral.AA_AUTORIZACION_DIV = "NO" || objTBLGeneral.AA_AUTORIZACION_DIV == false) && FechaHoy > objTBLGeneral.DATEEND) {


        objTBLGeneral.AA_ESTADO_AUTORIZA = "No Autorizado";
    }


}


function guardarAutorizacionDiv(ListaObjetosDetalles) {
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";//Pruebas
    var params = JSON.stringify({ valor: ListaObjetosDetalles });
    var metodo = "ActualizarAutorizacionDiv";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
    if (objJson.Codigo == 9999) {
        mensajeErrorGeneral("Error al momento de guardar");
        throw new Error(objJson.Descripcion);
    }
    else {
        var dialog = bootbox.dialog({
            message: '<p class="text-center mb-0"><i class="fa fa-spin fa-cog"></i>Guardando, por favor espere...</p>',
            closeButton: false
        });




        setTimeout(function () {

            dialog.modal('hide');



        }, 2000);
    }
}


function updateCount() {
    var total = $('table input[value="SI"]').length;
    var countApprove = $('table input[value="SI"]:checked').length;
    var countDeny = $('table input[value="reject"]:checked').length;





    $('#changesCount').text((countApprove + countDeny) + ' changes ');
    //console.log(total, countApprove, countDeny);



    if (total === countApprove) {
        $('#approveAll').prop("checked", true);
        return;
    }
    if (total === countDeny) {
        $('#denyAll').prop("checked", true);
        return;
    }
    $('#denyAll,#approveAll').prop("checked", false);
}


function unselect() {

    var totalSI = $('table input[value="SI"]').length;


    for (var i = 1; i <= totalSI; i++) {

        document.getElementById("approve" + i).checked = false;
        document.getElementById("deny" + i).checked = false;


    }


}

function selectAll() {


    var totalSI = $('table input[value="SI"]').length;


    for (var i = 1; i <= totalSI; i++) {

        document.getElementById("approve" + i).checked = true;


    }




}


function NoAutorizarTodas() {


    var totalSI = $('table input[value="SI"]').length;


    for (var i = 1; i <= totalSI; i++) {


        document.getElementById("deny" + i).checked = true;


    }

}




function toggle(box, theId) {
    if (document.getElementById) {
        var row = document.getElementById(theId);
        if (box.checked) {
            row.className = "on";
        } else {
            row.className = "off";
        }
    }
}


function ShowHideDiv() {


    $('table input:checked').change(function () {
        var parentTr = $(this).closest('tr');
        parentTr.siblings().css('background-color', 'white');
        parentTr.css('background-color', '#FFFFAA');
    });
}



//1420
var objTBLEntidadNueva;
function AprobacionTope(valorDetalle, ValorVenta, valorJob) {


    var params = JSON.stringify({ valor: valorDetalle, valor2: ValorVenta, jobcode: valorJob });
    var metodo = "AutorizarTope";

    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
    //console.log(objJson);
    var filasHtml = "";
    var filasHtmlExtendido = "";
    var cantt = 1;
    if (jQuery.isEmptyObject(objJson.ListaOperacionesIntPN)) {
        mensajeErrorGeneral("No se obtuvieron registros en Detalles.");
      

        TraerCargosAutTope("150000");
        valor = objCargosAut.DESCR;
    


    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaOperacionesIntPN[0].puedeAutorizar != "") {

                $.each(objJson.ListaOperacionesIntPN, function (key, value) {

                    objAutorizacionTope = {
                        puedeAutorizar: value.puedeAutorizar,
                        puedeAutorizar2: value.puedeAutorizar2

                    }

                  
                    cantt++;


                });
            }
        } else {

            mensajeErrorGeneral(objJson.Descripcion);

        }
    }


    for (i = 1; i <= cantt; i++) {

        $('#txtSeqNum' + i).prop('disabled', true);
    }
    cantidadTopes = cantt - 1;

}







var objTBLEntidadNueva;
function TraerCargosAutTope(ValorMontoTopeVenta) {


    var params = JSON.stringify({ valor: ValorMontoTopeVenta });
    var metodo = "TraerCargosAutorizan";

    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
    //console.log(objJson);
    var filasHtml = "";
    var filasHtmlExtendido = "";
    var cantt = 1;
    if (jQuery.isEmptyObject(objJson.ListaOperacionesIntPN)) {
      


    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaOperacionesIntPN[0].DESCR != "") {

                $.each(objJson.ListaOperacionesIntPN, function (key, value) {

                    objCargosAut = {
                        DESCR: value.DESCR

                    }
                    cantt++;
                });
            }
        } else {

            mensajeErrorGeneral(objJson.Descripcion);

        }
    }

    for (i = 1; i <= cantt; i++) {
        $('#txtSeqNum' + i).prop('disabled', true);
    }
    cantidadCargos = cantt - 1;

}





function ObtenerUsuario(valorGeneral, TipoDocumento) {


    var params = JSON.stringify({ valor: valorGeneral, valor2: TipoDocumento });
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var metodo = "TraerUsuarioSesion";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));


    // console.log(objJson);
    var filasHtml = "";
    var cant = 1;
    if (jQuery.isEmptyObject(objJson.ListaUsuario)) {
        mensajeErrorGeneral("No se obtuvo usuario.");

        objUsuarioSesion = {

            JOBCODE: "0"



        }

    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaUsuario[0].NOMBRECOMPLETO != "") {
                $.each(objJson.ListaUsuario, function (key, value) {


                    objUsuarioSesion = {

                        JOBCODE: value.JOBCODE.replace(/^(0+)/g, ''),
                        NOMBRECOMPLETO: value.NOMBRECOMPLETO


                    }
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