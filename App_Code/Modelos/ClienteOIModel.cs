using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

/// <summary>
/// Summary description for ClienteTPModel
/// </summary>
public class ClienteOIModel : RepositoryOracle
{

    public ClienteOIModel()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Boolean CampoNoVacio(string objeto)
    {
        bool resultado = string.IsNullOrEmpty(objeto);
        return resultado;
    }
    public string agregaCampo(string columna, string valorColumna, string sentencia)
    {
        string rsentencia = "";
        string validaW = sentencia.Substring(sentencia.Length - 6, 6).Trim();
        if (!validaW.Contains("WHERE") && !string.IsNullOrEmpty(valorColumna))
        {
            rsentencia = " AND ";

        }
        if (!string.IsNullOrEmpty(valorColumna))
        {
            rsentencia += columna + " = UPPER('" + valorColumna + "')";
        }

        return rsentencia;
    }
    public List<DetallesTPEntity> TraerClienteAvanzado_CRWORK_FIDEL(DetallesTPEntity Value)
    {
        List<DetallesTPEntity> resultado = new List<DetallesTPEntity>();

        //if (Value.USUARIOAPLICACION == "Usuario sin login")
        //{
        //    Value.USUARIOAPLICACION = "'999'";
        //}
        ContextOracle.Open();
        string sentencia = "SELECT TIPO_IDENTIF,NUMERO_IDENTIF,PN_NOMBRE1,PN_NOMBRE2,PN_APELLIDO1 , PN_APELLIDO2, (SELECT VALUE_DISPLAY FROM CRWORK.PS_AV_PAR_GRAL_DTL WHERE PARAM_ID = 'SEGMENTOS' AND VALUE_NBR = SEGMENTO) SEGMENTO, " +
            "(SELECT DESCR FROM CRWORK.PS_AV_PAR_GRAL_DTL WHERE PARAM_ID = 'ESTADO_CRM' AND VALUE_DISPLAY = ESTADO_CRM) ESTADO_CRM, FECHA_ACTUALIZACION,FECHA_CREACION, (SELECT CASE AA_DIVISAS WHEN 'Y' THEN 'SI' WHEN 'N' THEN 'NO' ELSE ' ' END FROM " +
            "DIVISAS.PS_AA_PN_OPR_INTER WHERE NRO_IDENTIDAD = NUMERO_IDENTIF) IND_OPE_INTER,PJ_RAZON_SOCIAL FROM CRWORK.AV_CR640_TBL ";


      



        switch (Value.TIPODOCUMENTO)
        {
            case "CC":
                Value.TIPODOCUMENTO = "C";
                break;
            case "CE":
                Value.TIPODOCUMENTO = "E";
                break;
            case "TI":
                Value.TIPODOCUMENTO = "T";
                break;
            case "RC":
                Value.TIPODOCUMENTO = "R";
                break;
            case "NIT":
                Value.TIPODOCUMENTO = "N";
                break;
        }
        if (!CampoNoVacio(Value.TIPODOCUMENTO) || !CampoNoVacio(Value.NRODOCUMENTO) || !CampoNoVacio(Value.PRIMERNOMBRE) || !CampoNoVacio(Value.SEGUNDONOMBRE) || !CampoNoVacio(Value.PRIMERAPELLIDO)
             || !CampoNoVacio(Value.SEGUNDOAPLELLIDO) || !CampoNoVacio(Value.ESTADOCLIENTE))
        {
            sentencia += " WHERE ";
        }
        if (CampoNoVacio(Value.TIPODOCUMENTO) && CampoNoVacio(Value.NRODOCUMENTO) && CampoNoVacio(Value.PRIMERNOMBRE) && CampoNoVacio(Value.SEGUNDONOMBRE) && CampoNoVacio(Value.PRIMERAPELLIDO)
             && CampoNoVacio(Value.SEGUNDOAPLELLIDO) && CampoNoVacio(Value.ESTADOCLIENTE))
        {
            sentencia += " WHERE NUMERO_IDENTIF=''";

        }




      

        sentencia += agregaCampo("TIPO_IDENTIF", Value.TIPODOCUMENTO, sentencia);
        sentencia += agregaCampo("NUMERO_IDENTIF", Value.NRODOCUMENTO, sentencia);
        sentencia += agregaCampo("PN_NOMBRE1", Value.PRIMERNOMBRE, sentencia);
        sentencia += agregaCampo("PN_NOMBRE2", Value.SEGUNDONOMBRE, sentencia);
        sentencia += agregaCampo("PN_APELLIDO1", Value.PRIMERAPELLIDO, sentencia);
        sentencia += agregaCampo("PN_APELLIDO2", Value.SEGUNDOAPLELLIDO, sentencia);
        sentencia += agregaCampo("SEGMENTO", Value.CODSEGMENTO, sentencia);
        sentencia += agregaCampo("ESTADO_CRM", Value.ESTADOCLIENTE, sentencia);
        sentencia += agregaCampo("FECHA_ACTUALIZACION", Value.FECHAULTIMAACTUALIZACION, sentencia);
        sentencia += agregaCampo("FECHA_CREACION", Value.FECHACREACION, sentencia);
        sentencia += agregaCampo("IND_OPE_INTER", Value.OPERACIONES_INTER, sentencia);





        OracleCommand command = new OracleCommand(sentencia, ContextOracle);
        // llamado a los procedimientos
        OracleDataReader reader = null;
        try
        {


            reader = command.ExecuteReader();


          





            if (reader.HasRows)
            {
                while (reader.Read())
                {


                    DetallesTPEntity cli = new DetallesTPEntity()
                    {
                        TIPODOCUMENTO = reader.GetValue(0).ToString(),
                        NRODOCUMENTO = reader.GetValue(1).ToString(),
                        PRIMERNOMBRE = reader.GetValue(2).ToString(),
                        SEGUNDONOMBRE = reader.GetValue(3).ToString(),
                        PRIMERAPELLIDO = reader.GetValue(4).ToString(),
                        SEGUNDOAPLELLIDO = reader.GetValue(5).ToString(),
                        CODSEGMENTO = reader.GetValue(6).ToString(),
                       
                        ESTADOCLIENTE = reader.GetValue(7).ToString(),
                        FECHAULTIMAACTUALIZACION = reader.GetValue(8).ToString(),
                        FECHACREACION = reader.GetValue(9).ToString(),
                        OPERACIONES_INTER = reader.GetValue(10).ToString(),
                        RAZON_SOCIAL= reader.GetValue(11).ToString()

                };

                   

                    resultado.Add(cli);
                }
            }
            ContextOracle.Close();
        }
        catch (Exception error)
        {

        }

        // retornar datos
        reader.Close();
        ContextOracle.Close();
        return resultado;
    }







    public List<DetallesTPEntity> TraerDetalleAutorizaciones_CRWORK_FIDEL(DetallesTPEntity Value)
    {
        List<DetallesTPEntity> resultado = new List<DetallesTPEntity>();

        //if (Value.USUARIOAPLICACION == "Usuario sin login")
        //{
        //    Value.USUARIOAPLICACION = "'999'";
        //}
        ContextOracle.Open();
        string sentencia = "SELECT NRO_IDENTIDAD, TIPO_IDENTIDAD, SEQ_NBR,AA_AUTORIZA_DIVISA,AA_TIPO_VIGENCIA,AA_SOPORTE_DIVISA,DATE_END,AA_MONTO_PROM_VENT," +
            "AA_MONTO_PROMEDIO,ROW_ADDED_DTTM,ROW_ADDED_OPRID,ROW_LASTMANT_DTTM,ROW_LASTMANT_OPRID,AA_BLOQUEO_DIVISAS  FROM DIVISAS.PS_AA_PN_OPR_INTDT";






        switch (Value.TIPODOCUMENTO)
        {
            case "CC":
                Value.TIPODOCUMENTO = "C";
                break;
            case "CE":
                Value.TIPODOCUMENTO = "E";
                break;
            case "TI":
                Value.TIPODOCUMENTO = "T";
                break;
            case "RC":
                Value.TIPODOCUMENTO = "R";
                break;
        }
        if (!CampoNoVacio(Value.TIPODOCUMENTO) || !CampoNoVacio(Value.NRODOCUMENTO) )
        {
            sentencia += " WHERE ";
        }


        sentencia += agregaCampo("NRO_IDENTIDAD", Value.NRODOCUMENTO, sentencia);
        sentencia += agregaCampo("TIPO_IDENTIDAD", Value.TIPODOCUMENTO, sentencia);
       





        OracleCommand command = new OracleCommand(sentencia, ContextOracle);
        // llamado a los procedimientos
        OracleDataReader reader = null;
        try
        {


            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {


                    DetallesTPEntity cli = new DetallesTPEntity()
                    {


                        NRODOCUMENTO = reader.GetValue(0).ToString(),
                        TIPODOCUMENTO = reader.GetValue(1).ToString(),
                        SEQNBR = reader.GetValue(2).ToString(),
                        AUTORIZADIVISA = reader.GetValue(3).ToString(),
                        TIPOVIGENCIA= reader.GetValue(4).ToString(),
                        SOPORTEDIVISA = reader.GetValue(5).ToString(),
                        DATEEND = reader.GetValue(6).ToString(),
                        MONTOPROMVENT = reader.GetValue(7).ToString(),
                        MONTOPROMEDIO = reader.GetValue(8).ToString(),
                        ROWADDEDDTTM = reader.GetValue(9).ToString(),
                        ROWADDEDOPPRID = reader.GetValue(10).ToString(),
                        ROWLASTMANDTTM = reader.GetValue(11).ToString(),
                        ROWLASTMANOPPRID= reader.GetValue(12).ToString(),
                        BLOQUEODIVISAS = reader.GetValue(13).ToString()

                    };



                    resultado.Add(cli);
                }
            }
            ContextOracle.Close();
        }
        catch (Exception error)
        {

        }

        // retornar datos
        reader.Close();
        ContextOracle.Close();
        return resultado;
    }








    public List<DetallesTPEntity> TraerEntidadOperaciones_CRWORK_FIDEL(DetallesTPEntity Value)
    {
        List<DetallesTPEntity> resultado = new List<DetallesTPEntity>();

        //if (Value.USUARIOAPLICACION == "Usuario sin login")
        //{
        //    Value.USUARIOAPLICACION = "'999'";
        //}
        ContextOracle.Open();
        string sentencia = "SELECT NRO_IDENTIDAD, TIPO_IDENTIDAD, SEQ_NBR,AA_AUTORIZA_DIVISA,AA_TIPO_VIGENCIA,AA_SOPORTE_DIVISA,DATE_END,AA_MONTO_PROM_VENT," +
            "AA_MONTO_PROMEDIO,ROW_ADDED_DTTM,ROW_ADDED_OPRID,ROW_LASTMANT_DTTM,ROW_LASTMANT_OPRID,AA_BLOQUEO_DIVISAS  FROM DIVISAS.PS_AA_PN_OPR_INTDT";






        switch (Value.TIPODOCUMENTO)
        {
            case "CC":
                Value.TIPODOCUMENTO = "C";
                break;
            case "CE":
                Value.TIPODOCUMENTO = "E";
                break;
            case "TI":
                Value.TIPODOCUMENTO = "T";
                break;
            case "RC":
                Value.TIPODOCUMENTO = "R";
                break;
        }
        if (!CampoNoVacio(Value.TIPODOCUMENTO) || !CampoNoVacio(Value.NRODOCUMENTO))
        {
            sentencia += " WHERE ";
        }


        sentencia += agregaCampo("NRO_IDENTIDAD", Value.NRODOCUMENTO, sentencia);
        sentencia += agregaCampo("TIPO_IDENTIDAD", Value.TIPODOCUMENTO, sentencia);






        OracleCommand command = new OracleCommand(sentencia, ContextOracle);
        // llamado a los procedimientos
        OracleDataReader reader = null;
        try
        {


            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {


                    DetallesTPEntity cli = new DetallesTPEntity()
                    {


                        NRODOCUMENTO = reader.GetValue(0).ToString(),
                        TIPODOCUMENTO = reader.GetValue(1).ToString(),
                        SEQNBR = reader.GetValue(2).ToString(),
                        AUTORIZADIVISA = reader.GetValue(3).ToString(),
                        TIPOVIGENCIA = reader.GetValue(4).ToString(),
                        SOPORTEDIVISA = reader.GetValue(5).ToString(),
                        DATEEND = reader.GetValue(6).ToString(),
                        MONTOPROMVENT = reader.GetValue(7).ToString(),
                        MONTOPROMEDIO = reader.GetValue(8).ToString(),
                        ROWADDEDDTTM = reader.GetValue(9).ToString(),
                        ROWADDEDOPPRID = reader.GetValue(10).ToString(),
                        ROWLASTMANDTTM = reader.GetValue(11).ToString(),
                        ROWLASTMANOPPRID = reader.GetValue(12).ToString(),
                        BLOQUEODIVISAS = reader.GetValue(13).ToString()

                    };



                    resultado.Add(cli);
                }
            }
            ContextOracle.Close();
        }
        catch (Exception error)
        {

        }

        // retornar datos
        reader.Close();
        ContextOracle.Close();
        return resultado;
    }





    public List<DetallesTPEntity> TraerClienteAvanzado_CRWORK(DetallesTPEntity Value)
    {
        List<DetallesTPEntity> resultado = new List<DetallesTPEntity>();

        if (Value.USUARIOAPLICACION == "Usuario sin login")
        {
            Value.USUARIOAPLICACION = "999";
        }
        ContextOracle.Open();
        string sentencia = "SELECT TIPO_IDENTIF,NUMERO_IDENTIF,PN_NOMBRE1,PN_NOMBRE2,PN_APELLIDO1 , PN_APELLIDO2, " +
            "(SELECT VALUE_DISPLAY FROM CRWORK.PS_AV_PAR_GRAL_DTL WHERE PARAM_ID = 'SEGMENTOS' AND VALUE_NBR = SEGMENTO) SEGMENTO,  " +
            " (SELECT DESCR FROM CRWORK.PS_RSF_TERRITORY WHERE TERRITORY_ID = (SELECT AA_OFI_ASIGNADA FROM CRWORK.PS_AA_PLANTA WHERE AA_FUNC_TIMBRE = '" + Value.USUARIOAPLICACION + "' )) OFICINA_ASIGNADA, " +
            "(SELECT DESCR FROM CRWORK.PS_AV_PAR_GRAL_DTL WHERE PARAM_ID = 'ESTADO_CRM' AND VALUE_DISPLAY = ESTADO_CRM) ESTADO_CRM, " +
            "FECHA_ACTUALIZACION,FECHA_CREACION  " +
            "FROM CRWORK.AV_CR640_TBL ";

        switch (Value.TIPODOCUMENTO)
        {
            case "CC":
                Value.TIPODOCUMENTO = "C";
                break;
            case "CE":
                Value.TIPODOCUMENTO = "E";
                break;
            case "TI":
                Value.TIPODOCUMENTO = "T";
                break;
            case "RC":
                Value.TIPODOCUMENTO = "R";
                break;
        }
        if (!CampoNoVacio(Value.TIPODOCUMENTO) || !CampoNoVacio(Value.NRODOCUMENTO) || !CampoNoVacio(Value.PRIMERNOMBRE) || !CampoNoVacio(Value.SEGUNDONOMBRE) || !CampoNoVacio(Value.PRIMERAPELLIDO)
             || !CampoNoVacio(Value.SEGUNDOAPLELLIDO) || !CampoNoVacio(Value.ESTADOCLIENTE))
        {
            sentencia += " WHERE ";
        }


        sentencia += agregaCampo("TIPO_IDENTIF", Value.TIPODOCUMENTO, sentencia);
        sentencia += agregaCampo("NUMERO_IDENTIF", Value.NRODOCUMENTO, sentencia);
        sentencia += agregaCampo("PN_NOMBRE1", Value.PRIMERNOMBRE, sentencia);
        sentencia += agregaCampo("PN_NOMBRE2", Value.SEGUNDONOMBRE, sentencia);
        sentencia += agregaCampo("PN_APELLIDO1", Value.PRIMERAPELLIDO, sentencia);
        sentencia += agregaCampo("PN_APELLIDO2", Value.SEGUNDOAPLELLIDO, sentencia);
        sentencia += agregaCampo("SEGMENTO", Value.CODSEGMENTO, sentencia);
        sentencia += agregaCampo("ESTADO_CRM", Value.ESTADOCLIENTE, sentencia);
        sentencia += agregaCampo("FECHA_ACTUALIZACION", Value.FECHAULTIMAACTUALIZACION, sentencia);
        sentencia += agregaCampo("FECHA_CREACION", Value.FECHACREACION, sentencia);





        OracleCommand command = new OracleCommand(sentencia, ContextOracle);
        // llamado a los procedimientos
        OracleDataReader reader = null;
        try
        {


            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {


                    DetallesTPEntity cli = new DetallesTPEntity()
                    {
                        TIPODOCUMENTO = reader.GetValue(0).ToString(),
                        NRODOCUMENTO = reader.GetValue(1).ToString(),
                        PRIMERNOMBRE = reader.GetValue(2).ToString(),
                        SEGUNDONOMBRE = reader.GetValue(3).ToString(),
                        PRIMERAPELLIDO = reader.GetValue(4).ToString(),
                        SEGUNDOAPLELLIDO = reader.GetValue(5).ToString(),
                        CODSEGMENTO = reader.GetValue(6).ToString(),
                        CODOFICINA = reader.GetValue(7).ToString(),
                        ESTADOCLIENTE = reader.GetValue(8).ToString(),
                        FECHAULTIMAACTUALIZACION = reader.GetValue(9).ToString(),
                        FECHACREACION = reader.GetValue(10).ToString()

                    };



                    resultado.Add(cli);
                }
            }
            ContextOracle.Close();
        }
        catch (Exception error)
        {

        }

        // retornar datos
        reader.Close();
        ContextOracle.Close();
        return resultado;
    }
    public Int32 TraerClienteAvanzadoFIDEL_TP(DetallesTPEntity Value)
    {
        int contador = 0;
        ContextOracle.Open();
        string sentencia = "SELECT COUNT(*) FROM TUPLUS.PS_AA_CU_FIDEL_TBL WHERE AA_NRO_DOCUMENTO = '" + Value.NRODOCUMENTO + "' AND AA_TIPO_DOC = '" + Value.TIPODOCUMENTO + "'";

        OracleCommand command = new OracleCommand(sentencia, ContextOracle);
        // llamado a los procedimientos
        OracleDataReader reader = null;
        try
        {


            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                contador = Convert.ToInt32(reader.GetValue(0).ToString());
            }
            ContextOracle.Close();
        }
        catch (Exception error)
        {

        }

        // retornar datos
        reader.Close();
        ContextOracle.Close();
        return contador;
    }
    internal bool CrearCliente(DetallesTPEntity valor)
    {
        bool respuesta = false;
        if (valor.USUARIOAPLICACION == "Usuario sin login")
        {
            valor.USUARIOAPLICACION = "999";
        }
        // instancia del procedimiento
        OracleCommand command = new OracleCommand("TUPLUS.AV_CREAR_CLIENTETP", ContextOracle);
        command.CommandType = CommandType.StoredProcedure;

        // parametros de entrada
        command.Parameters.Add(new OracleParameter("v_tipo_documento", OracleType.VarChar)).Value = valor.TIPODOCUMENTO;
        command.Parameters.Add(new OracleParameter("v_documento", OracleType.VarChar)).Value = valor.NRODOCUMENTO;
        command.Parameters.Add(new OracleParameter("v_usuario", OracleType.VarChar)).Value = valor.USUARIOAPLICACION;
        command.Parameters.Add(new OracleParameter("v_oficina", OracleType.VarChar)).Value = valor.CODOFICINA;


        //parametros de salida
        command.Parameters.Add(new OracleParameter("respuesta", OracleType.Number));
        command.Parameters["respuesta"].Direction = ParameterDirection.Output;

        //llamado al procedimiento
        ContextOracle.Open();
        OracleDataAdapter da = new OracleDataAdapter(command);
        command.ExecuteNonQuery();


        //valor retornado
        if (Convert.ToInt32(command.Parameters["respuesta"].Value.ToString()) > 0)
        {
            respuesta = true;
        }

        ContextOracle.Close();
        respuesta = true;
        return respuesta;
    }

    internal bool ExisteClienteTuPlus(DetallesTPEntity valor)
    {
        bool respuesta = false;
        int contador = 0;
        ContextOracle.Open();
        string sentencia = "SELECT * FROM TUPLUS.PS_AA_CU_FIDEL_TBL " +
            "WHERE AA_NRO_DOCUMENTO = '" + valor.NRODOCUMENTO + "' AND " +
            "AA_TIPO_DOC = '" + valor.TIPODOCUMENTO + "'";

        OracleCommand command = new OracleCommand(sentencia, ContextOracle);

        // llamado a los procedimientos
        OracleDataReader reader = null;
        reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            respuesta = true;
        }
        else
        {
            respuesta = false;
        }

        // retornar datos
        reader.Close();
        ContextOracle.Close();

        return respuesta;
    }
    internal bool ValidarClienteCRM(DetallesTPEntity valor)
    {
        bool respuesta = false;

        ContextOracle.Open();
        string sentencia = "SELECT * FROM CRWORK.AV_CR640_TBL " +
            "WHERE NUMERO_IDENTIF = '" + valor.NRODOCUMENTO + "' AND " +
            "TIPO_IDENTIF = '" + valor.TIPODOCUMENTOCR + "'";


        OracleCommand command = new OracleCommand(sentencia, ContextOracle);

        // llamado a los procedimientos
        OracleDataReader reader = null;
        reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                if (Convert.ToInt32(reader.GetValue(0).ToString()) > 0)
                {
                    respuesta = true;
                }
            }
        }

        // retornar datos
        reader.Close();
        ContextOracle.Close();

        return respuesta;
    }


    public string validaPoliticastp(DetallesTPEntity Value)
    {
        string respuestaPolitica = "";
        // instancia del procedimiento
        OracleCommand command = new OracleCommand("TUPLUS.AV_VALIDA_POLITICAS_TUPLUS", ContextOracle);
        command.CommandType = CommandType.StoredProcedure;

        // parametros de entrada
        command.Parameters.Add(new OracleParameter("TIPODOC", OracleType.VarChar)).Value = Value.TIPODOCUMENTO;
        command.Parameters.Add(new OracleParameter("NUMERO_DOCUMENTO", OracleType.VarChar)).Value = Value.NRODOCUMENTO;

        //parametros de salida

        command.Parameters.Add(new OracleParameter("respuesta", OracleType.VarChar, 300));
        command.Parameters["respuesta"].Direction = ParameterDirection.Output;

        //llamado al procedimiento
        ContextOracle.Open();
        OracleDataAdapter da = new OracleDataAdapter(command);
        command.ExecuteNonQuery();
        respuestaPolitica = command.Parameters[2].Value.ToString();

        //valor retornado
        ContextOracle.Close();
        return respuestaPolitica;

    }
}




public class UsuarioSesion
{
    private string _NROIDENTIDAD;
    private string _TIPOIDENTIDAD;
    private string _NOMBRECOMPLETO;
    private string _JOBCODE;




    public string JOBCODE
    {
        get
        {
            return _JOBCODE;
        }

        set
        {
            _JOBCODE = value;
        }
    }

    public string NOMBRECOMPLETO
    {
        get
        {
            return _NOMBRECOMPLETO;
        }

        set
        {
            _NOMBRECOMPLETO = value;
        }
    }

    public string NROIDENTIDAD
    {
        get
        {
            return _NROIDENTIDAD;
        }

        set
        {
            _NROIDENTIDAD = value;
        }
    }

    public string TIPOIDENTIDAD
    {
        get
        {
            return _TIPOIDENTIDAD;
        }

        set
        {
            _TIPOIDENTIDAD = value;
        }
    }
}









public class DetallesTPEntity
{
    private string _USUARIOAPLICACION;
    private string _NRODOCUMENTO;
    private string _TIPODOCUMENTO;
    private string _TIPODOCUMENTOCR;
    private string _PRIMERNOMBRE;
    private string _SEGUNDONOMBRE;
    private string _PRIMERAPELLIDO;
    private string _SEGUNDOAPLELLIDO;
    private string _CODSEGMENTO;
    private string _CODOFICINA;
    private string _ESTADOCLIENTE;
    private string _FECHAULTIMAACTUALIZACION;
    private string _FECHACREACION;
    private string _OPERACIONES_INTER;
    private string _SEQNBR;
    private string _AUTORIZADIVISA;
    private string _TIPOVIGENCIA;
    private string _SOPORTEDIVISA;
    private string _DATEEND;
    private string _MONTOPROMVENT;
    private string _MONTOPROMEDIO;
    private string _ROWADDEDDTTM;
    private string _ROWADDEDOPPRID;
    private string _ROWLASTMANDTTM;
    private string _ROWLASTMANOPPRID;

    private string _RAZON_SOCIAL;
    private string _BLOQUEODIVISAS;

    private string _VALUE_NBR;
    private bool _AUTORIZADIVISAS;


    public string RAZON_SOCIAL

     {
        get
        {
            return _RAZON_SOCIAL;
        }

        set
        {
            _RAZON_SOCIAL = value;
        }
    }
    

      public string VALUE_NBR
    {
        get
        {
            return _VALUE_NBR;
        }

        set
        {
            _VALUE_NBR = value;
        }
    }




    public bool AUTORIZADIVISAS


    {
        get
        {
            return _AUTORIZADIVISAS;
        }

        set
        {
            _AUTORIZADIVISAS = value;
        }
    }


    public string NRODOCUMENTO


    {
        get
        {
            return _NRODOCUMENTO;
        }

        set
        {
            _NRODOCUMENTO = value;
        }
    }

    public string TIPODOCUMENTO
    {
        get
        {
            return _TIPODOCUMENTO;
        }

        set
        {
            _TIPODOCUMENTO = value;
        }
    }

    public string PRIMERNOMBRE
    {
        get
        {
            return _PRIMERNOMBRE;
        }

        set
        {
            _PRIMERNOMBRE = value;
        }
    }

    public string SEGUNDONOMBRE
    {
        get
        {
            return _SEGUNDONOMBRE;
        }

        set
        {
            _SEGUNDONOMBRE = value;
        }
    }

    public string PRIMERAPELLIDO
    {
        get
        {
            return _PRIMERAPELLIDO;
        }

        set
        {
            _PRIMERAPELLIDO = value;
        }
    }

    public string PRIMERAPELLIDO1
    {
        get
        {
            return _PRIMERAPELLIDO;
        }

        set
        {
            _PRIMERAPELLIDO = value;
        }
    }

    public string SEGUNDOAPLELLIDO
    {
        get
        {
            return _SEGUNDOAPLELLIDO;
        }

        set
        {
            _SEGUNDOAPLELLIDO = value;
        }
    }

    public string CODSEGMENTO
    {
        get
        {
            return _CODSEGMENTO;
        }

        set
        {
            _CODSEGMENTO = value;
        }
    }

    public string ESTADOCLIENTE
    {
        get
        {
            return _ESTADOCLIENTE;
        }

        set
        {
            _ESTADOCLIENTE = value;
        }
    }

    public string FECHAULTIMAACTUALIZACION
    {
        get
        {
            return _FECHAULTIMAACTUALIZACION;
        }

        set
        {
            _FECHAULTIMAACTUALIZACION = value;
        }
    }

    public string FECHACREACION
    {
        get
        {
            return _FECHACREACION;
        }

        set
        {
            _FECHACREACION = value;
        }
    }

    public string CODOFICINA
    {
        get
        {
            return _CODOFICINA;
        }

        set
        {
            _CODOFICINA = value;
        }
    }

    public string TIPODOCUMENTOCR
    {
        get
        {
            return _TIPODOCUMENTOCR;
        }

        set
        {
            _TIPODOCUMENTOCR = value;
        }
    }

    public string USUARIOAPLICACION
    {
        get
        {
            return _USUARIOAPLICACION;
        }

        set
        {
            _USUARIOAPLICACION = value;
        }
    }



    public string OPERACIONES_INTER
    {
        get
        {
            return _OPERACIONES_INTER;
        }

        set
        {
            _OPERACIONES_INTER = value;
        }
    }


    
          public string SEQNBR
    {
        get
        {
            return _SEQNBR;
        }

        set
        {
            _SEQNBR = value;
        }
    }

    


          public string AUTORIZADIVISA
    {
        get
        {
            return _AUTORIZADIVISA;
        }

        set
        {
            _AUTORIZADIVISA = value;
        }
    }


    public string TIPOVIGENCIA
    {
        get
        {
            return _TIPOVIGENCIA;
        }

        set
        {
            _TIPOVIGENCIA = value;
        }
    }



    public string SOPORTEDIVISA
    {
        get
        {
            return _SOPORTEDIVISA;
        }

        set
        {
            _SOPORTEDIVISA = value;
        }
    }




    public string DATEEND
    {
        get
        {
            return _DATEEND;
        }

        set
        {
            _DATEEND = value;
        }
    }


    public string MONTOPROMVENT
    {
        get
        {
            return _MONTOPROMVENT;
        }

        set
        {
            _MONTOPROMVENT = value;
        }
    }

    public string MONTOPROMEDIO
    {
        get
        {
            return _MONTOPROMEDIO;
        }

        set
        {
            _MONTOPROMEDIO = value;
        }
    }

    public string ROWADDEDDTTM
    {
        get
        {
            return _ROWADDEDDTTM;
        }

        set
        {
            _ROWADDEDDTTM = value;
        }
    }


    public string ROWADDEDOPPRID
    {
        get
        {
            return _ROWADDEDOPPRID;
        }

        set
        {
            _ROWADDEDOPPRID = value;
        }
    }



    public string ROWLASTMANDTTM
    {
        get
        {
            return _ROWLASTMANDTTM;
        }

        set
        {
            _ROWLASTMANDTTM = value;
        }
    }




    public string ROWLASTMANOPPRID
    {
        get
        {
            return _ROWLASTMANOPPRID;
        }

        set
        {
            _ROWLASTMANOPPRID = value;
        }
    }


    public string BLOQUEODIVISAS
    {
        get
        {
            return _BLOQUEODIVISAS;
        }

        set
        {
            _BLOQUEODIVISAS = value;
        }
    }

}






                     
                       
                       