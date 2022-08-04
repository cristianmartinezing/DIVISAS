using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ParametrodtlModel
/// </summary>
public class ParametroOIModel : RepositoryOracle
{
    public ParametroOIModel()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    public List<OperacionesdtlEntity> TraerParametrosDetalle(string value, string TipoDocumento)
    {
        // contador de objetos
        int id = 0;


        //var dateFIN = DateTime.ParseExact(paramdtl.DATEEND, "MM//yyyy", null);

        bool autorizacionValida = false;
        DateTime dt = new DateTime(1900, 1, 1);
        string lastOprid;

        // respuesta de el metodo
        List<OperacionesdtlEntity> parametrosdtl = new List<OperacionesdtlEntity>();

        ParametrosDivisas lista = new ParametrosDivisas();

        List<ParamaetroOpDivisas> parametros = new List<ParamaetroOpDivisas>();

        ParametroDivModel param2 = new ParametroDivModel();

        ParametroOIModel AdicionarMes = new ParametroOIModel();




        List<ParamaetroOpDivisas> Lista = new List<ParamaetroOpDivisas>();




        if (TipoDocumento != "N")
        {

            ContextOracle.Open();
            string sentencia = "select NRO_IDENTIDAD,TIPO_IDENTIDAD,SEQ_NBR,  AA_AUTORIZA_DIVISA," +
                "CASE AA_TIPO_VIGENCIA WHEN 'P' THEN 'Puntual' WHEN 'M' THEN 'Mensual' WHEN 'S' THEN 'Semestral' END AS AA_TIPO_VIGENCIA,CASE AA_SOPORTE_DIVISA WHEN 'NO' THEN 'NO' WHEN 'SI' THEN 'SI'" +
                "WHEN 'DIGI' THEN 'DIGITAL' END AS AA_TIPO_VIGENCIA" +
                ",DATE_END,AA_MONTO_PROM_VENT,AA_MONTO_PROMEDIO,ROW_ADDED_DTTM,ROW_ADDED_OPRID,ROW_LASTMANT_DTTM,ROW_LASTMANT_OPRID,AA_BLOQUEO_DIVISAS,AA_ESTADO_AUTORIZA,AA_AUTORIZACION_DIV " +
                "from DIVISAS.PS_AA_PN_OPR_INTDT"; // tabla ....dtl





            sentencia = sentencia + " where NRO_IDENTIDAD = '" + value + "' ORDER BY SEQ_NBR";


            OracleCommand command = new OracleCommand(sentencia, ContextOracle);


            // llamado a los procedimientos
            OracleDataReader reader = null;


            reader = command.ExecuteReader();


            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    OperacionesdtlEntity paramdtl = new OperacionesdtlEntity()
                    {
                        ID = id++,
                        NRODOCUMENTO = reader.GetValue(0).ToString(),
                        TIPODOCUMENTO = reader.GetValue(1).ToString(),
                        SEQNBR = reader.GetValue(2).ToString(),
                        AUTORIZADIVISA = reader.GetValue(3).ToString(),
                        TIPOVIGENCIA = reader.GetValue(4).ToString(),
                        SOPORTEDIVISA = reader.GetValue(5).ToString(),

                        MONTOPROMVENT = reader.GetValue(7).ToString(),
                        MONTOPROMEDIO = reader.GetValue(8).ToString(),
                        ROWADDEDOPPRID = reader.GetValue(10).ToString(),

                        ROWADDEDDTTM = reader.GetValue(9).ToString(),
                        ROWLASTMANDTTM = reader.GetValue(11).ToString(),


                        ROWLASTMANOPPRID = reader.GetValue(12).ToString(),
                        BLOQUEODIVISAS = reader.GetValue(13).ToString(),
                        AA_ESTADO_AUTORIZA = reader.GetValue(14).ToString(),
                        AA_AUTORIZACION_DIV = reader.GetValue(15).ToString(),
                    };


                    paramdtl.AA_AUTORIZACION = "SI";
                    autorizacionValida = false;


                    if (!string.IsNullOrEmpty(reader.GetValue(6).ToString()))
                    {
                        //paramdtl.DATE_ADDED = reader.GetValue(4).ToString();
                        paramdtl.DATEEND = Convert.ToDateTime(reader.GetValue(6)).Day + "/" + Convert.ToDateTime(reader.GetValue(6)).Month.ToString("00") + "/" + Convert.ToDateTime(reader.GetValue(6)).Year;


                    }



                    var DATEEND_ = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(6).ToString()));
                    var ROW_LASTMANT_DTTM = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(6).ToString()));
                    var DIFERENCIA_DIAS = (DATEEND_ - DateTime.Now).Days;
                    var AUTORIZADIVISA = reader.GetValue(3).ToString();
                    paramdtl.lastOprid = reader.GetValue(12).ToString();



                    if (DIFERENCIA_DIAS >= 0 && AUTORIZADIVISA.Equals("SI"))
                    {
                        autorizacionValida = true;
                        var dias2 = (dt - ROW_LASTMANT_DTTM).Days;
                        if (dias2 >= 0)
                        {
                            dt = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(11).ToString()));
                            paramdtl.lastOprid = reader.GetValue(12).ToString();
                        }


                    }


                    if (!autorizacionValida)
                    {
                        paramdtl.AA_AUTORIZACION = "NO";
                    }




                    if (lista.ValidarListaBloqueo(paramdtl.NRODOCUMENTO))
                    {

                        paramdtl.AA_AUTORIZACION = "NO";
                        Lista = param2.BloqueoDateEnd(value);



                        if (paramdtl.AA_ESTADO_AUTORIZA == "Vigente")
                        {

                            paramdtl.DATEEND = Lista[0].DATEEND;
                            paramdtl.AA_AUTORIZACION = "NO";


                        }

                        paramdtl.BloqueoGlobal = "SI";
                    }


                    paramdtl.cantidadVigentes = 0;
                    if (paramdtl.AA_ESTADO_AUTORIZA == "Vigente")
                    {


                        paramdtl.cantidadVigentes = 1;
                    }



                    if (lista.ValidarEmpleado(paramdtl.NRODOCUMENTO))
                    {
                        paramdtl.EMPLEADO = "SI";

                    }
                    else
                    {
                        paramdtl.EMPLEADO = "NO";

                    }






                    parametrosdtl.Add(paramdtl);

                }




            }
            else
            {

                OperacionesdtlEntity paramdtl = new OperacionesdtlEntity()
                {

                };




                if (lista.ValidarEmpleado(value))
                {
                    paramdtl.EMPLEADO = "SI";

                }
                else
                {
                    paramdtl.EMPLEADO = "NO";

                }

                parametrosdtl.Add(paramdtl);


            }
            // retornar datos
            reader.Close();
            ContextOracle.Close();
            return parametrosdtl;
        }
        else
        {

            ContextOracle.Open();
            string sentencia = "select NRO_IDENTIDAD,TIPO_IDENTIDAD,SEQ_NBR, AA_AUTORIZA_DIVISA," +
                "CASE AA_TIPO_VIGENCIA WHEN 'P' THEN 'Puntual' WHEN 'M' THEN 'Mensual' WHEN 'S' THEN 'Semestral' END AS AA_TIPO_VIGENCIA,CASE AA_SOPORTE_DIVISA WHEN 'NO' THEN 'NO' WHEN 'SI' THEN 'SI'" +
                "WHEN 'DIGI' THEN 'DIGITAL' END AS AA_TIPO_VIGENCIA" +
                ",DATE_END,AA_MONTO_PROM_VENT,AA_MONTO_PROMEDIO,ROW_ADDED_DTTM,ROW_ADDED_OPRID,ROW_LASTMANT_DTTM,ROW_LASTMANT_OPRID,AA_BLOQUEO_DIVISAS,AA_ESTADO_AUTORIZA " +
                "from DIVISAS.PS_AA_PJ_OPR_INTDT"; // tabla ....dtl





            sentencia = sentencia + " where NRO_IDENTIDAD = '" + value + "' ORDER BY SEQ_NBR";


            OracleCommand command = new OracleCommand(sentencia, ContextOracle);


            // llamado a los procedimientos
            OracleDataReader reader = null;


            reader = command.ExecuteReader();


            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    OperacionesdtlEntity paramdtl = new OperacionesdtlEntity()
                    {
                        ID = id++,
                        NRODOCUMENTO = reader.GetValue(0).ToString(),
                        TIPODOCUMENTO = reader.GetValue(1).ToString(),
                        SEQNBR = reader.GetValue(2).ToString(),
                        AUTORIZADIVISA = reader.GetValue(3).ToString(),
                        TIPOVIGENCIA = reader.GetValue(4).ToString(),
                        SOPORTEDIVISA = reader.GetValue(5).ToString(),

                        MONTOPROMVENT = reader.GetValue(7).ToString(),
                        MONTOPROMEDIO = reader.GetValue(8).ToString(),
                        ROWADDEDOPPRID = reader.GetValue(10).ToString(),


                        ROWLASTMANOPPRID = reader.GetValue(12).ToString(),
                        BLOQUEODIVISAS = reader.GetValue(13).ToString(),
                        AA_ESTADO_AUTORIZA = reader.GetValue(14).ToString(),

                        ROWADDEDDTTM = reader.GetValue(9).ToString(),
                        ROWLASTMANDTTM = reader.GetValue(11).ToString(),


                    };



                    paramdtl.AA_AUTORIZACION = "SI";
                    autorizacionValida = false;

                    if (!string.IsNullOrEmpty(reader.GetValue(6).ToString()))
                    {
                        //paramdtl.DATE_ADDED = reader.GetValue(4).ToString();
                        paramdtl.DATEEND = Convert.ToDateTime(reader.GetValue(6)).Day + "/" + Convert.ToDateTime(reader.GetValue(6)).Month.ToString("00") + "/" + Convert.ToDateTime(reader.GetValue(6)).Year;
                    }







                    var DATEEND_ = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(6).ToString()));
                    var ROW_LASTMANT_DTTM = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(6).ToString()));

                    var DIFERENCIA_DIAS = (DATEEND_ - DateTime.Now).Days;
                    var AUTORIZADIVISA = reader.GetValue(3).ToString();
                    paramdtl.lastOprid = reader.GetValue(12).ToString();


                    if (DIFERENCIA_DIAS >= 0 && AUTORIZADIVISA.Equals("SI"))
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
                        paramdtl.AA_AUTORIZACION = "NO";
                    }



                    if (lista.ValidarListaBloqueo(paramdtl.NRODOCUMENTO))
                    {

                        paramdtl.AA_AUTORIZACION = "NO";
                        Lista = param2.BloqueoDateEnd(value);



                        if (paramdtl.AA_ESTADO_AUTORIZA == "Vigente")
                        {

                            paramdtl.DATEEND = Lista[0].DATEEND;
                            paramdtl.AA_AUTORIZACION = "NO";


                        }
                        paramdtl.BloqueoGlobal = "SI";

                    }


                    paramdtl.cantidadVigentes = 0;
                    if (paramdtl.AA_ESTADO_AUTORIZA == "Vigente")
                    {


                        paramdtl.cantidadVigentes = 1;
                    }



                    if (lista.ValidarEmpleado(paramdtl.NRODOCUMENTO))
                    {
                        paramdtl.EMPLEADO = "SI";

                    }
                    else
                    {
                        paramdtl.EMPLEADO = "NO";

                    }




                    parametrosdtl.Add(paramdtl);

                }




            }
            else
            {
                OperacionesdtlEntity paramdtl = new OperacionesdtlEntity()
                {

                };




                if (lista.ValidarEmpleado(value))
                {
                    paramdtl.EMPLEADO = "SI";

                }
                else
                {
                    paramdtl.EMPLEADO = "NO";

                }

                parametrosdtl.Add(paramdtl);
            }
            // retornar datos
            reader.Close();
            ContextOracle.Close();
            return parametrosdtl;

        }


    }





    public List<OperacionesdtlEntity> TraerCargoAutorizaDiv(string value, string TipoDocumento)
    {
        // contador de objetos
        int id = 0;


        //var dateFIN = DateTime.ParseExact(paramdtl.DATEEND, "MM//yyyy", null);

        bool autorizacionValida = false;
        DateTime dt = new DateTime(1900, 1, 1);
        string lastOprid;

        // respuesta de el metodo
        List<OperacionesdtlEntity> parametrosdtl = new List<OperacionesdtlEntity>();

        ParametrosDivisas lista = new ParametrosDivisas();

        List<ParamaetroOpDivisas> parametros = new List<ParamaetroOpDivisas>();

        ParametroDivModel param2 = new ParametroDivModel();

        ParametroOIModel AdicionarMes = new ParametroOIModel();




        List<ParamaetroOpDivisas> Lista = new List<ParamaetroOpDivisas>();




        if (TipoDocumento != "N")
        {

            ContextOracle.Open();
            string sentencia = "select min(ROW_LASTMANT_DTTM),t2.DESC_CARGO,t2.COD_CARGO from DIVISAS.PS_AA_PN_OPR_INTDT t1 JOIN CRWORK.AV_FUNCIONARIOS t2 on t1.ROW_LASTMANT_OPRID=t2.IDENTIFICACION  "; // tabla ....dtl





            sentencia = sentencia + " WHERE NRO_IDENTIDAD='" + value + "' AND TIPO_IDENTIDAD='" + TipoDocumento + "' AND AA_ESTADO_AUTORIZA='Vigente' group by t2.DESC_CARGO,t2.COD_CARGO";


            OracleCommand command = new OracleCommand(sentencia, ContextOracle);


            // llamado a los procedimientos
            OracleDataReader reader = null;


            reader = command.ExecuteReader();


            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    OperacionesdtlEntity paramdtl = new OperacionesdtlEntity()
                    {
                        ID = id++,
                        CARGO = reader.GetValue(1).ToString(),
                        CODIGOCARGO = reader.GetValue(2).ToString(),

                    };








                    parametrosdtl.Add(paramdtl);

                }




            }
            else
            {


            }
            // retornar datos
            reader.Close();
            ContextOracle.Close();
            return parametrosdtl;
        }
        else
        {

            ContextOracle.Open();
            string sentencia = "select min(ROW_LASTMANT_DTTM),t2.DESC_CARGO,t2.COD_CARGO  from DIVISAS.PS_AA_PJ_OPR_INTDT t1 JOIN CRWORK.AV_FUNCIONARIOS t2 on t1.ROW_LASTMANT_OPRID=t2.IDENTIFICACION "; // tabla ....dtl





            sentencia = sentencia + " WHERE NRO_IDENTIDAD='" + value + "' AND TIPO_IDENTIDAD='" + TipoDocumento + "' AND AA_ESTADO_AUTORIZA='Vigente' group by t2.DESC_CARGO,t2.COD_CARGO";


            OracleCommand command = new OracleCommand(sentencia, ContextOracle);


            // llamado a los procedimientos
            OracleDataReader reader = null;


            reader = command.ExecuteReader();


            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    OperacionesdtlEntity paramdtl = new OperacionesdtlEntity()
                    {
                        ID = id++,
                        CARGO = reader.GetValue(1).ToString(),
                        CODIGOCARGO = reader.GetValue(2).ToString(),


                    };







                    parametrosdtl.Add(paramdtl);

                }




            }
            // retornar datos
            reader.Close();
            ContextOracle.Close();
            return parametrosdtl;

        }


    }




    public List<EntidadesFinancierasentity> TraerEntidadesCliente(string value, string tipoDocumento)
    {
        // contador de objetos
        int id = 0;
        string lastOprid;

        // respuesta de el metodo
        List<EntidadesFinancierasentity> parametrosdtl = new List<EntidadesFinancierasentity>();

        List<EntidadesFinancierasentity> Lista = new List<EntidadesFinancierasentity>();


        if (tipoDocumento != "N")
        {

            ContextOracle.Open();
            string sentencia = "select * from DIVISAS.PS_AA_ENTI_OPRPN EF JOIN CRWORK.PS_AA_ENTFINAN_TBL ET ON EF.AA_COD_ENTIDAD=ET.AA_COD_ENTIDAD"; // tabla ....dtl


            sentencia = sentencia + " where NRO_IDENTIDAD = '" + value + "'  order by case when EF.AA_COD_ENTIDAD='28' then 1  end ";


            OracleCommand command = new OracleCommand(sentencia, ContextOracle);


            // llamado a los procedimientos
            OracleDataReader reader = null;


            reader = command.ExecuteReader();


            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    EntidadesFinancierasentity paramdtl = new EntidadesFinancierasentity()
                    {
                        ID = id++,
                        CodEntidad = reader.GetValue(2).ToString(),
                        EntidadFinanciera = reader.GetValue(15).ToString(),
                        Nrodocumento = reader.GetValue(0).ToString(),
                        TIPODOCUMENTO = reader.GetValue(1).ToString(),
                    };




                    parametrosdtl.Add(paramdtl);

                }




            }



            // retornar datos
            reader.Close();
            ContextOracle.Close();
            return parametrosdtl;
        }
        else
        {
            ContextOracle.Open();
            string sentencia = "select * from DIVISAS.PS_AA_ENTI_OPRPJ EF JOIN CRWORK.PS_AA_ENTFINAN_TBL ET ON EF.AA_COD_ENTIDAD=ET.AA_COD_ENTIDAD"; // tabla ....dtl


            sentencia = sentencia + " where NRO_IDENTIDAD = '" + value + "'  order by case when EF.AA_COD_ENTIDAD='28' then 1  end  ";


            OracleCommand command = new OracleCommand(sentencia, ContextOracle);


            // llamado a los procedimientos
            OracleDataReader reader = null;


            reader = command.ExecuteReader();


            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    EntidadesFinancierasentity paramdtl = new EntidadesFinancierasentity()
                    {
                        ID = id++,
                        CodEntidad = reader.GetValue(3).ToString(),
                        EntidadFinanciera = reader.GetValue(16).ToString(),
                        Nrodocumento = reader.GetValue(0).ToString(),
                        TIPODOCUMENTO = reader.GetValue(1).ToString(),

                    };




                    parametrosdtl.Add(paramdtl);

                }




            }



            // retornar datos
            reader.Close();
            ContextOracle.Close();
            return parametrosdtl;
        }

    }









    public List<OperacionesIntEntity> TraerOperacionesInt(string value, string TipoDocumento)
    {
        // contador de objetos
        int id = 0;

        List<OperacionesIntEntity> OperacionesInt = new List<OperacionesIntEntity>();
        List<OperacionesIntEntity> Lista = new List<OperacionesIntEntity>();

        if (TipoDocumento != "N")
        {

            ContextOracle.Open();
            string sentencia = "select NRO_IDENTIDAD,SEQ_NBR,AA_TIPO_DIVISA,AA_FECHA_TX,AA_COD_AUTORIZ,AA_MONTO_OPR,CURRENCY_CD," +
                "PARTICIPATION_PCT,AA_MONTO_TASA FROM DIVISAS.PS_AA_DIV_MOV_PN"; // tabla ....dtl


            sentencia = sentencia + " where NRO_IDENTIDAD = '" + value + "'";


            OracleCommand command = new OracleCommand(sentencia, ContextOracle);


            // llamado a los procedimientos
            OracleDataReader reader = null;


            reader = command.ExecuteReader();


            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    OperacionesIntEntity paramdtl = new OperacionesIntEntity()
                    {
                        ID = id++,
                        NRODOCUMENTO = reader.GetValue(0).ToString(),
                        SEQ_NBR = reader.GetValue(1).ToString(),
                        AA_TIPO_DIVISA = reader.GetValue(2).ToString(),
                        AA_FECHA_TX = reader.GetValue(3).ToString(),
                        AA_COD_AUTORIZ = reader.GetValue(4).ToString(),
                        AA_MONTO_OPR = reader.GetValue(5).ToString(),
                        CURRENCY_CD = reader.GetValue(6).ToString(),

                        PARTICIPATION_PCT = reader.GetValue(7).ToString(),

                        AA_MONTO_TASA = reader.GetValue(8).ToString(),

                    };


                    if (!string.IsNullOrEmpty(reader.GetValue(3).ToString()))
                    {
                        //paramdtl.DATE_ADDED = reader.GetValue(4).ToString();

                        paramdtl.AA_FECHA_TX = Convert.ToDateTime(reader.GetValue(3)).Day + "/" + Convert.ToDateTime(reader.GetValue(3)).Month.ToString("00") + "/" + Convert.ToDateTime(reader.GetValue(3)).Year;
                    }

                    OperacionesInt.Add(paramdtl);

                }


            }

            // retornar datos
            reader.Close();
            ContextOracle.Close();
            return OperacionesInt;


        }
        else
        {
            ContextOracle.Open();
            string sentencia = "select NRO_IDENTIDAD,SEQ_NBR,AA_TIPO_DIVISA,AA_FECHA_TX,AA_COD_AUTORIZ,AA_MONTO_OPR,CURRENCY_CD," +
                "PARTICIPATION_PCT,AA_MONTO_TASA FROM DIVISAS.PS_AA_DIV_MOV_PJ"; // tabla ....dtl


            sentencia = sentencia + " where NRO_IDENTIDAD = '" + value + "'";


            OracleCommand command = new OracleCommand(sentencia, ContextOracle);


            // llamado a los procedimientos
            OracleDataReader reader = null;


            reader = command.ExecuteReader();


            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    OperacionesIntEntity paramdtl = new OperacionesIntEntity()
                    {
                        ID = id++,
                        NRODOCUMENTO = reader.GetValue(0).ToString(),
                        SEQ_NBR = reader.GetValue(1).ToString(),
                        AA_TIPO_DIVISA = reader.GetValue(2).ToString(),
                        AA_FECHA_TX = reader.GetValue(3).ToString(),
                        AA_COD_AUTORIZ = reader.GetValue(4).ToString(),
                        AA_MONTO_OPR = reader.GetValue(5).ToString(),
                        CURRENCY_CD = reader.GetValue(6).ToString(),

                        PARTICIPATION_PCT = reader.GetValue(7).ToString(),

                        AA_MONTO_TASA = reader.GetValue(8).ToString(),

                    };


                    if (!string.IsNullOrEmpty(reader.GetValue(3).ToString()))
                    {
                        //paramdtl.DATE_ADDED = reader.GetValue(4).ToString();
                        paramdtl.AA_FECHA_TX = Convert.ToDateTime(reader.GetValue(3)).Day + "/" + Convert.ToDateTime(reader.GetValue(3)).Month.ToString("00") + "/" + Convert.ToDateTime(reader.GetValue(3)).Year;
                    }

                    OperacionesInt.Add(paramdtl);

                }


            }

            // retornar datos
            reader.Close();
            ContextOracle.Close();
            return OperacionesInt;
        }
    }








    public List<OperacionesdtlEntity> BloqueoDateEnd(string value)
    {
        // contador de objetos
        int id = 0;

        // respuesta de el metodo
        List<OperacionesdtlEntity> parametrosdtl = new List<OperacionesdtlEntity>();

        ContextOracle.Open();


        string sentencia = "SELECT Nvl(Max(ROW_LASTMANT_DTTM)-1, Max(ROW_ADDED_DTTM)-1) as variable1 FROM CRWORK.PS_AA_LISTA_BLOQ"; // tabla ....dtl
        sentencia = sentencia + " where AA_NRO_DOCUMENTO = '" + value + "' AND AA_ESTADO_BLQ='A' ";

        OracleCommand command = new OracleCommand(sentencia, ContextOracle);

        // llamado a los procedimientos
        OracleDataReader reader = null;



        reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {

                OperacionesdtlEntity paramdtl = new OperacionesdtlEntity()
                {

                    DATEEND = reader.GetValue(0).ToString(),

                };





                if (!string.IsNullOrEmpty(reader.GetValue(6).ToString()))
                {
                    //paramdtl.DATE_ADDED = reader.GetValue(4).ToString();
                    paramdtl.DATEEND = Convert.ToDateTime(reader.GetValue(6)).Month + "/" + Convert.ToDateTime(reader.GetValue(6)).Day + "/" + Convert.ToDateTime(reader.GetValue(6)).Year;


                }


                parametrosdtl.Add(paramdtl);
            }
        }



        // retornar datos
        reader.Close();
        ContextOracle.Close();
        return parametrosdtl;
    }



    public bool CrearParametrodtl(OperacionesdtlEntity Value)
    {
        bool respuesta = false;
        string[] dateAdded = Value.DATEEND.Split(' ');

        try
        {
            // instancia del procedimiento
            OracleCommand command = new OracleCommand("DIVISAS.av_crear_autorizaciones_div", ContextOracle);
            command.CommandType = CommandType.StoredProcedure;

            // parametros de entrada
            command.Parameters.Add(new OracleParameter("nro_documento", OracleType.VarChar)).Value = String.IsNullOrEmpty(Value.NRODOCUMENTO) ? " " : Value.NRODOCUMENTO;

            command.Parameters.Add(new OracleParameter("v_param_id", OracleType.VarChar)).Value = String.IsNullOrEmpty(Value.SEQNBR) ? " " : Value.SEQNBR;
            //command.Parameters.Add(new OracleParameter("v_descr100", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.AUTORIZADIVISA) ? " " : Value.AUTORIZADIVISA;
            //command.Parameters.Add(new OracleParameter("v_value_display", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.TIPOVIGENCIA) ? " " : Value.TIPOVIGENCIA;
            // command.Parameters.Add(new OracleParameter("v_value", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.SOPORTEDIVISA) ? " " : Value.SOPORTEDIVISA;
            // command.Parameters.Add(new OracleParameter("v_value_nbr", OracleType.Number)).Value = string.IsNullOrEmpty(Value.DATEEND) ? "1900/01/01" : Convert.ToDateTime(dateAdded[0]).ToString("yyyy/dd/MM");
            //command.Parameters.Add(new OracleParameter("v_date_added", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.MONTOPROMEDIO) ? " " : Value.MONTOPROMEDIO;
            //command.Parameters.Add(new OracleParameter("v_descr", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.AA_MONTO_PROM_VENT) ? " " : Value.AA_MONTO_PROM_VENT;
            //command.Parameters.Add(new OracleParameter("v_query_sql", OracleType.Number)).Value = string.IsNullOrEmpty(Value.ROWADDEDDTTM) ? "1900/01/01" : Convert.ToDateTime(dateAdded[0]).ToString("yyyy/dd/MM");

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

            throw;
        }


        return respuesta;
    }

    internal bool EliminarParametrodtl(OperacionesdtlEntity value)
    {
        bool respuesta = false;

        try
        {
            ContextOracle.Open();
            string sentencia = "DELETE DIVISAS.PS_AA_PN_OPR_INTDT where  SEQ_NBR = '" + value.SEQNBR + "'  and  NRO_IDENTIDAD = '" + value.NRODOCUMENTO + "'        ";

            OracleCommand command = new OracleCommand(sentencia, ContextOracle);

            // llamado a los procedimientos
            OracleDataReader reader = null;
            reader = command.ExecuteReader();


            // retornar datos
            reader.Close();
            ContextOracle.Close();
            respuesta = true;
        }
        catch (Exception error)
        {
            respuesta = false;
        }

        return respuesta;
    }





    internal bool EliminarEntidadClte(EntidadesFinancierasentity value)
    {
        bool respuesta = false;


        if (value.TIPODOCUMENTO != "N")
        {


            try
            {
                ContextOracle.Open();
                string sentencia = "DELETE DIVISAS.PS_AA_ENTI_OPRPN where  NRO_IDENTIDAD = '" + value.Nrodocumento + "' and AA_COD_ENTIDAD='" + value.CodEntidad + "' ";

                OracleCommand command = new OracleCommand(sentencia, ContextOracle);

                // llamado a los procedimientos
                OracleDataReader reader = null;
                reader = command.ExecuteReader();


                // retornar datos
                reader.Close();
                ContextOracle.Close();
                respuesta = true;
            }
            catch (Exception error)
            {
                respuesta = false;
            }

            return respuesta;

        }
        else
        {

            try
            {
                ContextOracle.Open();
                string sentencia = "DELETE DIVISAS.PS_AA_ENTI_OPRPJ where  NRO_IDENTIDAD = '" + value.Nrodocumento + "' and AA_COD_ENTIDAD='" + value.CodEntidad + "' ";

                OracleCommand command = new OracleCommand(sentencia, ContextOracle);

                // llamado a los procedimientos
                OracleDataReader reader = null;
                reader = command.ExecuteReader();


                // retornar datos
                reader.Close();
                ContextOracle.Close();
                respuesta = true;
            }
            catch (Exception error)
            {
                respuesta = false;
            }

            return respuesta;

        }



    }















    public bool ActualizarParametrodtl(OperacionesdtlEntity Value)
    {
        bool respuesta = false;
        string[] dateAdded = Value.DATEEND.Split(' ');

        try
        {
            // instancia del procedimiento
            OracleCommand command = new OracleCommand("DIVISAS.AV_ACTUALIZAR_PARAMETRO", ContextOracle);
            command.CommandType = CommandType.StoredProcedure;

            // parametros de entrada
            command.Parameters.Add(new OracleParameter("v_param_id", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.SEQNBR) ? "0" : Value.SEQNBR;
            command.Parameters.Add(new OracleParameter("v_descr100", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.AUTORIZADIVISA) ? " " : Value.AUTORIZADIVISA;
            command.Parameters.Add(new OracleParameter("v_value_display", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.TIPOVIGENCIA) ? " " : Value.TIPOVIGENCIA;
            command.Parameters.Add(new OracleParameter("v_value", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.SOPORTEDIVISA) ? " " : Value.SOPORTEDIVISA;
            command.Parameters.Add(new OracleParameter("v_value_nbr", OracleType.Number)).Value = string.IsNullOrEmpty(Value.DATEEND) ? "01/01/1900" : Convert.ToDateTime(dateAdded[0]).ToString("dd/MM/yyyy");
            command.Parameters.Add(new OracleParameter("v_date_added", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.MONTOPROMVENT) ? " " : Value.MONTOPROMVENT;
            command.Parameters.Add(new OracleParameter("v_descr", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.MONTOPROMEDIO) ? " " : Value.MONTOPROMEDIO;
            command.Parameters.Add(new OracleParameter("v_query_sql", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.ROWADDEDDTTM) ? "01/01/1900" : Convert.ToDateTime(dateAdded[0]).ToString("dd/MM/yyyy");
            command.Parameters.Add(new OracleParameter("v_added_opprid", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.ROWADDEDOPPRID) ? " " : Value.ROWADDEDOPPRID;
            command.Parameters.Add(new OracleParameter("v_lastmant_dttm", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.ROWLASTMANDTTM) ? "01/01/1900" : Convert.ToDateTime(dateAdded[0]).ToString("dd/MM/yyyy");
            command.Parameters.Add(new OracleParameter("v_lastmant_oprid", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.ROWLASTMANOPPRID) ? " " : Value.ROWLASTMANOPPRID; command.Parameters.Add(new OracleParameter("v_lastmant_oprid", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.ROWLASTMANOPPRID) ? " " : Value.ROWLASTMANOPPRID;
            command.Parameters.Add(new OracleParameter("v_bloqueo_divisas", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.BLOQUEODIVISAS) ? " " : Value.BLOQUEODIVISAS;


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

            throw;
        }


        return respuesta;
    }

    public bool ValidarParametrodtl(OperacionesdtlEntity value)
    {
        bool respuesta = false;

        ContextOracle.Open();
        string sentencia = "select count(*) from CRWORK.PS_AV_PAR_GRAL_DTL where  AND SEQ_NUM = '" + value.SEQNBR + "'";

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







    public List<ParamaetroOpDivisas> fn_AdicionarMeses(int value)
    {

        var FechaActual = DateTime.Now;

        List<ParamaetroOpDivisas> AdicionarMes = new List<ParamaetroOpDivisas>();

        ContextOracle.Open();
        string sentencia = "SELECT ADD_MONTHS(SYSDATE,12) from dual";

        OracleCommand command = new OracleCommand(sentencia, ContextOracle);

        // llamado a los procedimientos
        OracleDataReader reader = null;
        reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                ParamaetroOpDivisas adicionar = new ParamaetroOpDivisas()
                {


                    DATEEND = reader.GetValue(0).ToString()


                };



                AdicionarMes.Add(adicionar);

            }
        }

        // retornar datos
        reader.Close();
        ContextOracle.Close();

        return AdicionarMes;
    }














    public bool ValidarListaBloqueodtl(string value)
    {
        bool respuesta = false;

        ContextOracle.Open();
        string sentencia = "select count(*) from CRWORK.PS_AA_LISTA_BLOQ where  AA_NRO_DOCUMENTO = '" + value + "' AND AA_ESTADO_BLQ='A' ";

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







    public bool ValidarEmpleado(string value)
    {
        bool respuesta = false;

        ContextOracle.Open();
        string sentencia = "select * from CRWORK.AV_EMPLEADOS where  IDENTIFICACION = '" + value + "' AND ESTADO='E' ";

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









    //Este metodo trae los datos de la tabla #03
    public List<OperacionesdtlEntity> TraerDivisasPendientes(string value, string TipoDocumento)
    {
        // contador de objetos
        int id = 0;


        //var dateFIN = DateTime.ParseExact(paramdtl.DATEEND, "MM//yyyy", null);

        bool autorizacionValida = false;
        DateTime dt = new DateTime(1900, 1, 1);
        string lastOprid;

        // respuesta de el metodo
        List<OperacionesdtlEntity> parametrosdtl = new List<OperacionesdtlEntity>();

        ParametrosDivisas lista = new ParametrosDivisas();

        List<ParamaetroOpDivisas> parametros = new List<ParamaetroOpDivisas>();

        ParametroDivModel param2 = new ParametroDivModel();

        ParametroOIModel AdicionarMes = new ParametroOIModel();




        List<ParamaetroOpDivisas> Lista = new List<ParamaetroOpDivisas>();




        if (TipoDocumento != "N")
        {

            ContextOracle.Open();
            string sentencia = "select NRO_IDENTIDAD,TIPO_IDENTIDAD,SEQ_NBR, CASE AA_AUTORIZA_DIVISA WHEN 'Y' THEN 'SI' WHEN 'N' THEN 'NO' END AS AA_AUTORIZA_DIVISA," +
                "CASE AA_TIPO_VIGENCIA WHEN 'P' THEN 'Puntual' WHEN 'M' THEN 'Mensual' WHEN 'S' THEN 'Semestral' END AS AA_TIPO_VIGENCIA,CASE AA_SOPORTE_DIVISA WHEN 'NO' THEN 'NO' WHEN 'SI' THEN 'SI'" +
                "WHEN 'DIGI' THEN 'DIGITAL' END AS AA_TIPO_VIGENCIA" +
                ",DATE_END,AA_MONTO_PROM_VENT,AA_MONTO_PROMEDIO,ROW_ADDED_DTTM,ROW_ADDED_OPRID,ROW_LASTMANT_DTTM,ROW_LASTMANT_OPRID,AA_BLOQUEO_DIVISAS,AA_ESTADO_AUTORIZA,AA_AUTORIZACION_DIV " +
                "from DIVISAS.PS_AA_PN_OPR_INTDT"; // tabla ....dtl





            sentencia = sentencia + " where NRO_IDENTIDAD = '" + value + "' AND AA_AUTORIZA_DIVISA=' '  ORDER BY SEQ_NBR ";


            OracleCommand command = new OracleCommand(sentencia, ContextOracle);


            // llamado a los procedimientos
            OracleDataReader reader = null;


            reader = command.ExecuteReader();


            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    OperacionesdtlEntity paramdtl = new OperacionesdtlEntity()
                    {
                        ID = id++,
                        NRODOCUMENTO = reader.GetValue(0).ToString(),
                        TIPODOCUMENTO = reader.GetValue(1).ToString(),
                        SEQNBR = reader.GetValue(2).ToString(),
                        AUTORIZADIVISA = reader.GetValue(3).ToString(),
                        TIPOVIGENCIA = reader.GetValue(4).ToString(),
                        SOPORTEDIVISA = reader.GetValue(5).ToString(),

                        MONTOPROMVENT = reader.GetValue(7).ToString(),
                        MONTOPROMEDIO = reader.GetValue(8).ToString(),
                        ROWADDEDOPPRID = reader.GetValue(10).ToString(),

                        ROWLASTMANDTTM = reader.GetValue(11).ToString(),

                        ROWLASTMANOPPRID = reader.GetValue(12).ToString(),
                        BLOQUEODIVISAS = reader.GetValue(13).ToString(),
                        AA_ESTADO_AUTORIZA = reader.GetValue(14).ToString(),
                        AA_AUTORIZACION_DIV = reader.GetValue(15).ToString(),
                    };


                    paramdtl.AA_AUTORIZACION = "SI";
                    autorizacionValida = false;


                    if (!string.IsNullOrEmpty(reader.GetValue(6).ToString()))
                    {
                        //paramdtl.DATE_ADDED = reader.GetValue(4).ToString();
                        paramdtl.DATEEND = Convert.ToDateTime(reader.GetValue(6)).Day + "/" + Convert.ToDateTime(reader.GetValue(6)).Month.ToString("00") + "/" + Convert.ToDateTime(reader.GetValue(6)).Year;




                    }


                    if (!string.IsNullOrEmpty(reader.GetValue(9).ToString()))
                    {
                        //paramdtl.DATE_ADDED = reader.GetValue(4).ToString();
                        paramdtl.ROWADDEDDTTM = Convert.ToDateTime(reader.GetValue(9)).Day + "/" + Convert.ToDateTime(reader.GetValue(9)).Month + "/" + Convert.ToDateTime(reader.GetValue(9)).Year;
                    }

                    /*
                    if (!string.IsNullOrEmpty(reader.GetValue(11).ToString()))
                    {
                        //paramdtl.DATE_ADDED = reader.GetValue(4).ToString();
                        paramdtl.ROWLASTMANDTTM = Convert.ToDateTime(reader.GetValue(11)).Day + "/" + Convert.ToDateTime(reader.GetValue(11)).Month + "/" + Convert.ToDateTime(reader.GetValue(11)).Year;
                    }*/




                    var DATEEND_ = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(6).ToString()));
                    var ROW_LASTMANT_DTTM = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(6).ToString()));
                    var DIFERENCIA_DIAS = (DATEEND_ - DateTime.Now).Days;
                    var AUTORIZADIVISA = reader.GetValue(3).ToString();



                    if (DIFERENCIA_DIAS >= 0 && AUTORIZADIVISA.Equals("SI"))
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
                        paramdtl.AA_AUTORIZACION = "NO";
                    }





                    if (lista.ValidarListaBloqueo(paramdtl.NRODOCUMENTO))
                    {

                        paramdtl.AA_AUTORIZACION = "NO";
                        Lista = param2.BloqueoDateEnd(value);



                        if (paramdtl.AA_ESTADO_AUTORIZA == "Vigente")
                        {

                            paramdtl.DATEEND = Lista[0].DATEEND;
                            paramdtl.AA_AUTORIZACION = "NO";


                        }


                    }


                    /*
                    if (paramdtl.TIPOVIGENCIA == "Mensual")
                    {

                        fn_AdicionarMeses(12);


                    }
                    */




                    parametrosdtl.Add(paramdtl);

                }




            }
            // retornar datos
            reader.Close();
            ContextOracle.Close();
            return parametrosdtl;
        }
        else
        {

            ContextOracle.Open();
            string sentencia = "select NRO_IDENTIDAD,TIPO_IDENTIDAD,SEQ_NBR, CASE AA_AUTORIZA_DIVISA WHEN 'Y' THEN 'SI' WHEN 'N' THEN 'NO' END AS AA_AUTORIZA_DIVISA," +
                "CASE AA_TIPO_VIGENCIA WHEN 'P' THEN 'Puntual' WHEN 'M' THEN 'Mensual' WHEN 'S' THEN 'Semestral' END AS AA_TIPO_VIGENCIA,CASE AA_SOPORTE_DIVISA WHEN 'NO' THEN 'NO' WHEN 'SI' THEN 'SI'" +
                "WHEN 'DIGI' THEN 'DIGITAL' END AS AA_TIPO_VIGENCIA" +
                ",DATE_END,AA_MONTO_PROM_VENT,AA_MONTO_PROMEDIO,ROW_ADDED_DTTM,ROW_ADDED_OPRID,ROW_LASTMANT_DTTM,ROW_LASTMANT_OPRID,AA_BLOQUEO_DIVISAS,AA_ESTADO_AUTORIZA " +
                "from DIVISAS.PS_AA_PJ_OPR_INTDT"; // tabla ....dtl




            sentencia = sentencia + " where NRO_IDENTIDAD = '" + value + "' AND AA_AUTORIZA_DIVISA=' '  ORDER BY SEQ_NBR ";

            //sentencia = sentencia + " where NRO_IDENTIDAD = '" + value + "' ORDER BY SEQ_NBR";


            OracleCommand command = new OracleCommand(sentencia, ContextOracle);


            // llamado a los procedimientos
            OracleDataReader reader = null;


            reader = command.ExecuteReader();


            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    OperacionesdtlEntity paramdtl = new OperacionesdtlEntity()
                    {
                        ID = id++,
                        NRODOCUMENTO = reader.GetValue(0).ToString(),
                        TIPODOCUMENTO = reader.GetValue(1).ToString(),
                        SEQNBR = reader.GetValue(2).ToString(),
                        AUTORIZADIVISA = reader.GetValue(3).ToString(),
                        TIPOVIGENCIA = reader.GetValue(4).ToString(),
                        SOPORTEDIVISA = reader.GetValue(5).ToString(),

                        MONTOPROMVENT = reader.GetValue(7).ToString(),
                        MONTOPROMEDIO = reader.GetValue(8).ToString(),
                        ROWADDEDOPPRID = reader.GetValue(10).ToString(),

                        ROWLASTMANDTTM = reader.GetValue(11).ToString(),

                        ROWLASTMANOPPRID = reader.GetValue(12).ToString(),
                        BLOQUEODIVISAS = reader.GetValue(13).ToString(),
                        AA_ESTADO_AUTORIZA = reader.GetValue(14).ToString(),

                    };





                    if (!string.IsNullOrEmpty(reader.GetValue(6).ToString()))
                    {
                        //paramdtl.DATE_ADDED = reader.GetValue(4).ToString();
                        paramdtl.DATEEND = Convert.ToDateTime(reader.GetValue(6)).Day + "/" + Convert.ToDateTime(reader.GetValue(6)).Month + "/" + Convert.ToDateTime(reader.GetValue(6)).Year;
                    }


                    if (!string.IsNullOrEmpty(reader.GetValue(9).ToString()))
                    {
                        //paramdtl.DATE_ADDED = reader.GetValue(4).ToString();
                        paramdtl.ROWADDEDDTTM = Convert.ToDateTime(reader.GetValue(9)).Day + "/" + Convert.ToDateTime(reader.GetValue(9)).Month + "/" + Convert.ToDateTime(reader.GetValue(9)).Year;
                    }

                    /*
                    if (!string.IsNullOrEmpty(reader.GetValue(11).ToString()))
                    {
                        //paramdtl.DATE_ADDED = reader.GetValue(4).ToString();
                        paramdtl.ROWLASTMANDTTM = Convert.ToDateTime(reader.GetValue(11)).Day + "/" + Convert.ToDateTime(reader.GetValue(11)).Month + "/" + Convert.ToDateTime(reader.GetValue(11)).Year;
                    }
                    */


                    if (lista.ValidarListaBloqueo(paramdtl.NRODOCUMENTO))
                    {

                        paramdtl.AA_AUTORIZACION = "NO";
                        Lista = param2.BloqueoDateEnd(value);



                        if (paramdtl.AA_ESTADO_AUTORIZA == "Vigente")
                        {

                            paramdtl.DATEEND = Lista[0].DATEEND;
                            paramdtl.AA_AUTORIZACION = "NO";


                        }


                    }


                    /*
                    if (paramdtl.TIPOVIGENCIA == "Mensual")
                    {

                        fn_AdicionarMeses(12);


                    }
                    */

                    var DATEEND_ = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(6).ToString()));
                    var ROW_LASTMANT_DTTM = Convert.ToDateTime(string.Format("{0:yyy-MM-dd HH:mm:ss}", reader.GetValue(6).ToString()));
                    var dias = (DateTime.Now - DATEEND_).Days;
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
                        paramdtl.AA_AUTORIZACION = "NO";
                    }


                    parametrosdtl.Add(paramdtl);

                }




            }
            // retornar datos
            reader.Close();
            ContextOracle.Close();
            return parametrosdtl;

        }


    }





}







public class OperacionesdtlEntity
{
    public OperacionesdtlEntity()
    {
        this.AA_ESTADO_AUTORIZA = " ";
        this.ROWLASTMANOPPRID = " ";
        this.BLOQUEODIVISAS = " ";
        this.AA_AUTORIZACION = " ";
        this.ROWADDEDOPPRID = " ";
        this.TIPOVIGENCIA = "";
        this.AUTORIZADIVISA = " ";
        this.ID = 0;
        this.NRODOCUMENTO = " ";
        this.SEQNBR = " ";
        this.MONTOPROMVENT = " ";

        this.AA_AUTORIZACION_DIV = " ";
        this.SOPORTEDIVISA = " ";
        this.MONTOPROMEDIO = " ";
        this.DATEEND = " ";
        this.ROWADDEDDTTM = " ";
        this.ROWLASTMANDTTM = " ";
        this.BloqueoGlobal = " ";
        this.EMPLEADO = " ";
        this.lastOprid = " ";
        this.CARGO = " ";
        this.cantidadVigentes = 0;
        this.CODIGOCARGO = " ";
    }

    public int cantidadVigentes { get; set; }
    public string CARGO { get; set; }

    public string CODIGOCARGO { get; set; }

    public string EMPLEADO { get; set; }
    public string BloqueoGlobal { get; set; }
    public int ID { get; set; }

    public string AA_ESTADO_AUTORIZA { get; set; }
    public string AA_AUTORIZACION { get; set; }
    public string ROWLASTMANDTTM { get; set; }

    public string DATEEND { get; set; }

    public string ROWADDEDDTTM { get; set; }

    public string NRODOCUMENTO { get; set; } //

    public string SEQNBR { get; set; } // -

    public string TIPODOCUMENTO { get; set; } //


    public string AUTORIZADIVISA { get; set; } //

    public string TIPOVIGENCIA { get; set; } //

    public string SOPORTEDIVISA { get; set; } //

    public string MONTOPROMVENT { get; set; } //

    public string MONTOPROMEDIO { get; set; } //

    public string ROWADDEDOPPRID { get; set; }

    public string ROWLASTMANOPPRID { get; set; }

    public string BLOQUEODIVISAS { get; set; }

    public string AA_AUTORIZACION_DIV { get; set; }

    public string lastOprid { get; set; }
}







public class OperacionesIntEntity
{
    public OperacionesIntEntity()
    {

        this.SEQ_NBR = " ";
        this.AA_TIPO_DIVISA = " ";
        this.AA_FECHA_TX = " ";
        this.AA_COD_AUTORIZ = " ";

        this.AA_MONTO_OPR = "";
        this.CURRENCY_CD = " ";
        this.ID = 0;
        this.PARTICIPATION_PCT = " ";
        this.AA_MONTO_TASA = " ";
        this.NRODOCUMENTO = " ";

    }
    public int ID { get; set; }

    public string NRODOCUMENTO { get; set; }
    public string SEQ_NBR { get; set; }
    public string AA_TIPO_DIVISA { get; set; }

    public string AA_FECHA_TX { get; set; }

    public string AA_COD_AUTORIZ { get; set; }

    public string AA_MONTO_OPR { get; set; } //

    public string CURRENCY_CD { get; set; } // -

    public string PARTICIPATION_PCT { get; set; } //


    public string AA_MONTO_TASA { get; set; } //



}