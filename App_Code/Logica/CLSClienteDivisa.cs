using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Descripción breve de CLSClienteDiv
/// </summary>
public class CLSClienteDiv
{ /*LAURAN70ruizc*/
    private string partyID;
    private string accion;
    private string modulo;
    private string numeroDocumentoNuevo;
    private string estadoCliente;
    public string tipoDocumentoNuevo { get; set; }

    //
    public string usuarioAplicacion { get; set; }
    public string tipoDocumentoCod { get; set; }
    public string tipoDocumento { get; set; }
    public string numeroDocumento { get; set; }
    public int tipoPersona { get; set; }
    public string nombreRazonSocial { get; set; }
    public string primerNombre { get; set; }
    public string segundoNombre { get; set; }
    public string primerApellido { get; set; }
    public string segundoApellido { get; set; }
    public string lugarExpedicionDocumento { get; set; }

    public string NumeroDocumentoNuevo
    {
        get
        {
            return numeroDocumentoNuevo;
        }

        set
        {
            numeroDocumentoNuevo = value;
        }
    }

    public string Accion
    {
        get
        {
            return accion;
        }

        set
        {
            accion = value;
        }
    }

    public string Modulo
    {
        get
        {
            return modulo;
        }

        set
        {
            modulo = value;
        }
    }

    public string EstadoCliente
    {
        get
        {
            return estadoCliente;
        }

        set
        {
            estadoCliente = value;
        }
    }

    public string PartyID
    {
        get
        {
            return partyID;
        }

        set
        {
            partyID = value;
        }
    }



    //Constructor
    public CLSClienteDiv()
    {

        this.tipoDocumento = "0";
        this.numeroDocumento = "";
        this.tipoPersona = 0;
        this.nombreRazonSocial = "";
        this.primerNombre = "";
        this.segundoNombre = "";
        this.primerApellido = "";
        this.segundoApellido = "";
        this.lugarExpedicionDocumento = "";

        this.usuarioAplicacion = "";
    }
	
	
	/*
    public CLSRespuestaDiv Guardar_Auditoria_DR(List<AuditoriaModel> audit, CLSClienteDiv cliente)
    {
        CLSRespuestaDiv respuestaBD = new CLSRespuestaDiv();
        TipoDocumento tpIdentificacion = new TipoDocumento();
        ArchivoLogDiv log = new ArchivoLogDiv();
        PSAuditModel psAudit = new PSAuditModel();

        try
        {
            foreach (var item in audit)
            {
                psAudit.AUDIT_OPRID = cliente.usuarioAplicacion;
                psAudit.AUDIT_ACTN = cliente.accion;
                psAudit.RECNAME = cliente.modulo;
                psAudit.FIELDNAME = item.FieldName;
                psAudit.OLDVALUE = item.OldValue;
                psAudit.NEWVALUE = item.NewValue;
                psAudit.KEY1 = cliente.tipoDocumento;
                psAudit.KEY2 = cliente.numeroDocumento;
                psAudit.KEY4 = "IP: " + item.GetIP();
                respuestaBD = item.Insertar(psAudit);
            }

        }
        catch (Exception error)
        {
            respuestaBD.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuestaBD.Descripcion = "Error guardando en base de datos";
            respuestaBD.ErrorLog = error.InnerException + " , " + error.Message;
            log.GuardaLog(respuestaBD, cliente, "RespuestaBD");
        }
        return respuestaBD;
    }
	*/
	
	
	/*
    public CLSRespuestaDiv Guardar(CLSClienteDiv cliente)
    {
        TipoDocumento tpIdentificacion = new TipoDocumento();
        ArchivoLogDiv log = new ArchivoLogDiv();
        AuditoriaModel audit = new AuditoriaModel();

        CLSRespuestaDiv respuestaBD = new CLSRespuestaDiv();
        PSAuditModel psAudit = new PSAuditModel();
        //Eliminar cliente
        try
        {
            psAudit.AUDIT_OPRID = cliente.usuarioAplicacion;
            psAudit.AUDIT_ACTN = cliente.accion;
            psAudit.RECNAME = cliente.modulo;
            psAudit.KEY1 = tpIdentificacion.ListarPorId( cliente.tipoDocumento);
            psAudit.KEY2 = cliente.numeroDocumento;
            psAudit.KEY3 = cliente.primerNombre + " " + cliente.segundoNombre + " " + cliente.primerApellido + " " + cliente.segundoApellido;
            psAudit.KEY4 = "IP: " + audit.GetIP();

            respuestaBD = audit.Insertar(psAudit);


        }
        catch (Exception error)
        {
            respuestaBD.Codigo = CLSRespuestaDiv.codigo_respuesta.error;
            respuestaBD.Descripcion = "Error guardando en base de datos";
            respuestaBD.ErrorLog = error.InnerException + " , " + error.Message;
            log.GuardaLog(respuestaBD, cliente, "RespuestaBD");
        }
        return respuestaBD;
    }
*/

}
public class TipoDocumento
{
    string idTipoIdentificacion;
    string valorTipoIdentificacion;

    public string IdTipoIdentificacion
    {
        get
        {
            return idTipoIdentificacion;
        }

        set
        {
            idTipoIdentificacion = value;
        }
    }

    public string ValorTipoIdentificacion
    {
        get
        {
            return valorTipoIdentificacion;
        }

        set
        {
            valorTipoIdentificacion = value;
        }
    }

    public List<TipoDocumento> Listar()
    {
        List<TipoDocumento> lista_Tipo_Identificacion = new List<TipoDocumento>(){
                new TipoDocumento(){ IdTipoIdentificacion = "C", ValorTipoIdentificacion="CC"},
                new TipoDocumento(){ IdTipoIdentificacion = "T", ValorTipoIdentificacion="TI"},
                new TipoDocumento(){ IdTipoIdentificacion = "R", ValorTipoIdentificacion="RC"},
                new TipoDocumento(){ IdTipoIdentificacion = "P", ValorTipoIdentificacion="PS"},
                new TipoDocumento(){ IdTipoIdentificacion = "E", ValorTipoIdentificacion = "CE"},
                 new TipoDocumento(){ IdTipoIdentificacion = "N", ValorTipoIdentificacion = "NIT"}
        };
        return lista_Tipo_Identificacion;
    }

    public string ListarPorId(string idTipoDoc)
    {
        string documento = "9999";
        List<TipoDocumento> objTpDoc = new List<TipoDocumento>();
        objTpDoc = Listar();
        foreach (var item in objTpDoc)
        {
            if (item.IdTipoIdentificacion == idTipoDoc)
            {
                documento = item.ValorTipoIdentificacion.ToString();
            }
        }
        return documento;
    }
    public string Encontrar_valorTipoID(string tipodoc)
    {
        string documento = "9999";
        List<TipoDocumento> objTpDoc = new List<TipoDocumento>();
        objTpDoc = Listar();
        foreach (var item in objTpDoc)
        {
            if (item.valorTipoIdentificacion == tipodoc)
            {
                documento = item.idTipoIdentificacion.ToString();
            }
        }
        return documento;
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
            case "NIT":
                respuesta = "N";
                break;
        }
        return respuesta;
    }


}





public class OperacionesInternacionales
{
    string idOperacionesInt;
    string valorOperacionInt;

    public string IdOperacionesInt
    {
        get
        {
            return idOperacionesInt;
        }

        set
        {
            idOperacionesInt = value;
        }
    }

    public string ValorOperacionesInt
    {
        get
        {
            return valorOperacionInt;
        }

        set
        {
            valorOperacionInt = value;
        }
    }

    public List<OperacionesInternacionales> Listar()
    {
        List<OperacionesInternacionales> lista_Operaciones_Int = new List<OperacionesInternacionales>(){
                new OperacionesInternacionales(){ IdOperacionesInt = "1000001", ValorOperacionesInt="SI"},
                new OperacionesInternacionales(){ IdOperacionesInt = "1000002", ValorOperacionesInt="NO"},
                


        };
        return lista_Operaciones_Int;
    }

    public string ListarPorId(string idOperacionesInt)
    {
        string documento = "9999";
        List<OperacionesInternacionales> objTpOpe = new List<OperacionesInternacionales>();
        objTpOpe = Listar();
        foreach (var item in objTpOpe)
        {
            if (item.IdOperacionesInt == idOperacionesInt)
            {
                documento = item.ValorOperacionesInt.ToString();
            }
        }
        return documento;
    }
    public string Encontrar_valorTipoOp(string tipoOp)
    {
        string documento = "9999";
        List<OperacionesInternacionales> objTpOpe = new List<OperacionesInternacionales>();
        objTpOpe = Listar();
        foreach (var item in objTpOpe)
        {
            if (item.ValorOperacionesInt == tipoOp)
            {
                documento = item.idOperacionesInt.ToString();
            }
        }
        return documento;
    }

    public string getOperaciones(string tipoOperac)
    {
        string respuesta = "";
        tipoOperac = !string.IsNullOrEmpty(tipoOperac) ? tipoOperac : "SI";
        switch (tipoOperac)
        {
            case "SI":
                respuesta = "Y";
                break;
            case "NO":
                respuesta = "N";
                break;
        
        }
        return respuesta;
    }


}

