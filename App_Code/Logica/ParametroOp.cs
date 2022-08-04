using System;
using System.Collections.Generic;

using System.Web;



public class ParametroOp
{
    public CLSRespuestaDiv ObtenerParametros(ParamaetroOpDivisas valor)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaParametros = new List<ParamaetroOpDivisas>();
        try
        {
            ParametroDivModel param = new ParametroDivModel();

            List<ParamaetroOpDivisas> Lista = new List<ParamaetroOpDivisas>();




            ParametrosDivisas lista = new ParametrosDivisas();

            //No se encuentra en la lista de bloqueo
            if (!lista.ValidarListaBloqueo(valor.NRODOCUMENTO))
            {
                Lista = param.BloqueoDateEnd(valor.NRODOCUMENTO);
                Lista = param.TraerParametros(valor);

                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
                respuesta.Descripcion = "Consulta Exitosa2";
                respuesta.ListaParametros = Lista;
            }
            else
            {

                Lista = param.TraerParametros(valor);

                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
                respuesta.Descripcion = "Consulta Exitosa3";
                respuesta.ListaParametros = Lista;
                
            }

           
        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }




    public CLSRespuestaDiv ActualizarParametro(ParamaetroOpDivisas Paramaetro)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaParametros = new List<ParamaetroOpDivisas>();
        try
        {
            ParametroDivModel param = new ParametroDivModel();

            List<ParamaetroOpDivisas> Lista = new List<ParamaetroOpDivisas>();
            if (param.ActualizarParametro(Paramaetro))
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
                respuesta.Descripcion = "Consulta Exitosa4";
                respuesta.ListaParametros = Lista;
            }
            else
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
                respuesta.Descripcion = "Error: Error al momento de actualizar divisas";
                respuesta.ListaParametros = null;
            }


        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }






    public CLSRespuestaDiv ObtenerOperacionesAut(OperacionesInterPNEntity parametro2)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaOperacionesIntPN = new List<OperacionesInterPNEntity>();
        try
        {
            OperacionesInterModel param = new OperacionesInterModel();

           


            List<OperacionesInterPNEntity> Lista = new List<OperacionesInterPNEntity>();
           

            if (param.ActualizarOperaciones(parametro2))
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
                respuesta.Descripcion = "Consulta Exitosa5";
                respuesta.ListaOperacionesIntPN = Lista;
            }
            else
            {

              

                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
                respuesta.Descripcion = "Consulta Exitosa1";
                respuesta.ListaOperacionesIntPN = Lista;

            }


        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }



















    public CLSRespuestaDiv CrearEntidadCliente(EntidadesFinancierasentity valor)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaEntidades = new List<EntidadesFinancierasentity>();
        try
        {
            ParametroDivModel param = new ParametroDivModel();

            List<EntidadesFinancierasentity> Lista = new List<EntidadesFinancierasentity>();
            if (param.CrearEntidadesFinancieras(valor))
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
                respuesta.Descripcion = "Consulta Exitosa6";
                respuesta.ListaEntidades = Lista;
            }
            else
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
                respuesta.Descripcion = "Error: Error al momento de crear entidad bancaria";
                respuesta.ListaParametros = null;
            }


        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }









    public CLSRespuestaDiv CrearParametro(ParamaetroOpDivisas valor)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaParametros = new List<ParamaetroOpDivisas>();
        try
        {
            ParametroDivModel param = new ParametroDivModel();

            List<ParamaetroOpDivisas> Lista = new List<ParamaetroOpDivisas>();
            if (param.CrearParametro(valor))
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
                respuesta.Descripcion = "Consulta Exitosa7";
                respuesta.ListaParametros = Lista;
            }
            else
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
                respuesta.Descripcion = "Error: Error al momento de crear la divisa";
                respuesta.ListaParametros = null;
            }


        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }

    public bool ValidarParametro(string valor,string valor3, string valor2)
    {
        bool respuesta = false;

        try
        {
            ParametroDivModel param = new ParametroDivModel();

            List<ParamaetroOpDivisas> Lista = new List<ParamaetroOpDivisas>();
            if (param.ValidarParametro(valor,valor3,valor2))
            {

                
                return true;
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







    public CLSRespuestaDiv ObtenerDivPendientes(ParamaetroOpDivisas valor)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaParametros = new List<ParamaetroOpDivisas>();
        try
        {
            ParametroDivModel param = new ParametroDivModel();

            List<ParamaetroOpDivisas> Lista = new List<ParamaetroOpDivisas>();




            ParametrosDivisas lista = new ParametrosDivisas();

            //No se encuentra en la lista de bloqueo
            if (!lista.ValidarListaBloqueo(valor.NRODOCUMENTO))
            {
                Lista = param.BloqueoDateEnd(valor.NRODOCUMENTO);
                Lista = param.TraerDivisasPendientes(valor);

                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
                respuesta.Descripcion = "Consulta Exitosa2";
                respuesta.ListaParametros = Lista;
            }
            else
            {

                Lista = param.TraerDivisasPendientes(valor);

                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
                respuesta.Descripcion = "Consulta Exitosa3";
                respuesta.ListaParametros = Lista;

            }


        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }









}
