using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ParametroModel
/// </summary>
public class ParametroDivModel : RepositoryOracle
{
    public ParametroDivModel()
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


    public List<ParamaetroOpDivisas> TraerParametros(ParamaetroOpDivisas value)
    {

        string AA_AUTORIZACION = "SI";
        // respuesta de el metodo
        bool autorizacionValida = false;
        DateTime dt = new DateTime(1900, 1, 1);
        string lastOprid;

        // respuesta de el metodo
        ParametrosDivisas lista = new ParametrosDivisas();

        List<ParamaetroOpDivisas> Lista = new List<ParamaetroOpDivisas>();

        List<ParamaetroOpDivisas> parametros = new List<ParamaetroOpDivisas>();
        ParametroDivModel param2 = new ParametroDivModel();


        if (value.TIPODOCUMENTO != "N")
        {


            ContextOracle.Open();
            string sentencia = "SELECT NRO_IDENTIDAD,TIPO_IDENTIDAD,SEQ_NBR, CASE AA_AUTORIZA_DIVISA WHEN 'Y' THEN 'SI' WHEN 'N' THEN 'NO' END AS AA_AUTORIZA_DIVISA," +
                "CASE AA_TIPO_VIGENCIA WHEN 'P' THEN 'Puntual' WHEN 'M' THEN 'Mensual' END AS AA_TIPO_VIGENCIA,AA_SOPORTE_DIVISA,DATE_END,AA_MONTO_PROM_VENT,AA_MONTO_PROMEDIO," +
                "ROW_ADDED_DTTM,ROW_ADDED_OPRID,ROW_LASTMANT_DTTM,ROW_LASTMANT_OPRID,AA_BLOQUEO_DIVISAS,AA_ESTADO_AUTORIZA,AA_AUTORIZACION_DIV FROM DIVISAS.PS_AA_PN_OPR_INTDT ";


            string sentencia2 = "SELECT COUNT(*) FROM DIVISAS.PS_AA_PN_OPR_INTDT ";

            // validar parametro
            if (!string.IsNullOrEmpty(value.NRODOCUMENTO))
            {
                sentencia = sentencia + " where UPPER(NRO_IDENTIDAD) LIKE UPPER('" + value.NRODOCUMENTO + "')  ";
                sentencia2 = sentencia2 + " where UPPER(NRO_IDENTIDAD) LIKE UPPER('" + value.NRODOCUMENTO + "')  ";
            }

            OracleCommand command = new OracleCommand(sentencia, ContextOracle);
            OracleCommand command2 = new OracleCommand(sentencia2, ContextOracle);

            // llamado a los procedimientos
            OracleDataReader reader = null;
            OracleDataReader reader2 = null;
            try
            {

                reader2 = command2.ExecuteReader();
                reader = command.ExecuteReader();

                if (reader.HasRows && reader2.HasRows)
                {
                    while (reader.Read() && reader2.Read())
                    {


                        ParamaetroOpDivisas param = new ParamaetroOpDivisas()
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
                            BLOQUEODIVISAS = reader.GetValue(13).ToString(),
                            AA_ESTADO_AUTORIZA = reader.GetValue(14).ToString(),
                            AA_AUTORIZACION_DIV = reader.GetValue(15).ToString(),
                            TOTAL = reader2.GetValue(0).ToString(),
                        };

                        if (!string.IsNullOrEmpty(reader.GetValue(6).ToString()))
                        {
                            //param.DATEEND = reader.GetValue(6).ToString();


                            param.DATEEND = Convert.ToDateTime(reader.GetValue(6)).Day + "/" + Convert.ToDateTime(reader.GetValue(6)).Month.ToString("00") + "/" + Convert.ToDateTime(reader.GetValue(6)).Year;



                        }




                        param.BloqueoGlobal = "SI";

                        autorizacionValida = false;
                        param.AA_AUTORIZACION = "SI";

                        var DATEEND_ = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(6).ToString()));
                        var ROW_LASTMANT_DTTM = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(6).ToString()));
                        var dias = (DATEEND_ - DateTime.Now).Days;
                        var AUTORIZADIVISA = reader.GetValue(3).ToString();


                        if (dias >= 0 && AUTORIZADIVISA.Equals("SI"))
                        {
                            autorizacionValida = true;
                            var dias2 = (dt - ROW_LASTMANT_DTTM).Days;
                            if (dias2 >= 0)
                            {
                                dt = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(11).ToString()));
                                lastOprid = reader.GetValue(12).ToString();
                            }


                        }





                        if (!autorizacionValida)
                        {
                            param.AA_AUTORIZACION = "NO";
                        }


                        if (lista.ValidarListaBloqueo(param.NRODOCUMENTO))
                        {

                            param.AA_AUTORIZACION = "NO";
                            Lista = param2.BloqueoDateEnd(value.NRODOCUMENTO);



                            if (param.AA_ESTADO_AUTORIZA == "Vigente")
                            {
                                //var DATEEND2 = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", Lista[0].DATEEND));

                                //param.DATEEND = DATEEND2.ToString("dd/MM/yyyy");
                                param.DATEEND = Lista[0].DATEEND;
                                param.AA_AUTORIZACION = "SI";
                                //param.AA_ESTADO_AUTORIZA = "Vencido";
                                param.BloqueoGlobal = "SI";

                            }


                        }








                        if (parametros.Exists(x => x.AA_ESTADO_AUTORIZA == "Vigente"))
                        {
                            param.AA_AUTORIZACION = "SI";


                        }








                        parametros.Add(param);



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
            return parametros;

        }

        else
        {




            ContextOracle.Open();
            string sentencia = "SELECT NRO_IDENTIDAD,TIPO_IDENTIDAD,SEQ_NBR, CASE AA_AUTORIZA_DIVISA WHEN 'Y' THEN 'SI' WHEN 'N' THEN 'NO' END AS AA_AUTORIZA_DIVISA,CASE AA_TIPO_VIGENCIA WHEN 'P' THEN 'Puntual' WHEN 'M' THEN 'Mensual' END AS AA_TIPO_VIGENCIA,AA_SOPORTE_DIVISA,DATE_END,AA_MONTO_PROM_VENT,AA_MONTO_PROMEDIO,ROW_ADDED_DTTM,ROW_ADDED_OPRID,ROW_LASTMANT_DTTM,ROW_LASTMANT_OPRID,AA_BLOQUEO_DIVISAS,AA_ESTADO_AUTORIZA FROM DIVISAS.PS_AA_PJ_OPR_INTDT ";


            string sentencia2 = "SELECT COUNT(*) FROM DIVISAS.PS_AA_PJ_OPR_INTDT ";

            // validar parametro
            if (!string.IsNullOrEmpty(value.NRODOCUMENTO))
            {
                sentencia = sentencia + " where UPPER(NRO_IDENTIDAD) LIKE UPPER('" + value.NRODOCUMENTO + "')  ";
                sentencia2 = sentencia2 + " where UPPER(NRO_IDENTIDAD) LIKE UPPER('" + value.NRODOCUMENTO + "')  ";
            }

            OracleCommand command = new OracleCommand(sentencia, ContextOracle);
            OracleCommand command2 = new OracleCommand(sentencia2, ContextOracle);

            // llamado a los procedimientos
            OracleDataReader reader = null;
            OracleDataReader reader2 = null;

            try
            {

                reader2 = command2.ExecuteReader();
                reader = command.ExecuteReader();


                if (reader.HasRows && reader2.HasRows)
                {
                    while (reader.Read() && reader2.Read())
                    {


                        ParamaetroOpDivisas param = new ParamaetroOpDivisas()
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
                            BLOQUEODIVISAS = reader.GetValue(13).ToString(),
                            AA_ESTADO_AUTORIZA = reader.GetValue(14).ToString(),
                            TOTAL = reader2.GetValue(0).ToString(),


                        };

                        if (!string.IsNullOrEmpty(reader.GetValue(6).ToString()))
                        {
                            param.DATEEND = reader.GetValue(6).ToString();
                        }



                        autorizacionValida = false;
                        param.AA_AUTORIZACION = "SI";


                        var DATEEND_ = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(6).ToString()));
                        var ROW_LASTMANT_DTTM = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(6).ToString()));
                        var dias = (DateTime.Now - DATEEND_).Days;
                        var AUTORIZADIVISA = reader.GetValue(3).ToString();




                        if (lista.ValidarListaBloqueo(param.NRODOCUMENTO))
                        {

                            param.AA_AUTORIZACION = "NO";
                            Lista = param2.BloqueoDateEnd(value.NRODOCUMENTO);



                            if (param.AA_ESTADO_AUTORIZA == "Vigente")
                            {

                                param.DATEEND = Lista[0].DATEEND;
                                param.AA_AUTORIZACION = "NO";
                                param.BloqueoGlobal = "SI";


                            }


                        }

                        if (dias >= 0 && AUTORIZADIVISA.Equals("SI"))
                        {
                            autorizacionValida = true;
                            var dias2 = (dt - ROW_LASTMANT_DTTM).Days;
                            if (dias2 >= 0)
                            {
                                dt = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(11).ToString()));
                                lastOprid = reader.GetValue(12).ToString();
                            }


                        }


                        if (autorizacionValida)
                        {
                            param.AA_AUTORIZACION = "NO";
                        }


                        parametros.Add(param);
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
            return parametros;











        }








    }









    public List<ParamaetroOpDivisas> TraerOperacionesAut(string value)
    {

        
        // respuesta de el metodo
        bool autorizacionValida = false;
        DateTime dt = new DateTime(1900, 1, 1);
        string lastOprid;

        // respuesta de el metodo
        ParametrosDivisas lista = new ParametrosDivisas();

        List<ParamaetroOpDivisas> Lista = new List<ParamaetroOpDivisas>();

        List<ParamaetroOpDivisas> parametros = new List<ParamaetroOpDivisas>();
        ParametroDivModel param2 = new ParametroDivModel();



        ContextOracle.Open();
        string sentencia = "SELECT NRO_IDENTIDAD,TIPO_IDENTIDAD,SEQ_NBR, CASE AA_AUTORIZA_DIVISA WHEN 'Y' THEN 'SI' WHEN 'N' THEN 'NO' END " +
            "AS AA_AUTORIZA_DIVISA,CASE AA_TIPO_VIGENCIA WHEN 'P' THEN 'Puntual' WHEN 'M' THEN 'Mensual' END " +
            "AS AA_TIPO_VIGENCIA,AA_SOPORTE_DIVISA,DATE_END,AA_MONTO_PROM_VENT,AA_MONTO_PROMEDIO,ROW_ADDED_DTTM,ROW_ADDED_OPRID,ROW_LASTMANT_DTTM," +
            "ROW_LASTMANT_OPRID,AA_BLOQUEO_DIVISAS FROM DIVISAS.PS_AA_PN_OPR_INTDT ";

        // validar parametro
        if (!string.IsNullOrEmpty(value))
        {
            sentencia = sentencia + " where UPPER(NRO_IDENTIDAD) LIKE UPPER('" + value + "')  ";
        }

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


                    ParamaetroOpDivisas param = new ParamaetroOpDivisas()
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

                    if (!string.IsNullOrEmpty(reader.GetValue(6).ToString()))
                    {
                        param.DATEEND = reader.GetValue(6).ToString();
                    }





                    var DATEEND_ = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(6).ToString()));
                    var ROW_LASTMANT_DTTM = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(6).ToString()));
                    var dias = (DateTime.Now - DATEEND_).Days;
                    var AUTORIZADIVISA = reader.GetValue(3).ToString();


                    /*
                    if (lista.ValidarListaBloqueo(param.NRODOCUMENTO))
                    {

                        param.AA_AUTORIZACION = "NO";

                        Lista = param2.BloqueoDateEnd(value);

                        param.DATEEND= Lista[0].DATEEND;
                    }*/

                    if (dias >= 0 && AUTORIZADIVISA.Equals("SI"))
                    {
                        autorizacionValida = true;
                        var dias2 = (dt - ROW_LASTMANT_DTTM).Days;
                        if (dias2 >= 0)
                        {
                            dt = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(11).ToString()));
                            lastOprid = reader.GetValue(12).ToString();
                        }


                    }


                    if (!autorizacionValida)
                    {
                        param.AA_AUTORIZACION = "NO";
                    }


                    parametros.Add(param);
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
        return parametros;
    }














    public List<ParamaetroOpDivisas> BloqueoDateEnd(string value)
    {
        // contador de objetos


        // respuesta de el metodo
        List<ParamaetroOpDivisas> parametrosdtl = new List<ParamaetroOpDivisas>();

        ContextOracle.Open();


        string sentencia = "SELECT Nvl(Max(ROW_LASTMANT_DTTM)-1, Max(ROW_ADDED_DTTM)-1)  FROM CRWORK.PS_AA_LISTA_BLOQ"; // tabla ....dtl
        sentencia = sentencia + " where AA_NRO_DOCUMENTO = '" + value + "' AND AA_ESTADO_BLQ='A' ";

        OracleCommand command = new OracleCommand(sentencia, ContextOracle);

        // llamado a los procedimientos
        OracleDataReader reader = null;



        reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {

                ParamaetroOpDivisas paramdtl = new ParamaetroOpDivisas()
                {

                    DATEEND = reader.GetValue(0).ToString(),

                };





                if (!string.IsNullOrEmpty(reader.GetValue(0).ToString()))
                {
                    //paramdtl.DATE_ADDED = reader.GetValue(4).ToString();
                    //paramdtl.DATEEND = Convert.ToDateTime(reader.GetValue(0)).Month + "/" + Convert.ToDateTime(reader.GetValue(0)).Day + "/" + Convert.ToDateTime(reader.GetValue(0)).Year;
                    paramdtl.DATEEND = Convert.ToDateTime(reader.GetValue(0)).Day + "/" + Convert.ToDateTime(reader.GetValue(0)).Month.ToString("00") + "/" + Convert.ToDateTime(reader.GetValue(0)).Year;


                }


                parametrosdtl.Add(paramdtl);
            }
        }



        // retornar datos
        reader.Close();
        ContextOracle.Close();
        return parametrosdtl;
    }








    //Actualiza el estado de la Divisa y la fecha final
    public bool ActualizarParametro(ParamaetroOpDivisas Value)
    {


       // Value.DATEEND = Convert.ToDateTime(Value.DATEEND).Day + "/" + Convert.ToDateTime(Value.DATEEND).Month.ToString("00") + "/" + Convert.ToDateTime(Value.DATEEND).Year;



        bool respuesta = false;
        // respuesta de el metodo
        List<ParamaetroOpDivisas> parametros = new List<ParamaetroOpDivisas>();

        if (Value.TIPODOCUMENTO != "N")


        {

            ContextOracle.Open();
            string sentencia = " UPDATE DIVISAS.PS_AA_PN_OPR_INTDT SET AA_ESTADO_AUTORIZA='" + Value.AA_ESTADO_AUTORIZA + "', DATE_END='" + Value.DATEEND + "' ";
            // validar parametro
            if (!string.IsNullOrEmpty(Value.NRODOCUMENTO))
            {
                sentencia = sentencia + " WHERE NRO_IDENTIDAD= " + Value.NRODOCUMENTO + " and SEQ_NBR=" + Value.SEQNBR + " ";
            }

            OracleCommand command = new OracleCommand(sentencia, ContextOracle);
            // llamado a los procedimientos
            OracleDataReader reader = null;

            try
            {

                reader = command.ExecuteReader();

                if (reader.RecordsAffected > 0)
                {
                    respuesta = true;
                }
                else
                {
                    respuesta = false;

                }
                ContextOracle.Close();
            }
            catch (Exception error)
            {

            }

            // retornar datos
            reader.Close();
            ContextOracle.Close();
            return respuesta;


        }
        else
        {

            ContextOracle.Open();
            string sentencia = " UPDATE DIVISAS.PS_AA_PJ_OPR_INTDT SET AA_ESTADO_AUTORIZA='" + Value.AA_ESTADO_AUTORIZA + "', DATE_END='" + Value.DATEEND + "' ";
            // validar parametro
            if (!string.IsNullOrEmpty(Value.NRODOCUMENTO))
            {
                sentencia = sentencia + " WHERE NRO_IDENTIDAD= " + Value.NRODOCUMENTO + " and SEQ_NBR=" + Value.SEQNBR + " ";
            }

            OracleCommand command = new OracleCommand(sentencia, ContextOracle);
            // llamado a los procedimientos
            OracleDataReader reader = null;

            try
            {

                reader = command.ExecuteReader();

                if (reader.RecordsAffected > 0)
                {
                    respuesta = true;
                }
                else
                {
                    respuesta = false;

                }
                ContextOracle.Close();
            }
            catch (Exception error)
            {

            }

            // retornar datos
            reader.Close();
            ContextOracle.Close();
            return respuesta;



        }


    }
    public bool ValidarParametro(string value, string value3, string value2)
    {
        bool respuesta = false;


        if (value3 != "N")
        {


            ContextOracle.Open();
            string sentencia = "select count(*) from DIVISAS.PS_AA_PN_OPR_INTDT where NRO_IDENTIDAD = '" + value + "' and SEQ_NBR='" + value2 + "'     ";

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


        else
        {

            ContextOracle.Open();
            string sentencia = "select count(*) from DIVISAS.PS_AA_PJ_OPR_INTDT where NRO_IDENTIDAD = '" + value + "' and SEQ_NBR='" + value2 + "'     ";

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




    }



    public bool ValidarRol(string value2)
    {
        bool respuesta = false;

        ContextOracle.Open();
        string sentencia = "SELECT * FROM CRWORK.ps_aa_autoridiviza WHERE rolename = '" + value2 + "'";

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













    public List<AutorizaDivisaEntity> ValidarRolMonto(string value2)
    {
        List<AutorizaDivisaEntity> listAut = new List<AutorizaDivisaEntity>();

        ContextOracle.Open();
        string sentencia = "SELECT * FROM ps_aa_autoridiviza WHERE rolename=" + value2 + "'";

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
                    AutorizaDivisaEntity param = new AutorizaDivisaEntity()
                    {
                        DESCR = reader.GetValue(2).ToString(),
                        AA_VENTAS = reader.GetValue(4).ToString(),
                    };
                    listAut.Add(param);
                }
            }

        }
        catch (Exception error)
        {

        }
        // retornar datos
        reader.Close();
        ContextOracle.Close();

        return listAut;
    }











    public bool CrearParametro(ParamaetroOpDivisas Value)
    {


        if (Value.MONTOPROMVENT == null)
        {

            Value.MONTOPROMVENT = "0.0";
        }
        if (Value.ROWLASTMANDTTM == null)
        {

            Value.ROWLASTMANDTTM = "01011999";
        }

        bool respuesta = false;
        string NuevoValor = Value.MONTOPROMVENT.Replace(".", ",");
        //string[] dateAdded = Value.DATEEND.Split(' ');
        //string[] FechaLastmant = Value.ROWLASTMANDTTM.Split(' ');
        DateTime FechaHoy = DateTime.Now;

        try
        {
            // instancia del procedimiento
            OracleCommand command = new OracleCommand("DIVISAS.av_crear_autorizaciones_div", ContextOracle);
            command.CommandType = CommandType.StoredProcedure;

            // parametros de entrada
            command.Parameters.Add(new OracleParameter("tipo_documento", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.TIPODOCUMENTO) ? " " : Value.TIPODOCUMENTO;

            command.Parameters.Add(new OracleParameter("nro_documento", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.NRODOCUMENTO) ? " " : Value.NRODOCUMENTO;
            command.Parameters.Add(new OracleParameter("v_param_id", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.SEQNBR) ? " " : Value.SEQNBR;
            command.Parameters.Add(new OracleParameter("v_descr100", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.AUTORIZADIVISA) ? " " : Value.AUTORIZADIVISA;
            command.Parameters.Add(new OracleParameter("v_value_display", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.TIPOVIGENCIA) ? " " : Value.TIPOVIGENCIA;
            command.Parameters.Add(new OracleParameter("v_value", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.SOPORTEDIVISA) ? " " : Value.SOPORTEDIVISA;
            command.Parameters.Add(new OracleParameter("v_value_nbr", OracleType.DateTime)).Value = string.IsNullOrEmpty(Value.DATEEND) ? "01/01/1900" : Value.DATEEND;


            command.Parameters.Add(new OracleParameter("v_date_added", OracleType.VarChar)).Value = string.IsNullOrEmpty(NuevoValor) ? "0" : NuevoValor;
            command.Parameters.Add(new OracleParameter("v_descr", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.MONTOPROMEDIO) ? "0" : Value.MONTOPROMEDIO;
            //command.Parameters.Add(new OracleParameter("v_query_sql", OracleType.DateTime)).Value = string.IsNullOrEmpty(Value.ROWADDEDDTTM) ? Convert.ToDateTime(FechaHoy).ToString("dd/MM/yyyy HH:mm") : Convert.ToDateTime(FechaHoy).ToString("dd/MM/yyyy HH:mm");

            command.Parameters.Add(new OracleParameter("aa_estado_autoriza", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.AA_ESTADO_AUTORIZA) ? " " : Value.AA_ESTADO_AUTORIZA;

            command.Parameters.Add(new OracleParameter("row_added_opprid", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.ROWADDEDOPPRID) ? " " : Value.ROWADDEDOPPRID;



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
        }
        catch (Exception ex)
        {
            respuesta = false;
            throw;
        }


        return respuesta;
    }











    public bool CrearEntidadesFinancieras(EntidadesFinancierasentity Value)
    {
        bool respuesta = false;



        try
        {
            // instancia del procedimiento
            OracleCommand command = new OracleCommand("DIVISAS.av_crear_entidades", ContextOracle);
            command.CommandType = CommandType.StoredProcedure;

            // parametros de entrada
            command.Parameters.Add(new OracleParameter("tipo_documento", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.TIPODOCUMENTO) ? " " : Value.TIPODOCUMENTO;

            command.Parameters.Add(new OracleParameter("nro_documento", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.Nrodocumento) ? " " : Value.Nrodocumento;
            command.Parameters.Add(new OracleParameter("v_cod_entidad", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.CodEntidad) ? " " : Value.CodEntidad;
            command.Parameters.Add(new OracleParameter("rowaddedoprid", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.ROWADDEDOPRID) ? " " : Value.ROWADDEDOPRID;



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
        }
        catch (Exception ex)
        {
            respuesta = false;
            throw;
        }


        return respuesta;
    }











    public List<ParamaetroOpDivisas> TraerDivisasPendientes(ParamaetroOpDivisas value)
    {

        
        // respuesta de el metodo
        bool autorizacionValida = false;
        DateTime dt = new DateTime(1900, 1, 1);
        string lastOprid;

        // respuesta de el metodo
        ParametrosDivisas lista = new ParametrosDivisas();

        List<ParamaetroOpDivisas> Lista = new List<ParamaetroOpDivisas>();

        List<ParamaetroOpDivisas> parametros = new List<ParamaetroOpDivisas>();
        ParametroDivModel param2 = new ParametroDivModel();


        if (value.TIPODOCUMENTO != "N")
        {


            ContextOracle.Open();
            string sentencia = "SELECT NRO_IDENTIDAD,TIPO_IDENTIDAD,SEQ_NBR, CASE AA_AUTORIZA_DIVISA WHEN 'Y' THEN 'SI' WHEN 'N' THEN 'NO' END AS AA_AUTORIZA_DIVISA," +
                "CASE AA_TIPO_VIGENCIA WHEN 'P' THEN 'Puntual' WHEN 'M' THEN 'Mensual' END AS AA_TIPO_VIGENCIA,AA_SOPORTE_DIVISA,DATE_END,AA_MONTO_PROM_VENT,AA_MONTO_PROMEDIO," +
                "ROW_ADDED_DTTM,ROW_ADDED_OPRID,ROW_LASTMANT_DTTM,ROW_LASTMANT_OPRID,AA_BLOQUEO_DIVISAS,AA_ESTADO_AUTORIZA,AA_AUTORIZACION_DIV FROM DIVISAS.PS_AA_PN_OPR_INTDT ";

            // validar parametro
            if (!string.IsNullOrEmpty(value.NRODOCUMENTO))
            {
                sentencia = sentencia + " where  UPPER(NRO_IDENTIDAD) LIKE UPPER('" + value.NRODOCUMENTO + "')   ";
            }

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


                        ParamaetroOpDivisas param = new ParamaetroOpDivisas()
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
                            BLOQUEODIVISAS = reader.GetValue(13).ToString(),
                            AA_ESTADO_AUTORIZA = reader.GetValue(14).ToString(),
                            AA_AUTORIZACION_DIV = reader.GetValue(15).ToString(),
                        };

                        if (!string.IsNullOrEmpty(reader.GetValue(6).ToString()))
                        {
                            //param.DATEEND = reader.GetValue(6).ToString();


                            param.DATEEND = Convert.ToDateTime(reader.GetValue(6)).Day + "/" + Convert.ToDateTime(reader.GetValue(6)).Month.ToString("00") + "/" + Convert.ToDateTime(reader.GetValue(6)).Year;



                        }




                        autorizacionValida = false;
                        param.AA_AUTORIZACION = "SI";

                        var DATEEND_ = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(6).ToString()));
                        var ROW_LASTMANT_DTTM = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(6).ToString()));
                        var dias = (DATEEND_ - DateTime.Now).Days;
                        var AUTORIZADIVISA = reader.GetValue(3).ToString();


                        if (dias >= 0 && AUTORIZADIVISA.Equals("SI"))
                        {
                            autorizacionValida = true;
                            var dias2 = (dt - ROW_LASTMANT_DTTM).Days;
                            if (dias2 >= 0)
                            {
                                dt = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(11).ToString()));
                                lastOprid = reader.GetValue(12).ToString();
                            }


                        }




                        if (!autorizacionValida)
                        {
                            param.AA_AUTORIZACION = "NO";
                        }


                        if (lista.ValidarListaBloqueo(param.NRODOCUMENTO))
                        {

                            param.AA_AUTORIZACION = "NO";
                            Lista = param2.BloqueoDateEnd(value.NRODOCUMENTO);



                            if (param.AA_ESTADO_AUTORIZA == "Vigente")
                            {

                                param.DATEEND = Lista[0].DATEEND;
                                param.AA_AUTORIZACION = "NO";


                            }


                        }







                        parametros.Add(param);
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
            return parametros;

        }

        else
        {




            ContextOracle.Open();
            string sentencia = "SELECT NRO_IDENTIDAD,TIPO_IDENTIDAD,SEQ_NBR, CASE AA_AUTORIZA_DIVISA WHEN 'Y' THEN 'SI' WHEN 'N' THEN 'NO' END AS AA_AUTORIZA_DIVISA,CASE AA_TIPO_VIGENCIA WHEN 'P' THEN 'Puntual' WHEN 'M' THEN 'Mensual' END AS AA_TIPO_VIGENCIA,AA_SOPORTE_DIVISA,DATE_END,AA_MONTO_PROM_VENT,AA_MONTO_PROMEDIO,ROW_ADDED_DTTM,ROW_ADDED_OPRID,ROW_LASTMANT_DTTM,ROW_LASTMANT_OPRID,AA_BLOQUEO_DIVISAS,AA_ESTADO_AUTORIZA FROM DIVISAS.PS_AA_PJ_OPR_INTDT ";

            // validar parametro
            if (!string.IsNullOrEmpty(value.NRODOCUMENTO))
            {
                sentencia = sentencia + " where UPPER(NRO_IDENTIDAD) LIKE UPPER('" + value.NRODOCUMENTO + "')  ";
            }

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


                        ParamaetroOpDivisas param = new ParamaetroOpDivisas()
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
                            BLOQUEODIVISAS = reader.GetValue(13).ToString(),
                            AA_ESTADO_AUTORIZA = reader.GetValue(14).ToString(),

                        };

                        if (!string.IsNullOrEmpty(reader.GetValue(6).ToString()))
                        {
                            param.DATEEND = reader.GetValue(6).ToString();
                        }





                        var DATEEND_ = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(6).ToString()));
                        var ROW_LASTMANT_DTTM = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(6).ToString()));
                        var dias = (DateTime.Now - DATEEND_).Days;
                        var AUTORIZADIVISA = reader.GetValue(3).ToString();




                        if (lista.ValidarListaBloqueo(param.NRODOCUMENTO))
                        {

                            param.AA_AUTORIZACION = "NO";
                            Lista = param2.BloqueoDateEnd(value.NRODOCUMENTO);



                            if (param.AA_ESTADO_AUTORIZA == "Vigente")
                            {

                                param.DATEEND = Lista[0].DATEEND;
                                param.AA_AUTORIZACION = "NO";


                            }


                        }

                        if (dias >= 0 && AUTORIZADIVISA.Equals("SI"))
                        {
                            autorizacionValida = true;
                            var dias2 = (dt - ROW_LASTMANT_DTTM).Days;
                            if (dias2 >= 0)
                            {
                                dt = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(11).ToString()));
                                lastOprid = reader.GetValue(12).ToString();
                            }


                        }


                        if (!autorizacionValida)
                        {
                            param.AA_AUTORIZACION = "NO";
                        }


                        parametros.Add(param);
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
            return parametros;











        }








    }

}
























public class AutorizaDivisaEntity
{
    public AutorizaDivisaEntity()
    {
        this.ROLENNAME = " ";
        this.DESCR = " ";
        this.AA_VENTAS = " ";


    }
    public string ROLENNAME { get; set; } //

    public string DESCR { get; set; }

    public string AA_VENTAS { get; set; } //

}


















public class DetallesEntity
{
    public DetallesEntity()
    {
        this.VALUE_NBR = 0;
        this.DESCR100 = " ";
        this.VALUE_DISPLAY = " ";
        this.VALUE = " ";
        this.VALUE_NBR = 0;
        this.DATE_ADDED = null;
        this.DESCR = " ";
        this.QUERY_SQL = " ";

    }
    public string PARAM_ID { get; set; } //

    public string DESCR100 { get; set; }

    public string VALUE_DISPLAY { get; set; } //

    public string VALUE { get; set; }

    public int VALUE_NBR { get; set; } //

    public string DATE_ADDED { get; set; } //

    public string DESCR { get; set; }

    public string QUERY_SQL { get; set; }
}




public class ParamaetroOpDivisas
{


    public ParamaetroOpDivisas()
    {

        this.ID = 0;

    }
    private string _AA_AUTORIZACION_DIV;
    private string _AA_ESTADO_AUTORIZA;
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
    private string _AA_AUTORIZACION;
    private string _BLOQUEODIVISAS;
    private string _BloqueoGlobal;
    private string _TOTAL;




    public string TOTAL


    {
        get
        {
            return _TOTAL;
        }

        set
        {
            _TOTAL = value;
        }
    }



    public string BloqueoGlobal


    {
        get
        {
            return _BloqueoGlobal;
        }

        set
        {
            _BloqueoGlobal = value;
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


    public string AA_AUTORIZACION_DIV


    {
        get
        {
            return _AA_AUTORIZACION_DIV;
        }

        set
        {
            _AA_AUTORIZACION_DIV = value;
        }
    }

    public string AA_ESTADO_AUTORIZA


    {
        get
        {
            return _AA_ESTADO_AUTORIZA;
        }

        set
        {
            _AA_ESTADO_AUTORIZA = value;
        }
    }

    public string AA_AUTORIZACION


    {
        get
        {
            return _AA_AUTORIZACION;
        }

        set
        {
            _AA_AUTORIZACION = value;
        }
    }


    public int ID { get; set; }

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




