using System;
using System.Collections.Generic;
using System.Web;


public class ParametrosDivisas
{




    public CLSRespuestaDiv ObtenerParametrosDetalle(string valor, string valorTipoDoc)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaParametrosDetalle = new List<OperacionesdtlEntity>();
        try
        {
            ParametroOIModel param = new ParametroOIModel();

            List<OperacionesdtlEntity> Lista = new List<OperacionesdtlEntity>();
            Lista = param.TraerParametrosDetalle(valor, valorTipoDoc);

            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaB";
            respuesta.ListaParametrosDetalle = Lista;
        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametrosDetalle = null;
        }

        return respuesta;
    }






    public CLSRespuestaDiv ObtenerCargoAutoriza(string valor, string valorTipoDoc)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaParametrosDetalle = new List<OperacionesdtlEntity>();
        try
        {
            ParametroOIModel param = new ParametroOIModel();

            List<OperacionesdtlEntity> Lista = new List<OperacionesdtlEntity>();
            Lista = param.TraerCargoAutorizaDiv(valor, valorTipoDoc);

            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaB";
            respuesta.ListaParametrosDetalle = Lista;
        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametrosDetalle = null;
        }

        return respuesta;
    }

    public CLSRespuestaDiv ObtenerEntidades(string valor, string valorTipoDoc)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaEntidades = new List<EntidadesFinancierasentity>();
        try
        {
            ParametroOIModel param = new ParametroOIModel();

            List<EntidadesFinancierasentity> Lista = new List<EntidadesFinancierasentity>();
            Lista = param.TraerEntidadesCliente(valor, valorTipoDoc);

            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaC";
            respuesta.ListaEntidades = Lista;
        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametrosDetalle = null;
        }

        return respuesta;
    }











    public CLSRespuestaDiv ObtenerOperacionesInt(string valor, string valorTipoDoc)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaOperacionesInt = new List<OperacionesIntEntity>();
        try
        {
            ParametroOIModel param = new ParametroOIModel();

            List<OperacionesIntEntity> Lista = new List<OperacionesIntEntity>();
            Lista = param.TraerOperacionesInt(valor,valorTipoDoc);

            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaD";
            respuesta.ListaOperacionesInt = Lista;
        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaOperacionesInt = null;
        }

        return respuesta;
    }




    public CLSRespuestaDiv ActualizarParametro(OperacionesdtlEntity Paramaetro)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaParametrosDetalle = new List<OperacionesdtlEntity>();
        try
        {
            ParametroOIModel param = new ParametroOIModel();

            List<OperacionesdtlEntity> Lista = new List<OperacionesdtlEntity>();
            if (param.ActualizarParametrodtl(Paramaetro))
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
                respuesta.Descripcion = "Consulta ExitosaE";
                respuesta.ListaParametrosDetalle = Lista;
            }
            else
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
                respuesta.Descripcion = "Error: Error al momento de actualizar el parametro";
                respuesta.ListaParametrosDetalle = null;
            }


        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametrosDetalle = null;
        }

        return respuesta;
    }

    public CLSRespuestaDiv CrearParametro(OperacionesdtlEntity valor)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaParametrosDetalle = new List<OperacionesdtlEntity>();
        try
        {
            ParametroOIModel param = new ParametroOIModel();

            List<OperacionesdtlEntity> Lista = new List<OperacionesdtlEntity>();
            if (param.CrearParametrodtl(valor))
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
                respuesta.Descripcion = "Consulta ExitosaA";
                respuesta.ListaParametrosDetalle = Lista;
            }
            else
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
                respuesta.Descripcion = "Error: Error al momento de crear el parametro";
                respuesta.ListaParametrosDetalle = null;
            }


        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametrosDetalle = null;
        }

        return respuesta;
    }

    public bool ValidarParametro(OperacionesdtlEntity valor)
    {
        bool respuesta = false;

        try
        {
            ParametroOIModel param = new ParametroOIModel();

            List<OperacionesdtlEntity> Lista = new List<OperacionesdtlEntity>();
            if (param.ValidarParametrodtl(valor))
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


    public bool ValidarListaBloqueo(string valor)
    {
        bool respuesta = false;

        try
        {
            ParametroOIModel param = new ParametroOIModel();

            List<OperacionesdtlEntity> Lista = new List<OperacionesdtlEntity>();
            if (param.ValidarListaBloqueodtl(valor))
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




    public bool ValidarEmpleado(string valor)
    {
        bool respuesta = false;

        try
        {
            ParametroOIModel param = new ParametroOIModel();

            List<OperacionesdtlEntity> Lista = new List<OperacionesdtlEntity>();
            if (param.ValidarEmpleado(valor))
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


    internal bool EliminarParametro(OperacionesdtlEntity valor)
    {
        bool respuesta = false;

        try
        {
            ParametroOIModel param = new ParametroOIModel();

            List<OperacionesdtlEntity> Lista = new List<OperacionesdtlEntity>();
            if (param.EliminarParametrodtl(valor))
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




    internal bool EliminarEntidadClte(EntidadesFinancierasentity valor)
    {
        bool respuesta = false;

        try
        {
            ParametroOIModel param = new ParametroOIModel();

            List<EntidadesFinancierasentity> Lista = new List<EntidadesFinancierasentity>();
            if (param.EliminarEntidadClte(valor))
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




    public CLSRespuestaDiv ObtenerDivisasPendientes(string valor, string valorTipoDoc)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaParametrosDetalle = new List<OperacionesdtlEntity>();
        try
        {
            ParametroOIModel param = new ParametroOIModel();

            List<OperacionesdtlEntity> Lista = new List<OperacionesdtlEntity>();
            Lista = param.TraerDivisasPendientes(valor, valorTipoDoc);

            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaB";
            respuesta.ListaParametrosDetalle = Lista;
        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametrosDetalle = null;
        }

        return respuesta;
    }









}




