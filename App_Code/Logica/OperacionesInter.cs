using System;
using System.Collections.Generic;

using System.Web;



public class OperacionesInter
{
    public CLSRespuestaDiv ObtenerOperacionesInter(string valor, string valor2)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaOperacionesIntPN = new List<OperacionesInterPNEntity>();
        try
        {


            OperacionesInterModel param = new OperacionesInterModel();

            List<OperacionesInterPNEntity> Lista = new List<OperacionesInterPNEntity>();
            Lista = param.TraerOperacionesInt(valor, valor2);

            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaI";
            respuesta.ListaOperacionesIntPN = Lista;



        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }






    public CLSRespuestaDiv ObtenerCargosAutorizan(string valor)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaOperacionesIntPN = new List<OperacionesInterPNEntity>();
        try
        {


            OperacionesInterModel param = new OperacionesInterModel();

            List<OperacionesInterPNEntity> Lista = new List<OperacionesInterPNEntity>();
            Lista = param.CargosAutorizanDiv(valor);
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaII";
            respuesta.ListaOperacionesIntPN = Lista;



        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }






    public CLSRespuestaDiv ActualizarOperacionesInt(OperacionesInterPNEntity Paramaetro)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaOperacionesIntPN = new List<OperacionesInterPNEntity>();
        try
        {
            OperacionesInterModel param = new OperacionesInterModel();

            List<OperacionesInterPNEntity> Lista = new List<OperacionesInterPNEntity>();
            if (param.ActualizarOperaciones(Paramaetro))
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
                respuesta.Descripcion = "Actualizacion exitosa";
                respuesta.ListaOperacionesIntPN = Lista;
            }
            else
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
                respuesta.Descripcion = "Error: Error al momento de actualizar operaciones Internacionales";
                respuesta.ListaOperacionesIntPN = null;
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




    public CLSRespuestaDiv CrerOperacionesInt(OperacionesInterPNEntity parametro2)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaOperacionesIntPN = new List<OperacionesInterPNEntity>();
        try
        {
            OperacionesInterModel param = new OperacionesInterModel();




            List<OperacionesInterPNEntity> Lista = new List<OperacionesInterPNEntity>();


            if (param.CrerOperacionesInt(parametro2))
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
                respuesta.Descripcion = "Consulta ExitosaIV";
                respuesta.ListaOperacionesIntPN = Lista;
            }
            else
            {



                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
                respuesta.Descripcion = "Consulta ExitosaV";
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


    public CLSRespuestaDiv ActualizarAutorizacion(ParamaetroOpDivisas Paramaetro)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaParametros = new List<ParamaetroOpDivisas>();
        try
        {
            OperacionesInterModel param = new OperacionesInterModel();

            List<ParamaetroOpDivisas> Lista = new List<ParamaetroOpDivisas>();
            if (param.ActualizarAutorizacionDivisa(Paramaetro))
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
                respuesta.Descripcion = "Consulta ExitosaVI";
                respuesta.ListaParametros = Lista;
            }
            else
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
                respuesta.Descripcion = "Error: Error al momento de aprobar la divisa";
                respuesta.ListaOperacionesIntPN = null;
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






    public bool ValidarOperacionesInt(string valor, string tipoDocumento)
    {
        bool respuesta = false;

        try
        {
            OperacionesInterModel param = new OperacionesInterModel();

            List<OperacionesInterPNEntity> Lista = new List<OperacionesInterPNEntity>();
            if (param.ValidarOperaciones(valor, tipoDocumento))
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



    public CLSRespuestaDiv entidadesFinancieras()
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.listaEntidades = new List<EntidadesFinancierasentity>();
        try
        {


            OperacionesInterModel param = new OperacionesInterModel();

            List<EntidadesFinancierasentity> Lista = new List<EntidadesFinancierasentity>();
            Lista = param.EntidadesFinancieras();
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaVIII";
            respuesta.listaEntidades = Lista;



        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }






    public CLSRespuestaDiv tope_venta_usd(string documento, string tipoDocumento)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.listaOperacionesIntPN = new List<OperacionesInterPNEntity>();
        try
        {


            OperacionesInterModel param = new OperacionesInterModel();

            List<OperacionesInterPNEntity> Lista = new List<OperacionesInterPNEntity>();
            Lista = param.Venta_usd_tope(documento, tipoDocumento);
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaIX";
            respuesta.listaOperacionesIntPN = Lista;



        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }




    public CLSRespuestaDiv info_actualizado(DateTime valor)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaClientesTP = new List<DetallesTPEntity>();
        try
        {


            OperacionesInterModel param = new OperacionesInterModel();

            List<DetallesTPEntity> Lista = new List<DetallesTPEntity>();
            Lista = param.Av_info_actualizado(valor);
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaX";
            respuesta.ListaClientesTP = Lista;



        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }



    public CLSRespuestaDiv puede_Autorizar_monto(string valor, string valor2, string jobcode)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.listaOperacionesIntPN = new List<OperacionesInterPNEntity>();
        try
        {

            OperacionesInterModel param = new OperacionesInterModel();
            List<OperacionesInterPNEntity> Lista = new List<OperacionesInterPNEntity>();
            Lista = param.puedeAutorizar(valor, valor2, jobcode);
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaXI";
            respuesta.listaOperacionesIntPN = Lista;

        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }
        return respuesta;
    }





    public CLSRespuestaDiv AutorizarMontoPromedioCompra(string valor, string valor2, string jobcode)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.listaOperacionesIntPN = new List<OperacionesInterPNEntity>();
        try
        {

            OperacionesInterModel param = new OperacionesInterModel();
            List<OperacionesInterPNEntity> Lista = new List<OperacionesInterPNEntity>();
            Lista = param.AutorizarMontoCompra(valor, valor2, jobcode);
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaXII";
            respuesta.listaOperacionesIntPN = Lista;

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
                respuesta.Descripcion = "Consulta ExitosaXVI";
                respuesta.ListaParametros = Lista;
            }
            else
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
                respuesta.Descripcion = "Error: Error al momento de actualizar el parametro";
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
                respuesta.Descripcion = "Consulta ExitosaXVII";
                respuesta.ListaParametros = Lista;
            }
            else
            {
                respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
                respuesta.Descripcion = "Error: Error al momento de crear el detalle";
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

    public bool ValidarParametro(string valor, string valor3, string valor2)
    {
        bool respuesta = false;

        try
        {
            ParametroDivModel param = new ParametroDivModel();

            List<ParamaetroOpDivisas> Lista = new List<ParamaetroOpDivisas>();
            if (param.ValidarParametro(valor, valor3, valor2))
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





    public CLSRespuestaDiv ListadoPaises()
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.listaPaises = new List<PaisesProductoExterior>();
        try
        {


            OperacionesInterModel param = new OperacionesInterModel();

            List<PaisesProductoExterior> Lista = new List<PaisesProductoExterior>();
            Lista = param.ListaPaises();
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaXVIII";
            respuesta.listaPaises = Lista;



        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }








    public CLSRespuestaDiv ListadoMonedas()
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.listaTiposMoneda = new List<TiposMonedaExterior>();
        try
        {


            OperacionesInterModel param = new OperacionesInterModel();

            List<TiposMonedaExterior> Lista = new List<TiposMonedaExterior>();
            Lista = param.ListaMonedasExtranjero();
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaXIX";
            respuesta.listaTiposMoneda = Lista;



        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }










    public CLSRespuestaDiv mesa_dinero(string documento, string tipoDocumento)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.listaOperacionesMesaDinero = new List<OperacionesMesaDinero>();
        try
        {


            OperacionesInterModel param = new OperacionesInterModel();

            List<OperacionesMesaDinero> Lista = new List<OperacionesMesaDinero>();
            Lista = param.Monto_op_mesa_dinero(documento, tipoDocumento);
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaXX";
            respuesta.listaOperacionesMesaDinero = Lista;



        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }





    public CLSRespuestaDiv mesa_dinero_venta(string documento, string tipoDocumento)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.listaOperacionesMesaDineroVenta = new List<OperacionesMesaDineroVenta>();
        try
        {


            OperacionesInterModel param = new OperacionesInterModel();

            List<OperacionesMesaDineroVenta> Lista = new List<OperacionesMesaDineroVenta>();
            Lista = param.Monto_op_mesa_dinero_venta(documento, tipoDocumento);
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaXX";
            respuesta.listaOperacionesMesaDineroVenta = Lista;



        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }







    public CLSRespuestaDiv mesa_dinero_compra(string documento, string tipoDocumento)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.listaOperacionesMesaDineroCompra = new List<OperacionesMesaDineroCompra>();
        try
        {


            OperacionesInterModel param = new OperacionesInterModel();

            List<OperacionesMesaDineroCompra> Lista = new List<OperacionesMesaDineroCompra>();
            Lista = param.Monto_op_mesa_dinero_compra(documento, tipoDocumento);
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaXX";
            respuesta.listaOperacionesMesaDineroCompra = Lista;



        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }








    public CLSRespuestaDiv mesa_dinero_cop(string documento, string tipoDocumento)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.listaOperacionesMesaDinero = new List<OperacionesMesaDinero>();
        try
        {


            OperacionesInterModel param = new OperacionesInterModel();

            List<OperacionesMesaDinero> Lista = new List<OperacionesMesaDinero>();
            Lista = param.Monto_op_mesa_dinero_cop(documento, tipoDocumento);
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaXX";
            respuesta.listaOperacionesMesaDinero = Lista;



        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }




    public CLSRespuestaDiv ObtenerUsuario(string valor, string valor2)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaUsuario = new List<UsuarioSesion>();
        try
        {


            OperacionesInterModel param = new OperacionesInterModel();

            List<UsuarioSesion> Lista = new List<UsuarioSesion>();
            Lista = param.TraerUsuarioSesion(valor, valor2);

            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaXXI";
            respuesta.ListaUsuario = Lista;



        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }







    public CLSRespuestaDiv ObtenerDescrPais(string valor)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaOperacionesIntPN = new List<OperacionesInterPNEntity>();
        try
        {


            OperacionesInterModel param = new OperacionesInterModel();

            List<OperacionesInterPNEntity> Lista = new List<OperacionesInterPNEntity>();
            Lista = param.TraerPaisDescr(valor);

            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaXXI";
            respuesta.ListaOperacionesIntPN = Lista;



        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }





    public CLSRespuestaDiv ObtenerDescrMoneda(string valor)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaOperacionesIntPN = new List<OperacionesInterPNEntity>();
        try
        {


            OperacionesInterModel param = new OperacionesInterModel();

            List<OperacionesInterPNEntity> Lista = new List<OperacionesInterPNEntity>();
            Lista = param.TraerMonedaDescr(valor);

            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta Exitosa";
            respuesta.ListaOperacionesIntPN = Lista;



        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }








    public CLSRespuestaDiv ObtenerValidacion(string valor)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaOperacionesIntPN = new List<OperacionesInterPNEntity>();
        try
        {


            OperacionesInterModel param = new OperacionesInterModel();

            List<OperacionesInterPNEntity> Lista = new List<OperacionesInterPNEntity>();
            Lista = param.ValidacionListaBloqueo(valor);

            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta Lista de Bloqueo, exitosa";
            respuesta.ListaOperacionesIntPN = Lista;



        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }





    public CLSRespuestaDiv mesa_dinero_cop_venta(string documento, string tipoDocumento)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.listaOperacionesMesaDineroCopVenta = new List<OperacionesMesaDineroCopVenta>();
        try
        {


            OperacionesInterModel param = new OperacionesInterModel();

            List<OperacionesMesaDineroCopVenta> Lista = new List<OperacionesMesaDineroCopVenta>();
            Lista = param.Monto_op_mesa_dinero_cop_venta(documento, tipoDocumento);
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaXX";
            respuesta.listaOperacionesMesaDineroCopVenta = Lista;



        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }







    public CLSRespuestaDiv mesa_dinero_cop_compra(string documento, string tipoDocumento)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.listaOperacionesMesaDineroCopCompra = new List<OperacionesMesaDineroCopCompra>();
        try
        {


            OperacionesInterModel param = new OperacionesInterModel();

            List<OperacionesMesaDineroCopCompra> Lista = new List<OperacionesMesaDineroCopCompra>();
            Lista = param.Monto_op_mesa_dinero_cop_compra(documento, tipoDocumento);
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta ExitosaXX";
            respuesta.listaOperacionesMesaDineroCopCompra = Lista;



        }
        catch (Exception error)
        {
            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuesta.Descripcion = "Error: " + error.InnerException + " - " + error.Message;
            respuesta.ListaParametros = null;
        }

        return respuesta;
    }








    public CLSRespuestaDiv ValidacionDivisasVigentes(string valor)
    {
        CLSRespuestaDiv respuesta = new CLSRespuestaDiv();
        respuesta.ListaOperacionesIntPN = new List<OperacionesInterPNEntity>();
        try
        {


            OperacionesInterModel param = new OperacionesInterModel();

            List<OperacionesInterPNEntity> Lista = new List<OperacionesInterPNEntity>();
            Lista = param.ValidacionDivisasVigentes(valor);

            respuesta.Codigo = CLSRespuestaDiv.codigo_respuesta.correcto;
            respuesta.Descripcion = "Consulta Divisas Vigentes, exitosa";
            respuesta.ListaOperacionesIntPN = Lista;



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
