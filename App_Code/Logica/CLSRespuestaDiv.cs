using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Descripción breve de EnumRespuesta
/// </summary>
public class CLSRespuestaDiv
{

    private List<CLSClienteDiv> listaClientes;
    private List<ParamaetroOpDivisas> listaParametros;
    private List<OperacionesdtlEntity> listaParametrosDetalle;
    private List<DetallesTPEntity> listaClientesTP;

    public List<OperacionesInterPNEntity> listaOperacionesIntPN;
    public List<DatosUsuarioPNentity> listaValidaRolesPersona;
    public List<DatosUsuarioPNentity> listaAutorizaMonto;
    public List<EntidadesFinancierasentity> listaEntidades;
    public List<PaisesProductoExterior> listaPaises;
  
    public List<OperacionesIntEntity> listaOperacionesInt;
    public List<TiposMonedaExterior> listaTiposMoneda;
    public List<OperacionesMesaDinero> listaOperacionesMesaDinero;
    public List<OperacionesMesaDineroVenta> listaOperacionesMesaDineroVenta;
    public List<OperacionesMesaDineroCompra> listaOperacionesMesaDineroCompra;
    public List<OperacionesMesaDineroCopCompra> listaOperacionesMesaDineroCopCompra;
    public List<OperacionesMesaDineroCopVenta> listaOperacionesMesaDineroCopVenta;




    public List<UsuarioSesion> listaUsuario;
    public enum codigo_respuesta
    {
        correcto = 0000,
        error = 9999
    }



    public CLSRespuestaDiv()
    {
        codigo = 0000;
        descripcion = "";

    }
    private codigo_respuesta codigo;
    private string descripcion;
    private string errorLog;

    public codigo_respuesta Codigo
    {
        get
        {
            return codigo;
        }

        set
        {
            codigo = value;
        }
    }

    public string Descripcion
    {
        get
        {
            return descripcion;
        }

        set
        {
            descripcion = value;
        }
    }

    public List<CLSClienteDiv> ListaClientes
    {
        get
        {
            return listaClientes;
        }

        set
        {
            listaClientes = value;
        }
    }

    public string ErrorLog
    {
        get
        {
            return errorLog;
        }

        set
        {
            errorLog = value;
        }
    }

    public List<ParamaetroOpDivisas> ListaParametros
    {
        get
        {
            return listaParametros;
        }

        set
        {
            listaParametros = value;
        }
    }


    



   public List<UsuarioSesion> ListaUsuario
    {
        get
        {
            return listaUsuario;
        }

        set
        {
            listaUsuario = value;
        }
    }




    public List<OperacionesInterPNEntity> ListaOperacionesIntPN
    {
        get
        {
            return listaOperacionesIntPN;
        }

        set
        {
            listaOperacionesIntPN = value;
        }
    }


    public List<DatosUsuarioPNentity> ListaValidacionRolesPersona
    {
        get
        {
            return listaValidaRolesPersona;
        }

        set
        {
            listaValidaRolesPersona = value;
        }
    }
    
   public List<DatosUsuarioPNentity> ListaAutorizarMonto
    {
        get
        {
            return listaAutorizaMonto;
        }

        set
        {
            listaAutorizaMonto = value;
        }
    }




    public List<OperacionesMesaDineroCopVenta> ListaOperacionesMesaDineroCopVenta
    {
        get
        {
            return listaOperacionesMesaDineroCopVenta;
        }

        set
        {
            listaOperacionesMesaDineroCopVenta = value;
        }
    }



    public List<OperacionesMesaDineroCopCompra> ListaOperacionesMesaDineroCopCompra
    {
        get
        {
            return listaOperacionesMesaDineroCopCompra;
        }

        set
        {
            listaOperacionesMesaDineroCopCompra = value;
        }
    }



    public List<EntidadesFinancierasentity> ListaEntidades
    {
        get
        {
            return listaEntidades;
        }

        set
        {
            listaEntidades = value;
        }
    }





    public List<PaisesProductoExterior> ListaPaises
    {
        get
        {
            return listaPaises;
        }

        set
        {
            listaPaises = value;
        }
    }



    public List<OperacionesMesaDinero> ListaOperacionesMesaDinero
    {
        get
        {
            return listaOperacionesMesaDinero;
        }

        set
        {
            listaOperacionesMesaDinero = value;
        }
    }



    public List<OperacionesMesaDineroVenta> ListaOperacionesMesaDineroVenta
    {
        get
        {
            return listaOperacionesMesaDineroVenta;
        }

        set
        {
            listaOperacionesMesaDineroVenta = value;
        }
    }






    public List<OperacionesMesaDineroCompra> ListaOperacionesMesaDineroCompra
    {
        get
        {
            return listaOperacionesMesaDineroCompra;
        }

        set
        {
            listaOperacionesMesaDineroCompra = value;
        }
    }



    public List<TiposMonedaExterior> ListaMonedas
    {
        get
        {
            return listaTiposMoneda;
        }

        set
        {
            listaTiposMoneda = value;
        }
    }



    public List<OperacionesdtlEntity> ListaParametrosDetalle
    {
        get
        {
            return listaParametrosDetalle;
        }

        set
        {
            listaParametrosDetalle = value;
        }
    }




    public List<OperacionesIntEntity> ListaOperacionesInt
    {
        get
        {
            return listaOperacionesInt;
        }

        set
        {
            listaOperacionesInt = value;
        }
    }


    public List<DetallesTPEntity> ListaClientesTP
    {
        get
        {
            return listaClientesTP;
        }

        set
        {
            listaClientesTP = value;
        }
    }

    /*
    public List<Territory> ListaOficinas
    {
        get
        {
            return listaOficinas;
        }

        set
        {
            listaOficinas = value;
        }
    }*/
}

/*
public class CLSOpcionBD
{
    public CLSOpcionBD()
    {

    }
    public enum opcion
    {
        actualizar = 1,
        eliminar = 2,
        insertar = 3,
    }
}*/