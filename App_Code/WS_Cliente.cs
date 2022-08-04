using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for WS_Cliente
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WS_Cliente : System.Web.Services.WebService
{

    public WS_Cliente()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string BuscarClienteTP_CRWORK(DetallesTPEntity valor)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();

        string json = "";
        string politica = "";
        CLSClienteOp lista = new CLSClienteOp();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            if (lista.ExisteClienteTP(valor))
            {
                lst = lista.ObtenerClientesTP_FIDEL(valor);
                lst.ErrorLog = "Cliente existe";
                js.MaxJsonLength = 500000000;
                json = js.Serialize(lst);

            }
            else
            {
                politica = lista.ValidaPoliticaCRM(valor);
                if (politica == "")
                {
                    //lst = lista.CrearClienteTP(valor);
                    lst = lista.ObtenerClientesTP_CRWORK(valor);
                    lst.Descripcion = "Consulta Exitosa";
                    lst.ErrorLog = "Cliente encontrado";

                }
                else
                {
                    lst.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
                    lst.Descripcion = politica;
                    lst.ErrorLog = politica;
                }


            }
            //lst = lista.ObtenerClientesTP_CRWORK(valor);
            //js.MaxJsonLength = 500000000;
            //json = js.Serialize(lst);

        }
        catch (Exception error)
        {
            lst.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            lst.Descripcion = "Error al momento de obtener el resultado";
            lst.ErrorLog = error.InnerException + " , " + error.Message;

        }
        json = js.Serialize(lst);
        int num = json.Length;
        return json;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string BuscarClienteTP_FIDEL(DetallesTPEntity valor)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();

        string json = "";
        CLSClienteOp lista = new CLSClienteOp();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        try
        {
            lst = lista.ObtenerClientesTP_FIDEL(valor);
            js.MaxJsonLength = 500000000;
            json = js.Serialize(lst);

        }
        catch (Exception error)
        {
            lst.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            lst.Descripcion = "Error al momento de obtener el resultado";
            lst.ErrorLog = error.InnerException + " , " + error.Message;

        }
        int num = json.Length;
        return json;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Guardar(DetallesTPEntity valor)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = 500000000;
        string json = "";
        CLSClienteOp lista = new CLSClienteOp();
        CLSRespuestaDiv lst = new CLSRespuestaDiv();
        string politica = "";
        try
        {

            if (lista.ExisteClienteTP(valor))
            {
                lst = lista.ObtenerClientesTP_FIDEL(valor);
                lst.ErrorLog = "Cliente existe";


            }
            else
            {
                politica = lista.ValidaPoliticaCRM(valor);
                if (politica == "")
                {
                    lst = lista.CrearClienteTP(valor);
                    lst = lista.ObtenerClientesTP_FIDEL(valor);
                    lst.Descripcion = "Cliente creado correctamente";
                    lst.ErrorLog = "Cliente creado correctamente";
                    json = js.Serialize(lst);
                }
                else
                {
                    lst.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
                    lst.Descripcion = politica;
                    lst.ErrorLog = politica;
                }


            }
            //if (lista.ClienteTPExiste(valor))
            //{

            //    lst = lista.ActualizarClienteTP(valor);
            //}
            //else
            //{
            //    lst = lista.CrearClienteTP(valor);
            //}
            json = js.Serialize(lst);
        }
        catch (Exception error)
        {

        }
        return json;
    }

}
