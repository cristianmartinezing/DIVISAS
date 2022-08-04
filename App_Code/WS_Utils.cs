using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for WS_Utils
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class WS_Utils : System.Web.Services.WebService
{

    public WS_Utils()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Lista_TipoIdentificacion(string numero)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        TipoDocumento lista = new TipoDocumento();
        json = js.Serialize(lista.Listar());
        return json;
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Aa_divisas(string numero)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInternacionales lista = new OperacionesInternacionales();
        json = js.Serialize(lista.Listar());
        return json;
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Lista_OperacInter(string numero)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInternacionales lista = new OperacionesInternacionales();
        json = js.Serialize(lista.Listar());
        return json;

    }


 
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Lista_Entidades(string numero)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        json = js.Serialize(lista.entidadesFinancieras());
        return json;
    }




    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Autenticar(string vdu, string vdc, string app)
    {
        string respuesta = "";
        Autenticador obj = new Autenticador();
        string autenticado = obj.AutenticarUsuario(vdu, vdc, app);
        string[] json = autenticado.Split('@');
        respuesta = "{\"Codigo\":\"" + json[0] + "\",\"Descripcion\":\"" + json[1] + "\"}";
       
        return respuesta;
    }




    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Lista_Paises(string numero)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        json = js.Serialize(lista.ListadoPaises());
        return json;
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Lista_Tipos_Moneda(string numero)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        OperacionesInter lista = new OperacionesInter();
        json = js.Serialize(lista.ListadoMonedas());
        return json;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string ListaEstados(string numero)
    {

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = "";
        ModificarClienteModel lista = new ModificarClienteModel();
        json = js.Serialize(lista.ObtenerOficinas());
        return json;
    }


}
