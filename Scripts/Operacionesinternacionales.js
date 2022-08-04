
$.holdReady(true);
$.getScript("./scripts/Common.js", function () {
    $.holdReady(false);
});


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

    //$('#divResultadosDetalle').show();  //Muestra la tabla 2 y 3 
    $('#divResultadosBusqueda2').hide(); //Muestra los encabezados de la tabla 1, sin haberle dado BUSCAR
    $('#panelPrincipal').show(); //Muestra configuracion de autorizaciones,los botones BUSCAR y  NUEVO REGISTRO(PANEL PRINCIPAL)



    var acceso = autenticarModulo("05");


    if (acceso.Codigo != "0000") {
        //redirecciona a pagina de error por permisos
        window.location.href = "NoAutorizado.aspx?codigo=" + acceso.Codigo + "&descripcion=" + acceso.Descripcion
        //$('#page').show();
    } else {
        $('#page').show();
    }



    //Al dar click en el boton BUSCAR 
    $("#btnBuscarCliente").click(function () {



        if (getCookie("usuarioNR") == "") {
            bootbox.alert("Iniciar sesión, por favor");


        }
        else {


            ObtenerUsuario(getCookie("usuarioNR"), "C");
            $("#divPanelInscripcion").hide();
            $("#btnInscribir").hide();
            $("#panelInformacion :input").prop("readonly", true);
            $("#panelPrincipal2 :input").prop("readonly", true);

            $("#divPanelInformacion").css("margin-left", "0");
            $("#divPanelPrincipal").css("margin-left", "0");
            $("#divTableResultadosBusqueda").removeClass("col-lg-6");
            $("#divTableResultadosBusqueda").addClass("col-lg-12");
            $("#divTableResultadosDetallesAut").removeClass("col-lg-6");
            $("#divTableResultadosDetallesAut").addClass("col-lg-12");

            $("#divResultadosBusqueda").show();
            $("#divResultadosDetallesAutorizaciones").show();

            $("#divTableResultadosBusqueda").show();
            $("#divTableResultadosDetallesAut").show();
            $("#panelInformacion").show();
            $("#panelPrincipal2").show();

            $("#btnGuardar").hide();
            $("#divPanelInformacion").hide();
            $("#divPanelPrincipal").hide();


            validaArrayCampos("campo_validacion", false);
            BuscarCliente();
        }




    });


    $('#BotonEntidades').click(function () {


        ListaEntidades();
    });



    $('#BotonPaises').click(function () {


        ListaPaises();
    });


    $('#BotonMonedas').click(function () {


        ListaTiposMoneda();
    });


    $("#btnNuevaFilaEntidad").click(function () {


        var NroDocumento2 = $('#txtNumeroDocumento').val();

        var TipoDocumento2 = $('#dllTipoDocumentoB').val();  //Si hay un valor en BUSCAR ejemplo 1020 se va a la funcion ListarTBLGeneral




        ListarTBLEntidades(NroDocumento2, TipoDocumento2, "tblEntidadesCliente");
        nuevaFilaEntidad();

    });

    //Ejecucion boton nueva Fila
    $("#btnNuevaFila").click(function () {




        nuevaFila();

    });


    $('#btnNuevoGeneral').click(function () {
        cantRegistros = 0;
        $('#divResultadosDetalle').show();

        $('#divResultadosBusqueda2').hide();
        cantidad = 0;
        ListaObjetosDetalles = [];
        ListaObjetosOperaciones = [];
        ListaObjetosEntidades = [];

        $('#tblResultadoBusquedaDetalle').empty();
        $('#tblResultadoBusquedaDetalleExtendido').empty();

        $('#txtNumeroDocumento').prop('disabled', false);
        $('#txtNumeroDocumento').val("");
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







    //Al dar click en el boton guardar se muestra el boton NUEVO REGISTRO
    $('#btnGuardarForm').click(function () {



        validaArrayCamposParam("campo_Parametros", true);  //Valida los campos de fecha
        //ListarTBLGeneral($('#txtNumeroDocumento').val(), "tblResultadoBusqueda2");
        // Actualizar(objTBLGeneral);

        if ($('#txtNumeroDocumento').val() == "") {    //Si no hay numero de cedula genera mensaje de error
            mensajeErrorGeneral("NUMERO DE DOCUMENTO requerido");
        }



        //Se digita información de la tabla #03
        if (objTBLDetalleNuevo != undefined) {



            GuardarRegistroEntidadesClte();

            $("#TablaEntidades").dataTable().fnDestroy();

            //GuardarRegistroOpInter();
            ListarTBLEntidades(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO, "tblEntidadesCliente");

     
            DataTableEntidades();
            //ListarOperacionesInterDetall(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO, 'tblResultadoBusqueda2');
            var cantValidacionesDetalles = 0;
            GuardarRegistroOpInter();


            var regExp = /[a-zA-Z.]/g;

            cantRegistros = 0

            if (cantidadEntidadesListaLL == 0) {

                var elemento = document.getElementById("divPanelInformacion11");
                elemento.scrollIntoView();
                document.getElementById("btnNuevaFilaEntidad").style.background = "#FF0000";
                bootbox.alert("Si el cliente efectua Operaciones Internacionales se debe diligenciar al menos una Entidad");
                cantValidacionesDetalles++;

            }

            else if (objTBLGeneral2.AA_IMPORTACION == "N" && objTBLGeneral2.AA_EXPORTACION == "N" && objTBLGeneral2.AA_INVERSION == "N" && objTBLGeneral2.AA_PRESTAMOS == "N" && objTBLGeneral2.AA_GIROS == "N" && objTBLGeneral2.AA_ENVIO_GIROS == "N" && objTBLGeneral2.AA_SERVICIOS == "N") {

                var elemento = document.getElementById("divPanelInformacion11");
                elemento.scrollIntoView();
                cantValidacionesDetalles++;

                bootbox.alert("Si el cliente efectua Operaciones Internacionales se debe diligenciar al menos un tipo de Operación");
            }

            else if (objTBLGeneral2.AA_CTA_EXTERIOR == "Y") {

                if (objTBLGeneral2.COUNTRY == "" || objTBLGeneral2.COUNTRY == " " || objTBLGeneral2.AA_SALDO_FIN == "" || objTBLGeneral2.AA_SALDO_FIN == " " || objTBLGeneral2.AA_CIUDAD_OPR == "" || objTBLGeneral2.AA_CIUDAD_OPR == " " ||
                    objTBLGeneral2.AA_ENTIDAD_OPR == "" || objTBLGeneral2.AA_ENTIDAD_OPR == " " || objTBLGeneral2.AA_TIPO_CUENTA == "" || objTBLGeneral2.AA_TIPO_CUENTA == " " || objTBLGeneral2.CURRENCY_CD_BASE == "" || objTBLGeneral2.CURRENCY_CD_BASE == " "
                    || objTBLGeneral2.AA_NRO_CUENTA == "" || objTBLGeneral2.AA_NRO_CUENTA == " ") {

                    var elemento = document.getElementById("choice");
                    elemento.scrollIntoView();


                    if (objTBLGeneral2.COUNTRY == "" || objTBLGeneral2.COUNTRY == " ") {
                        document.getElementById("txtPais").style.background = "#FFB9B9";
                    }
                    else {
                        document.getElementById("txtPais").style.background = "#FFFFFF";
                    }
                    if (objTBLGeneral2.AA_SALDO_FIN == "" || objTBLGeneral2.AA_SALDO_FIN == " ") {
                        document.getElementById("txtSaldoExt").style.background = "#FFB9B9";
                    }
                    else {
                        document.getElementById("txtSaldoExt").style.background = "#FFFFFF";
                    }
                    if (objTBLGeneral2.AA_CIUDAD_OPR == "" || objTBLGeneral2.AA_CIUDAD_OPR == " ") {
                        document.getElementById("txtCiudad").style.background = "#FFB9B9";
                    }
                    else {
                        document.getElementById("txtCiudad").style.background = "#FFFFFF";
                    }

                    if (objTBLGeneral2.AA_ENTIDAD_OPR == "" || objTBLGeneral2.AA_ENTIDAD_OPR == " ") {
                        document.getElementById("txtEntidad").style.background = "#FFB9B9";
                    }
                    else {
                        document.getElementById("txtEntidad").style.background = "#FFFFFF";
                    }
                    if (objTBLGeneral2.AA_TIPO_CUENTA == "" || objTBLGeneral2.AA_TIPO_CUENTA == " ") {
                        document.getElementById("txtTipoCuenta").style.background = "#FFB9B9";
                    }
                    else {
                        document.getElementById("txtTipoCuenta").style.background = "#FFFFFF";
                    }
                    if (objTBLGeneral2.CURRENCY_CD_BASE == "" || objTBLGeneral2.CURRENCY_CD_BASE == " ") {
                        document.getElementById("txtTipoMoneda").style.background = "#FFB9B9";
                    }
                    else {
                        document.getElementById("txtTipoMoneda").style.background = "#FFFFFF";
                    }

                    if (objTBLGeneral2.AA_NRO_CUENTA == "" || objTBLGeneral2.AA_NRO_CUENTA == " ") {
                        document.getElementById("txtNroProducto").style.background = "#FFB9B9";
                    }
                    else {
                        document.getElementById("txtNroProducto").style.background = "#FFFFFF";
                    }

                    if (objTBLGeneral2.AA_SALDO_FIN == "" || objTBLGeneral2.AA_SALDO_FIN == " ") {
                        document.getElementById("txtSaldoExt").style.background = "#FFB9B9";
                    }
                    else {
                        document.getElementById("txtSaldoExt").style.background = "#FFFFFF";
                    }

                    bootbox.alert("Todos los campos de productos en el exterior son requeridos.");

                    cantValidacionesDetalles++;


                }
                else if (regExp.test(objTBLGeneral2.AA_SALDO_FIN)) {

                    document.getElementById("txtSaldoExt").style.background = "#FFB9B9";


                    var txtSaldoExt = document.getElementById("text");

                    txtSaldoExt.scrollIntoView();
                    bootbox.alert("El valor digitado de saldo producto en el exterior es inválido y");

                    cantValidacionesDetalles++;

                }

            }

            else if (objTBLDetalleNuevo.TIPOVIGENCIA == "M" && (objTBLDetalleNuevo.SOPORTEDIVISA == " " || objTBLDetalleNuevo.SOPORTEDIVISA == "NO")) {
                var elemento = document.getElementById("mienlace");
                elemento.scrollIntoView();
                bootbox.alert("Para una vigencia Mensual, se requiere un soporte para la divisa");
                cantValidacionesDetalles++;
            }
            else if (objTBLDetalleNuevo.TIPOVIGENCIA == "P" && objTBLDetalleNuevo.SOPORTEDIVISA == " ") {
                var elemento = document.getElementById("mienlace");
                elemento.scrollIntoView();
                bootbox.alert("El campo Soportes para Divisas es obligatorio para vigencias Puntuales");
                document.getElementById("DATEFIELD_N" + objTBLDetalleNuevo.SEQNBR).style.background = "#FFB9B9";
                cantValidacionesDetalles++;

            }


            else if (objTBLDetalleNuevo.TIPOVIGENCIA == "S" && objTBLDetalleNuevo.SOPORTEDIVISA == " ") {

                var elemento = document.getElementById("mienlace");
                elemento.scrollIntoView();
                bootbox.alert("El campo Soportes para Divisas es obligatorio para vigencias Semestrales");
                cantValidacionesDetalles++;

            }

            else if (objTBLDetalleNuevo.MONTOPROMEDIO == "" && objTBLDetalleNuevo.MONTOPROMVENT == "") {

                var elemento = document.getElementById("mienlace");
                elemento.scrollIntoView();
                document.getElementById("txtDescrlong_N" + objTBLDetalleNuevo.SEQNBR).style.background = "#FFB9B9";
                document.getElementById("txtNbr_N" + objTBLDetalleNuevo.SEQNBR).style.background = "#FFB9B9";
                bootbox.alert("Una Autorización no debe tener valores en cero para compra y venta simultaneamente");
                cantValidacionesDetalles++;

            }


            else if (regExp.test(objTBLDetalleNuevo.MONTOPROMEDIO) && regExp.test(objTBLDetalleNuevo.MONTOPROMVENT)) {


                var elemento = document.getElementById("mienlace");
                elemento.scrollIntoView();
                document.getElementById("txtDescrlong_N" + objTBLDetalleNuevo.SEQNBR).style.background = "#FFB9B9";
                document.getElementById("txtNbr_N" + objTBLDetalleNuevo.SEQNBR).style.background = "#FFB9B9";
                bootbox.alert("Solo valores númericos separados por coma,por favor válide la información diligenciada de los topes");
                cantValidacionesDetalles++;

            }


            else if (regExp.test(objTBLDetalleNuevo.MONTOPROMEDIO)) {


                var elemento = document.getElementById("mienlace");
                elemento.scrollIntoView();
                document.getElementById("txtNbr_N" + objTBLDetalleNuevo.SEQNBR).style.background = "#FFB9B9";
                bootbox.alert("Solo valores númericos separados por coma, por favor válide la información diligenciada del tope de compra.");
                cantValidacionesDetalles++;

            }



            else if (regExp.test(objTBLDetalleNuevo.MONTOPROMVENT)) {

                var elemento = document.getElementById("mienlace");
                elemento.scrollIntoView();
                document.getElementById("txtDescrlong_N" + objTBLDetalleNuevo.SEQNBR).style.background = "#FFB9B9";
                bootbox.alert("Solo valores númericos separados por coma, por favor válide la información diligenciada del tope de venta. Ej 1500,20");
                cantValidacionesDetalles++;

            }


            else if (objTBLDetalleNuevo.TIPOVIGENCIA == " ") {

                var elemento = document.getElementById("mienlace");
                elemento.scrollIntoView();



                cantValidacionesDetalles++;

                bootbox.alert("El campo Tipo de Vigencia y Soportes para Divisas es obligatorio");
            }

            else if (cantidadEntidadesListaLL == 0) {

                var elemento = document.getElementById("divPanelInformacion11");
                elemento.scrollIntoView();
                document.getElementById("btnNuevaFilaEntidad").style.background = "#FF0000";
                bootbox.alert("Si el cliente efectua Operaciones Internacionales se debe diligenciar al menos una Entidad 284");


                //document.getElementById("txtDescrlong_N" + objTBLDetalleNuevo.SEQNBR).style.background = "#FFB9B9";


                // document.getElementById("txtDescrlong_N" + objTBLDetalleNuevo.SEQNBR).style.background = "#FFC7C7";
                // document.getElementById("txtNbr_N" + objTBLDetalleNuevo.SEQNBR).style.background = "#FFC7C7";

                cantValidacionesDetalles++;
            }

            else if (objTBLGeneral2.AA_IMPORTACION == "N" && objTBLGeneral2.AA_EXPORTACION == "N" && objTBLGeneral2.AA_INVERSION == "N" && objTBLGeneral2.AA_PRESTAMOS == "N" && objTBLGeneral2.AA_GIROS == "N" && objTBLGeneral2.AA_ENVIO_GIROS == "N" && objTBLGeneral2.AA_SERVICIOS == "N") {

                var elemento = document.getElementById("divPanelInformacion11");
                elemento.scrollIntoView();
                cantValidacionesDetalles++;

                bootbox.alert("Si el cliente efectua Operaciones Internacionales se debe diligenciar al menos un tipo de Operación");
            }



            if (cantValidacionesDetalles == 0) {

                guardarDetalle(ListaObjetosDetalles);

                GuardarRegistroOpInter();  // Guardar lo relacionado con PS_AA_PN_OPR_INTER
                //ListarOperacionesInterDetall(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO, 'tblResultadoBusqueda2');
                TopeVentaUSD(objTBLGeneral2.NRODOCUMENTO, objTBLGeneral2.TIPODOCUMENTO);




            }
            //ListarTBLEntidades(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO, "tblEntidadesCliente");
            //ListarOperacionesInterDetall(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO, 'tblResultadoBusqueda2');



            if (cantValidacionesDetalles == 0) {

                // guardarDetalle(ListaObjetosDetalles);

                $('#txtNumeroDocumento').prop('disabled', true);
                GuardarRegistroOpInter();  // Guardar lo relacionado con PS_AA_PN_OPR_INTER

                GuardarRegistro();

                objDivBuscar = {


                    NRODOCUMENTO: $('#txtNumeroDocumento').val(),
                    TIPODOCUMENTO: $('#dllTipoDocumentoB').val()
                }


                ListarOperacionesInterDetall(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO, 'tblResultadoBusqueda2');
                GuardarRegistroOpInter();  // Guardar lo relacionado con PS_AA_PN_OPR_INTER

            }


        }








        //Si no se inserta información en la tabla 3
        else {

            var validacion340 = 0;
            cantRegistros = 0

            GuardarRegistroEntidadesClte();
            $("#TablaEntidades").dataTable().fnDestroy();

            //GuardarRegistroOpInter();
            ListarTBLEntidades(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO, "tblEntidadesCliente");
            //ListarOperacionesInterDetall(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO, 'tblResultadoBusqueda2');
            DataTableEntidades();

            GuardarRegistroOpInter();

            var regExp = /[a-zA-Z.]/g;


            if (cantidadEntidadesListaLL == 0) {

                var elemento = document.getElementById("divPanelInformacion11");
                elemento.scrollIntoView();
                document.getElementById("btnNuevaFilaEntidad").style.background = "#FF0000";
                bootbox.alert("Si el cliente efectua Operaciones Internacionales se debe diligenciar al menos una Entidad");
                validacion340++;
            }

            else if (objTBLGeneral2.AA_IMPORTACION == "N" && objTBLGeneral2.AA_EXPORTACION == "N" && objTBLGeneral2.AA_INVERSION == "N" && objTBLGeneral2.AA_PRESTAMOS == "N" && objTBLGeneral2.AA_GIROS == "N" && objTBLGeneral2.AA_ENVIO_GIROS == "N" && objTBLGeneral2.AA_SERVICIOS == "N") {

                var elemento = document.getElementById("divPanelInformacion11");
                elemento.scrollIntoView();
                validacion340++;

                bootbox.alert("Si el cliente efectua Operaciones Internacionales se debe diligenciar al menos un tipo de Operación");
            }


            else if (objTBLGeneral2.AA_CTA_EXTERIOR == "Y") {

                if (objTBLGeneral2.COUNTRY == "" || objTBLGeneral2.COUNTRY == " " || objTBLGeneral2.AA_SALDO_FIN == "" || objTBLGeneral2.AA_SALDO_FIN == " " || objTBLGeneral2.AA_CIUDAD_OPR == "" || objTBLGeneral2.AA_CIUDAD_OPR == " " ||
                    objTBLGeneral2.AA_ENTIDAD_OPR == "" || objTBLGeneral2.AA_ENTIDAD_OPR == " " || objTBLGeneral2.AA_TIPO_CUENTA == "" || objTBLGeneral2.AA_TIPO_CUENTA == " " || objTBLGeneral2.CURRENCY_CD_BASE == "" || objTBLGeneral2.CURRENCY_CD_BASE == " "
                    || objTBLGeneral2.AA_NRO_CUENTA == "" || objTBLGeneral2.AA_NRO_CUENTA == " ") {

                    var elemento = document.getElementById("choice");
                    elemento.scrollIntoView();


                    if (objTBLGeneral2.COUNTRY == "" || objTBLGeneral2.COUNTRY == " ") {
                        document.getElementById("txtPais").style.background = "#FFB9B9";
                    }
                    else {
                        document.getElementById("txtPais").style.background = "#FFFFFF";
                    }
                    if (objTBLGeneral2.AA_SALDO_FIN == "" || objTBLGeneral2.AA_SALDO_FIN == " ") {
                        document.getElementById("txtSaldoExt").style.background = "#FFB9B9";
                    }
                    else {
                        document.getElementById("txtSaldoExt").style.background = "#FFFFFF";
                    }
                    if (objTBLGeneral2.AA_CIUDAD_OPR == "" || objTBLGeneral2.AA_CIUDAD_OPR == " ") {
                        document.getElementById("txtCiudad").style.background = "#FFB9B9";
                    }
                    else {
                        document.getElementById("txtCiudad").style.background = "#FFFFFF";
                    }

                    if (objTBLGeneral2.AA_ENTIDAD_OPR == "" || objTBLGeneral2.AA_ENTIDAD_OPR == " ") {
                        document.getElementById("txtEntidad").style.background = "#FFB9B9";
                    }
                    else {
                        document.getElementById("txtEntidad").style.background = "#FFFFFF";
                    }
                    if (objTBLGeneral2.AA_TIPO_CUENTA == "" || objTBLGeneral2.AA_TIPO_CUENTA == " ") {
                        document.getElementById("txtTipoCuenta").style.background = "#FFB9B9";
                    }
                    else {
                        document.getElementById("txtTipoCuenta").style.background = "#FFFFFF";
                    }
                    if (objTBLGeneral2.CURRENCY_CD_BASE == "" || objTBLGeneral2.CURRENCY_CD_BASE == " ") {
                        document.getElementById("txtTipoMoneda").style.background = "#FFB9B9";
                    }
                    else {
                        document.getElementById("txtTipoMoneda").style.background = "#FFFFFF";
                    }

                    if (objTBLGeneral2.AA_NRO_CUENTA == "" || objTBLGeneral2.AA_NRO_CUENTA == " ") {
                        document.getElementById("txtNroProducto").style.background = "#FFB9B9";
                    }
                    else {
                        document.getElementById("txtNroProducto").style.background = "#FFFFFF";
                    }

                    if (objTBLGeneral2.AA_SALDO_FIN == "" || objTBLGeneral2.AA_SALDO_FIN == " ") {
                        document.getElementById("txtSaldoExt").style.background = "#FFB9B9";
                    }
                    else {
                        document.getElementById("txtSaldoExt").style.background = "#FFFFFF";
                    }

                    bootbox.alert("Todos los campos en rojo de productos en el exterior son requeridos");

                    validacion340++;


                }
                else if (regExp.test(objTBLGeneral2.AA_SALDO_FIN)) {

                    document.getElementById("txtSaldoExt").style.background = "#FFB9B9";


                    var txtSaldoExt = document.getElementById("text");

                    txtSaldoExt.scrollIntoView();


                    validacion340++;

                }

            }

            else {

                document.getElementById("btnNuevaFilaEntidad").style.background = "#337ab7";
                GuardarRegistroOpInter();  // Guardar lo relacionado con PS_AA_PN_OPR_INTER 
                //ListarOperacionesInterDetall(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO, 'tblResultadoBusqueda2');
                $('#txtNumeroDocumento').prop('disabled', true);
                ListarTBLGeneral(objDivBuscar, "tblResultadoBusqueda2"); //ListarTBLGeneral(1020,tblResultadoBusqueda2) Muestra los datos de la tabla 1

            }




        }




        if (validacion340 == 0) {


            GuardarRegistroOpInter();  // Guardar lo relacionado con PS_AA_PN_OPR_INTER
            ListarOperacionesInterDetall(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO, 'tblResultadoBusqueda2');
            $('#txtNumeroDocumento').prop('disabled', true);
            GuardarRegistroOpInter();  // Guardar lo relacionado con PS_AA_PN_OPR_INTER
            ListarTBLGeneral(objDivBuscar, "tblResultadoBusqueda2"); //ListarTBLGeneral(1020,tblResultadoBusqueda2) Muestra los datos de la tabla 1


        }



    });




    $('#btnVolverBusqueda').click(function () {

        $('#divResultadosDetalle').hide();
        $('#divResultadosBusqueda2').hide();
        $('#panelPrincipal').show();
        $('#btnNuevoGeneral').show();

    });
});





function ListaEntidades() {
    var svc = "./Servicios/WS_Utils.asmx/";//Pruebas
    var params = JSON.stringify({ numero: "100veces" });
    var metodo = "Lista_Entidades";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
    var filasHtml = "";
    var cant = 1;
    if (jQuery.isEmptyObject(objJson.ListaEntidades)) {
        mensajeErrorGeneral("No se obtuvieron registros.");
        mensaje(objJson.Descripcion);
        $("#tblResultadoEntidades").html(filasHtml);
    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaEntidades[0].CodEntidad != "") {
                $('#tblResultadoEntidades').show();

                $.each(objJson.ListaEntidades, function (key, value) {


                    objEntidades = {
                        CodEntidad: value.CodEntidad,
                        SEQNBR: value.SEQNBR

                    }


                    filasHtml += "<tr onclick='InsertarEntidad(" + JSON.stringify(value) + "," + JSON.stringify(cantidadEntidad) + ")'>";

                    filasHtml += "<td><a>" + cant + "</td>";
                    filasHtml += "<td><a>" + value.CodEntidad + "</td>";
                    filasHtml += "<td><a>" + value.EntidadFinanciera + "</td>";

                    filasHtml += "</tr>";

                    cant++;
                });
            }
        } else {
            mensajeErrorGeneral(objJson.Descripcion);
        }


        $("#tblResultadoEntidades").html(filasHtml);
        $("#tblResultadoEntidades").show();
        $("#dllOficina").prop("disabled", true);




        $("#example").dataTable({



            "bDestroy": true,
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

            //dom: 'Blfrtip',

        });
    }
}













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





var cantRegistros = 0;

function nuevaFila() {

    /*
    objDivBuscar = {


        NRODOCUMENTO: $('#txtNumeroDocumento').val(),
        TIPODOCUMENTO: $('#dllTipoDocumentoB').val(),
    }




    ListarTBLGeneral(objDivBuscar, "tblResultadoBusqueda2"); //ListarTBLGeneral(1020,tblResultadoBusqueda2) Muestra los datos de la tabla 1
    */



    if (cantRegistros == 1) {

        bootbox.alert("Debe guardar los cambios antesde agregar otra fila");


    }

    else {


        ListarTBLGeneral(objDivBuscar, "tblResultadoBusqueda2"); //ListarTBLGeneral(1020,tblResultadoBusqueda2) Muestra los datos de la tabla 1




        cantRegistros++;


        if (cantidadDatosTopes !== 0) {
            var TotalRegistros = parseInt(objTBLGeneral.TOTAL)







            var cantidadNew = TotalRegistros + 1;



        }
        else {

            var cantidadNew = 1;

        }


        cantidad++;
        var filasHtml = "";
        var filasHtmlExtendido = "";
        filasHtml += "<tr id='tr_p1_" + cantidadNew + "' >";
        filasHtml += "<td><a>" + cantidadNew + "</a></td>";

        filasHtml += "<td><input class='form-control inputAvvillas' autocomplete='off' style='width:20px' type='text' value='" + cantidadNew + "' id='txtSeqNum_N" + cantidadNew + "'   maxlength='30' onchange='InsertaFila(" + JSON.stringify(cantidadNew) + ")'/></td>";
        // filasHtml += "<td><input class='form-control inputAvvillas' autocomplete='off' style='width:100px' type='text'  id='txtAutorizacion_N" + cantidad + "'   maxlength='30' onchange='InsertaFila(" + JSON.stringify(cantidad) + ")'/></td>";

        filasHtml += "<td><input class='form-control inputAvvillas' autocomplete='off' disabled='disabled' type='text' id='txtDescOther_N" + cantidadNew + "'   maxlength='30' onchange='InsertaFila(" + JSON.stringify(cantidadNew) + ")' > </td>";
        filasHtml += "<td><select class='form-control inputAvvillas' autocomplete='off'  type='text' id = 'DATEADD_N" + cantidadNew + "'  onchange='InsertaFila(" + JSON.stringify(cantidadNew) + ")'     >  <option value=' '>Seleccionar</option>  <option value='P'>Puntual</option>  <option value='M'>Mensual</option> </td>";
        filasHtml += "<td><select class='form-control inputAvvillas' autocomplete='off'  type='text' id = 'DATEFIELD_N" + cantidadNew + "'  onchange='InsertaFila(" + JSON.stringify(cantidadNew) + ")'         > <option value=' '>Seleccionar</option>  <option value='SI'>SI</option>  <option value='NO'>NO</option>   <option value='DIGI'>DIGITAL</option>      </select> </td>";
        filasHtml += "<td><input class='form-control inputAvvillas' autocomplete='off' type='text' disabled='disabled' id='txtDescr_N" + cantidadNew + "'   maxlength='30' onchange='InsertaFila(" + JSON.stringify(cantidadNew) + ")'/></td>";
        filasHtml += "<td><input class='CurrencyInput input form-control inputAvvillas' autocomplete='off'  id='txtDescrlong_N" + cantidadNew + "'    onchange='InsertaFila(" + JSON.stringify(cantidadNew) + ")' /></td>";
        filasHtml += "<td><input class='form-control inputAvvillas' autocomplete='off'  type='text' id='txtNbr_N" + cantidadNew + "'  onchange='InsertaFila(" + JSON.stringify(cantidadNew) + ")'/></td>";
        filasHtml += "<td><input class='form-control inputAvvillas' autocomplete='off' type='text' disabled='disabled' id='txtEstado_N" + cantidadNew + "'   maxlength='30' onchange='InsertaFila(" + JSON.stringify(cantidadNew) + ")'/></td>";

        filasHtml += "<td><input class='form-control inputAvvillas' autocomplete='off' type='text' disabled='disabled' id='txtValueDisplay_N" + cantidadNew + "'   maxlength='254' onchange='InsertaFila(" + JSON.stringify(cantidadNew) + ")'/></td>";


        filasHtml += "<td style='text-align:center; cursor:pointer;' onclick='EliminaTextos(" + JSON.stringify(cantidadNew) + ")' ><a><img src='./style/deleteDivisa.png' alt='' border=0  ></img></a></td>";


        //filasHtml += "<td > <input type='radio' id='regular" + cantidad + " name='radio'></td>";
        // filasHtml += "<td><a>" + value.PartyID + "</td>";
        filasHtml += "</tr>";

        //Campos extendidos
        filasHtmlExtendido += "<tr id='tr_p2_" + cantidadNew + "' >";
        filasHtmlExtendido += "<td><a> </a></td>";


        filasHtmlExtendido += "<td><input class='form-control inputAvvillas' autocomplete='off'  disabled='disabled' type='text' id = 'DESCR254_1_N" + cantidadNew + "'  onchange='InsertaFila(" + JSON.stringify(cantidadNew) + ")'/></td>";
        filasHtmlExtendido += "<td><input class='form-control inputAvvillas' autocomplete='off'  disabled='disabled' type='text' id='DESCR254_2_N" + cantidadNew + "'  onchange='InsertaFila(" + JSON.stringify(cantidadNew) + ")'/></td>";
        filasHtmlExtendido += "<td><input class='form-control inputAvvillas' autocomplete='off' disabled='disabled'  type='text' id = 'DESCR254_3_N" + cantidadNew + "'  onchange='InsertaFila(" + JSON.stringify(cantidadNew) + ")'/></td>";

        // filasHtml += "<td><a>" + value.PartyID + "</td>";

        filasHtmlExtendido += "</tr>";







        //
        $("#tblResultadoBusquedaDetalleExtendido").append(filasHtmlExtendido);
        $("#tblResultadoBusquedaDetalle").append(filasHtml);
        //$("#DATEADD_N" + (cantidad)).datepicker({dateFormat: 'dd/mm/yy' , changeMonth: true,changeYear: true});
        //$("#DATEFIELD_N" + (cantidad)).datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true });
        $('#txtSeqNum_N' + cantidadNew).prop('disabled', true);


    }

}






//Funcion que muestra la tabla 3 con toda la información

var objTBLDetalleNuevo;
function ListarTBLDetalle(valorDetalle, valorTipoDocumento, tblListado) {


    var params = JSON.stringify({ valor: valorDetalle, valorTipoDoc: valorTipoDocumento });
    var metodo = "TraerParametroDetalle";

    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
    // console.log(objJson);
    var filasHtml = "";
    var filasHtmlExtendido = "";
    var cant = 1;

    if (jQuery.isEmptyObject(objJson.ListaParametrosDetalle)) {
        mensajeErrorGeneral("No se obtuvieron registros en detalles de operaciones.");
        $("#" + tblListado).html(filasHtml);
        if (objTBLGeneral2.AA_DIVISAS == ' ') {



        }
    } else {
        if (objJson.Codigo != 9999) {


            if (objJson.ListaParametrosDetalle[0].NRODOCUMENTO != "") {

                var ejecutar = true;
            


                $.each(objJson.ListaParametrosDetalle, function (key, value) {

                    objetoTable = {

                        SEQNBR: value.SEQNBR,
                        AUTORIZADIVISA: value.AUTORIZADIVISA,
                        DATEEND: fechaNormalizada(value.DATEEND),
                        BloqueoGlobal: value.BloqueoGlobal,
                        EMPLEADO: value.EMPLEADO,
                        ROWLASTMANDTTM: value.ROWLASTMANDTTM,
                        AA_ESTADO_AUTORIZA: value.AA_ESTADO_AUTORIZA,
                        cantidadVigentes: value.cantidadVigentes

                    }

                    var cantidad = 0;






                    //Si  efectua operaciones internacionales se ejecuta el siguiente codigo 
                    if (objTBLGeneral2.AA_DIVISAS == 'Y' || objTBLGeneral2.AA_DIVISAS == "1000001") {


                        //Monto de Venta
                        var TopeVenta = value.MONTOPROMVENT.replace(',', '.');

                        //Monto de Compra
                        var TopeCompra = value.MONTOPROMEDIO.replace(',', '.');

                        //Fecha Actual
                        var fechaActualObj = new Date();
                        var month = fechaActualObj.getMonth() + 1;
                        var day = fechaActualObj.getDate();
                        var year = fechaActualObj.getFullYear();
                        nuevaFecha = year + "/" + month + "/" + day;



                        var d = new Date(nuevaFecha);
                        var n = d.getTime();//Obtener el numero de milisegundos de la fecha actual


                        var fecha = (objetoTable.DATEEND).split('/');    //fecha final  
                        var fechaNew = fecha[2] + "/" + fecha[1] + "/" + fecha[0];
                        var s = new Date(fechaNew);
                        var l = s.getTime(); //Obtener el numero de milisegundos de fecha final



                        // Si esta autorizada la divisa y la fecha actual es mayor a la fecha final el estado es Vencido
                        if (value.AUTORIZADIVISA == "SI" && n > l) {


                            value.AA_ESTADO_AUTORIZA = "Vencido";
                        }


                        // Si NO esta autorizada la divisa y la fecha actual es mayor o igual a la fecha final el estado es Bloqueado
                        if (value.AUTORIZADIVISA == "NO" && n >= l) {


                            value.AA_ESTADO_AUTORIZA = "No Autorizado";
                        }


                        // Si esta autorizada la divisa y la fecha actual es menor o  a la fecha final el estado es Vigente
                        if (value.AUTORIZADIVISA == "SI" && n < l) {


                            value.AA_ESTADO_AUTORIZA = "Vigente";
                        }


                        // Si no se ha efectuado la autorizacion y la fecha actual es menor o igual a la fecha final el estado es Pendiente
                        if (value.AUTORIZADIVISA == " " && n <= l) {


                            value.AA_ESTADO_AUTORIZA = "Pendiente";
                        }



                        ListaActualizarDivisas = [];





                        objetoTable2 = {

                            SEQNBR: value.SEQNBR,
                            AUTORIZADIVISA: value.AUTORIZADIVISA,
                            DATEEND: fechaNormalizada(value.DATEEND),
                            AA_ESTADO_AUTORIZA: value.AA_ESTADO_AUTORIZA,
                            NRODOCUMENTO: value.NRODOCUMENTO,
                            TIPODOCUMENTO: value.TIPODOCUMENTO

                        };


                        (function (old) {
                            var dec = 0.12 .toLocaleString().charAt(1),
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
                        var signo = "$";

                        var top = parseFloat(TopeVenta);
                        var topeCompra = parseFloat(TopeCompra);
                        var topeVenta = signo.concat(top .toLocaleString());

                        //(objetoTable2);

                        if (objetoTable.BloqueoGlobal == "SI") {


                            ListaActualizarDivisas.push(objetoTable2);
                            guardarDetalle(ListaActualizarDivisas);

                        }
                        
                        var fechaFinalDia = fechaNormalizadaRow(value.ROWADDEDDTTM.slice(0, 2));
                        var fechaFinal = fechaNormalizadaRow(value.ROWADDEDDTTM.slice(0, 10));

                     

                        var dias = fechaFinal.slice(0, 2);
                     

                        if (fechaFinal.slice(0, 2) < 10) {
                            var horaFechaFinal = value.ROWADDEDDTTM.slice(9, 25);

                        }
                        else {
                            var horaFechaFinal = value.ROWADDEDDTTM.slice(10, 25);

                        }

                        filasHtml += "<tr id='tr" + cant + "' >";
                        filasHtml += "<td>" + cant + "</td>";
                        filasHtml += "<td>" + value.SEQNBR + "</td>";
                        filasHtml += "<td>" + value.AUTORIZADIVISA + "</td>";
                        filasHtml += "<td>" + value.TIPOVIGENCIA + "</td>";
                        filasHtml += "<td>" + value.SOPORTEDIVISA + "</td>";
                        filasHtml += "<td>" + fechaNormalizada(value.DATEEND) + "</td>";

                        filasHtml += "<td>" + topeVenta + "</td>";
                        filasHtml += "<td>" + signo.concat(topeCompra .toLocaleString()) + "</td>";

                        filasHtml += "<td>" + value.AA_ESTADO_AUTORIZA + "</td>";
                        filasHtml += "<td>" + fechaFinal + "" + horaFechaFinal + "</td>";



                        filasHtml += "<td style='text-align:center; cursor:pointer;' onclick='confirmaEliminacion(" + JSON.stringify(value) + ")' ><a><img src='./style/deleteDivisa.png' alt='' border=0 ></img></a></td>";
                        filasHtml += "</tr>";


                        filasHtmlExtendido += "<tr onchange='ActualizaFilas(" + JSON.stringify(value) + "," + JSON.stringify(cant) + ")'>";
                        filasHtmlExtendido += "<td><a>" + value.SEQNBR + "</a></td>";
                        filasHtmlExtendido += "<td><input class='form-control inputAvvillas' disabled='disabled' autocomplete='off' type='text'    id='DESCR254_1" + cant + "' value='" + value.ROWADDEDOPPRID + "' onchange='ActualizaFilas(" + JSON.stringify(value) + "," + JSON.stringify(cant) + ")'/></td>";

                        filasHtmlExtendido += "<td><input class='form-control ' disabled='disabled'  type='text' autocomplete='off'  name='campo_Parametros'   data-nombre='Fecha_campo'  id='DESCR254_2" + cant + "' value='" + value.ROWLASTMANDTTM + "' maxlength='10' onchange='ActualizaFilas(" + JSON.stringify(value) + "," + JSON.stringify(cant) + ")'/></td>";
                        filasHtmlExtendido += "<td><input class='form-control inputAvvillas' disabled='disabled' autocomplete='off' type='text' id='DESCR254_3" + cant + "' value='" + value.ROWLASTMANOPPRID + "'  maxlength='254' onchange='ActualizaFilas(" + JSON.stringify(value) + "," + JSON.stringify(cant) + ")'/></td>";



                        filasHtmlExtendido += "</tr>";

                        cant++;
                    }




                });
            }






             if (objJson.ListaParametrosDetalle[0].EMPLEADO == "SI") {

                 
                 //$('#Principal3').hide(); 
                 //$('#botonesExtendidos').hide();
                 $('#divResultadosDetalle').hide();
                 $('#btnNuevaFila').hide();
                $("#dllOperacionesInt2").val("N");
                $('#divPanelInformacion11').hide();
                $('#AprobacionVigenteDiv').hide();
                $('#AprobacionCargoDiv').hide();
                objTBLGeneral2.AA_DIVISAS = 'N';
                $('#dllOperacionesInt2').prop('disabled', true);

                filasHtml += "<div><img src='./style/cancelar.png' alt='' border=0  ></img><span style='font-weight:bold;'>Este cliente tiene el indicador de empleado del Banco por lo tanto no puede realizar Operaciones Internacionales.</span></div>"; //Al hacer click sobre la fila se va a otro panel
                $("#info_actualizada1").empty().append(filasHtml);

            }




             if (objJson.ListaParametrosDetalle[0].BloqueoGlobal == "SI" && ejecutar == true) {

                var llamada = false;

                $("#dllOperacionesInt2").val("N");
                $('#divResultadosDetalle').hide();
                $('#divPanelInformacion11').hide();
                $('#AprobacionVigenteDiv').hide();
                $('#AprobacionCargoDiv').hide();
                objTBLGeneral2.AA_DIVISAS = 'N';
                $('#dllOperacionesInt2').prop('disabled', true);



                /*
                
                                filasHtml += "<div><img src='./style/cancelar.png' alt='' border=0  ></img><span style='font-weight:bold;'>CCliente en Lista de Bloqueo.</span></div>"; //Al hacer click sobre la fila se va a otro panel
                
                
                                $("#info_actualizada1").empty().append(filasHtml);
                
                */



            }



        } else {

            mensajeErrorGeneral(objJson.Descripcion);

        }
    }
    $("#" + tblListado).html(filasHtml);
    $("#" + tblListado).show();
    $("#tblResultadoBusquedaDetalleExtendido").html(filasHtmlExtendido);



    $("#extendido").dataTable({


        "bDestroy": true,
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
        },

        dom: 'Blfrtip',
        buttons: [
            {

                extend: 'collection',
                text: 'Exportar',
                buttons: [
                    { extend: 'copy', text: 'Copiar' },
                    { extend: 'excel', text: 'Excel' },
                    { extend: 'csv', text: 'CSV' },
                    { extend: 'pdf', text: 'Pdf' },
                    { extend: 'print', text: 'Imprimir' }



                ]
            }
        ]
    });


    $("#tableColor").dataTable({



        "bDestroy": true,
        "processing": true,
        "filter": true,
        "lengthChange": true,
        "lengthMenu": [[10, 20, 30, -1], [10, 20, 30, 'Todos']],
        "order": [[9, "desc"]],

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
        },

        dom: 'Blfrtip',
        buttons: [
            {

                extend: 'collection',
                text: 'Exportar',
                buttons: [
                    { extend: 'copy', text: 'Copiar' },
                    { extend: 'excel', text: 'Excel' },
                    { extend: 'csv', text: 'CSV' },
                    { extend: 'pdf', text: 'Pdf' },
                    { extend: 'print', text: 'Imprimir' },



                ]
            }
        ]
    });






    for (i = 1; i <= cant; i++) {

        $('#txtSeqNum' + i).prop('disabled', true);



    }
    cantidad = cant - 1;

}















var objTBLDetalleNuevo;
function ListarCargoAutoriza(valorDetalle, valorTipoDocumento) {


    var params = JSON.stringify({ valor: valorDetalle, valorTipoDoc: valorTipoDocumento });
    var metodo = "TraerCargoAutoriza";

    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
    // console.log(objJson);
    var filasHtml = "";
    var filasHtmlExtendido = "";
    var cant = 1;

    if (jQuery.isEmptyObject(objJson.ListaParametrosDetalle)) {
        mensajeErrorGeneral("No se obtuvo cargo");

    } else {
        if (objJson.Codigo != 9999) {
            var ejecutar = true;



            if (objJson.ListaParametrosDetalle[0].CARGO != "") {


                $.each(objJson.ListaParametrosDetalle, function (key, value) {




                    var CodCargo = value.CODIGOCARGO.replace(/^(0+)/g, '');
                    var Cargo = value.CARGO.replace(/\s*$/, "") + "";

                    var CargoCodigo = Cargo + ' - ' + CodCargo;
                    $('#txtImportanci').val(CargoCodigo);


                });
            }
        } else {

            mensajeErrorGeneral(objJson.Descripcion);

        }
    }









    for (i = 1; i <= cant; i++) {

        $('#txtSeqNum' + i).prop('disabled', true);



    }
    cantidad = cant - 1;

}















var objTBLEntidadNueva;
function ListarTBLEntidades(valorDetalle, valorTipoDocumento, tblListado) {




    var params = JSON.stringify({ valor: valorDetalle, valorTipoDoc: valorTipoDocumento });
    var metodo = "TraerEntidadesCliente";

    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));

    var filasHtml = "";
    var filasHtmlExtendido = "";
    var cantt = 1;
    var cantidadEntidadesLista = 0;

    if (jQuery.isEmptyObject(objJson.ListaEntidades)) {

        mensajeErrorGeneral("No se obtuvieron registros en Entidades bancarias.");

        $("#" + tblListado).html(filasHtml);
    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaEntidades[0].NRODOCUMENTO != "") {

                $.each(objJson.ListaEntidades, function (key, value) {


                    //console.log("TABLA ENTIDADES");


                    filasHtml += "<tr id='tr" + cantt + "' >";
                    filasHtml += "<td><a>" + cantt + "</a></td>";
                    filasHtml += "<td>" + value.CodEntidad + "</td>";
                    filasHtml += "<td>" + value.EntidadFinanciera + "</td>";
                    filasHtml += "<td style='text-align:center; cursor:pointer;' onclick='confirmaEliminacionEntidad(" + JSON.stringify(value) + ")' ><a><img src='./style/deleteDivisa.png' alt='' border=0 ></img></a></td>";
                    filasHtml += "</tr>";


                    cantt++;
                    cantidadEntidadesLista++;

                });
            }
        } else {

            mensajeErrorGeneral(objJson.Descripcion);

        }
    }
    $("#" + tblListado).html(filasHtml);
    $("#" + tblListado).show();
    $("#tblEntidadesCliente").html(filasHtml);







    $("#TablaEntidades").dataTable({

        "pageLength": 1,
        "bDestroy": true,
        "processing": true,
        "filter": true,
        "lengthChange": true,
        "lengthMenu": [[10, 20, 30, -1], [10, 20, 30, 'Todos']],
        //"order": [[9, "desc"]],
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
        },

        dom: 'Blfrtip',
        buttons: [
            {

                extend: 'collection',
                text: 'Exportar',
                buttons: [
                    { extend: 'copy', text: 'Copiar' },
                    { extend: 'excel', text: 'Excel' },
                    { extend: 'csv', text: 'CSV' },
                    { extend: 'pdf', text: 'Pdf' },
                    { extend: 'print', text: 'Imprimir' }



                ]
            }
        ]
    });

    // $("#divResultadosDetalle").show();
    for (i = 1; i <= cantt; i++) {
        // $("#tr" + i).change(function () { alert("Hola" + i); });
        //$("#DATEADD_" + (i)).datepicker({dateFormat: 'dd/mm/yy' , changeMonth: true,changeYear: true});
        //$("#DATEFIELD_" + (i)).datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true });
        $('#txtSeqNum' + i).prop('disabled', true);
    }
    cantidadEntidades = cantt - 1;
    cantidadEntidadesListaLL = cantidadEntidadesLista;

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
        // $("#" + tblListado).html(filasHtml);

        TraerCargosAutTope("150000");
        valor = objCargosAut.DESCR;
        //alert("El tope debe ser autorizado-")
        objTBLDetalleNuevo.AA_ESTADO_AUTORIZA = "Pendientee";



    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaOperacionesIntPN[0].puedeAutorizar != "") {

                $.each(objJson.ListaOperacionesIntPN, function (key, value) {

                    objAutorizacionTope = {
                        puedeAutorizar: value.puedeAutorizar,
                        puedeAutorizar2: value.puedeAutorizar2

                    }



                    var FechaActual = Date.now();

                    var today = new Date();
                    var dd = String(today.getDate()).slice(-2);
                    var mm = String(today.getMonth() + 1).slice(-2); //January is 0!
                    var yyyy = today.getFullYear();

                    today = mm + '/' + dd + '/' + yyyy;
                    var fechaHoy = new Date(today);
                    var fechaRenderizada = new Date(objTBLDetalleNuevo.DATEEND);

                    var fecha = fechaRenderizada.getTime();
                    // console.log("Valor de fecha "+fecha);

                    if ((objAutorizacionTope.puedeAutorizar == 'Y' && objAutorizacionTope.puedeAutorizar2 == 'Y') && fechaHoy.getTime() <= fecha) {

                        objTBLDetalleNuevo.AUTORIZADIVISA = 'SI'

                        objTBLDetalleNuevo.AA_ESTADO_AUTORIZA = "Vigente";
                    }



                    else if ((objAutorizacionTope.puedeAutorizar == 'Y' && objAutorizacionTope.puedeAutorizar2 == 'N' && objTBLDetalleNuevo.MONTOPROMEDIO == "") && fechaHoy.getTime() <= fecha) {

                        objTBLDetalleNuevo.AUTORIZADIVISA = 'SI'

                        objTBLDetalleNuevo.AA_ESTADO_AUTORIZA = "Vigente";
                    }


                    else if ((objAutorizacionTope.puedeAutorizar2 == 'Y' && objAutorizacionTope.puedeAutorizar == 'N' && objTBLDetalleNuevo.MONTOPROMVENT == "") && fechaHoy.getTime() <= fecha) {

                        objTBLDetalleNuevo.AUTORIZADIVISA = 'SI'

                        objTBLDetalleNuevo.AA_ESTADO_AUTORIZA = "Vigente";
                    }




                    //Tope compra
                    else if (objAutorizacionTope.puedeAutorizar == 'Y' && objAutorizacionTope.puedeAutorizar2 == 'N' && objTBLDetalleNuevo.MONTOPROMEDIO !== "" && cantt == 1 && fechaHoy.getTime() <= new Date(objTBLDetalleNuevo.DATEEND).getTime()) {


                        TraerCargosAutTope(objTBLDetalleNuevo.MONTOPROMEDIO);
                        valor = objCargosAut.DESCR;
                        var autorizados = valor.replace(",", "<br>");
                        bootbox.alert("El tope  registrado de compra requiere que sea aprobado por: <br><br>" + autorizados + "");
                        objTBLDetalleNuevo.AA_ESTADO_AUTORIZA = "Pendiente";
                        objTBLDetalleNuevo.AUTORIZADIVISA = ' '


                    }

                    //Tope Venta
                    else if (objAutorizacionTope.puedeAutorizar2 == 'Y' && objAutorizacionTope.puedeAutorizar == 'N' && objTBLDetalleNuevo.MONTOPROMVENT !== "" && cantt == 1 && fechaHoy.getTime() <= new Date(objTBLDetalleNuevo.DATEEND).getTime()) {


                        TraerCargosAutTope(objTBLDetalleNuevo.MONTOPROMVENT);
                        valor = objCargosAut.DESCR;
                        var autorizados = valor.replace(",", "<br>");
                        bootbox.alert("El tope  registrado de venta requiere que sea aprobado por: <br><br>" + autorizados + "");
                        objTBLDetalleNuevo.AA_ESTADO_AUTORIZA = "Pendiente";
                        objTBLDetalleNuevo.AUTORIZADIVISA = ' '


                    }


                    //Compra
                    else if (objAutorizacionTope.puedeAutorizar2 == 'N' && objAutorizacionTope.puedeAutorizar == 'N' && objTBLDetalleNuevo.MONTOPROMEDIO !== "" && cantt == 1 && fechaHoy.getTime() <= new Date(objTBLDetalleNuevo.DATEEND).getTime()) {


                        TraerCargosAutTope(objTBLDetalleNuevo.MONTOPROMEDIO);
                        valor = objCargosAut.DESCR;
                        var autorizados = valor.replace(",", "<br>");
                        bootbox.alert("El tope registrado de compra requiere que sea aprobado por: <br><br>" + autorizados + "");
                        objTBLDetalleNuevo.AA_ESTADO_AUTORIZA = "Pendiente";
                        objTBLDetalleNuevo.AUTORIZADIVISA = ' '


                    }


                    //Venta
                    else if (objAutorizacionTope.puedeAutorizar2 == 'N' && objAutorizacionTope.puedeAutorizar == 'N' && objTBLDetalleNuevo.MONTOPROMVENT !== "" && cantt == 1 && fechaHoy.getTime() <= new Date(objTBLDetalleNuevo.DATEEND).getTime()) {


                        TraerCargosAutTope(objTBLDetalleNuevo.MONTOPROMVENT);
                        valor = objCargosAut.DESCR;
                        var autorizados = valor.replace(",", "<br>");
                        bootbox.alert("El tope  registrado de venta requiere que sea aprobado por: <br><br>" + autorizados + "");
                        objTBLDetalleNuevo.AA_ESTADO_AUTORIZA = "Pendiente";
                        objTBLDetalleNuevo.AUTORIZADIVISA = ' '


                    }



                    else if ((objJson.ListaOperacionesIntPN[0].puedeAutorizar == 'Y' || objJson.ListaOperacionesIntPN[0].puedeAutorizar2 == 'Y') && fechaHoy.getTime() > new Date(objTBLDetalleNuevo.DATEEND).getTime()) {

                        objTBLDetalleNuevo.AUTORIZADIVISA = 'SI'

                        objTBLDetalleNuevo.AA_ESTADO_AUTORIZA = "Vencido";

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
        //mensajeErrorGeneral("No se obtuvieron registros en Detalles.");
        // $("#" + tblListado).html(filasHtml);
        

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


var cantRegistrosEntidad = 0;

function nuevaFilaEntidad() {
    if (cantRegistrosEntidad == 100) {
        bootbox.alert("Debe guardar los cambios antesde agregar otra fila");





    } else {
        cantRegistrosEntidad++;
        cantidadEntidad++;
        var filasHtml = "";

        filasHtml += "<tr id='tr_p1_" + cantidadEntidad + "' >";
        filasHtml += "<td><a>" + cantidadEntidad + "</a></td>";
        filasHtml += "<td><input class='form-control inputAvvillas' autocomplete='off' type='text'   id='txtCodEntidad_N" + cantidadEntidad + "'   maxlength='30' onchange='InsertaFilaEntidades(" + JSON.stringify(cantidadEntidad) + ")'><input type='image' src='./style/buscar.png'  data-toggle='modal' data-target='#myModal'  id='BotonEntidades'/></input></td>";
        filasHtml += "<td><input class='form-control inputAvvillas' autocomplete='off' type='text' value='  ' id='txtEntidad_N" + cantidadEntidad + "'   maxlength='30' onchange='InsertaFilaEntidades(" + JSON.stringify(cantidadEntidad) + ")'/></td>";
        filasHtml += "<td style='text-align:center; cursor:pointer;' onclick='EliminaTextosEntidades(" + JSON.stringify(cantidadEntidad) + ")' ><a><img src='./style/deleteDivisa.png' alt='' border=0  ></img></a></td>";
        filasHtml += "<td></td>";
        filasHtml += "</tr>";



        $("#tblEntidadesCliente").append(filasHtml);



        /*
        $("#tblEntidadesCliente").dataTable({

            "pageLength": 1,
            "bDestroy": true,
            "processing": true,
            "filter": true,
            "lengthChange": true,
            "lengthMenu": [[10, 20, 30, -1], [10, 20, 30, 'Todos']],
            //"order": [[9, "desc"]],
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
            },

            dom: 'Blfrtip',
            buttons: [
                {

                    extend: 'collection',
                    text: 'Exportar',
                    buttons: [
                        { extend: 'copy', text: 'Copiar' },
                        { extend: 'excel', text: 'Excel' },
                        { extend: 'csv', text: 'CSV' },
                        { extend: 'pdf', text: 'Pdf' },
                        { extend: 'print', text: 'Imprimir' }



                    ]
                }
            ]
        });
        */


        $('#txtSeqNum_N' + cantidad).prop('disabled', true);
    }

}




//Funcion que muestra la tabla 1 AL DARLE BUSCAR
//Funcion ListarTBLGeneral(1020, "tblResultadoBusqueda2") muestra los datos de la tabla 1
function ListarOperacionesInt(valorGeneral, TipoDocumento, tblListado) {


    var params = JSON.stringify({ valor: valorGeneral, valorTipoDoc: TipoDocumento });
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var metodo = "TraerOpMesaDinero";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));


    // console.log(objJson);
    var filasHtml = "";
    var cant = 1;
    if (jQuery.isEmptyObject(objJson.ListaOperacionesInt)) {
        mensajeErrorGeneral("No se obtuvieron registros en operaciones internacionales.");
        $("#" + tblListado).html(filasHtml);
    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaOperacionesInt[0].NRODOCUMENTO != "") {
                $.each(objJson.ListaOperacionesInt, function (key, value) {

                    objOperacionesInt = {
                        NRODOCUMENTO: value.NRODOCUMENTO,
                        SEQ_NBR: value.SEQ_NBR,
                        AA_TIPO_DIVISA: value.AA_TIPO_DIVISA,
                        AA_FECHA_TX: value.AA_FECHA_TX,
                        AA_COD_AUTORIZ: value.AA_COD_AUTORIZ,
                        AA_MONTO_OPR: value.AA_MONTO_OPR,
                        CURRENCY_CD: value.CURRENCY_CD,
                        PARTICIPATION_PCT: value.PARTICIPATION_PCT,
                        AA_MONTO_TASA: value.AA_MONTO_TASA

                    }

                    if (value.AA_TIPO_DIVISA == "V") {

                        value.AA_TIPO_DIVISA = "Venta";
                    }
                    if (value.AA_TIPO_DIVISA == "C") {

                        value.AA_TIPO_DIVISA = "Compra";

                    }

                    var AA_MONTO_OPR = value.AA_MONTO_OPR.replace(',', '.');


                    (function (old) {
                        var dec = 0.12 .toLocaleString().charAt(1),
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
                    var signo = "$";


                    // console.log("L1349" + value.AA_MONTO_OPR);
                    var participation = parseFloat(value.PARTICIPATION_PCT);
                    var AA_MONTO_TASA = parseFloat(value.AA_MONTO_TASA);


                    filasHtml += "<tr onclick='Actualizar(" + JSON.stringify(objTBLGeneral) + ")'>"; //Al hacer click sobre la fila se va a otro panel
                    filasHtml += "<td>" + cant + "</td>";

                    filasHtml += "<td>" + value.AA_TIPO_DIVISA + "</td>";
                    filasHtml += "<td>" + value.AA_FECHA_TX + "</td>";
                    filasHtml += "<td>" + value.AA_COD_AUTORIZ + "</td>";

                    filasHtml += "<td>" + signo.concat(AA_MONTO_OPR .toLocaleString()) + "</td>";
                    filasHtml += "<td>" + value.CURRENCY_CD + "</td>";
                    filasHtml += "<td>" + "Dólar de EE.UU." + "</td>";

                    filasHtml += "<td>" + signo.concat(participation .toLocaleString()) + "</td>";
                    filasHtml += "<td>" + signo.concat(AA_MONTO_TASA .toLocaleString()) + "</td>";



                    //Actualizar(objOperacionesInt);
                    // filasHtml += "<td><a>" + value.PartyID + "</td>";
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
    $("#" + tblListado).html(filasHtml);
    $("#" + tblListado).show();
    $("#divOperaciones").show(); //Encabezados tabla 1 y datos


    $("#TablaMesaDinero").dataTable({



        "bDestroy": true,
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
        },

        dom: 'Blfrtip',
        buttons: [
            {

                extend: 'collection',
                text: 'Exportar',
                buttons: [
                    { extend: 'copy', text: 'Copiar' },
                    { extend: 'excel', text: 'Excel' },
                    { extend: 'csv', text: 'CSV' },
                    { extend: 'pdf', text: 'Pdf' },
                    { extend: 'print', text: 'Imprimir' },



                ]
            }
        ]
    });


}








function EliminaTextos(cant) {
    //debugger;
    cantRegistros--;

    $('#tr_p1_' + cant).remove();
    $('#tr_p2_' + cant).remove();
    //ListaObjetosDetalles[cant + 1] = [];

    var NroDocumento2 = $('#txtNumeroDocumento').val();

    var TipoDocumento2 = $('#dllTipoDocumentoB').val();  //Si hay un valor en BUSCAR ejemplo 1020 se va a la funcion ListarTBLGeneral

    ListarTBLDetalle(NroDocumento2, TipoDocumento2, "tblResultadoBusquedaDetalle");
    

}


function EliminaTextosEntidades(cant) {
    //debugger;
    cantRegistros--;

    $('#tr_p1_' + cant).remove();
    $('#tr_p2_' + cant).remove();
    //ListaObjetosDetalles[cant + 1] = [];

    var NroDocumento2 = $('#txtNumeroDocumento').val();

    var TipoDocumento2 = $('#dllTipoDocumentoB').val();  //Si hay un valor en BUSCAR ejemplo 1020 se va a la funcion ListarTBLGeneral

    


    ListarTBLEntidades(NroDocumento2, TipoDocumento2, "tblEntidadesCliente");
}





//lista resultados
function getFecha(FechaI) {
    var date = FechaI;
    var Fecha = new Date(parseInt(date.substr(6)));
    var dia = Fecha.getDate() < 10 ? "0" + Fecha.getDate() : Fecha.getDate();
    var mes = Fecha.getMonth() < 9 ? "0" + parseInt(Fecha.getMonth() + 1) : parseInt(Fecha.getMonth() + 1);
    var anio = Fecha.getFullYear() < 10 ? "0" + Fecha.getFullYear() : Fecha.getFullYear();

    var horas = Fecha.getHours() < 10 ? "0" + Fecha.getHours() : Fecha.getHours();
    var minutos = Fecha.getMinutes() < 10 ? "0" + Fecha.getMinutes() : Fecha.getMinutes();
    var segundos = Fecha.getSeconds() < 10 ? "0" + Fecha.getSeconds() : Fecha.getSeconds();
    return dia + "/" + mes + "/" + anio;
}
var objTBLGeneral;

var objAutorizacionTope;
var cantidad = 1;
var cantidadEntidad = 1;
var cantidadPais = 1;
var cantidadMonedasExtranjeras = 1;



//Funcion que muestra la tabla 1 AL DARLE BUSCAR
//Funcion ListarTBLGeneral(1020, "tblResultadoBusqueda2") muestra los datos de la tabla 1
function ListarTBLGeneral(objDivBuscar, tblListado) {


    var params = JSON.stringify({ valor: objDivBuscar });
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var metodo = "TraerParametroMaestro";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));


    //console.log(objJson);
    var filasHtml = "";
    var cant = 1;
    var NumeroDatos = 0;
    if (jQuery.isEmptyObject(objJson.ListaParametros)) {
        $('#AprobacionVigente').val("NO");

        mensajeErrorGeneral("Cliente sin detalles de autorizaciones");
        $("#" + tblListado).html(filasHtml);



    } else {
        if (objJson.Codigo != 9999) {

            $.each(objJson.ListaParametros, function (key, value) {


                if (value.AA_ESTADO_AUTORIZA == 'Vigente') {

                    value.AA_AUTORIZACION = "SI";
                }



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
                    AA_AUTORIZACION: value.AA_AUTORIZACION,
                    AA_ESTADO_AUTORIZA: value.AA_ESTADO_AUTORIZA,
                    TIPODOCUMENTO: value.TIPODOCUMENTO,
                    TOTAL: value.TOTAL
                    //AA_AUTORIZACION_DIV: value.AA_AUTORIZACION_DIV,

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
                filasHtml += "<td><a>" + value.ROWADDEDDTTM + "</td>";
                filasHtml += "<td><a>" + value.ROWADDEDOPPRID + "</td>";
                filasHtml += "<td><a>" + value.ROWLASTMANDTTM + "</td>";
                filasHtml += "<td><a>" + value.ROWLASTMANOPPRID + "</td>";
                filasHtml += "<td><a>" + value.BLOQUEODIVISAS + "</td>";
                filasHtml += "<td><a>" + value.AA_ESTADO_AUTORIZA + "</td>";
                // filasHtml += "<td><a>" + value.AA_AUTORIZACION_DIV + "</td>";


                Actualizar(objTBLGeneral);
                // filasHtml += "<td><a>" + value.PartyID + "</td>";
                filasHtml += "</tr>";
                cant++;
                NumeroDatos++;

            });

        } else {

            mensajeErrorGeneral(objJson.Descripcion);

        }

    }
    if (objJson.Codigo == 9999) {
        mensajeErrorGeneral(objJson.Descripcion);
        throw new Error(objJson.Descripcion);
    }
    $("#" + tblListado).html(filasHtml);
    $("#" + tblListado).show();
    $("#divResultadosBusqueda2").show(); //Encabezados tabla 1 y datos

    cantidadDatosTopes = NumeroDatos;

}







function ListarOperacionesInterDetall(valorGeneral, TipoDocumento, tblListadoOpInt) {


    var params = JSON.stringify({ valor: valorGeneral, valor2: TipoDocumento });
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var metodo = "TraerOperacionesInter";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));


    //console.log(objJson);
    var filasHtml = "";
    var cant = 1;
    if (jQuery.isEmptyObject(objJson.ListaOperacionesIntPN)) {
        mensajeErrorGeneral("No se obtuvieron registros de operaciones detalle.");
        alert("SIN REGISTROS");
        //$("#dllOperacionesInt option:selected").text("NO");

        $("#" + tblListadoOpInt).html(filasHtml);
    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaOperacionesIntPN[0].NRODOCUMENTO != "") {
                $.each(objJson.ListaOperacionesIntPN, function (key, value) {




                    // console.log(value.AA_MONTO_PROM_VENT);


                    if (value.AA_DIVISAS == "N") {

                        $("#dllOperacionesInt2").val(value.AA_DIVISAS);


                        $('#divResultadosDetalle').hide();

                        $('#divPanelInformacion11').hide();

                        $('#AprobacionVigenteDiv').hide();
                        $('#AprobacionCargoDiv').hide();


                        var valor = $('#txtNumeroDocumento').val();  //Si hay un valor en BUSCAR ejemplo 1020 se va a la funcion ListarTBLGeneral
                        var TipoDocumento = $('#dllTipoDocumentoU').val();  //Si hay un valor en BUSCAR ejemplo 1020 se va a la funcion ListarTBLGeneral
                        //console.log("LINEA 1694");
                        ListarTBLEntidades(valor, TipoDocumento, "tblEntidadesCliente");


                    } else {

                        $("#dllOperacionesInt2").val(value.AA_DIVISAS);
                        $('#divResultadosDetalle').show();
                        $('#divPanelInformacion11').show();
                        $('#AprobacionVigenteDiv').show();
                        $('#AprobacionCargoDiv').show();

                        // console.log("LINEA 1706");

                        //var valor = $('#txtNumeroDocumento').val();  //Si hay un valor en BUSCAR ejemplo 1020 se va a la funcion ListarTBLGeneral
                        // var TipoDocumento = $('#dllTipoDocumentoU').val();  //Si hay un valor en BUSCAR ejemplo 1020 se va a la funcion ListarTBLGeneral
                        // ListarTBLEntidades(valor, TipoDocumento, "tblEntidadesCliente");

                    }





                    if (value.AA_MONTO_PROM_VENT !== null) {

                        var nuevoValor2 = value.AA_MONTO_PROM_VENT.replace(',', '.');


                    }

                    if (value.AA_MONTO_PROM_VENT == null || value.AA_MONTO_PROM_VENT == "") {
                        value.AA_MONTO_PROM_VENT = "0, 0";

                        var nuevoValor2 = value.AA_MONTO_PROM_VENT.replace(',', '.');

                    }

                    if (value.AA_TOTAL_VENTA_USD == null) {
                        value.AA_TOTAL_VENTA_USD = "0, 0";

                        value.AA_TOTAL_VENTA_USD = value.AA_MONTO_PROM_VENT.replace(',', '.');

                    }


                    if (value.AA_TOTAL_COMPR_USD != null) {


                        value.AA_TOTAL_COMPR_USD = value.AA_TOTAL_COMPR_USD.replace(',', '.');

                    }


                    // console.log(value.AA_TOTAL_VENTA_USD);

                    if (value.AA_TOTAL_VENTA_USD != null) {


                        value.AA_TOTAL_VENTA_USD = value.AA_TOTAL_VENTA_USD.replace(',', '.');

                    }


                    if (value.AA_MONTO_PROMEDIO == null) {


                        value.AA_MONTO_PROMEDIO = "0";

                    }


                    //console.log("value.AA_SALDO_FIN " + value.AA_SALDO_FIN);

                    //console.log(formatoMoneda.format(value.AA_TOTAL_COMPR_USD));

                    var AA_SALDO_FIN = value.AA_SALDO_FIN.replace(',', '.');

                    //console.log("AA_SALDO_FIN " + AA_SALDO_FIN);
                    //var string = numeral(1000).format('0,0');


                    (function (old) {
                        var dec = 0.12 .toLocaleString().charAt(1),
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


                    var totn_string = 'Tech';

                    // console.log(totn_string.concat('On','The','Net'));

                    var signo = "$";
                    var precio = 0 .toLocaleString()
                    //console.log(signo.concat(precio).replace(/[,]/g,'.'));


                    //console.log("PRUEBA1"+value.AA_MONTO_PROM_VENT);

                    var prueba = signo.concat(290 .toLocaleString());
                    //console.log("PRUEBA"+prueba);


                    // console.log("LINEA 1844  " + value.AA_TOTAL_VENTA_USD);
                    var TOTAL = signo.concat(value.AA_TOTAL_VENTA_USD .toLocaleString());

                    var SALDO_INT = parseFloat(AA_SALDO_FIN)
                    //console.log("SALDO_INT " + SALDO_INT);


                    var SALDO_FIN = signo.concat(SALDO_INT .toLocaleString());
                    var AA_MONTO_PROMEDIO = parseFloat(value.AA_MONTO_PROMEDIO);
                    var AA_TOTAL_COMPR_USD = parseFloat(value.AA_TOTAL_COMPR_USD);
                    var AA_TOTAL_VENTA_USD = parseFloat(value.AA_TOTAL_VENTA_USD);

                    // console.log("AA_TOTAL_VENTA_USD " + AA_TOTAL_VENTA_USD);
                    //console.log("value.AA_TOTAL_VENTA_USD" + value.AA_TOTAL_VENTA_USD);

                    //objTBLGeneral2.ROW_LASTMANT_OPRID = "1098746160";


                    objTBLGeneral2 = {

                        NRODOCUMENTO: value.NRODOCUMENTO,
                        AA_IMPORTACION: value.AA_IMPORTACION,
                        AA_EXPORTACION: value.AA_EXPORTACION,
                        AA_INVERSION: value.AA_INVERSION,
                        AA_PRESTAMOS: value.AA_PRESTAMOS,
                        AA_GIROS: value.AA_GIROS,
                        AA_ENVIO_GIROS: value.AA_ENVIO_GIROS,
                        AA_SERVICIOS: value.AA_SERVICIOS,
                        AA_MONTO_PROM_VENT: value.AA_MONTO_PROM_VENT,
                        AA_MONTO_PROMEDIO: signo.concat(AA_MONTO_PROMEDIO .toLocaleString()),
                        AA_TOTAL_VENTA_USD: signo.concat(AA_TOTAL_VENTA_USD .toLocaleString()),
                        AA_TOTAL_COMPR_USD: signo.concat(AA_TOTAL_COMPR_USD .toLocaleString()),
                        AA_TOTAL_VENTA_COP: value.AA_TOTAL_VENTA_COP,
                        AA_TOTAL_COMPR_COP: value.AA_TOTAL_COMPR_COP,
                        AA_MONTO_OPR: value.AA_MONTO_OPR,
                        AA_MONTO_VENTA_OPR: value.AA_MONTO_VENTA_OPR,
                        AA_CTA_EXTERIOR: value.AA_CTA_EXTERIOR,
                        AA_DIVISAS: value.AA_DIVISAS,
                        AA_CIUDAD_OPR: value.AA_CIUDAD_OPR,
                        COUNTRY: value.COUNTRY,
                        AA_ENTIDAD_OPR: value.AA_ENTIDAD_OPR,
                        AA_TIPO_CUENTA: value.AA_TIPO_CUENTA,
                        CURRENCY_CD_BASE: value.CURRENCY_CD_BASE,
                        AA_NRO_CUENTA: value.AA_NRO_CUENTA,
                        AA_SALDO_FIN: SALDO_FIN,
                        ROW_LASTMANT_OPRID: value.ROW_LASTMANT_OPRID,
                        ROW_LASTMANT_DTTM: value.ROW_LASTMANT_DTTM,
                        TIPODOCUMENTO: value.TIPODOCUMENTO
                    }

                    //console.log(objTBLGeneral2);

                    ListaObjetosOperacionesInt = [];
                    ListaObjetosOperacionesInt.push(objTBLGeneral2);




                    // console.log(objTBLGeneral2);


                    //MUESTRA LOS DATOS DE LA ULTIMA SECCION
                    Actualizar2(objTBLGeneral2);


                    TopeVentaUSD(objTBLGeneral2.NRODOCUMENTO, objTBLGeneral2.TIPODOCUMENTO);
                    
                    OperacionesMesaDineroVenta(objTBLGeneral2.NRODOCUMENTO, objTBLGeneral2.TIPODOCUMENTO);
                    OperacionesMesaDineroCompra(objTBLGeneral2.NRODOCUMENTO, objTBLGeneral2.TIPODOCUMENTO);
                    OperacionesMesaDineroCopVenta(objTBLGeneral2.NRODOCUMENTO, objTBLGeneral2.TIPODOCUMENTO);
                    OperacionesMesaDineroCopCompra(objTBLGeneral2.NRODOCUMENTO, objTBLGeneral2.TIPODOCUMENTO);
                    



                    //cant++;
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
    // $("#" + tblListadoOpInt).html(filasHtml);



}




function InsertarEntidad(valor) {
    var msg;
    //var confirma = confirm("¿Esta seguro de insertar esta entidad?");


    bootbox.confirm({

        title: "<b>INSERTAR ENTIDAD BANCARIA<b/>",
        message: "¿Esta seguro de insertar esta entidad?",
        className: 'rubberBand animated',

        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-success'
            },
            cancel: {
                label: 'No',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            //console.log('This was logged in the callback: ' + result);




            if (result == true) {
                // EliminarDetalle(valor);

                $('#txtCodEntidad_N' + cantidadEntidad).val(valor.CodEntidad);
                $('#txtEntidad_N' + cantidadEntidad).val(valor.EntidadFinanciera);
                ListaObjetosEntidades = [];

                objEntidades = {

                    TIPODOCUMENTO: $('#dllTipoDocumentoB').val(),

                    NRODOCUMENTO: $('#txtNumeroDocumento').val(),
                    SEQNBR: cantidadEntidad,
                    CodEntidad: $('#txtCodEntidad_N' + cantidadEntidad).val(),
                    EntidadFinanciera: $('#txtEntidad_N' + cantidadEntidad).val()


                }


                ListaObjetosEntidades.push(objEntidades);


            } else {


                alert("Operación cancelada!");






            }

        }
    });


}




function confirmaEliminacion(valor) {


    bootbox.confirm({
        message: "¿Esta seguro de eliminar este registro?",
        buttons: {
            confirm: {
                label: 'Si',
                className: 'btn-success'
            },
            cancel: {
                label: 'No',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result == true && valor.AUTORIZADIVISA !== "SI") {
                EliminarDetalle(valor);

            }

            else if (result == false) {

                alert('Operacion cancelada!');
                // close on click




            }



            else {




                alert('No se puede eliminar una divisa autorizada!');
                // close on click



            }
        }
    });







}

function confirmaEliminacionEntidad(valor) {



    bootbox.confirm({
        message: "¿Esta seguro de eliminar la entidad?",
        buttons: {
            confirm: {
                label: 'Si',
                className: 'btn-success'
            },
            cancel: {
                label: 'No',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result == true) {
                EliminarEntidadCliente(valor);

            } else {
                mensajeErrorGeneral("Operacion cancelada");
            }
        }
    });


}




function EliminarDetalle(valor) {
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";//Pruebas
    var params = JSON.stringify({ valor: valor });
    var metodo = "EliminarParametroDetalle";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
    if (objJson) {
        ListarTBLDetalle(valor.NRODOCUMENTO, valor.TIPODOCUMENTO, "tblResultadoBusquedaDetalle");
        ListaObjetosDetalles = [];
    } else {
        mensajeErrorGeneral("Error al momento de eliminar el detalle");
    }
}



function EliminarEntidadCliente(valor) {
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";//Pruebas
    var params = JSON.stringify({ valor: valor });
    var metodo = "EliminarEntidadClte";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
    if (objJson) {


        ListarTBLEntidades(valor.Nrodocumento, valor.TIPODOCUMENTO, "tblEntidadesCliente");
        ListaObjetosEntidades = [];

    } else {
        mensajeErrorGeneral("Error al momento de eliminar el detalle");
    }
}



function TopeventaAutorizada(objeto) {

    ListaObjetosTopes = [];



    $('#txtTopeVenta').val(objeto.AA_MONTO_PROM_VENT);
    $('#txtTopeCompra').val(objeto.AA_MONTO_PROMEDIO);



}


function MesaDineroOperaciones(objeto) {

    ListaObjMesaDinero = [];


    $('#txtTotalComprasUSD').val(objeto.TOTAL_COMPR_USD);
    $('#txtTotalVentasUSD').val(objeto.TOTAL_VENTA_USD);



}



function MesActual(objeto) {

    ListaObjetosTopes = [];



    $('#txtTopeVenta').val(objeto.AA_MONTO_PROM_VENT);
    $('#txtTopeCompra').val(objeto.AA_MONTO_PROMEDIO);



}




//Funcion actualizar que al hacer click sobre alguna fila de la tabla 1 , se dirige a otra vista
//Viene el objeto de la tabla con sus datos
function Actualizar(objeto) {
    ListaObjetosDetalles = [];


    $('#dllTipoDocumentoB').val(objeto.TIPODOCUMENTO),

        $('#txtNumeroDocumento').val(objeto.NRODOCUMENTO);
    $('#txtID').val(objeto.SEQNBR);
    $('#txtTitulo').val(objeto.AUTORIZADIVISA);
    $('#txtValorNumerico').val(objeto.TIPOVIGENCIA);
    $('#txtFecha').val(fechaNormalizada(objeto.DATEEND));
    $('#txtDescr100').val(objeto.SOPORTEDIVISA);
    $('#txtValue').val(objeto.MONTOPROMVENT);
    $('#txtValueDisplay').val(objeto.MONTOPROMEDIO);//Tope Compra
    $('#txtSqlQuery').val(objeto.ROWADDEDDTTM); //Fecha/Hora Introducción:
    $('#txtEstado').val(objeto.AA_ESTADO_AUTORIZA);
    $('#txtAñadido').val(objeto.ROWADDEDOPPRID); //Añadido Por:
    $('#txtModificado').val(objeto.ROWLASTMANDTTM); //Ultima modificación:
    $('#txtMantenimiento').val(fechaNormalizada(objeto.ROWLASTMANOPPRID));
    $('#txtBloqueo').val(objeto.BLOQUEODIVISAS); //Bloqueo Divisas:
    $('#AprobacionVigente').val(objeto.AA_AUTORIZACION);



    ListarTBLDetalle(objeto.NRODOCUMENTO, objeto.TIPODOCUMENTO, "tblResultadoBusquedaDetalle"); //Muestra los datos de la tabla 3, usando la funcion 



    $('#divResultadosBusqueda2').hide(); // se oculta la tabla 1
    
}




function Actualizar2(objeto) {
    ListaObjetosDetalles = [];
    ObtenerDescrPais(objeto.COUNTRY);
    ObtenerDescrMoneda(objeto.CURRENCY_CD_BASE);


    var AA_NRO_CUENTA = parseInt(objeto.AA_NRO_CUENTA);

    $('#txtExportacion').val(objeto.AA_EXPORTACION);
    $('#txtImportancion').val(objeto.AA_IMPORTACION);
    $('#txtInversion').val(objeto.AA_INVERSION);
    $('#txtPrestamos').val(objeto.AA_PRESTAMOS);
    $('#txtGiros').val(objeto.AA_GIROS);
    $('#txtEnvioGiros').val(objeto.AA_ENVIO_GIROS);
    $('#txtServicios').val(objeto.AA_SERVICIOS);
    $('#txtTopeVenta').val(objeto.AA_MONTO_PROM_VENT);
    $('#txtTopeCompra').val(objeto.AA_MONTO_PROMEDIO);
    $('#txtCiudad').val(objeto.AA_CIUDAD_OPR);
    $('#txtPais').val(objeto.COUNTRY);
    $('#txtPaisDescr').val(objDescrPais.DESCRPPAIS);
    $('#txtMonedaDescr').val(objDescrMoneda.DESCRMONEDA);
    $('#txtEntidad').val(objeto.AA_ENTIDAD_OPR);
    $('#txtTipoCuenta').val(objeto.AA_TIPO_CUENTA);
    $('#txtTipoMoneda').val(objeto.CURRENCY_CD_BASE);
    $('#txtNroProducto').val(AA_NRO_CUENTA);
    $('#txtSaldoExt').val(objeto.AA_SALDO_FIN);
    //$('#txtTotalVentasUSD').val(objeto.AA_TOTAL_VENTA_USD);

    $('#txtTotalVentasCOP').val(objeto.AA_TOTAL_VENTA_COP);
    $('#txtTotalComprasCOP').val(objeto.AA_TOTAL_COMPR_COP);

    $('#txtMontoDisponibleVentas').val(objeto.AA_MONTO_OPR);
    $('#txtMontoDisponibleCompras').val(objeto.AA_MONTO_VENTA_OPR);
    $('#choice').val(objeto.AA_CTA_EXTERIOR);
    $('#txtUltimaModif').val(objeto.ROW_LASTMANT_DTTM);
    //$('#txtTotalComprasUSD').val(objeto.AA_TOTAL_COMPR_USD);
    //av_error_cupo_pn_opr(ListaObjetosDetalles, 'tblResultadoBusqueda2')
    //Autorizar_Monto(objTBLValidarCupon, 'tblResultadoBusqueda2')



    // console.log("KKKKKKKKKKKKKKKKK " + $('#txtTotalVentasUSD').val(objeto.AA_TOTAL_VENTA_USD));


    if (objeto.AA_IMPORTACION == "Y") {

        $("#txtImportancion").prop('checked', true);
    }

    else {
        $("#txtImportancion").prop('checked', false);

    }

    if (objeto.AA_EXPORTACION == "Y") {

        $("#txtExportacion").prop('checked', true);
    }

    else {
        $("#txtExportacion").prop('checked', false);
    }

    if (objeto.AA_INVERSION == "Y") {

        $("#txtInversion").prop('checked', true);
    }

    else {
        $("#txtInversion").prop('checked', false);
    }

    if (objeto.AA_PRESTAMOS == "Y") {

        $("#txtPrestamos").prop('checked', true);
    }

    else {
        $("#txtPrestamos").prop('checked', false);
    }

    if (objeto.AA_GIROS == "Y") {

        $("#txtGiros").prop('checked', true);
    }

    else {
        $("#txtGiros").prop('checked', false);
    }

    if (objeto.AA_ENVIO_GIROS == "Y") {

        $("#txtEnvioGiros").prop('checked', true);
    }

    else {
        $("#txtEnvioGiros").prop('checked', false);
    }

    if (objeto.AA_SERVICIOS == "Y") {

        $("#txtServicios").prop('checked', true);
    }

    else {
        $("#txtServicios").prop('checked', false);
    }



    if (objeto.AA_CTA_EXTERIOR == "Y") {


        $("#choice").prop('checked', true);
        myFunction();


        $('#idPaisProducto').show();

        $('#txtCiudadProducto').hide();

    } else {

        $("#choice").prop('checked', false);
    }


}






var ListaObjetosDetalles = [];

//funcion que se ejecuta al momento de actualizar filas de la tabla 3
function ActualizaFilas(objeto, cant) {
    mensaje("Actualizando filas");
    var listaFinal = [];

    var SOPORTE = $('#DATEFIELD_' + cant).val();

    objTBLDetalleNuevo = {

        NRODOCUMENTO: objeto.NRODOCUMENTO,
        TIPODOCUMENTO: objeto.TIPODOCUMENTO,
        SEQNBR: objeto.SEQNBR,
        AUTORIZADIVISA: $('#txtDescOther' + cant).val(),
        TIPOVIGENCIA: "L",
        SOPORTEDIVISA: SOPORTE[0],
        DATEEND: $('#txtDescr' + cant).val(),
        MONTOPROMVENT: $('#txtDescrlong' + cant).val(),
        MONTOPROMEDIO: $('#txtNbr' + cant).val(),
        ROWADDEDDTTM: $('#txtDttm' + cant).val(),
        ROWADDEDOPPRID: $('#DESCR254_1' + cant).val(),
        ROWLASTMANDTTM: $('#DESCR254_2' + cant).val(),
        ROWLASTMANOPPRID: $('#DESCR254_3' + cant).val(),
        BLOQUEODIVISAS: $('#DESCR254_4' + cant).val(),
        ESTADOCLIENTE: $("#txtEstadoU").val()
    }

    if (objTBLDetalleNuevo.SOPORTEDIVISA == "") {

        alert("---------------------");
    }




    var posicion = existeSeqNuminArray(objTBLDetalleNuevo.SEQNBR); // Si existe el SEQNBR en el array
    if (posicion != 9999) {
        ListaObjetosDetalles[posicion] = objTBLDetalleNuevo;



    } else {
        ListaObjetosDetalles.push(objTBLDetalleNuevo);
        AprobacionTope(ListaObjetosDetalles);



    }

}


function InsertaFilaEntidades(cant) {
    ListaObjetosEntidades = [];
    objEntidades = {

        NRODOCUMENTO: $('#txtNumeroDocumento').val(),
        SEQNBR: cant,
        CodEntidad: $('#txtCodEntidad_N' + cant).val(),
        EntidadFinanciera: $('#txtEntidad_N' + cantidadEntidad).val()


    }
    var posicion = existeSeqNuminArrayEnt(objEntidades.SEQNBR);
    if (posicion != 9999) {

        ListaObjetosEntidades[posicion] = objEntidades;


    } else {

        ListaObjetosEntidades.push(objEntidades);

    }
    // console.log(ListaObjetosDetalles);
}




function InsertaFila(cant) {

    var SOPORTE = $('#DATEFIELD_N' + cant).val();
    var TIPOVIGENCIA = $('#DATEADD_N' + cant).val();


    //console.log(typeof SOPORTE);

    //console.log("soporte " + SOPORTE);//SI
    //console.log("soporte " + SOPORTE[0]);//S

    if (typeof SOPORTE == 'object') {

        objTBLDetalleNuevo = {


            TIPODOCUMENTO: $('#dllTipoDocumentoB').val(),
            NRODOCUMENTO: $('#txtNumeroDocumento').val(),
            SEQNBR: cant,
            AUTORIZADIVISA: $('#txtDescOther_N' + cant).val(),
            TIPOVIGENCIA: TIPOVIGENCIA[0],
            SOPORTEDIVISA: SOPORTE[0],
            DATEEND: $('#txtDescr_N' + cant).val(),
            MONTOPROMVENT: $("#txtDescrlong_N" + cant).val().replace(",", ","),
            MONTOPROMEDIO: $('#txtNbr_N' + cant).val().replace(",", ","),



            //AA_AUTORIZACION_DIV : $('#txtAutorizacion_N'+cant).val(),
            AA_ESTADO_AUTORIZA: $('#txtEstado_N' + cant).val(),
            ROWADDEDDTTM: $('#txtValueDisplay_N' + cant).val(),
            ROWADDEDOPPRID: $('#DESCR254_1_N' + cant).val(),
            ROWLASTMANDTTM: $('#DESCR254_2_N' + cant).val(),
            ROWLASTMANOPPRID: $('#DESCR254_3_N' + cant).val(),
            BLOQUEODIVISAS: $('#DESCR254_4_N' + cant).val()


        }

    }
    else {


        objTBLDetalleNuevo = {


            TIPODOCUMENTO: $('#dllTipoDocumentoB').val(),
            NRODOCUMENTO: $('#txtNumeroDocumento').val(),
            SEQNBR: cant,
            AUTORIZADIVISA: $('#txtDescOther_N' + cant).val(),
            TIPOVIGENCIA: $('#DATEADD_N' + cant).val(),
            SOPORTEDIVISA: $('#DATEFIELD_N' + cant).val(),
            DATEEND: $('#txtDescr_N' + cant).val(),
            MONTOPROMVENT: $("#txtDescrlong_N" + cant).val().replace(",", ","),
            MONTOPROMEDIO: $('#txtNbr_N' + cant).val().replace(",", ","),



            //AA_AUTORIZACION_DIV : $('#txtAutorizacion_N'+cant).val(),
            AA_ESTADO_AUTORIZA: $('#txtEstado_N' + cant).val(),
            ROWADDEDDTTM: $('#txtValueDisplay_N' + cant).val(),
            ROWADDEDOPPRID: $('#DESCR254_1_N' + cant).val(),
            ROWLASTMANDTTM: $('#DESCR254_2_N' + cant).val(),
            ROWLASTMANOPPRID: $('#DESCR254_3_N' + cant).val(),
            BLOQUEODIVISAS: $('#DESCR254_4_N' + cant).val()


        }




    }

    //console.log("1");
    // console.log(objTBLDetalleNuevo);



    var posicion = existeSeqNuminArray(objTBLDetalleNuevo.SEQNBR);
    if (posicion != 9999) {

        ListaObjetosDetalles[posicion] = objTBLDetalleNuevo;

        TIPO_VIGENCIA();
        //AV_ANALIZA_ESTADO_AUTOIZACION();


        //COMPRA
        if (objTBLDetalleNuevo.MONTOPROMEDIO != "" && objTBLDetalleNuevo.MONTOPROMVENT == "") {

            AprobacionTope(objTBLDetalleNuevo.MONTOPROMEDIO, objTBLDetalleNuevo.MONTOPROMVENT, objUsuarioSesion.JOBCODE)

        }
        //VENTA
        else if (objTBLDetalleNuevo.MONTOPROMVENT != "" && objTBLDetalleNuevo.MONTOPROMEDIO == "") {


            AprobacionTope(objTBLDetalleNuevo.MONTOPROMEDIO, objTBLDetalleNuevo.MONTOPROMVENT, objUsuarioSesion.JOBCODE)



        }
        else if (objTBLDetalleNuevo.MONTOPROMVENT != "" && objTBLDetalleNuevo.MONTOPROMEDIO != "") {
            AprobacionTope(objTBLDetalleNuevo.MONTOPROMEDIO, objTBLDetalleNuevo.MONTOPROMVENT, objUsuarioSesion.JOBCODE)

        }





    } else {

        ListaObjetosDetalles.push(objTBLDetalleNuevo);

        TIPO_VIGENCIA();

        if (objTBLDetalleNuevo.MONTOPROMVENT != "") {

            AprobacionTope(objTBLDetalleNuevo.MONTOPROMVENT, objUsuarioSesion.JOBCODE);//519

        }
        if (objTBLDetalleNuevo.MONTOPROMEDIO != "") {


            AprobacionTope(objTBLDetalleNuevo.MONTOPROMEDIO, objUsuarioSesion.JOBCODE);//519
        }

        //AprobacionTope(objTBLDetalleNuevo.MONTOPROMVENT, objUsuarioSesion.JOBCODE);//519
        //AV_ANALIZA_ESTADO_AUTOIZACION();
        //AprobacionTope(objTBLDetalleNuevo.MONTOPROMEDIO, objUsuarioSesion.JOBCODE)



        // guardarMaestro(objTBLDetalleNuevo);   //CON LOS DATOS DE LA TABLA 1
        // guardarDetalle(objTBLDetalleNuevo);

    }
    // console.log(ListaObjetosDetalles);
}



function TIPO_VIGENCIA() {
    var FechaActual = new Date();
    //console.log(FechaActual);





    objTBLDetalleNuevo.ROWADDEDOPPRID = getCookie("usuarioNR");



    if (objTBLDetalleNuevo.TIPOVIGENCIA == 'M') {



        FechaActual.setMonth(FechaActual.getMonth() + 12);
        // console.log(FechaActual.getDate() + "/" + FechaActual.getMonth() + 1 + "/" + FechaActual.getFullYear());



        if (FechaActual.getDate() < 10) {

            // $('#txtDescr_N' + objTBLDetalleNuevo.SEQNBR).val((FechaActual.getMonth() + 1) + "/0" + FechaActual.getDate() + "/" + FechaActual.getFullYear());
            $('#txtDescr_N' + objTBLDetalleNuevo.SEQNBR).val(("0" + FechaActual.getDate()) + "/" + (FechaActual.getMonth() + 1) + "/" + FechaActual.getFullYear());

        }
        else {
            $('#txtDescr_N' + objTBLDetalleNuevo.SEQNBR).val((FechaActual.getMonth() + 1) + "/" + FechaActual.getDate() + "/" + FechaActual.getFullYear());


        }

        objTBLDetalleNuevo.DATEEND = (FechaActual.getMonth() + 1) + "/" + FechaActual.getDate() + "/" + FechaActual.getFullYear();


        var today = new Date();
        var dd = String(today.getDate()).slice(-2);
        var mm = String(today.getMonth() + 1).slice(-2); //January is 0!
        var yyyy = today.getFullYear();

        today = mm + '/' + dd + '/' + yyyy;



        objTBLDetalleNuevo.ROWLASTMANDTTM = today;
    }


    else if (objTBLDetalleNuevo.TIPOVIGENCIA == 'P') {

        var FechaPuntual = new Date(FechaActual.getFullYear(), FechaActual.getMonth() + 1, 0)

        objTBLDetalleNuevo.DATEEND = (FechaPuntual.getMonth() + 1) + "/" + FechaPuntual.getDate() + "/" + FechaPuntual.getFullYear();

        //objTBLDetalleNuevo.DATEEND = FechaPuntual.getDate() + "/" + (FechaPuntual.getMonth() + 1) + "/" + FechaPuntual.getFullYear();
        var today = new Date();
        var dd = String(today.getDate()).slice(-2);
        var mm = String(today.getMonth() + 1).slice(-2); //January is 0!
        var yyyy = today.getFullYear();

        today = mm + '/' + dd + '/' + yyyy;



        objTBLDetalleNuevo.ROWLASTMANDTTM = today;
        $('#txtDescr_N' + objTBLDetalleNuevo.SEQNBR).val(FechaPuntual.getDate() + "/" + (FechaPuntual.getMonth() + 1) + "/" + FechaPuntual.getFullYear());

    }

    else if (objTBLDetalleNuevo.TIPOVIGENCIA == 'S') {


        FechaActual.setMonth(FechaActual.getMonth() + 6);

        objTBLDetalleNuevo.DATEEND = FechaActual;
        objTBLDetalleNuevo.ROWLASTMANDTTM = new Date();

    }

}





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




//Funcion para saber si existe el SEQ NBR En el array
function existeSeqNuminArrayEnt(seqNum) {
    var pos = 9999;
    if (ListaObjetosEntidades.length > 0) {
        for (i = 0; i < ListaObjetosEntidades.length; i++) {
            if (ListaObjetosEntidades[i].SEQNBR == seqNum) {
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



function GuardarRegistroOpInter() {
    //validaArrayCampos("campo_validacion", true);
    //OperacionesMesaDinero(objTBLGeneral2.NRODOCUMENTO, objTBLGeneral2.TIPODOCUMENTO);

    //TopeVentaUSD(objTBLGeneral2.NRODOCUMENTO, objTBLGeneral2.TIPODOCUMENTO);



    objTBLGeneral2 = {

        ROW_LASTMANT_OPRID: '1023800564',
        ROW_ADDED_OPRID: '109874616066',
        TIPODOCUMENTO: $('#dllTipoDocumentoB').val(),
        NRODOCUMENTO: $('#txtNumeroDocumento').val(),
        AA_CIUDAD_OPR: $('#txtCiudad').val(),
        AA_IMPORTACION: $('#txtImportancion').val(),
        AA_EXPORTACION: $('#txtExportacion').val(),
        AA_INVERSION: $('#txtInversion').val(),
        AA_PRESTAMOS: $('#txtPrestamos').val(),
        AA_GIROS: $('#txtGiros').val(),
        AA_ENVIO_GIROS: $('#txtEnvioGiros').val(),
        AA_SERVICIOS: $('#txtServicios').val(),
        AA_MONTO_PROM_VENT: $('#txtTopeVenta').val().replace(/[$]/g, '').replace(/[.]/g, ''),
        AA_MONTO_PROMEDIO: $('#txtTopeCompra').val().replace(/[$]/g, '').replace(/[.]/g, ''),
        COUNTRY: $('#txtPais').val(),
        AA_ENTIDAD_OPR: $('#txtEntidad').val(),
        AA_TIPO_CUENTA: $('#txtTipoCuenta').val(),
        CURRENCY_CD_BASE: $('#txtTipoMoneda').val(),
        AA_NRO_CUENTA: $('#txtNroProducto').val(),
        AA_SALDO_FIN: $('#txtSaldoExt').val().replace(/[$]/g, '').replace(/[.]/g, ''),
        AA_CTA_EXTERIOR: $('#choice').val(),
        AA_TOTAL_VENTA_USD: $('#txtTotalVentasUSD').val(),
        AA_TOTAL_COMPR_USD: $('#txtTotalComprasUSD').val(),
        AA_TOTAL_VENTA_COP: $('#txtTotalVentasCOP').val(),
        AA_TOTAL_COMPR_COP: $('#txtTotalComprasCOP').val(),
        AA_DIVISAS: $('#dllOperacionesInt2').val(),
        AA_MONTO_OPR: $('#txtMontoDisponibleVentas').val().replace(/[$]/g, '').replace(/[.]/g, ''),
        AA_MONTO_VENTA_OPR: $('#txtMontoDisponibleCompras').val()
    }


    var nuevo = objTBLGeneral2.AA_MONTO_PROM_VENT - objTBLGeneral2.AA_TOTAL_VENTA_USD;
    ListaObjetosOperaciones = [];
    ListaObjetosOperaciones.push(objTBLGeneral2);


    if (objTBLGeneral2.AA_DIVISAS == "Y") {

    }
    else {
        //  console.log("3041");
        GuardarRegistro();
        // console.log(ListaObjetosOperaciones);
    }


    if (objTBLGeneral2.AA_CTA_EXTERIOR == "Y") {

        if (objTBLGeneral2.COUNTRY == "" || objTBLGeneral2.COUNTRY == " " || objTBLGeneral2.AA_SALDO_FIN == "" || objTBLGeneral2.AA_SALDO_FIN == " " || objTBLGeneral2.AA_CIUDAD_OPR == "" || objTBLGeneral2.AA_CIUDAD_OPR == " " ||
            objTBLGeneral2.AA_ENTIDAD_OPR == "" || objTBLGeneral2.AA_ENTIDAD_OPR == " " || objTBLGeneral2.AA_TIPO_CUENTA == "" || objTBLGeneral2.AA_TIPO_CUENTA == " " || objTBLGeneral2.CURRENCY_CD_BASE == "" || objTBLGeneral2.CURRENCY_CD_BASE == " "
            || objTBLGeneral2.AA_NRO_CUENTA == "" || objTBLGeneral2.AA_NRO_CUENTA == " ") {

            var elemento = document.getElementById("choice");
            elemento.scrollIntoView();


            if (objTBLGeneral2.COUNTRY == "" || objTBLGeneral2.COUNTRY == " ") {
                document.getElementById("txtPais").style.background = "#FFB9B9";
            }
            else {
                document.getElementById("txtPais").style.background = "#FFFFFF";
            }
            if (objTBLGeneral2.AA_SALDO_FIN == "" || objTBLGeneral2.AA_SALDO_FIN == " ") {
                document.getElementById("txtSaldoExt").style.background = "#FFB9B9";
            }
            else {
                document.getElementById("txtSaldoExt").style.background = "#FFFFFF";
            }
            if (objTBLGeneral2.AA_CIUDAD_OPR == "" || objTBLGeneral2.AA_CIUDAD_OPR == " ") {
                document.getElementById("txtCiudad").style.background = "#FFB9B9";
            }
            else {
                document.getElementById("txtCiudad").style.background = "#FFFFFF";
            }

            if (objTBLGeneral2.AA_ENTIDAD_OPR == "" || objTBLGeneral2.AA_ENTIDAD_OPR == " ") {
                document.getElementById("txtEntidad").style.background = "#FFB9B9";
            }
            else {
                document.getElementById("txtEntidad").style.background = "#FFFFFF";
            }
            if (objTBLGeneral2.AA_TIPO_CUENTA == "" || objTBLGeneral2.AA_TIPO_CUENTA == " ") {
                document.getElementById("txtTipoCuenta").style.background = "#FFB9B9";
            }
            else {
                document.getElementById("txtTipoCuenta").style.background = "#FFFFFF";
            }
            if (objTBLGeneral2.CURRENCY_CD_BASE == "" || objTBLGeneral2.CURRENCY_CD_BASE == " ") {
                document.getElementById("txtTipoMoneda").style.background = "#FFB9B9";
            }
            else {
                document.getElementById("txtTipoMoneda").style.background = "#FFFFFF";
            }

            if (objTBLGeneral2.AA_NRO_CUENTA == "" || objTBLGeneral2.AA_NRO_CUENTA == " ") {
                document.getElementById("txtNroProducto").style.background = "#FFB9B9";
            }
            else {
                document.getElementById("txtNroProducto").style.background = "#FFFFFF";
            }

            if (objTBLGeneral2.AA_SALDO_FIN == "" || objTBLGeneral2.AA_SALDO_FIN == " ") {
                document.getElementById("txtSaldoExt").style.background = "#FFB9B9";
            }
            else {
                document.getElementById("txtSaldoExt").style.background = "#FFFFFF";
            }

            // bootbox.alert("Todos los campos en rojo de Producto en el exterior en la página de Operaciones Internacionales son requeridos.");



        }

        else {
            var regExp = /[a-zA-Z.]/g;
            cantRegistros = 0
            //GuardarRegistroEntidadesClte();

            ListarTBLEntidades(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO, "tblEntidadesCliente");
            //GuardarRegistroEntidadesClte();

            var cantValidacion = 0;

            if (regExp.test(objTBLGeneral2.AA_SALDO_FIN)) {

                document.getElementById("txtSaldoExt").style.background = "#FFB9B9";


                var txtSaldoExt = document.getElementById("text");

                txtSaldoExt.scrollIntoView();
                bootbox.alert("El valor digitado de saldo producto en el exterior es inválido");
                cantValidacion++;
            }
            else if (objTBLGeneral2.AA_IMPORTACION == "N" && objTBLGeneral2.AA_EXPORTACION == "N" && objTBLGeneral2.AA_INVERSION == "N" && objTBLGeneral2.AA_PRESTAMOS == "N" && objTBLGeneral2.AA_GIROS == "N" && objTBLGeneral2.AA_ENVIO_GIROS == "N" && objTBLGeneral2.AA_SERVICIOS == "N") {

                cantValidacion++;
                /*
                var elemento = document.getElementById("txtImportancion");
                elemento.scrollIntoView();
          

                bootbox.alert("Si el cliente efectua Operaciones Internacionales se debe diligenciar al menos un tipo de Operación 3169");*/
            }



            else if (cantidadEntidadesListaLL == 0) {

                cantValidacion++;

            }     //ListarOperacionesInterDetall(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO, 'tblResultadoBusqueda2');




            else {


                if (cantValidacion == 0) {
                    document.getElementById("txtSaldoExt").style.background = "#FFFFFF";
                    guardarOperacionesInt(ListaObjetosOperaciones);
                    document.getElementById("txtPais").style.background = "#FFFFFF";
                    document.getElementById("txtSaldoExt").style.background = "#FFFFFF";
                    document.getElementById("txtCiudad").style.background = "#FFFFFF";
                    document.getElementById("txtEntidad").style.background = "#FFFFFF";
                    document.getElementById("txtTipoCuenta").style.background = "#FFFFFF";
                    document.getElementById("txtTipoMoneda").style.background = "#FFFFFF";
                    document.getElementById("txtNroProducto").style.background = "#FFFFFF";

                    //guardarDetalle(ListaObjetosDetalles);



                    var dialog = bootbox.dialog({
                        message: '<p class="text-center mb-0"><i class="fa fa-spin fa-cog"></i>Guardando, por favor espere...</p>',
                        closeButton: false
                    });




                    setTimeout(function () {

                        dialog.modal('hide');
                        //GuardarRegistroOpInter();  // Guardar lo relacionado con PS_AA_PN_OPR_INTER


                    }, 4000);
                }



            }


        }

    }



    else {
        var regExp = /[a-zA-Z.]/g;
        cantRegistros = 0
        //GuardarRegistroEntidadesClte();

        ListarTBLEntidades(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO, "tblEntidadesCliente");
        //GuardarRegistroEntidadesClte();

        var cantValidacion = 0;

        if (regExp.test(objTBLGeneral2.AA_SALDO_FIN)) {

            document.getElementById("txtSaldoExt").style.background = "#FFB9B9";


            var txtSaldoExt = document.getElementById("text");

            txtSaldoExt.scrollIntoView();
            bootbox.alert("El valor digitado de saldo producto en el exterior es inválido");
            cantValidacion++;
        }
        else if (objTBLGeneral2.AA_IMPORTACION == "N" && objTBLGeneral2.AA_EXPORTACION == "N" && objTBLGeneral2.AA_INVERSION == "N" && objTBLGeneral2.AA_PRESTAMOS == "N" && objTBLGeneral2.AA_GIROS == "N" && objTBLGeneral2.AA_ENVIO_GIROS == "N" && objTBLGeneral2.AA_SERVICIOS == "N") {

            cantValidacion++;
            /*
            var elemento = document.getElementById("txtImportancion");
            elemento.scrollIntoView();
      

            bootbox.alert("Si el cliente efectua Operaciones Internacionales se debe diligenciar al menos un tipo de Operación 3169");*/
        }



        else if (cantidadEntidadesListaLL == 0) {

            cantValidacion++;

        }     //ListarOperacionesInterDetall(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO, 'tblResultadoBusqueda2');




        else {


            if (cantValidacion == 0) {
                document.getElementById("txtSaldoExt").style.background = "#FFFFFF";
                guardarOperacionesInt(ListaObjetosOperaciones);
                document.getElementById("txtPais").style.background = "#FFFFFF";
                document.getElementById("txtSaldoExt").style.background = "#FFFFFF";
                document.getElementById("txtCiudad").style.background = "#FFFFFF";
                document.getElementById("txtEntidad").style.background = "#FFFFFF";
                document.getElementById("txtTipoCuenta").style.background = "#FFFFFF";
                document.getElementById("txtTipoMoneda").style.background = "#FFFFFF";
                document.getElementById("txtNroProducto").style.background = "#FFFFFF";

                //guardarDetalle(ListaObjetosDetalles);



                var dialog = bootbox.dialog({
                    message: '<p class="text-center mb-0"><i class="fa fa-spin fa-cog"></i>Guardando, por favor espere...</p>',
                    closeButton: false
                });




                setTimeout(function () {

                    dialog.modal('hide');
                    //GuardarRegistroOpInter();  // Guardar lo relacionado con PS_AA_PN_OPR_INTER


                }, 4000);
            }



        }


    }







    //Funcion que guarda la informacion

    // var NroDocumento2 = $('#txtNumeroDocumento').val();

    // var TipoDocumento2 = $('#dllTipoDocumentoB').val();  //Si hay un valor en BUSCAR ejemplo 1020 se va a la funcion ListarTBLGeneral
    //ListarTBLDetalle(NroDocumento2, TipoDocumento2, "tblResultadoBusquedaDetalle");
    // ListarOperacionesInterDetall(objDivBuscar.NRODOCUMENTO, objDivBuscar.TIPODOCUMENTO, 'tblResultadoBusqueda2');
    //guardarOperacionesInt(ListaObjetosOperaciones);


}






function GuardarRegistro() {



    objTBLGeneral = {
        NRODOCUMENTO: $('#txtNumeroDocumento').val(),
        SEQNBR: $('#txtID').val(),
        AUTORIZADIVISA: $('#txtTitulo').val(),

        //TIPOVIGENCIA: $('#txtValorNumerico').val(),
        TIPOVIGENCIA: "P",
        // DATEEND: $('#txtFecha').val(),
        //SOPORTEDIVISA: $('#txtDescr100').val(),
        SOPORTEDIVISA: "NI",

        DATEEND: $('#txtFecha').val(),

        MONTOPROMVENT: $('#txtValue').val(),
        MONTOPROMEDIO: $('#txtValueDisplay').val(),
        ROWADDEDDTTM: $('#txtSqlQuery').val(),
        AA_ESTADO_AUTORIZA: $('#txtEstado').val(),
        ROWADDEDOPPRID: $('#txtAñadido').val(),
        ROWLASTMANDTTM: $('#txtModificado').val(),
        ROWLASTMANOPPRID: $('#txtMantenimiento').val(),
        BLOQUEODIVISAS: $('#txtBloqueo').val(),
        AA_AUTORIZACION: $('#AprobacionVigente').val(),
        AA_DIVISAS: $('#dllOperacionesInt2').val()
        //AA_AUTORIZACION_DIV:$('#txtAutorizacion').val(),

    };
    

    $("#tableColor").dataTable().fnDestroy();

    $("#extendido").dataTable().fnDestroy();




    var NroDocumento2 = $('#txtNumeroDocumento').val();

    var TipoDocumento2 = $('#dllTipoDocumentoB').val();  //Si hay un valor en BUSCAR ejemplo 1020 se va a la funcion ListarTBLGeneral

    ListarTBLDetalle(NroDocumento2, TipoDocumento2, "tblResultadoBusquedaDetalle");



    $("#extendido").dataTable({


        "bDestroy": true,
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
        },

        dom: 'Blfrtip',
        buttons: [
            {

                extend: 'collection',
                text: 'Exportar',
                buttons: [
                    { extend: 'copy', text: 'Copiar' },
                    { extend: 'excel', text: 'Excel' },
                    { extend: 'csv', text: 'CSV' },
                    { extend: 'pdf', text: 'Pdf' },
                    { extend: 'print', text: 'Imprimir' }



                ]
            }
        ]
    });


    $("#tableColor").dataTable({


        "bDestroy": true,
        "processing": true,
        "filter": true,
        "lengthChange": true,
        "lengthMenu": [[10, 20, 30, -1], [10, 20, 30, 'Todos']],
        "order": [[9, "desc"]],
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
        },

        dom: 'Blfrtip',
        buttons: [
            {

                extend: 'collection',
                text: 'Exportar',
                buttons: [
                    { extend: 'copy', text: 'Copiar' },
                    { extend: 'excel', text: 'Excel' },
                    { extend: 'csv', text: 'CSV' },
                    { extend: 'pdf', text: 'Pdf' },
                    { extend: 'print', text: 'Imprimir' }



                ]
            }
        ]
    });


}



//Guardar panel de tipo de operaciones y si efectua operaciones internacionales
function guardarOperacionesInt(ListaObjetosOperaciones) {
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";//Pruebas
    var params = JSON.stringify({ valor: ListaObjetosOperaciones });
    var metodo = "GuardarOperacionesInt";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
    if (objJson.Codigo == 9999) {
        mensajeErrorGeneral("Error al momento de guardar");
        throw new Error(objJson.Descripcion);
    }
}



function GuardarRegistroEntidadesClte() {


    ListaObjetosEntidades = [];

    objEntidades = {

        TIPODOCUMENTO: $('#dllTipoDocumentoB').val(),
        NRODOCUMENTO: $('#txtNumeroDocumento').val(),
        SEQNBR: cantidadEntidad,
        CodEntidad: $('#txtCodEntidad_N' + cantidadEntidad).val(),
        EntidadFinanciera: $('#txtEntidad_N' + cantidadEntidad).val(),
        ROWADDEDOPRID: '1098746160'

    }
    ListaObjetosEntidades.push(objEntidades);


    if (objEntidades.CodEntidad !== undefined && objEntidades.CodEntidad !== undefined) {


        guardarEntidadesCliente(ListaObjetosEntidades);

    }





}


function MontoPromedio(monto) {
    var montof;

    if (monto != null && monto < 5000) {



        return monto;
    } else {
        return "0";
    }

}


/*
var formatoMoneda = new Intl.NumberFormat('en-US', {

    style: 'currency',
    currency: 'USD'
});*/



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




function fechaNormalizadaRow(fecha) {
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
        var fechaFinal = result[1] + "/" + result[0] + "/" + result[2];
        return fechaFinal;
    } else {
        return "01/01/1900";
    }

}


function MontoPromedio(monto) {
    var montof;

    if (monto != null && monto < 5000) {



        return monto;
    } else {
        return "0";
    }

}


/*
var formatoMoneda = new Intl.NumberFormat('en-US', {

    style: 'currency',
    currency: 'USD'
});*/



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








function Autorizacion() {
    var autorizacionValida;
    var date = Date.now();
    var lastOprid = "";


    if (fecha != null) {

        return fechaFinal;
    } else {
        return "01/01/1900";
    }

}



//Guarda parametro maestro y actualiza
function guardarDetalle(ListaObjetosDetalles) {

    
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";//Pruebas
    var params = JSON.stringify({ valor: ListaObjetosDetalles });
    var metodo = "GuardarParametroMaestro";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
   
    if (objJson.Codigo) {
        mensajeErrorGeneral("Error al momento de guardar.Por favor valide los parametros");
        throw new Error(objJson.Descripcion);
    } else {
        mensaje("Proceso exitoso");

    }
}



function guardarEntidadesCliente(ListaObjetosEntidades) {
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";//Pruebas
    var params = JSON.stringify({ valor: ListaObjetosEntidades });
    var metodo = "GuardarEntidadesCliente";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
    if (objJson.Codigo) {
        mensajeErrorGeneral("Error al momento de guardar.Por favor valide los parametros");
        //throw new Error(objJson.Descripcion);
    } else {
        var valor = $('#txtNumeroDocumento').val();  //Si hay un valor en BUSCAR ejemplo 1020 se va a la funcion ListarTBLGeneral
        var TipoDocumento = $('#dllTipoDocumentoU').val();  //Si hay un valor en BUSCAR ejemplo 1020 se va a la funcion ListarTBLGeneral



        ListarTBLEntidades(valor, TipoDocumento, "tblEntidadesCliente");

        mensaje("Proceso exitoso");
        ListaObjetosEntidades = [];

    }
}






function updateCount() {
    var total = $('table input[value="approve"]').length;
    var countApprove = $('table input[value="approve"]:checked').length;
    var countDeny = $('table input[value="reject"]:checked').length;
    $('#changesCount').text((countApprove + countDeny) + ' changes ');
    

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

var era;
function uncheckRadio(rbutton) {

    if (rbutton.checked == true && era == true) {
        rbutton.checked = false;
    }
    era = rbutton.checked;
}


var category = null;
$("input[name='radio']").click(function () {

    radio = this.value;

});






var objTBLTopeVenta;
function TopeVentaUSD(valorDocumento, valorTipoDoc) {



    var params = JSON.stringify({ valor: valorDocumento, valorTipoDoc: valorTipoDoc });
    var metodo = "Venta_usd_tope";

    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
    
    var filasHtml = "";
    var filasHtmlExtendido = "";
    var cantt = 1;
    if (jQuery.isEmptyObject(objJson.ListaOperacionesIntPN)) {
        mensajeErrorGeneral("No se obtuvieron registros en topes.");
        $("#" + tblListado).html(filasHtml);
    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaOperacionesIntPN[0].NRODOCUMENTO != "") {

                $.each(objJson.ListaOperacionesIntPN, function (key, value) {


                    var nuevoValor2 = value.AA_MONTO_PROM_VENT.replace(',', '.');


                    (function (old) {
                        var dec = 0.12 .toLocaleString().charAt(1),
                            tho = dec === "," ? "." : ",";

                        if (1000 .toLocaleString() !== "1,000.00") {
                            Number.prototype.toLocaleString = function () {
                                var neg = this < 0,
                                    f = this.toFixed(2).slice(+neg);
                           
                                return (neg ? "-" : "")
                                    + f.slice(0, -3).replace(/(?=(?!^)(?:\d{3})+(?!\d))/g, tho)
                                    + dec + f.slice(-2);
                            }
                        }
                    })(Number.prototype.toLocaleString);




                    var signo = "$";

                    if (nuevoValor2 == "") {


                       
                        objTBLTopeVenta = {

                            AA_MONTO_PROM_VENT: signo.concat(0 .toLocaleString()).replace(/[,]/g, '.'),
                            AA_MONTO_PROMEDIO: signo.concat(0 .toLocaleString()).replace(/[,]/g, '.')


                        }
                    }
                    else {

                        var nuevoval = parseFloat(nuevoValor2);
                        var AA_MONTO_PROMEDIO = parseFloat(value.AA_MONTO_PROMEDIO);

                        


                        objTBLTopeVenta = {


                            AA_MONTO_PROM_VENT: signo.concat(nuevoval .toLocaleString()),
                            AA_MONTO_PROMEDIO: signo.concat(AA_MONTO_PROMEDIO .toLocaleString())
                        }

                    }


                 


                    TopeventaAutorizada(objTBLTopeVenta);

                    cantt++;
                });
            }
        } else {

            mensajeErrorGeneral(objJson.Descripcion);

        }
    }

}









function ListaPaises() {
    var svc = "./Servicios/WS_Utils.asmx/";//Pruebas
    var params = JSON.stringify({ numero: "100veces" });
    var metodo = "Lista_Paises";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
    var filasHtml = "";
    var cant = 1;
    if (jQuery.isEmptyObject(objJson.ListaPaises)) {
        //mensajeErrorGeneral("No se obtuvieron registros.");
        mensaje(objJson.Descripcion);
        $("#tblResultadoPaises").html(filasHtml);
    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaPaises[0].COUNTRY != "") {
                $('#tblResultadoPaises').show();

                $.each(objJson.ListaPaises, function (key, value) {


                    objPaises = {
                        COUNTRY: value.COUNTRY,
                        DESCR: value.DESCR

                    }




                    filasHtml += "<tr onclick='InsertarPais(" + JSON.stringify(value) + "," + JSON.stringify(cantidadPais) + ")'>";

                    filasHtml += "<td><a>" + cant + "</td>";
                    filasHtml += "<td><a>" + value.COUNTRY + "</td>";
                    filasHtml += "<td><a>" + value.DESCR + "</td>";

                    filasHtml += "</tr>";

                    cant++;
                });
            }
        } else {
            mensajeErrorGeneral(objJson.Descripcion);
        }


        $("#tblResultadoPaises").html(filasHtml);
        $("#tblResultadoPaises").show();
        $("#dllOficina").prop("disabled", true);





        $("#paises").dataTable({



            "bDestroy": true,
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

    }
}



function InsertarPais(valor) {
    var msg;
    var confirma = confirm("¿Esta seguro de insertar este País ?");
    if (confirma == true) {
        // EliminarDetalle(valor);

        $('#txtPais').val(valor.COUNTRY);
        $('#txtPaisDescr').val(valor.DESCR);
        //$('#txtEntidad_N' + cantidadEntidad).val(valor.EntidadFinanciera);




    } else {
        mensajeErrorGeneral("Operacion cancelada");
    }
}







function ListaTiposMoneda() {
    var svc = "./Servicios/WS_Utils.asmx/";//Pruebas
    var params = JSON.stringify({ numero: "100veces" });
    var metodo = "Lista_Tipos_Moneda";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
    var filasHtml = "";
    var cant = 1;
    if (jQuery.isEmptyObject(objJson.ListaMonedas)) {
        //mensajeErrorGeneral("No se obtuvieron registros.");
        mensaje(objJson.Descripcion);
        $("#tblResultadoMonedas").html(filasHtml);
    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaMonedas[0].DESCR != "") {
                $('#tblResultadoMonedas').show();

                $.each(objJson.ListaMonedas, function (key, value) {


                    objMonedas = {

                        DESCR: value.DESCR,
                        CODIGO: value.CODIGO
                    }




                    filasHtml += "<tr onclick='InsertarMonedaExtranjera(" + JSON.stringify(value) + "," + JSON.stringify(cantidadMonedasExtranjeras) + ")'>";

                    filasHtml += "<td><a>" + cant + "</td>";
                    filasHtml += "<td><a>" + value.CODIGO + "</td>";
                    filasHtml += "<td><a>" + value.DESCRIPCION + "</td>";

                    filasHtml += "</tr>";

                    cant++;
                });
            }
        } else {
            mensajeErrorGeneral(objJson.Descripcion);
        }


        $("#tblResultadoMonedas").html(filasHtml);
        $("#tblResultadoMonedas").show();
        $("#dllOficina").prop("disabled", true);



        $("#monedas").dataTable({



            "bDestroy": true,
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
    }
}


function InsertarMonedaExtranjera(valor) {
    var msg;
    var confirma = confirm("¿Esta seguro de insertar esta moneda extranjera ?");
    if (confirma == true) {

        $('#txtTipoMoneda').val(valor.CODIGO);
        $('#txtMonedaDescr').val(valor.DESCRIPCION);


    } else {
        mensajeErrorGeneral("Operacion cancelada");
    }
}





var objMesaDinero;
function OperacionesMesaDineroCopCompra(valorDocumento, valorTipoDoc) {



    var params = JSON.stringify({ valor: valorDocumento, valorTipoDoc: valorTipoDoc });
    var metodo = "Mesa_Dinero_Cop_Mes_Compra";
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
    var filasHtml = "";
    var filasHtmlExtendido = "";
    var cantt = 1;
    if (jQuery.isEmptyObject(objJson.ListaOperacionesMesaDineroCopCompra)) {
        //mensajeErrorGeneral("No se obtuvieron registros en mesa de dinero compra");



        $('#txtTotalComprasUSD').val("$0");
        

        $('#txtMontoDisponibleCompras').val(objTBLGeneral2.AA_MONTO_PROMEDIO);

       
    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaOperacionesMesaDineroCopCompra[0].TOTAL_COMPR_USD != "") {





                $.each(objJson.ListaOperacionesMesaDineroCopCompra, function (key, value) {





                    if (value.TOTAL_COMPR_USD == 788) {


                        objMesaDinero = {

                            AA_TIPO_DIVISA: value.AA_TIPO_DIVISA,
                            //TOTAL_VENTA_USD: value.TOTAL_VENTA_USD,
                            TOTAL_COMPR_USD: "0.0"
                        }

                    }
                    else {

                        objMesaDinero = {

                            AA_TIPO_DIVISA: value.AA_TIPO_DIVISA,
                            // TOTAL_VENTA_USD: value.TOTAL_VENTA_USD,
                            TOTAL_COMPR_USD: value.TOTAL_COMPR_USD
                        }
                    }





                    var suma = 0;
                    var SumaVentas = 0;
                    var objeto = objJson.ListaOperacionesMesaDineroCopCompra;


                    if (objMesaDinero.AA_TIPO_DIVISA == 'C') {

                        var TotalCompras1 = objMesaDinero.TOTAL_COMPR_USD;
                        var valor3 = TotalCompras1.replace(',', '.');
                        var totalc1 = parseFloat(valor3);
                        var signo = "$";
                        var TotalCompras = signo.concat(totalc1 .toLocaleString());
                        $('#txtTotalComprasCOP').val(TotalCompras);





                    }




                    cantt++;
                });
            }
        }
        else {

            mensajeErrorGeneral(objJson.Descripcion);

        }
    }

    for (i = 1; i <= cantt; i++) {

        $('#txtSeqNum' + i).prop('disabled', true);
    }
    cantidadMesa = cantt - 1;

}






var objMesaDinero;
function OperacionesMesaDineroCopVenta(valorDocumento, valorTipoDoc) {



    var params = JSON.stringify({ valor: valorDocumento, valorTipoDoc: valorTipoDoc });
    var metodo = "Mesa_Dinero_Cop_Mes_Venta";
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
    var filasHtml = "";
    var filasHtmlExtendido = "";
    var cantt = 1;
    if (jQuery.isEmptyObject(objJson.ListaOperacionesMesaDineroCopVenta)) {
        mensajeErrorGeneral("No se obtuvieron registros en mesa de dinero venta");

        $('#txtTotalVentasCOP').val("$0");

  



        (function (old) {
            var dec = 0.12 .toLocaleString().charAt(1),
                tho = dec === "," ? "." : ".";

            if (1000 .toLocaleString() !== "1,000.00") {
                Number.prototype.toLocaleString = function () {
                    var neg = this < 0,
                        f = this.toFixed(2).slice(+neg);
                  
                    return (neg ? "-" : "")
                        + f.slice(0, -3).replace(/(?=(?!^)(?:\d{3})+(?!\d))/g, tho)
                        + dec + f.slice(-2);
                }
            }
        })(Number.prototype.toLocaleString);
        var signo = "$";

        var top = parseFloat(objTBLGeneral2.AA_MONTO_PROM_VENT);
      

        var MontoDisponibleVentas = signo.concat(top .toLocaleString());

    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaOperacionesMesaDineroCopVenta[0].TOTAL_VENTA_USD != "") {




                $.each(objJson.ListaOperacionesMesaDineroCopVenta, function (key, value) {






                    if (value.TOTAL_VENTA_USD == 788) {


                        objMesaDinero = {

                            AA_TIPO_DIVISA: value.AA_TIPO_DIVISA,
                            TOTAL_VENTA_USD: value.TOTAL_VENTA_USD
                        }

                    }
                    else {

                        objMesaDinero = {

                            AA_TIPO_DIVISA: value.AA_TIPO_DIVISA,
                            TOTAL_VENTA_USD: value.TOTAL_VENTA_USD
                        }
                    }



                    var suma = 0;
                    var SumaVentas = 0;
                    var objeto = objJson.ListaOperacionesMesaDineroCopVenta;





                    if (objMesaDinero.AA_TIPO_DIVISA == 'V') {

                        $.each(objeto, function (key, value) {

                            if (value.TOTAL_VENTA_USD != null) {

                                var valor3 = value.TOTAL_VENTA_USD;
                                var valor2 = parseFloat((valor3).replace(',', '.'))
                                suma += valor2
                                SumaVentas += value["TOTAL_VENTA_USD"];
                              

                            }

                        });



                        var sumaTotalVentas = suma.toFixed(2);
                      


                        (function (old) {
                            var dec = 0.12 .toLocaleString().charAt(1),
                                tho = dec === "," ? "." : ".";

                            if (1000 .toLocaleString() !== "1,000.00") {
                                Number.prototype.toLocaleString = function () {
                                    var neg = this < 0,
                                        f = this.toFixed(2).slice(+neg);

                                    return (neg ? "-" : "")
                                        + f.slice(0, -3).replace(/(?=(?!^)(?:\d{3})+(?!\d))/g, tho)
                                        + dec + f.slice(-2);
                                }
                            }
                        })(Number.prototype.toLocaleString);


                        var signo = "$";
                        var suma33 = signo.concat(parseFloat(sumaTotalVentas) .toLocaleString());


                        $('#txtTotalVentasCOP').val(suma33);



                    }





                    cantt++;
                });
            }
        }
        else {

            mensajeErrorGeneral(objJson.Descripcion);

        }
    }

    for (i = 1; i <= cantt; i++) {

        $('#txtSeqNum' + i).prop('disabled', true);
    }
    cantidadMesa = cantt - 1;

}















var objMesaDinero;
function OperacionesMesaDinero(valorDocumento, valorTipoDoc) {



    var params = JSON.stringify({ valor: valorDocumento, valorTipoDoc: valorTipoDoc });
    var metodo = "Mesa_Dinero_Mes";
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
   
    var filasHtml = "";
    var filasHtmlExtendido = "";
    var cantt = 1;
    if (jQuery.isEmptyObject(objJson.ListaOperacionesMesaDinero)) {
        mensajeErrorGeneral("No se obtuvieron registros en mesa de dinero");

        $('#txtTotalComprasUSD').val("0");
        $('#txtTotalVentasUSD').val("0");


        //$("#" + tblListado).html(filasHtml);
    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaOperacionesMesaDinero[0].TOTAL_VENTA_USD != "") {





                $.each(objJson.ListaOperacionesMesaDinero, function (key, value) {



                    if (value.TOTAL_COMPR_USD == 788) {


                        objMesaDinero = {

                            AA_TIPO_DIVISA: value.AA_TIPO_DIVISA,
                            TOTAL_VENTA_USD: value.TOTAL_VENTA_USD,
                            TOTAL_COMPR_USD: "0.0"
                        }

                    }
                    else {

                        objMesaDinero = {

                            AA_TIPO_DIVISA: value.AA_TIPO_DIVISA,
                            TOTAL_VENTA_USD: value.TOTAL_VENTA_USD,
                            TOTAL_COMPR_USD: value.TOTAL_COMPR_USD
                        }
                    }





                    var suma = 0;
                    var SumaVentas = 0;
                    var objeto = objJson.ListaOperacionesMesaDinero;


                  


                    if (objMesaDinero.AA_TIPO_DIVISA == 'V' || objMesaDinero.TOTAL_COMPR_USD == "0.0") {

                        $.each(objeto, function (key, value) {

                            if (value.TOTAL_VENTA_USD != null) {

                                var valor3 = value.TOTAL_VENTA_USD;
                                var valor2 = parseFloat((valor3).replace(',', '.'))
                                suma += valor2
                                SumaVentas += value["TOTAL_VENTA_USD"];
                               

                            }

                        });



                        var sumaTotalVentas = suma.toFixed(2);
                     


                        (function (old) {
                            var dec = 0.12 .toLocaleString().charAt(1),
                                tho = dec === "," ? "." : ".";

                            if (1000 .toLocaleString() !== "1,000.00") {
                                Number.prototype.toLocaleString = function () {
                                    var neg = this < 0,
                                        f = this.toFixed(2).slice(+neg);

                                    return (neg ? "-" : "")
                                        + f.slice(0, -3).replace(/(?=(?!^)(?:\d{3})+(?!\d))/g, tho)
                                        + dec + f.slice(-2);
                                }
                            }
                        })(Number.prototype.toLocaleString);


                        var signo = "$";
                        var suma33 = signo.concat(parseFloat(sumaTotalVentas) .toLocaleString());

                        $('#txtTotalVentasUSD').val(suma33);
                        var valor4 = objTBLGeneral2.AA_MONTO_PROM_VENT.replace(/[$]/g, '');
                        var valor3 = valor4.replace('.', '');
                        var valor2 = parseFloat(valor4.replace(',', '.'))
                        var valor3 = objMesaDinero.TOTAL_VENTA_USD.replace(',', '.');
                        var valor22 = (valor2 - (valor2 % 100)) / 100;
                   

                        var valorRestado = valor2 - suma;
                        var valorRestado2 = signo.concat(valorRestado .toLocaleString());



                        $('#txtMontoDisponibleVentas').val(valorRestado2);
                        objTBLGeneral2.AA_MONTO_OPR = valorRestado2;


                    }




                    if (objMesaDinero.AA_TIPO_DIVISA == 'C' || objMesaDinero.TOTAL_COMPR_USD == "0.0") {

                        var TotalCompras1 = objMesaDinero.TOTAL_COMPR_USD;
                        var valor3 = TotalCompras1.replace(',', '.');
                        var totalc1 = parseFloat(valor3);
                        var signo = "$";
                        var TotalCompras = signo.concat(totalc1 .toLocaleString());
                        $('#txtTotalComprasUSD').val(TotalCompras);



                        var valor4 = objTBLGeneral2.AA_MONTO_PROMEDIO.replace(/[$]/g, '');
                        var valor38 = valor4.replace('.', '');
                        var valor2 = parseFloat(valor38);
                        var valor3 = objMesaDinero.TOTAL_COMPR_USD.replace(',', '.');
                        var valorRestado = valor2 - valor3
                        var valorRestado2 = signo.concat(valorRestado .toLocaleString());
                        $('#txtMontoDisponibleCompras').val(valorRestado2);




                        $.each(objeto, function (elemento, indice) {
                            if (indice.TOTAL_VENTA_USD != null) {
                                var valor3 = indice.TOTAL_VENTA_USD;
                                var valor2 = parseFloat((valor3).replace(',', '.'))
                                suma += valor2
                                SumaVentas += indice["TOTAL_VENTA_USD"];
                            }
                        });



                        var sumaTotalVentas = suma.toFixed(2);
                        var suma33 = signo.concat(parseFloat(sumaTotalVentas) .toLocaleString());
                        $('#txtTotalVentasUSD').val(suma33);

                        var valor4 = objTBLGeneral2.AA_MONTO_PROM_VENT.replace(/[$]/g, '');
                        var valor3 = valor4.replace('.', '');
                        var valor2 = parseFloat(valor4.replace('.', ''))
                        var valor22 = (valor2 - (valor2 % 100)) / 100;
                        var valorRestado = valor2 - suma;
                        var valorRestado2 = signo.concat(valorRestado .toLocaleString());


               
                        $('#txtMontoDisponibleVentas').val(valorRestado2);
                        objTBLGeneral2.AA_MONTO_OPR = valorRestado2;


                    }




                    cantt++;
                });
            }
        }
        else {

            mensajeErrorGeneral(objJson.Descripcion);

        }
    }

    for (i = 1; i <= cantt; i++) {

        $('#txtSeqNum' + i).prop('disabled', true);
    }
    cantidadMesa = cantt - 1;

}










var objMesaDinero;
function OperacionesMesaDineroVenta(valorDocumento, valorTipoDoc) {



    var params = JSON.stringify({ valor: valorDocumento, valorTipoDoc: valorTipoDoc });
    var metodo = "Mesa_Dinero_Mes_Venta";
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
  
    var filasHtml = "";
    var filasHtmlExtendido = "";
    var cantt = 1;
    if (jQuery.isEmptyObject(objJson.ListaOperacionesMesaDineroVenta)) {
        mensajeErrorGeneral("No se obtuvieron registros en mesa de dinero venta");

        $('#txtTotalVentasUSD').val("$0");

     



        (function (old) {
            var dec = 0.12 .toLocaleString().charAt(1),
                tho = dec === "," ? "." : ".";

            if (1000 .toLocaleString() !== "1,000.00") {
                Number.prototype.toLocaleString = function () {
                    var neg = this < 0,
                        f = this.toFixed(2).slice(+neg);
              
                    return (neg ? "-" : "")
                        + f.slice(0, -3).replace(/(?=(?!^)(?:\d{3})+(?!\d))/g, tho)
                        + dec + f.slice(-2);
                }
            }
        })(Number.prototype.toLocaleString);
        var signo = "$";

        var top = parseFloat(objTBLGeneral2.AA_MONTO_PROM_VENT);
  

        var MontoDisponibleVentas = signo.concat(top .toLocaleString());

  
        $('#txtMontoDisponibleVentas').val($('#txtTopeVenta').val());




       
    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaOperacionesMesaDineroVenta[0].TOTAL_VENTA_USD != "") {





                $.each(objJson.ListaOperacionesMesaDineroVenta, function (key, value) {

                  



                    if (value.TOTAL_COMPR_USD == 788) {


                        objMesaDinero = {

                            AA_TIPO_DIVISA: value.AA_TIPO_DIVISA,
                            TOTAL_VENTA_USD: value.TOTAL_VENTA_USD
                        }

                    }
                    else {

                        objMesaDinero = {

                            AA_TIPO_DIVISA: value.AA_TIPO_DIVISA,
                            TOTAL_VENTA_USD: value.TOTAL_VENTA_USD
                        }
                    }



                    var suma = 0;
                    var SumaVentas = 0;
                    var objeto = objJson.ListaOperacionesMesaDineroVenta;





                    if (objMesaDinero.AA_TIPO_DIVISA == 'V') {

                        $.each(objeto, function (key, value) {

                            if (value.TOTAL_VENTA_USD != null) {

                                var valor3 = value.TOTAL_VENTA_USD;
                                var valor2 = parseFloat((valor3).replace(',', '.'))
                                suma += valor2
                                SumaVentas += value["TOTAL_VENTA_USD"];
                          

                            }

                        });



                        var sumaTotalVentas = suma.toFixed(2);
                      

                        (function (old) {
                            var dec = 0.12 .toLocaleString().charAt(1),
                                tho = dec === "," ? "." : ".";

                            if (1000 .toLocaleString() !== "1,000.00") {
                                Number.prototype.toLocaleString = function () {
                                    var neg = this < 0,
                                        f = this.toFixed(2).slice(+neg);

                                    return (neg ? "-" : "")
                                        + f.slice(0, -3).replace(/(?=(?!^)(?:\d{3})+(?!\d))/g, tho)
                                        + dec + f.slice(-2);
                                }
                            }
                        })(Number.prototype.toLocaleString);


                        var signo = "$";
                        var suma33 = signo.concat(parseFloat(sumaTotalVentas) .toLocaleString());

                    
                        $('#txtTotalVentasUSD').val(suma33);
                        var valor4 = objTBLGeneral2.AA_MONTO_PROM_VENT.replace(/[$]/g, '');
                        var valor3 = valor4.replace('.', '');
                        var valor2 = parseFloat(valor4.replace(',', '.'))
                        var valor3 = objMesaDinero.TOTAL_VENTA_USD.replace(',', '.');
                        var valor22 = (valor2 - (valor2 % 100)) / 100;
                     

                        var valorRestado = valor2 - suma;
                        var valorRestado2 = signo.concat(valorRestado .toLocaleString());



                        $('#txtMontoDisponibleVentas').val(valorRestado2);
                        objTBLGeneral2.AA_MONTO_OPR = valorRestado2;


                    }






                    cantt++;
                });
            }
        }
        else {

            mensajeErrorGeneral(objJson.Descripcion);

        }
    }

    for (i = 1; i <= cantt; i++) {

        $('#txtSeqNum' + i).prop('disabled', true);
    }
    cantidadMesa = cantt - 1;

}











var objMesaDinero;
function OperacionesMesaDineroCompra(valorDocumento, valorTipoDoc) {



    var params = JSON.stringify({ valor: valorDocumento, valorTipoDoc: valorTipoDoc });
    var metodo = "Mesa_Dinero_Mes_Compra";
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
 
    var filasHtml = "";
    var filasHtmlExtendido = "";
    var cantt = 1;
    if (jQuery.isEmptyObject(objJson.ListaOperacionesMesaDineroCompra)) {
        mensajeErrorGeneral("No se obtuvieron registros en mesa de dinero compra");

        $('#txtTotalComprasUSD').val("$0");
     

        $('#txtMontoDisponibleCompras').val(objTBLGeneral2.AA_MONTO_PROMEDIO);

        
    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaOperacionesMesaDineroCompra[0].TOTAL_COMPR_USD != "") {





                $.each(objJson.ListaOperacionesMesaDineroCompra, function (key, value) {

                   



                    if (value.TOTAL_COMPR_USD == 788) {


                        objMesaDinero = {

                            AA_TIPO_DIVISA: value.AA_TIPO_DIVISA,
                            //TOTAL_VENTA_USD: value.TOTAL_VENTA_USD,
                            TOTAL_COMPR_USD: "0.0"
                        }

                    }
                    else {

                        objMesaDinero = {

                            AA_TIPO_DIVISA: value.AA_TIPO_DIVISA,
                            // TOTAL_VENTA_USD: value.TOTAL_VENTA_USD,
                            TOTAL_COMPR_USD: value.TOTAL_COMPR_USD
                        }
                    }





                    var suma = 0;
                    var SumaVentas = 0;
                    var objeto = objJson.ListaOperacionesMesaDinero;


                    if (objMesaDinero.AA_TIPO_DIVISA == 'C') {

                        var TotalCompras1 = objMesaDinero.TOTAL_COMPR_USD;
                        var valor3 = TotalCompras1.replace(',', '.');
                        var totalc1 = parseFloat(valor3);
                        var signo = "$";
                        var TotalCompras = signo.concat(totalc1 .toLocaleString());
                        $('#txtTotalComprasUSD').val(TotalCompras);



                        var valor4 = objTBLGeneral2.AA_MONTO_PROMEDIO.replace(/[$]/g, '');
                        var valor38 = valor4.replace('.', '');
                        var valor2 = parseFloat(valor38);
                        var valor3 = objMesaDinero.TOTAL_COMPR_USD.replace(',', '.');
                        var valorRestado = valor2 - valor3
                        var valorRestado2 = signo.concat(valorRestado .toLocaleString());
                        $('#txtMontoDisponibleCompras').val(valorRestado2);





                    }




                    cantt++;
                });
            }
        }
        else {

            mensajeErrorGeneral(objJson.Descripcion);

        }
    }

    for (i = 1; i <= cantt; i++) {

        $('#txtSeqNum' + i).prop('disabled', true);
    }
    cantidadMesa = cantt - 1;

}


















var objMesaDineroCOP;
function OperacionesMesaDineroCOP(valorDocumento, valorTipoDoc) {



    var params = JSON.stringify({ valor: valorDocumento, valorTipoDoc: valorTipoDoc });
    var metodo = "Mesa_Dinero_Mes_COP";

    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));
    //console.log(objJson);
    var filasHtml = "";
    var filasHtmlExtendido = "";
    var cantt = 1;
    if (jQuery.isEmptyObject(objJson.ListaOperacionesMesaDinero)) {
        mensajeErrorGeneral("No se obtuvieron registros en mesa de dinero");
        //$("#" + tblListado).html(filasHtml);
    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaOperacionesMesaDinero[0].TOTAL_VENTA_USD != "") {

                $.each(objJson.ListaOperacionesMesaDinero, function (key, value) {




                    if (value.TOTAL_VENTA_USD != null) {

                        var TOTAL_VENTA_USD_FORM = value.TOTAL_VENTA_USD.replace(',', '.');

                    }

                    if (value.TOTAL_COMPR_USD == null) {


                        objMesaDineroCOP = {

                            AA_TIPO_DIVISA: value.AA_TIPO_DIVISA,
                            TOTAL_VENTA_USD: value.TOTAL_VENTA_USD,
                            TOTAL_COMPR_USD: "0.0"
                        }

                    }
                    else {


                        objMesaDineroCOP = {

                            AA_TIPO_DIVISA: value.AA_TIPO_DIVISA,
                            TOTAL_VENTA_USD: value.TOTAL_VENTA_USD,
                            TOTAL_COMPR_USD: value.TOTAL_COMPR_USD
                        }
                    }





                    var suma = 0;
                    var SumaVentas = 0;
                    var objeto = objJson.ListaOperacionesMesaDinero;


                    if (objMesaDineroCOP.AA_TIPO_DIVISA == 'V' || objMesaDineroCOP.TOTAL_VENTA_USD == "0.0") {






                        $.each(objeto, function (elemento, indice) {


                            if (indice.TOTAL_VENTA_USD != null) {

                                var valor3 = indice.TOTAL_VENTA_USD;
                                var valor2 = parseFloat((valor3).replace(',', '.'))


                                suma += valor2
                                SumaVentas += indice["TOTAL_VENTA_USD"];


                            }

                        });









                        /*
                                                objeto.forEach(function (elemento, indice) {
                        
                        
                                                    if (elemento.TOTAL_VENTA_USD != null) {
                        
                                                        var valor3 = elemento.TOTAL_VENTA_USD;
                                                        var valor2 = parseFloat((valor3).replace(',', '.'))
                        
                        
                                                        suma += valor2
                                                        SumaVentas += elemento["TOTAL_VENTA_USD"];
                        
                        
                                                    }
                        
                                                });*/



                        var sumaTotalVentas = suma.toFixed(2);
                        var suma33 = parseFloat(sumaTotalVentas).toLocaleString('en-US', { style: 'currency', currency: 'USD', currencyDisplay: 'symbol' });




                        var TotalVentaCop = objMesaDineroCOP.TOTAL_VENTA_USD;
                        var TotalVenta = TotalVentaCop.replace(',', '.');

                        var totalc1 = parseFloat(TotalVenta);

                        var signo = "$";



                        var TotalVentaCo = signo.concat(totalc1 .toLocaleString());

                   

                        $('#txtTotalVentasCOP').val(TotalVentaCo);


                        var valor4 = objTBLGeneral2.AA_MONTO_PROM_VENT.replace(/[$]/g, '');
                        var valor3 = valor4.replace('.', '');
                        var valor2 = parseFloat(valor4.replace(',', '.'))
                        var valor3 = objMesaDineroCOP.TOTAL_VENTA_USD.replace(',', '.');





                        var valorRestado = valor2 - suma;
                        var valorRestado2 = valorRestado.toLocaleString('en-US', { style: 'currency', currency: 'USD', currencyDisplay: 'symbol' });
                     

                    }




                    if (objMesaDineroCOP.AA_TIPO_DIVISA == 'C' || objMesaDineroCOP.TOTAL_COMPR_USD == "0.0") {


                        var TotalCompras1 = objMesaDineroCOP.TOTAL_COMPR_USD;
                        var valor3 = TotalCompras1.replace(',', '.');

                        var totalc1 = parseFloat(valor3);

                        var signo = "$";



                        var TotalCompras = signo.concat(totalc1 .toLocaleString());

                        //$('#txtTotalComprasUSD').val(TotalCompras);

                        $('#txtTotalComprasCOP').val(TotalCompras);



                        var valor4 = objTBLGeneral2.AA_MONTO_PROMEDIO.replace(/[$]/g, '');



                        var valor38 = valor4.replace(',', '');



                        var valor2 = parseFloat(valor38).toFixed(2);



                        var valor3 = objMesaDineroCOP.TOTAL_COMPR_USD.replace(',', '.');
                      

                        var valorRestado = valor2 - valor3;


                        var valorRestado2 = signo.concat(valorRestado .toLocaleString());

                       

                    }





                    //TopeventaAutorizada(objTBLTopeVenta);

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
    cantidadMesa = cantt - 1;

}




















$('input.CurrencyInput').on('blur', function () {
    var value = this.value.replace(/,/g, '');
    this.value = parseFloat(value).toLocaleString('en-US', {
        style: 'decimal',
        maximumFractionDigits: 2,
        minimumFractionDigits: 2
    });
});


function OnSelectedIndexChange() {

    ValidarDivisasVigentes(objDivBuscar.NRODOCUMENTO);



    if (document.getElementById('dllOperacionesInt2').value == 'N') {

        $('#divResultadosDetalle').hide();

        $('#divPanelInformacion11').hide();

        $('#AprobacionVigenteDiv').hide();
        $('#AprobacionCargoDiv').hide();


        if (ObjDivisasVigentes.DIVISASVIGENTE == "SI") {


            bootbox.alert({
                message: "El cliente ya tiene aprobadas autorizaciones de divisas, por tanto no puede realizar esta modificación",
                className: 'rubberBand animated'
            });



            $("#dllOperacionesInt2").val("Y");

        }

    }

    if (document.getElementById('dllOperacionesInt2').value == 'Y') {

        $('#divResultadosDetalle').show();
        $('#divPanelInformacion11').show();
        $('#AprobacionVigenteDiv').show();
        $('#AprobacionCargoDiv').show();
    }







    /*

    if (objetoTable.cantidadVigentes>0 && document.getElementById('dllOperacionesInt2').value == 'N') {

        
        bootbox.alert("El cliente ya tiene aprobadas autorizaciones de divisas, por tanto no puede realizar esta modificación");

    }
    */

}




function ObtenerDescrPais(Pais) {


    var params = JSON.stringify({ valor: Pais });
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var metodo = "TraerDescripcionPais";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));


    var filasHtml = "";
    var cant = 1;
    if (jQuery.isEmptyObject(objJson.ListaOperacionesIntPN)) {


        objDescrPais = {

            DESCRPPAIS: " "



        }

    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaOperacionesIntPN[0].DESCRPAIS != "") {





                objDescrPais = {

                    DESCRPPAIS: objJson.ListaOperacionesIntPN[0].DESCRPAIS



                }



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





function ObtenerDescrMoneda(Pais) {


    var params = JSON.stringify({ valor: Pais });
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var metodo = "TraerDescripcionMoneda";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));


    
    var filasHtml = "";
    var cant = 1;
    if (jQuery.isEmptyObject(objJson.ListaOperacionesIntPN)) {


        objDescrMoneda = {

            DESCRMONEDA: " "



        }

    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaOperacionesIntPN[0].DESCRMONEDA != "") {





                objDescrMoneda = {

                    DESCRMONEDA: objJson.ListaOperacionesIntPN[0].DESCRMONEDA



                }



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



function Obtener_cargo_usuario(valorGeneral) {


    var params = JSON.stringify({ valor: valorGeneral });
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var metodo = "AV_ES_VALIDO_CUPO_PN_OPR_INT";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));


    var filasHtml = "";
    var cant = 1;
    if (jQuery.isEmptyObject(objJson.ListaValidacionRolesPersona)) {
        mensajeErrorGeneral("No se obtuvieron registros. L 4350");
        // $("#" + tblListadoOpInt).html(filasHtml);
    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaValidacionRolesPersona[0].ROLENAME != "") {
                $.each(objJson.ListaValidacionRolesPersona, function (key, value) {

                    objTBLValidarCupon = {
                        ROLENAME: value.ROLENAME

                    }
                    filasHtml += "<tr onclick='Actualizar(" + JSON.stringify(objTBLValidarCupon) + ")'>"; //Al hacer click sobre la fila se va a otro panel
                    filasHtml += "<td><a>" + cant + "</td>";


                    //Actualizar2(objTBLValidarCupon);
                    Autorizar_Monto(objTBLValidarCupon, 'tblResultadoBusqueda2')
                    // filasHtml += "<td><a>" + value.PartyID + "</td>";
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
    // $("#" + tblListadoOpInt).html(filasHtml);
    // $("#" + tblListadoOpInt).show();
    // $("#divResultadosBusqueda2").show(); //Encabezados tabla 1 y datos


}




function ObtenerUsuario(valorGeneral, TipoDocumento) {


    var params = JSON.stringify({ valor: valorGeneral, valor2: TipoDocumento });
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var metodo = "TraerUsuarioSesion";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));

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
    // $("#" + tblListadoOpInt).html(filasHtml);
    // $("#" + tblListadoOpInt).show();
    // $("#divResultadosBusqueda2").show(); //Encabezados tabla 1 y datos


}

/*
function formatCurrency(locales, currency, fractionDigits, number) {
    var formatted = new Intl.NumberFormat(locales, {
        style: 'currency',
        currency: currency,
        minimumFractionDigits: fractionDigits
    }).format(number);
    return formatted;
}*/




var objTBLEntidadNueva;
function ValidarDivisasVigentes(NumeroDocumento) {


    var params = JSON.stringify({ valor: NumeroDocumento });
    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var metodo = "ValidarDivisas";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));

    var filasHtml = "";
    var filasHtmlExtendido = "";
    var cantt = 1;
    if (jQuery.isEmptyObject(objJson.ListaOperacionesIntPN)) {
        //mensajeErrorGeneral("No se obtuvieron registros en Detalles.");
     


    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaOperacionesIntPN[0].LISTA_BLOQUEO != "") {

                $.each(objJson.ListaOperacionesIntPN, function (key, value) {

                    ObjDivisasVigentes = {
                        DIVISASVIGENTE: value.DIVISASVIGENTE

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

var objTBLEntidadNueva;
function ValidarListaBloqueo(NumeroDocumento) {


    var params = JSON.stringify({ valor: NumeroDocumento });
    var metodo = "ValidarBloqueo";

    var svc = "./Servicios/WS_DetalleAutorizaciones.asmx/";
    var objJson = JSON.parse(ajaxExecute(svc, metodo, "POST", params, false));

    var filasHtml = "";
    var filasHtmlExtendido = "";
    var cantt = 1;
    if (jQuery.isEmptyObject(objJson.ListaOperacionesIntPN)) {
        mensajeErrorGeneral("No se obtuvieron registros en lista de bloqueo.");
  


    } else {
        if (objJson.Codigo != 9999) {
            if (objJson.ListaOperacionesIntPN[0].LISTA_BLOQUEO != "") {

                $.each(objJson.ListaOperacionesIntPN, function (key, value) {

                    ObjListaBloqueo = {
                        LISTA_BLOQUEO: value.LISTA_BLOQUEO

                    }

                    var llamada = false;



                    //$("#dllOperacionesInt2").val(value.AA_DIVISAS);


                    if (ObjListaBloqueo.LISTA_BLOQUEO == "SI") {



                                             $('#btnNuevaFila').hide();


                       // $('#divResultadosDetalle').hide();
                        

                        $('#AprobacionVigenteDiv').hide();
                        $('#AprobacionCargoDiv').hide();


                        $('#dllOperacionesInt2').prop('disabled', true);
                        $("#dllOperacionesInt2").val("N");

                        objTBLGeneral2.AA_DIVISAS = 'N';

                        
                        filasHtml += "<div><img src='./style/cancelar.png' alt='' border=0  ></img><span style='font-weight:bold;'>Cliente en Lista de Bloqueo.</span></div>"; //Al hacer click sobre la fila se va a otro panel


                        $("#info_actualizada1").empty().append(filasHtml);






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




function filter_letters_and_symbols(evt) {

    var hold = String.fromCharCode(evt.which);

    if ((/[a-z A-Z*!@#$%^&*()_./[\]}=+><{?":;'"|]/.test(hold))) {

        evt.preventDefault();

    }
}





function checkDollarInput() {

    var value = parseFloat(dollars.value),
        valueInt = parseInt(value);

    if (dollars.value.match(/[^0-9.-]/)) {
        showMessageModal('please use only numbers', value);
        return false;
    }

    // check the max value
    if (value > 999.5) {
        showMessageModal('over max value', value);
        return false;
    }

    // check the min value
    if (value < 0.5) {
        showMessageModal('under min value', value);
        return false;
    }

    // ensure the correct decimal using modulo division remainer
    if (value % valueInt !== 0 && value % valueInt !== .5) {
        showMessageModal('needs to be .0 or .50', value);
        return false;
    }

  
    return true;
}






function DataTableEntidades() {



    $("#TablaEntidades").dataTable({

        "bDestroy": true,
        "processing": true,
        "filter": true,
        "lengthChange": true,
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
        },

        dom: 'Blfrtip',
        buttons: [
            {

                extend: 'collection',
                text: 'Exportar',
                buttons: [
                    { extend: 'copy', text: 'Copiar' },
                    { extend: 'excel', text: 'Excel' },
                    { extend: 'csv', text: 'CSV' },
                    { extend: 'pdf', text: 'Pdf' },
                    { extend: 'print', text: 'Imprimir' },



                ]
            }
        ],

        "lengthMenu": [[10, 20, 30, -1], [10, 20, 30, 'Todos']]
    });


}