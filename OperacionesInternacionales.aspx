<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OperacionesInternacionales.aspx.cs" Inherits="_Default" %>

<!-- #INCLUDE FILE="Home.asp" -->
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <!--<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE11" /> -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />




    <script src="Scripts/jquery.1.11.3.min.js"></script>
    <script src="Scripts/JSON2.js"></script>
    <script src="Scripts/bootbox.min.js"></script>
    <script src="Scripts/bootbox.locales.min.js"></script>
    <link href="Style/avvillas-divisas.css" rel="stylesheet" />
    <script src="Scripts/DetalleAutorizaciones.js" type="text/javascript"></script>
    <script src="Scripts/OperacionesInternacionales.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.16/datatables.min.js"></script>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/v/dt/dt-1.10.16/datatables.min.css" />
    <link href="Style/bootstrap.slim.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.min.css">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.5.2/css/buttons.dataTables.min.css">
    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/select/1.2.7/js/dataTables.select.min.js"></script>
    <script type="text/javascript" language="javascript" src=" https://cdn.datatables.net/buttons/1.5.4/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript" language="javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script type="text/javascript" language="javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/pdfmake.min.js"></script>
    <script type="text/javascript" language="javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.36/vfs_fonts.js"></script>
    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/buttons/1.5.4/js/buttons.html5.min.js"></script>
    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/buttons/1.5.4/js/buttons.print.min.js"></script>





    <title>DIVISAS</title>



    <style>
        #tblResultadoBusqueda tr:nth-child(even) {
            background-color: #f2f2f2;
        }


        #tblResultadoBusqueda tr:hover {
            background-color: #ddd;
            cursor: pointer;
        }
    </style>
    <style>
        body {
            font-family: Arial;
        }

        /* Style the tab */
        .tab {
            overflow: hidden;
            border: 1px solid #ccc;
            background-color: #f1f1f1;
        }

            /* Style the buttons inside the tab */
            .tab button {
                background-color: inherit;
                float: left;
                border: none;
                outline: none;
                cursor: pointer;
                padding: 14px 16px;
                transition: 0.3s;
                font-size: 17px;
            }

                /* Change background color of buttons on hover */
                .tab button:hover {
                    background-color: #ddd;
                }

                /* Create an active/current tablink class */
                .tab button.active {
                    background-color: #ccc;
                }

        /* Style the tab content */
        .tabcontent {
            display: none;
            padding: 6px 12px;
            border: 1px solid #ccc;
            border-top: none;
        }

        .form-control {
            width: auto;
            height: 30px !important;
            padding: 0 !important;
        }

        input {
            text-transform: uppercase;
        }

        .col-lg-12 {
            width: 100% !important;
        }

        .col-lg-6 {
            width: 100% !important;
            float: left;
            align-content: center
        }

        [disabled].form-control {
            background-color: white !important;
            /*color:darkblue!important;*/
        }

        .scroll350 {
            height: 550px !important;
        }
    </style>
</head>
<body style="width: 93%;">



    <div class="loader"></div>
    <div id="page">



        <div class="row" style="margin-top: 25px">
            <div class="col-lg-12" id="panelPrincipal">
                <div class="panel panel-primary CompExplore" style="border-color: red; border-color: #cccccc;">
                    <div class="panel-heading PanelAvvillas" style="background-color: red; border-color: #cccccc">
                        <h3 class="panel-title" style="color: #fff"><b>Operaciones Internacionales de Divisas</b></h3>
                    </div>

                    <!-- alertas -->
                    <div class="row col-lg-12">
                        <div class="idAlerta cssAlerta" role="alert" style="height: 30px">
                            <b class="alertasMsj"></b>
                        </div>
                    </div>
                    <div class="panel-body BodyPanelAvvillas">
                        <h4>
                            <b>CRITERIOS DE BUSQUEDA</b>
                        </h4>
                        <br />
                        <br />
                        <hr style="padding: 3px; margin: 0px" />
                        <center>


                            <table class="tableformulario">
                                <tr>
                                    <td style="width: 260px"><b>Tipo y Número de documento</b></td>
                                    <td>
                                        <select class="form-control SelectAvvillas" style="width: 80px!important;" id="dllTipoDocumentoB">
                                            <option value=""></option>
                                        </select>

                                        <input type="text" data-nombre="Numero de Documento" data-validador="identificacion" class="form-control inputAvvillas" name="campo_validacion" data-requerido="true" data-mensaje="El campo solo admite numeros" id="txtNumeroDocumento" placeholder="12345" required="required" />
                                    </td>
                                    <td rowspan="7">
                                        <img src="Style/logo-avvillas.png" width="180" height="140" /></td>

                                </tr>

                                <tr>

                                    <td><b>Primer Nombre</td>
                                    <td>
                                        <input type="text" data-nombre="Primer nombre" class="form-control inputAvvillas" id="txtPrimerNombre" name="campo_validacion" data-requerido="false" data-mensaje="El campo solo admite letras" data-validador="string" />
                                    </td>


                                </tr>

                                <tr>

                                    <td><b>Segundo Nombre</td>
                                    <td>
                                        <input type="text" data-nombre="Segundo Nombre" class="form-control inputAvvillas" name="campo_validacion" data-validador="string" data-mensaje="El campo solo admite letras" id="txtSegundoNombre" />
                                    </td>



                                </tr>


                                <tr>

                                    <td><b>Primer Apellido</td>
                                    <td>
                                        <input type="text" data-nombre="Primer apellido" class="form-control inputAvvillas" name="campo_validacion" data-requerido="false" data-validador="string" data-mensaje="El campo solo admite letras" id="txtPrimerApellido" />
                                    </td>


                                </tr>


                                <tr>


                                    <td><b>Segundo Apellido</td>
                                    <td>
                                        <input type="text" data-nombre="Segundo apellido" class="form-control inputAvvillas" name="campo_validacion" data-validador="string" data-mensaje="El campo solo admite letras" id="txtSegundoApellido" />
                                    </td>


                                </tr>




                                <tr>
                                    <td colspan="1"></td>
                                    <td colspan="5">
                                        <button class="btn btn-outline-secondary BtnAvvillas" id="btnBuscarCliente" style="margin: 15px 15px 15px 0; padding: 6px 12px" type="button"><b>Buscar</b></button>


                                        <!--<input value='Nuevo Registro' type='button' id="btnNuevoRegistro" class='btn btn-success BtnAvvillas' style="margin: 15px 15px 15px 0; padding: 6px 12px" />  -->
                                    </td>
                                </tr>

                            </table>
                        </center>
                    </div>
                </div>
            </div>
        </div>





        <!--Tabla de busqueda-->
        <div class="row" style="padding: 15px;" id="divResultadosBusqueda">

            <div class="col-lg-6 scroll250" id="divResultadosBusqueda2">
                <a name="arriba"></a>
                <div class="row col-lg-12">
                    <div class="idAlerta cssAlerta" role="alert" style="height: 30px">
                        <b class="alertasMsj"></b>
                    </div>
                </div>
                <table class="table">
                    <thead>
                        <tr>
                            <th scope="col">#</th>
                            <th scope="col">TIPO Y NUMERO DE DOCUMENTO</th>
                            <th scope="col">NOMBRES Y APELLIDOS</th>
                            <th scope="col">ESTADO</th>


                        </tr>
                    </thead>
                    <tbody id="tblResultadoBusqueda">
                    </tbody>
                </table>
            </div>

            <%--INFORMACION--%>

            <div class="col-lg-6" id="divPanelInscripcion">
                <div class="panel panel-primary CompExplore">
                    <div class="panel-heading PanelAvvillas">
                        <h3 class="panel-title" style="color: #fff"><b>Inscribir Cliente </b></h3>
                    </div>
                    <!-- alertas -->

                    <div class="panel-body BodyPanelAvvillas" style="min-width: 0!important" id="panelInscripcion">
                        <table class="table" style="margin-left: 0px; margin-right: 0px;">

                            <tbody>
                                <tr>
                                    <td colspan="4">
                                        <h4>
                                            <center><b>MODULO DE INSCRIPCIÓN</b></center>
                                        </h4>
                                    </td>
                                </tr>


                                <tr>
                                    <td><b>TIPO DOCUMENTO:</b></td>
                                    <td>
                                        <select class="form-control SelectAvvillas width75" id="dllTipoDocumentoNuevo">
                                        </select>
                                    </td>
                                    <td><b>NUMERO DOCUMENTO:</b></td>
                                    <td>
                                        <input class="form-control inputAvvillas" type="text" data-nombre="Numero de documento" name="campo_Registro" data-requerido="true" data-validador="int" data-mensaje="El campo solo admite números" id="txtDocumentoNuevo" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <input value='BUSCAR CLIENTE' type='button' id="btnBuscarClienteInscripcion" class='btn btn-outline-secondary BtnAvvillas' style="float: left" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>







            <div class="col-lg-12" id="divPanelInformacion">
                <div class="panel panel-primary CompExplore">
                    <div class="panel-heading PanelAvvillas">
                        <h3 class="panel-title" style="color: #fff"><b>Detalles Cliente</b></h3>

                    </div>
                    <!-- alertas -->

                    <div class="panel-body BodyPanelAvvillas" style="min-width: 0!important" id="panelInformacion">

                        <!-- TABLA #01-->
                        <table class="table" style="margin-left: 0px; margin-right: 0px;">
                            <tbody>


                                <div id="tipo_cliente">
                                </div>

                                <h4>Alertas Cliente</h4>

                                <div id="info_actualizada1" style="background-color: #eeeeee; border-color: #ffeeba; color: #000000; border-radius: 10px">
                                </div>
                                <div id="info_actualizada" style="background-color: #eeeeee; border-color: #ffeeba; color: #000000; border-radius: 10px">
                                </div>

                                <tr>
                                    <td><b>TIPO DOCUMENTO:</b></td>
                                    <td>
                                        <select class="form-control SelectAvvillas width75" id="dllTipoDocumentoU">
                                        </select>

                                    </td>
                                    <td><b>NUMERO DOCUMENTO:</b></td>
                                    <td>
                                        <input class="form-control inputAvvillas" type="text" data-nombre="Numero de documento" name="campo_Edicion" data-validador="int" data-mensaje="El campo solo admite números" id="txtDocumentoU" />
                                    </td>
                                </tr>

                                <tr>

                                    <td><b>RAZON SOCIAL:</b></td>
                                    <td>
                                        <input class="form-control inputAvvillas" type="text" data-nombre="Primer nombre" name="campo_Edicion" data-validador="string" data-mensaje="El campo solo admite letras" id="txtRazonSocial" />

                                    </td>


                                    <td><b>PRIMER NOMBRE:</b></td>
                                    <td>
                                        <input class="form-control inputAvvillas" type="text" data-nombre="Primer nombre" name="campo_Edicion" data-validador="string" data-mensaje="El campo solo admite letras" id="txtPrimerNombreU" />

                                    </td>

                                    <td><b>SEGUNDO NOMBRE:</b></td>
                                    <td>
                                        <input class="form-control inputAvvillas" type="text" data-nombre="Segundo nombre" name="campo_Edicion" data-validador="string" data-mensaje="El campo solo admite letras" id="txtSegundoNombreU" /></td>
                                </tr>

                                <tr>
                                    <td><b>PRIMER APELLIDO:</b></td>
                                    <td>
                                        <input class="form-control inputAvvillas" type="text" data-nombre="Primer apellido" name="campo_Edicion" data-validador="string" data-mensaje="El campo solo admite letras" id="txtPrimerApellidoU" /></td>

                                    <td><b>SEGUNDO APELLIDO:</b></td>
                                    <td>
                                        <input class="form-control inputAvvillas" type="text" data-nombre="Segundo apellido" name="campo_Edicion" data-validador="string" data-mensaje="El campo solo admite letras" id="txtSegundoApellidoU" /></td>
                                </tr>

                                <tr>
                                    <td><b>SEGMENTO:</b></td>
                                    <td>
                                        <input class="form-control inputAvvillas" type="text" data-nombre="Segmento" name="campo_Edicion" data-validador="string" data-mensaje="El campo solo admite letras" id="txtSegmentoU" /></td>
                                    <td></td>
                                    <td></td>
                                </tr>


                                <tr>
                                    <td colspan="4">
                                        <input value='INSCRIBIR' type='button' id="btnInscribir" class='btn btn-success BtnAvvillas' style="float: left" />
                                    </td>
                                </tr>

                            </tbody>

                        </table>
                        <!--FIN TABLA #01-->


                        <div class="container-fluid">
                            <!-- Trigger the modal with a button -->

                            <!--<button type="button" class="btn btn-info btn-sm pull-right" data-toggle="modal" data-target="#myModal"  id="BotonEntidades">Seleccionar</button>  <br>  <br> -->


                            <!-- Modal -->

                        </div>



                        <!-- TABLA MONEDAS -->

                        <div class="container-fluid">
                            <!-- Trigger the modal with a button -->

                            <!--<button type="button" class="btn btn-info btn-sm pull-right" data-toggle="modal" data-target="#myModal"  id="BotonEntidades">Seleccionar</button>  <br>  <br> -->


                            <!-- Modal -->

                        </div>






                        <!-- Trigger the modal with a button -->

                        <!--<button type="button" class="btn btn-info btn-sm pull-right" data-toggle="modal" data-target="#myModal"  id="BotonEntidades">Seleccionar</button>  <br>  <br> -->














                    </div>





















                    <!--AQUI PARA ABAJO LOSP ANELES-->
                </div>







                <!-- Modal -->








                <!-- PANEL #02-->
                <div id="divPanelInformacion2">
                    <div class="panel panel-primary ">
                        <div class="panel-body BodyPanelAvvillas" style="min-width: 0!important" id="panelInformacion2">


                            <!-- TABLA #02-->
                            <table class="table" style="margin-left: 0px; margin-right: 0px;" id="tabla1">
                                <tbody>

                                    <tr>
                                        <td><b>Efectua Operaciones internacionales?</b></td>
                                        <td>


                                            <select name="cars" class="form-control SelectAvvillas width30" id="dllOperacionesInt2" onchange="OnSelectedIndexChange()">
                                                <option value="Y">SI</option>
                                                <option value="N">NO</option>

                                            </select>
                                        </td>

                                    </tr>



                                    <tr id="AprobacionVigenteDiv">
                                        <td><b>¿Aprobacion Vigente?</b></td>
                                        <td>

                                            <input type="text" class="form-control SelectAvvillas" id="AprobacionVigente" disabled="disabled" />
                                        </td>
                                    </tr>

                                    <tr id="AprobacionCargoDiv">
                                        <td><b>Cargo que Aprobó</b></td>
                                        <td>
                                            <input type="text" class="form-control SelectAvvillas" id="txtImportanci" size="25" disabled="disabled"/>
                                        </td>
                                    </tr>

                                </tbody>
                            </table>
                            <!--FIN TABLA #02-->
                        </div>
                    </div>
                </div>
                <!-- FIN PANEL #02-->













                <!--PANEL #03  ENTIDADES-->
                <div id="divPanelInformacion11">
                    <div class="panel panel-primary CompExplore">


                        <div class="panel-heading PanelAvvillas">
                            <h3 class="panel-title" style="color: #fff"><b>Operaciones Internacionales</b></h3>
                        </div>


                        <div class="panel-body BodyPanelAvvillas" style="min-width: 0!important" id="panelInformacion">
                            <div class="scroll15">





                                <table class="table" style="margin-left: 0px; margin-right: 0px;">

                                    <tbody>


                                        <tr>
                                            <td colspan="4">


                                                <div id="PrincipalEntidades" class="tabcontent" style="display: block">
                                                    <table id="TablaEntidades" class="table stripe">

                                                        <thead>
                                                            <tr>
                                                                <th scope="col">#</th>
                                                                <th scope="col">Código Entidad</th>
                                                                <th scope="col">Nombre</th>
                                                                <th scope="col">Eliminar</th>

                                                            </tr>
                                                        </thead>


                                                        <tbody id="tblEntidadesCliente">
                                                        </tbody>
                                                    </table>
                                                </div>




                                            </td>
                                        </tr>



                                        <tr>
                                            <td colspan="3">
                                                <!--      <input value='GUARDAR' type='button' id="btnGuardar" class='btn btn-success BtnAvvillas' style="float: left" />  -->
                                                <input value='Insertar Entidad' style="margin-left: 30px; float: left; font-weight: bold; text-transform: uppercase;" type='button' id="btnNuevaFilaEntidad" class='btn btn-primary BtnAvvillas' />

                                            </td>
                                        </tr>

                                    </tbody>
                                </table>




                                <div id="checkbox-value"></div>

                                <h4></h4>





                                <hr size="4px" width="100%" align="right" color="blue" />


                                <h3>Tipo de operación</h3>
                                <br />
                                <div class="col-lg-12 row">




                                    <div class="column">
                                        <label>
                                            <input type="checkbox" class="type_checkbox" name="acceptRules" id="txtImportancion" onclick="$(this).attr('value', this.checked?  'Y' : 'N' )" />
                                            Importaciones</label>
                                    </div>

                                    <div class="column">
                                        <label>
                                            <input type="checkbox" name="acceptRules" id="txtExportacion" onclick="$(this).attr('value', this.checked?  'Y' : 'N' )" />Exportaciones</label>
                                    </div>

                                    <div class="column">
                                        <label>
                                            <input type="checkbox" name="acceptRules" id="txtInversion" onclick="$(this).attr('value', this.checked?  'Y' : 'N' )" />Inversiones</label>
                                    </div>

                                    <div class="column">
                                        <label>
                                            <input type="checkbox" name="acceptRules" id="txtPrestamos" onclick="$(this).attr('value', this.checked?  'Y' : 'N' )" />Prestamos</label>
                                    </div>

                                    <div class="column">
                                        <label>
                                            <input type="checkbox" name="acceptRules" id="txtGiros" onclick="$(this).attr('value', this.checked?  'Y' : 'N' )" />Recepcion de giros</label>
                                    </div>

                                    <div class="column">
                                        <label>
                                            <input type="checkbox" name="acceptRules" id="txtEnvioGiros" onclick="$(this).attr('value', this.checked?  'Y' : 'N' )" />Envio de Giros</label>
                                    </div>

                                    <div class="column">
                                        <label>
                                            <input type="checkbox" name="acceptRules" id="txtServicios" onclick="$(this).attr('value', this.checked?  'Y' : 'N' )" />Pago de servicios</label>
                                    </div>



                                </div>











                                <!---  INICIO PANEL ENTIDADES -->






                                <!---  FIN PANEL ENTIDADES -->




                            </div>

                        </div>




                        <!--  INICIO -->
                        <hr size="4px" width="100%" align="right" color="blue" />
                        <div>
                            <div>


                                <div id="text2">
                                    <label style="font-weight: bold">Tiene Productos en el exterior?: </label>
                                    <input class="type_checkbox" type="checkbox" name="choice-animals" id="choice" onclick="$(this).attr('value', this.checked?  'Y' : 'N' ); myFunction();" />
                                </div>


                                <div id="text" style="display: none">





                                    <div class="block">
                                        <label style="font-weight: bold">País producto en el Exterior:</label>
                                        <input class="require-if-active hometown" data-require-pair="#choice-animals-dogs" id="txtPais" name="which-dog" size="1" />
                                        <input type='image' style="width: 20px; height: 16px;" src='./style/buscar.png' data-toggle='modal' data-target='#myModal2' id='BotonPaises' />
                                        <input class="require-if-active hometown" data-require-pair="#choice-animals-dogs" readonly="readonly" id="txtPaisDescr" name="which-dog" size="30" />
                                    </div>
                                    <div class="block">
                                        <label style="font-weight: bold">Ciudad Producto en el Exterior:</label>
                                        <input type="text" class="require-if-active hometown" data-require-pair="#choice-animals-dogs" name="which-dog" id="txtCiudad" />
                                    </div>

                                    <div class="block">
                                        <label style="font-weight: bold">Entidad Producto en el Exterior:</label>
                                        <input class="require-if-active hometown" data-require-pair="#choice-animals-dogs" id="txtEntidad" name="which-dog" size="50" />
                                    </div>


                                    <div class="block">
                                        <label style="font-weight: bold">Tipo de Producto en el Exterior:</label>
                                        <!--<input class="require-if-active hometown" data-require-pair="#choice-animals-dogs" id="txtTipoCuenta" name="which-dog" />  -->

                                        <select name="TipoProducto" class="require-if-active  hometown width30" id="txtTipoCuenta" onchange="OnSelectedIndexChange()" data-require-pair="#choice-animals-dogs" style="border-radius: 4px; border: 1px solid #ccc;">
                                            <option value=" "></option>
                                            <option value="05">Créditos</option>
                                            <option value="02">Cuenta Corriente</option>

                                            <option value="01">Cuenta de Ahorros</option>

                                            <option value="04">Inversiones</option>

                                            <option value="03">Otra</option>


                                        </select>

                                    </div>


                                    <div class="block">
                                        <label style="font-weight: bold">Tipo de Moneda producto en el Exterior:</label>
                                        <input class="require-if-active hometown" data-require-pair="#choice-animals-dogs" id="txtTipoMoneda" name="which-dog" size="1" />
                                        <input type='image' src='./style/buscar.png' style="width: 20px; height: 16px;" data-toggle='modal' data-target='#myModal3' id='BotonMonedas' />
                                        <input class="require-if-active hometown" data-require-pair="#choice-animals-dogs" readonly="readonly" id="txtMonedaDescr" name="which-dog" size="30" />
                                    </div>

                                    <div class="block">
                                        <label style="font-weight: bold">Número de producto en el Exterior:</label>
                                        <input class="require-if-active hometown" type="number" data-require-pair="#choice-animals-dogs" id="txtNroProducto" name="which-dog" />
                                    </div>


                                    <div class="block">
                                        <label style="font-weight: bold">Saldo producto en el Exterior:</label>

                                        <input class="require-if-active hometown" id="txtSaldoExt" />

                                        <!--<input class="require-if-active hometown" onkeypress="return IsNumeric(event);" data-require-pair="#choice-animals-dogs" id="txtSaldoExt" name="which-dog" type='text' step='0.0' />-->
                                    </div>


                                    <span id="error" style="color: Red; display: none">*Debes introducir un valor número válido. Ejemplo: 1200,20</span><br />



                                </div>







                            </div>
                        </div>

                        <!-- FIN-->



                        <!--FIN TABLA #03-->

                    </div>
                </div>
                <!--FIN PANEL #03  -->







                <!--Tabla de DETALLE DE AUTORIZACIONES-->
                <div  id="divResultadosDetalle">
                    <div>
                        <div class="panel panel-primary CompExplore">

                            <div class="panel-heading PanelAvvillas">
                                <h3 class="panel-title" style="color: #fff"><b>Detalle Autorizaciones</b></h3>
                            </div>


                            <div class="panel-body BodyPanelAvvillas" style="min-width: 0!important">

                                <!-- INICIO TABLA  #04 -->
                                <table class="table" style="margin-left: 0px; margin-right: 0px;">

                                    <tbody>


                                        <tr>
                                            <td colspan="4">
                                                <div class="tab" id="botonesExtendidos">
                                                    <button class="tablinks" id="mienlace" onclick="DetallesDivisas('Principal3')">Principal</button>
                                                    <button class="tablinks" onclick="DetallesDivisas('Extendido')">Extendido</button>
                                                </div>






                                                <div id="Principal3" class="tabcontent" style="display: block">
                                                    <table id="tableColor" class="table stripe">

                                                        <thead>
                                                            <tr>
                                                                <th scope="col">#</th>
                                                                <th scope="col">N° Sec</th>

                                                                <th scope="col">Aprobación</th>
                                                                <th scope="col">*Tipo Vigencia Divisa</th>
                                                                <th scope="col">Soportes para Divisas</th>
                                                                <th scope="col">Fecha Final</th>
                                                                <th scope="col">Tope Venta</th>
                                                                <th scope="col">Tope Compra</th>
                                                                <th scope="col">Estado</th>
                                                                <th scope="col">Fecha/Hora Introducción</th>
                                                                <th scope="col">Eliminar</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="tblResultadoBusquedaDetalle">
                                                        </tbody>
                                                    </table>


                                                </div>



                                                <div id="Extendido" class="tabcontent">
                                                    <table id="extendido" class="table">
                                                        <thead>
                                                            <tr>
                                                                <th scope="col">N° Sec</th>
                                                                <th scope="col">Añadido Por</th>
                                                                <th scope="col">Ultima Modificación</th>
                                                                <th scope="col">Último Mantenimiento Por</th>


                                                            </tr>
                                                        </thead>
                                                        <tbody id="tblResultadoBusquedaDetalleExtendido">
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td colspan="3">
                                                <input value='GUARDAR' type='button' id="btnGuardar" class='btn btn-success BtnAvvillas' style="float: left; font-weight: bold;" />
                                                <input value='Insertar Aprobación' style="margin-left: 30px; float: left; font-weight: bold;" type='button' id="btnNuevaFila" class='btn btn-primary ' />

                                            </td>
                                        </tr>

                                    </tbody>
                                </table>
                                <!-- FIN TABLA  #04 -->








                            </div>
                        </div>
                    </div>


                </div>





                <!--Tabla DETALLE DE AUTORIZACIONES-->
                <div id="divResultadosDetalle2 container">
                    <div >
                        <div class="panel panel-primary CompExplore">

                            <div class="panel-heading PanelAvvillas">
                                <h3 class="panel-title" style="color: #fff"><b>Detalle Operaciones Internacionales</b></h3>
                            </div>


                            <div class="panel-body BodyPanelAvvillas" style="min-width: 0!important">

                                <!-- INICIO TABLA  #04 -->
                                <table class="table" style="margin-left: 0px; margin-right: 0px;">

                                    <tbody>


                                        <tr>
                                            <td colspan="4">


                                                <div id="Principal2" class="tabcontent" style="display: block">
                                                    <table id="TablaMesaDinero" class="table stripe">

                                                        <thead>
                                                            <tr>
                                                                <th scope="col">#</th>
                                                                <th scope="col">Tipo Operación</th>
                                                                <th scope="col">Fecha Operación</th>
                                                                <th scope="col">Oficina</th>
                                                                <th scope="col">Monto Operación</th>
                                                                <th scope="col">Moneda</th>
                                                                <th scope="col"></th>
                                                                <th scope="col">Tasa de Cambio</th>
                                                                <th scope="col">Monto Calculado</th>
                                                            </tr>


                                                        </thead>
                                                        <tbody id="tableOperaciones">
                                                        </tbody>
                                                    </table>
                                                </div>




                                            </td>
                                        </tr>





                                    </tbody>
                                </table>
                                <!-- FIN TABLA  #04 -->

                            </div>
                        </div>
                    </div>





                </div>








                <div  id="divPanelInformacion">
                    <div class="panel panel-primary CompExplore">

                        <div class="panel-body BodyPanelAvvillas" style="min-width: 0!important" id="panelInformacion">
                        </div>




                        <!-- INICIO TABLA #05 -->
                        <table class="table" style="margin-left: 0px; margin-right: 0px;">

                            <tbody>

                                <tr>
                                    <td><b><font color="#3369FF">VENTA USD(DOLAR AMERICANO)</font></b></td>
                                    <td></td>

                                    <td><b><font color="#3369FF">COMPRA USD(DOLAR AMERICANO):</font></b></td>

                                </tr>

                                <tr>
                                    <td><b>Tope:</b></td>
                                    <td>
                                        <input class="form-control inputAvvillas" type="text" data-nombre="Segundo nombre" name="campo_Edicion" data-validador="string" data-mensaje="El campo solo admite letras" id="txtTopeVenta" disabled="disabled" />


                                    </td>

                                    <td><b>Tope:</b></td>
                                    <td>
                                        <input class="form-control inputAvvillas" type="text" data-nombre="Segundo nombre" name="campo_Edicion" data-validador="string" data-mensaje="El campo solo admite letras" id="txtTopeCompra" disabled="disabled" /></td>
                                </tr>



                                <tr>
                                    <td colspan="4" bgcolor="D5DAFF"><font color="#000000"><strong>Información Totalizada Mes Actual<strong></td>
                                </tr>


                                <tr>
                                    <td><b>Total Ventas USD:</b></td>
                                    <td>
                                        <input class="form-control inputAvvillas" type="text" data-nombre="Primer apellido" name="campo_Edicion" data-validador="string" data-mensaje="El campo solo admite letras" id="txtTotalVentasUSD" disabled="disabled" /></td>

                                    <td><b>Total Compras USD:</b></td>
                                    <td>
                                        <input class="form-control inputAvvillas" type="text" data-nombre="Segundo apellido" name="campo_Edicion" data-validador="string" data-mensaje="El campo solo admite letras" id="txtTotalComprasUSD" disabled="disabled" /></td>
                                </tr>


                                <tr>
                                    <td><b>Total Ventas COP:</b></td>
                                    <td>
                                        <input class="form-control inputAvvillas" type="text" data-nombre="Primer apellido" name="campo_Edicion" data-validador="string" data-mensaje="El campo solo admite letras" id="txtTotalVentasCOP" disabled="disabled" /></td>

                                    <td><b>Total Compras COP:</b></td>
                                    <td>
                                        <input class="form-control inputAvvillas" type="text" data-nombre="Segundo apellido" name="campo_Edicion" data-validador="string" data-mensaje="El campo solo admite letras" id="txtTotalComprasCOP" disabled="disabled" /></td>
                                </tr>

                                <tr>
                                    <td colspan="4" bgcolor="D5DAFF"><font color="#000000"><strong>Información Negociación<strong></td>
                                </tr>


                                <tr>
                                    <td><b>Monto disponible Ventas:</b></td>
                                    <td>
                                        <input class="form-control inputAvvillas" type="text" data-nombre="Primer apellido" name="campo_Edicion" data-validador="string" data-mensaje="El campo solo admite letras" id="txtMontoDisponibleVentas" disabled="disabled" /></td>

                                    <td><b>Monto disponible Compras:</b></td>
                                    <td>
                                        <input class="form-control inputAvvillas" type="text" data-nombre="Segundo apellido" name="campo_Edicion" data-validador="string" data-mensaje="El campo solo admite letras" id="txtMontoDisponibleCompras" disabled="disabled" /></td>
                                </tr>


                                <tr>
                                    <td><b>Última Modificación:</b></td>
                                    <td>
                                        <input class="form-control inputAvvillas" type="text" data-nombre="Primer apellido" name="campo_Edicion" data-validador="string" data-mensaje="El campo solo admite letras" id="txtUltimaModif" disabled="disabled" /></td>


                                </tr>






                            </tbody>
                        </table>




                    </div>


                </div>



                <table class="table" style="margin-left: 0px; margin-right: 0px;">






                    <tbody>




                        <tr>
                            <td style="width: 150px;">
                                <input value='GUARDAR' type='button' id="btnGuardarForm" class='btn btn-success BtnAvvillas' style="float: left; font-weight: bold;" />

                            </td>

                            <td colspan="4">
                                <input value='IR A BUSCAR' type='button' class='btn btn-warning BtnAvvillas' onclick="location.reload();" style="float: left; font-weight: bold;" />

                            </td>
                        </tr>




                    </tbody>

                </table>

            </div>









        </div>

























    </div>












    <div id="myModal2" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Consultar País producto en el Exterior</h4>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">


                        <table class="table stripe" id="paises">

                            <thead>

                                <tr>
                                    <th>#</th>
                                    <th>Páis</th>
                                    <th>Descripción</th>

                                </tr>

                            </thead>

                            <tbody id="tblResultadoPaises">
                            </tbody>

                        </table>


                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                </div>
            </div>

        </div>
    </div>






    <div id="myModal3" class="modal" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Consultar Tipo de Moneda producto en el Exterior</h4>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">


                        <table class="table stripe" id="monedas">

                            <thead>

                                <tr>
                                    <th>#</th>
                                    <th>Código Moneda</th>
                                    <th>Descripción</th>

                                </tr>

                            </thead>

                            <tbody id="tblResultadoMonedas">
                            </tbody>

                        </table>


                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                </div>
            </div>

        </div>
    </div>






    <div id="myModal" class="modal" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Entidades</h4>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">


                        <table class="table" id="example">

                            <thead>

                                <tr>
                                    <th>#</th>
                                    <th>Código Entidad</th>
                                    <th>Entidad Financiera</th>

                                </tr>

                            </thead>

                            <tbody id="tblResultadoEntidades">
                            </tbody>

                        </table>


                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>

    <script>

        function openCity2(tabName) {
            if (tabName == "Principal") {
                $("#Principal").show();
                $("#Extendido").hide();
            } else {
                $("#Principal").hide();
                $("#Extendido").show();
            }
        }

        function DetallesDivisas(tabName) {
            if (tabName == "Principal3") {
                $("#Principal3").show();
                $("#Extendido").hide();
            } else {
                $("#Principal3").hide();
                $("#Extendido").show();
            }
        }


        $(document).ready(function () {




            document.getElementById("txtNroProducto").readOnly = false;


            $("#myInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#tblEntidadesCliente tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });



        });

        $('input.type_checkbox[value="Y"]').prop('checked', true);


        var variable = document.getElementById("choice").value;



        var FormStuff = {

            init: function () {
                this.applyConditionalRequired();
                this.bindUIActions();
            },

            bindUIActions: function () {
                $("input[type='radio'], input[type='checkbox']").on("change", this.applyConditionalRequired);
            },

            applyConditionalRequired: function () {

                $(".require-if-active").each(function () {
                    var el = $(this);
                    if ($(el.data("require-pair")).is(":checked")) {
                        el.prop("required", true);
                    } else {
                        el.prop("required", false);
                    }
                });

            }

        };

        FormStuff.init();



        $(window).on('load', function () {


            $(".loader").fadeOut("slow");






        });






        function openCity(evt, tabName) {
            var i, tabcontent, tablinks;
            tabcontent = document.getElementsByClassName("tabcontent");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }
            tablinks = document.getElementsByClassName("tablinks");
            for (i = 0; i < tablinks.length; i++) {
                tablinks[i].className = tablinks[i].className.replace(" active", "");
            }
            document.getElementById(tabName).style.display = "block";
            evt.currentTarget.className += " active";
        }



        function IsNumeric(e) {
            var IsValidationSuccessful = false;

            switch (e.key) {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                case "Backspace":
                    IsValidationSuccessful = true;
                    break;

                case "Decimal":  //Numpad Decimal in Edge Browser
                case ".":
                case ",":        //Numpad Decimal in Chrome and Firefox                      
                //Numpad Decimal in Chrome and Firefox
                case "Del": 			// Internet Explorer 11 and less Numpad Decimal 
                    if (e.target.value.indexOf(".") >= 1) //Checking if already Decimal exists
                    {
                        IsValidationSuccessful = false;
                    }
                    else {
                        IsValidationSuccessful = true;
                    }
                    break;

                default:
                    IsValidationSuccessful = false;
            }
            //debugger;
            if (IsValidationSuccessful == false) {

                document.getElementById("error").style = "display:Block";
            } else {
                document.getElementById("error").style = "display:none";
            }

            return IsValidationSuccessful;
        }


        function myFunction() {
            var checkBox = document.getElementById("choice");
            var text = document.getElementById("text");
            if (checkBox.checked == true) {
                text.style.display = "block";
            } else {
                text.style.display = "none";
            }
        }



        $('#txtTopeVenta').keypress(function (event) {
            var code = (event.keyCode ? event.keyCode : event.which);
            if ($(this).val().indexOf('$') === -1) {
                document.getElementById("txtTopeVenta").value = "$" + $(this).val();
            } else {
                document.getElementById("txtTopeVenta").value = $(this).val();
            }
        });



        $(document).ready(function () {
            $("#error").hide();
            $("#txtSaldoExt").on("change", function () {
                var nameSub = $('#txtSaldoExt').val();
                if (/^(?=.*?\d)^\$?(([1-9]\d{0,2}(.\d{3})*)|\d+)?(\,\d{1,2})?$/.test(nameSub)) {
                    $("#error").hide();

                }
                else {
                    $("#error").show();
                    document.getElementById("txtSaldoExt").value = '';

                }
            });
        });



    </script>

</body>
</html>
