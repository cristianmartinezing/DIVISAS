using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for WS_ParametroGeneral
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class WS_DetalleAutorizaciones : System.Web.Services.WebService
{

    public WS_DetalleAutorizaciones()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }


    //Metodo para traer datos maestros
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string TraerParametroMaestro(ParamaetroOpDivisas valor)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        ParametroOp lista = new ParametroOp();
        ParametrosDivisas lista2 = new ParametrosDivisas();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();

        try
        {

            lst = lista.ObtenerParametros(valor);
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }






    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string TraerOperacionesInter(string valor, string valor2)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.ObtenerOperacionesInter(valor, valor2);
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }






    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string entidadesFinancieras()
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.entidadesFinancieras();
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }




    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Venta_usd_tope(string valor, string valorTipoDoc)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.tope_venta_usd(valor, valorTipoDoc);
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Av_info_actualizada(DateTime valor)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.info_actualizado(valor);
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }





    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string TraerCargosAutorizan(string valor)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.ObtenerCargosAutorizan(valor);
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string AutorizarTope(string valor, string valor2, string jobcode)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.puede_Autorizar_monto(valor, valor2, jobcode);
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }





    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string AutorizarTopeCompra(string valor, string valor2, string jobcode)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.AutorizarMontoPromedioCompra(valor, valor2, jobcode);
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }




   
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GuardarParametroMaestro(List<ParamaetroOpDivisas> valor)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        ParametroOp lista = new ParametroOp();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();

        try
        {
            foreach (var item in valor)
            {
                if (lista.ValidarParametro(item.NRODOCUMENTO, item.TIPODOCUMENTO, item.SEQNBR))
                {
                    lst = lista.ActualizarParametro(item);
                }
                else
                {
                    lst = lista.CrearParametro(item);
                }
            }
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }




    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GuardarEntidadesCliente(List<EntidadesFinancierasentity> valor)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        ParametroOp lista = new ParametroOp();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();

        try
        {
            foreach (var item in valor)
            {
                if (item.Nrodocumento == "0")
                {

                }
                else
                {
                    lst = lista.CrearEntidadCliente(item);
                }
            }
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }




    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GuardarOperacionesInt(List<OperacionesInterPNEntity> valor)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();

        try
        {
            foreach (var item in valor)
            {
                if (lista.ValidarOperacionesInt(item.NRODOCUMENTO, item.TIPODOCUMENTO))
                {
                    lst = lista.ActualizarOperacionesInt(item);
                }
                else
                {
                    lst = lista.CrerOperacionesInt(item);
                }
            }
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }





    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string ActualizarAutorizacionDiv(List<ParamaetroOpDivisas> valor)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();

        try
        {
            foreach (var item in valor)
            {
                if (lista.ValidarOperacionesInt(item.NRODOCUMENTO, item.TIPODOCUMENTO))
                {
                    lst = lista.ActualizarAutorizacion(item);
                }
                else
                {

                }
            }
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }



   
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string TraerParametroDetalle(string valor, string valorTipoDoc)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        ParametrosDivisas lista = new ParametrosDivisas();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.ObtenerParametrosDetalle(valor, valorTipoDoc);
            json = js.Serialize(lst);

        }
        catch (Exception error)
        {

        }
        return json;
    }




    //Metodo para traer detalles 
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string TraerCargoAutoriza(string valor, string valorTipoDoc)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        ParametrosDivisas lista = new ParametrosDivisas();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.ObtenerCargoAutoriza(valor, valorTipoDoc);
            json = js.Serialize(lst);

        }
        catch (Exception error)
        {

        }
        return json;
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string TraerEntidadesCliente(string valor, string valorTipoDoc)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        ParametrosDivisas lista = new ParametrosDivisas();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {


            lst = lista.ObtenerEntidades(valor, valorTipoDoc);
            json = js.Serialize(lst);



        }
        catch (Exception error)
        {

        }
        return json;
    }






    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string TraerAutorizacionTope(string valor, string valor2, string jobcode)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {


            lst = lista.puede_Autorizar_monto(valor, valor2, jobcode);
            json = js.Serialize(lst);



        }
        catch (Exception error)
        {

        }
        return json;
    }





    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string TraerOpMesaDinero(string valor, string valorTipoDoc)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        ParametrosDivisas lista = new ParametrosDivisas();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {


            lst = lista.ObtenerOperacionesInt(valor, valorTipoDoc);
            json = js.Serialize(lst);



        }
        catch (Exception error)
        {

        }
        return json;
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string EliminarParametroDetalle(OperacionesdtlEntity valor)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        ParametrosDivisas lista = new ParametrosDivisas();
        bool respuesta = new bool();

        try
        {
            if (lista.EliminarParametro(valor))
            {
                respuesta = true;
            }
            else
            {
                respuesta = false;
            }

        }
        catch (Exception error)
        {
            respuesta = false;
        }
        json = js.Serialize(respuesta);
        return json;
    }




    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string EliminarEntidadClte(EntidadesFinancierasentity valor)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        ParametrosDivisas lista = new ParametrosDivisas();
        bool respuesta = new bool();

        try
        {
            if (lista.EliminarEntidadClte(valor))
            {
                respuesta = true;
            }
            else
            {
                respuesta = false;
            }

        }
        catch (Exception error)
        {
            respuesta = false;
        }
        json = js.Serialize(respuesta);
        return json;
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Mesa_Dinero_Cop_Mes_Compra(string valor, string valorTipoDoc)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.mesa_dinero_cop_compra(valor, valorTipoDoc);
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Mesa_Dinero_Cop_Mes_Venta(string valor, string valorTipoDoc)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.mesa_dinero_cop_venta(valor, valorTipoDoc);
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Mesa_Dinero_Mes(string valor, string valorTipoDoc)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.mesa_dinero(valor, valorTipoDoc);
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Mesa_Dinero_Mes_Venta(string valor, string valorTipoDoc)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.mesa_dinero_venta(valor, valorTipoDoc);
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }




    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Mesa_Dinero_Mes_Compra(string valor, string valorTipoDoc)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.mesa_dinero_compra(valor, valorTipoDoc);
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }






    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Mesa_Dinero_Mes_COP(string valor, string valorTipoDoc)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.mesa_dinero_cop(valor, valorTipoDoc);
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }



/*
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Cargo_usuario_autoriza(string valor)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.validar_cupo_pn_opr_int(valor);
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }

    */



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string TraerUsuarioSesion(string valor, string valor2)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.ObtenerUsuario(valor, valor2);
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }





    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string TraerDescripcionPais(string valor)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.ObtenerDescrPais(valor);
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }




    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string TraerDescripcionMoneda(string valor)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.ObtenerDescrMoneda(valor);
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }





    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string TraeDivisasPendiente(ParamaetroOpDivisas valor)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        ParametroOp lista = new ParametroOp();
        ParametrosDivisas lista2 = new ParametrosDivisas();

        CLSRespuestaDiv lst = new CLSRespuestaDiv();

        try
        {

            lst = lista.ObtenerDivPendientes(valor);
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string TraerOperacionesPendientes(string valor, string valorTipoDoc)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        ParametrosDivisas lista = new ParametrosDivisas();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {


            lst = lista.ObtenerDivisasPendientes(valor, valorTipoDoc);
            json = js.Serialize(lst);



        }
        catch (Exception error)
        {

        }
        return json;
    }




    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string ValidarBloqueo(string valor)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.ObtenerValidacion(valor);
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string ValidarDivisas(string valor)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.ValidacionDivisasVigentes(valor);
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }

}



















