using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CLSClienteTP
/// </summary>
public class CLSClienteOp
{
    public CLSClienteOp()
    {
        //
        // TODO: Add constructor logic here
        //

    }
  
    public CLSRespuestaDiv ObtenerClientesTP_FIDEL(DetallesTPEntity valor)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaClientesTP = new List<DetallesTPEntity>();
        try
        {
            ClienteOIModel param = new ClienteOIModel();

            List<DetallesTPEntity> Lista = new List<DetallesTPEntity>();
            Lista = param.TraerClienteAvanzado_CRWORK_FIDEL(valor);

            if (Lista.Count == 0)
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
                respuesta.Descripcion = "Cliente no encontrado, verifique los campos";
                respuesta.ListaClientesTP = Lista;
            }
            else
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
                respuesta.Descripcion = "Consulta Exitosa";
                respuesta.ListaClientesTP = Lista;


            }

           
        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
           
            respuesta.ListaClientesTP = null;
        }

        return respuesta;
    }

    public CLSRespuestaDiv ObtenerClientesTP_CRWORK(DetallesTPEntity valor)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaClientesTP = new List<DetallesTPEntity>();
        try
        {
            ClienteOIModel param = new ClienteOIModel();

            List<DetallesTPEntity> Lista = new List<DetallesTPEntity>();
            Lista = param.TraerClienteAvanzado_CRWORK(valor);

            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta Exitosa22";
            respuesta.ListaClientesTP = Lista;
        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaClientesTP = null;
        }

        return respuesta;
    }




    public CLSRespuestaDiv ObtenerDetalleAuto_CRWORK(DetallesTPEntity valor)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaClientesTP = new List<DetallesTPEntity>();
        try
        {
            ClienteOIModel param = new ClienteOIModel();

            List<DetallesTPEntity> Lista = new List<DetallesTPEntity>();
            Lista = param.TraerDetalleAutorizaciones_CRWORK_FIDEL(valor);

            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta Exitosa33";
            respuesta.ListaClientesTP = Lista;
        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaClientesTP = null;
        }

        return respuesta;
    }



    internal string ValidaPoliticaCRM(DetallesTPEntity valor)
    {
        string respuesta = "";
        ClienteOIModel cliente = new ClienteOIModel();
        try
        {
            respuesta = cliente.validaPoliticastp(valor);
        }
        catch (Exception error)
        {
            respuesta = error.InnerException + "--" + error.Message;
        }
        return respuesta;
    }

    internal bool ExisteClienteTP(DetallesTPEntity valor)
    {
        bool respuesta = false;
        ClienteOIModel cliente = new ClienteOIModel();
        try
        {
            respuesta = cliente.ExisteClienteTuPlus(valor);
        }
        catch (Exception error)
        {
            respuesta = false;
        }
        return respuesta;
    }
    


    internal CLSRespuestaDiv CrearClienteTP(DetallesTPEntity valor)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaClientesTP = new List<DetallesTPEntity>();
        try
        {
            ClienteOIModel cliente = new ClienteOIModel();

            List<DetallesTPEntity> Lista = new List<DetallesTPEntity>();
            if (cliente.CrearCliente(valor))
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
                respuesta.Descripcion = "Cliente creado";
                respuesta.ListaClientesTP = Lista;
            }
            else
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
                respuesta.Descripcion = "Error: Error al momento de crear el parametro";
                respuesta.ListaClientesTP = null;
            }


        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaClientesTP = null;
        }

        return respuesta;
    }

    internal bool ClienteTPExiste(DetallesTPEntity valor)
    {
        bool respuesta = false;

        try
        {
            ClienteOIModel cliente = new ClienteOIModel();

            List<DetallesTPEntity> Lista = new List<DetallesTPEntity>();
            if (cliente.ExisteClienteTuPlus(valor))
            {
                valor.TIPODOCUMENTOCR = getDocumento(valor.TIPODOCUMENTO);
                if (cliente.ValidarClienteCRM(valor))
                {
                    return true;
                }
                else
                {
                    respuesta = false;
                }
            }
            else
            {
                respuesta = false;
            }


        }
        catch
        {
            respuesta = false;
        }

        return respuesta;
    }

    public string getDocumento(string tipoDocu)
    {
        string respuesta = "";
        tipoDocu = !string.IsNullOrEmpty(tipoDocu) ? tipoDocu : "CC";
        switch (tipoDocu)
        {
            case "CC":
                respuesta = "C";
                break;
            case "CE":
                respuesta = "E";
                break;
            case "TI":
                respuesta = "T";
                break;
            case "RC":
                respuesta = "R";
                break;
            case "PS":
                respuesta = "P";
                break;
        }
        return respuesta;
    }
}