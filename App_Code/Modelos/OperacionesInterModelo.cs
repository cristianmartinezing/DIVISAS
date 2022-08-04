using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Globalization;


/// <summary>
/// Summary description for ParametroModel
/// </summary>
public class OperacionesInterModel : RepositoryOracle
{
    public OperacionesInterModel()
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




    public bool ValidarOperaciones(string value, string tipoDocumento)
    {
        bool respuesta = false;

        if (tipoDocumento != "N")
        {

            ContextOracle.Open();
            string sentencia = "select count(*) from DIVISAS.PS_AA_PN_OPR_INTER where NRO_IDENTIDAD = '" + value + "'     ";

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
            string sentencia = "select count(*) from DIVISAS.PS_AA_PJ_OPR_INTER where NRO_IDENTIDAD = '" + value + "'     ";

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








    public bool ValidarEmpleado(string value, string tipoDocumento)
    {
        bool respuesta = false;

        if (tipoDocumento != "N")
        {

            ContextOracle.Open();
            string sentencia = "select count(*) from CRWORK.AV_EMPLEADOS where NRO_IDENTIDAD = '" + value + "'     ";

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
            string sentencia = "select count(*) from DIVISAS.PS_AA_PJ_OPR_INTER where NRO_IDENTIDAD = '" + value + "'     ";

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










    public List<OperacionesInterPNEntity> TraerOperacionesInt(string value, string value2)
    {
        // respuesta de el metodo
        List<OperacionesInterPNEntity> parametros = new List<OperacionesInterPNEntity>();


        if (value2 != "N")
        {

            ContextOracle.Open();



            string sentencia = "select AA_IMPORTACION,AA_EXPORTACION,AA_INVERSION,AA_PRESTAMOS,AA_GIROS,AA_ENVIO_GIROS,AA_SERVICIOS," +
            "AA_MONTO_PROM_VENT,AA_MONTO_PROMEDIO,CURRENCY_CD,AA_TOTAL_VENTA_USD,AA_TOTAL_COMPR_USD,AA_TOTAL_VENTA_COP,AA_TOTAL_COMPR_COP," +
            "AA_MONTO_OPR,AA_MONTO_VENTA_OPR,ROW_LASTMANT_DTTM,ROW_LASTMANT_OPRID,AA_CTA_EXTERIOR,AA_DIVISAS,AA_CIUDAD_OPR,COUNTRY,AA_ENTIDAD_OPR" +
            ",AA_TIPO_CUENTA,CURRENCY_CD_BASE,AA_NRO_CUENTA,AA_SALDO_FIN,NRO_IDENTIDAD,TIPO_IDENTIDAD from DIVISAS.PS_AA_PN_OPR_INTER"; // tabla ....tbl






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


                        OperacionesInterPNEntity param = new OperacionesInterPNEntity()
                        {

                            AA_IMPORTACION = reader.GetValue(0).ToString(),
                            AA_EXPORTACION = reader.GetValue(1).ToString(),
                            AA_INVERSION = reader.GetValue(2).ToString(),
                            AA_PRESTAMOS = reader.GetValue(3).ToString(),
                            AA_GIROS = reader.GetValue(4).ToString(),
                            AA_ENVIO_GIROS = reader.GetValue(5).ToString(),
                            AA_SERVICIOS = reader.GetValue(6).ToString(),
                            AA_MONTO_PROM_VENT = reader.GetValue(7).ToString(),

                            AA_MONTO_PROMEDIO = reader.GetValue(8).ToString(),
                            CURRENCY_CD = reader.GetValue(9).ToString(),
                            AA_TOTAL_VENTA_USD = reader.GetValue(10).ToString(),
                            AA_TOTAL_COMPR_USD = reader.GetValue(11).ToString(),
                            AA_TOTAL_VENTA_COP = reader.GetValue(12).ToString(),
                            AA_TOTAL_COMPR_COP = reader.GetValue(13).ToString(),
                            AA_MONTO_OPR = reader.GetValue(14).ToString(),

                            AA_MONTO_VENTA_OPR = reader.GetValue(15).ToString(),

                            ROW_LASTMANT_DTTM = reader.GetValue(16).ToString(),
                            ROW_LASTMANT_OPRID = reader.GetValue(17).ToString(),
                            AA_CTA_EXTERIOR = reader.GetValue(18).ToString(),
                            AA_DIVISAS = reader.GetValue(19).ToString(),
                            AA_CIUDAD_OPR = reader.GetValue(20).ToString(),
                            COUNTRY = reader.GetValue(21).ToString(),
                            AA_ENTIDAD_OPR = reader.GetValue(22).ToString(),
                            AA_TIPO_CUENTA = reader.GetValue(23).ToString(),
                            CURRENCY_CD_BASE = reader.GetValue(24).ToString(),
                            AA_NRO_CUENTA = reader.GetValue(25).ToString(),
                            AA_SALDO_FIN = reader.GetValue(26).ToString(),
                            NRODOCUMENTO = reader.GetValue(27).ToString(),
                            TIPODOCUMENTO = reader.GetValue(28).ToString(),



                        };


                        /*
                        if (!string.IsNullOrEmpty(reader.GetValue(16).ToString()))
                        {
                            param.ROW_LASTMANT_DTTM = Convert.ToDateTime(reader.GetValue(16)).Month + "/" + Convert.ToDateTime(reader.GetValue(16)).Day + "/" + Convert.ToDateTime(reader.GetValue(16)).Year;
                        }*/



                        parametros.Add(param);
                    }
                }
                else
                {

                    if (!reader.Read())
                    {


                        OperacionesInterPNEntity param = new OperacionesInterPNEntity()
                        {

                            AA_IMPORTACION = "N",
                            AA_EXPORTACION = "N",
                            AA_INVERSION = "N",
                            AA_PRESTAMOS = "N",
                            AA_GIROS = "N",
                            AA_SERVICIOS = "N",
                            AA_ENVIO_GIROS = "N",
                            AA_TOTAL_VENTA_USD = "0",


                            AA_TOTAL_COMPR_USD = "0",
                            AA_DIVISAS = "N",
                            AA_MONTO_PROM_VENT = "0",
                            AA_SALDO_FIN = "0",
                            AA_MONTO_PROMEDIO="0"


                        };


                        /*
                        if (!string.IsNullOrEmpty(reader.GetValue(5).ToString()))
                        {
                            param.DATE_ADDED = Convert.ToDateTime(reader.GetValue(5)).Month + "/" + Convert.ToDateTime(reader.GetValue(5)).Day + "/" + Convert.ToDateTime(reader.GetValue(5)).Year;
                        }
                        */


                        parametros.Add(param);



                    }

                }
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



            string sentencia = "select AA_IMPORTACION,AA_EXPORTACION,AA_INVERSION,AA_PRESTAMOS,AA_GIROS,AA_ENVIO_GIROS,AA_SERVICIOS," +
            "AA_MONTO_PROM_VENT,AA_MONTO_PROMEDIO,CURRENCY_CD,AA_TOTAL_VENTA_USD,AA_TOTAL_COMPR_USD,AA_TOTAL_VENTA_COP,AA_TOTAL_COMPR_COP," +
            "AA_MONTO_OPR,AA_MONTO_VENTA_OPR,ROW_LASTMANT_DTTM,ROW_LASTMANT_OPRID,AA_CTA_EXTERIOR,AA_DIVISAS,AA_CIUDAD_OPR,COUNTRY,AA_BANCO" +
            ",AA_TIPO_CUENTA,CURRENCY_CD_BASE,AA_NRO_CUENTA,AA_SALDO_FIN,NRO_IDENTIDAD,TIPO_IDENTIDAD  from DIVISAS.PS_AA_PJ_OPR_INTER"; // tabla ....tbl






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


                        OperacionesInterPNEntity param = new OperacionesInterPNEntity()
                        {

                            AA_IMPORTACION = reader.GetValue(0).ToString(),
                            AA_EXPORTACION = reader.GetValue(1).ToString(),
                            AA_INVERSION = reader.GetValue(2).ToString(),
                            AA_PRESTAMOS = reader.GetValue(3).ToString(),
                            AA_GIROS = reader.GetValue(4).ToString(),
                            AA_ENVIO_GIROS = reader.GetValue(5).ToString(),
                            AA_SERVICIOS = reader.GetValue(6).ToString(),
                            AA_MONTO_PROM_VENT = reader.GetValue(7).ToString(),

                            AA_MONTO_PROMEDIO = reader.GetValue(8).ToString(),
                            CURRENCY_CD = reader.GetValue(9).ToString(),
                            AA_TOTAL_VENTA_USD = reader.GetValue(10).ToString(),
                            AA_TOTAL_COMPR_USD = reader.GetValue(11).ToString(),
                            AA_TOTAL_VENTA_COP = reader.GetValue(12).ToString(),
                            AA_TOTAL_COMPR_COP = reader.GetValue(13).ToString(),
                            AA_MONTO_OPR = reader.GetValue(14).ToString(),

                            AA_MONTO_VENTA_OPR = reader.GetValue(15).ToString(),

                            ROW_LASTMANT_DTTM = reader.GetValue(16).ToString(),
                            ROW_LASTMANT_OPRID = reader.GetValue(17).ToString(),
                            AA_CTA_EXTERIOR = reader.GetValue(18).ToString(),
                            AA_DIVISAS = reader.GetValue(19).ToString(),
                            AA_CIUDAD_OPR = reader.GetValue(20).ToString(),
                            COUNTRY = reader.GetValue(21).ToString(),
                            AA_ENTIDAD_OPR = reader.GetValue(22).ToString(),
                            AA_TIPO_CUENTA = reader.GetValue(23).ToString(),
                            CURRENCY_CD_BASE = reader.GetValue(24).ToString(),
                            AA_NRO_CUENTA = reader.GetValue(25).ToString(),
                            AA_SALDO_FIN = reader.GetValue(26).ToString(),
                            NRODOCUMENTO = reader.GetValue(27).ToString(),
                            TIPODOCUMENTO = reader.GetValue(28).ToString(),


                        };


                        /*
                        if (!string.IsNullOrEmpty(reader.GetValue(5).ToString()))
                        {
                            param.DATE_ADDED = Convert.ToDateTime(reader.GetValue(5)).Month + "/" + Convert.ToDateTime(reader.GetValue(5)).Day + "/" + Convert.ToDateTime(reader.GetValue(5)).Year;
                        }
                        */


                        parametros.Add(param);
                    }
                }
                else
                {

                    if (!reader.Read())
                    {


                        OperacionesInterPNEntity param = new OperacionesInterPNEntity()
                        {


                            AA_IMPORTACION = "N",
                            AA_EXPORTACION = "N",
                            AA_INVERSION = "N",
                            AA_PRESTAMOS = "N",
                            AA_GIROS = "N",
                            AA_DIVISAS = "N",
                            AA_SERVICIOS = "N",
                            AA_ENVIO_GIROS = "N",
                            AA_TOTAL_VENTA_USD = "0",
                            AA_TOTAL_COMPR_USD = "0",
                            AA_MONTO_PROM_VENT = "0",
                            AA_SALDO_FIN = "0",
                            AA_MONTO_PROMEDIO="0",
                        };


                        /*
                        if (!string.IsNullOrEmpty(reader.GetValue(5).ToString()))
                        {
                            param.DATE_ADDED = Convert.ToDateTime(reader.GetValue(5)).Month + "/" + Convert.ToDateTime(reader.GetValue(5)).Day + "/" + Convert.ToDateTime(reader.GetValue(5)).Year;
                        }
                        */


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






    public List<EntidadesFinancierasentity> EntidadesFinancieras()
    {
        List<EntidadesFinancierasentity> resultado = new List<EntidadesFinancierasentity>();

        ContextOracle.Open();
        string sentencia = "select * from CRWORK.PS_AA_ENTFINAN_TBL";

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


                    EntidadesFinancierasentity ofi = new EntidadesFinancierasentity()
                    {
                        CodEntidad = reader.GetValue(0).ToString(),
                        EntidadFinanciera = reader.GetValue(1).ToString(),

                    };



                    resultado.Add(ofi);
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

    public List<OperacionesInterPNEntity> Venta_usd_tope(string NroDocumento, string TipoDocumento)
    {
        List<OperacionesInterPNEntity> resultado = new List<OperacionesInterPNEntity>();


        if (TipoDocumento != "N")
        {



            ContextOracle.Open();
            string sentencia = "select sum(AA_MONTO_PROM_VENT) AS Monto, sum(AA_MONTO_PROMEDIO) as MONTOPROM from DIVISAS.PS_AA_PN_OPR_INTDT where AA_ESTADO_AUTORIZA='Vigente' and NRO_IDENTIDAD='" + NroDocumento + "' and TIPO_IDENTIDAD='" + TipoDocumento + "' ";

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


                        OperacionesInterPNEntity ofi = new OperacionesInterPNEntity()
                        {
                            AA_MONTO_PROM_VENT = reader.GetValue(0).ToString(),
                            AA_MONTO_PROMEDIO = reader.GetValue(1).ToString(),

                        };




                        resultado.Add(ofi);
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
        else
        {


            ContextOracle.Open();
            string sentencia = "select sum(AA_MONTO_PROM_VENT) AS Monto, sum(AA_MONTO_PROMEDIO) as MONTOPROM from DIVISAS.PS_AA_PJ_OPR_INTDT where AA_ESTADO_AUTORIZA='Vigente' and NRO_IDENTIDAD='" + NroDocumento + "' and TIPO_IDENTIDAD='" + TipoDocumento + "' ";

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


                        OperacionesInterPNEntity ofi = new OperacionesInterPNEntity()
                        {
                            AA_MONTO_PROM_VENT = reader.GetValue(0).ToString(),
                            AA_MONTO_PROMEDIO = reader.GetValue(1).ToString(),

                        };




                        resultado.Add(ofi);
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


    }






    public List<DetallesTPEntity> Av_info_actualizado(DateTime valor)
    {




        List<DetallesTPEntity> resultado = new List<DetallesTPEntity>();

        ContextOracle.Open();
        string sentencia = "SELECT VALUE_NBR FROM CRWORK.PS_AV_PAR_GRAL_TBL WHERE PARAM_ID = 'DIAS_LMT_DIVISA'";

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


                    DetallesTPEntity ofi = new DetallesTPEntity()
                    {
                        VALUE_NBR = reader.GetValue(0).ToString(),





                    };


                    DateTime fechaActual = DateTime.Today;

                    int res = DateTime.Compare(valor, fechaActual);
                    Console.WriteLine(res);

                    TimeSpan ts = fechaActual - valor;

                    int diferenciaDias = ts.Days;
                    int valueNbr = Int32.Parse(ofi.VALUE_NBR);

                    if (diferenciaDias > valueNbr)
                    {
                        ofi.AUTORIZADIVISAS = false;
                    }
                    else
                    {
                        ofi.AUTORIZADIVISAS = true;
                    }


                    resultado.Add(ofi);
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







    public List<DatosUsuarioPNentity> AV_INFO_ACTUALIZADO()
    {
        List<DatosUsuarioPNentity> resultado = new List<DatosUsuarioPNentity>();

        ContextOracle.Open();
        string sentencia = "SELECT VALUE_NBR FROM CRWORK.PS_AV_PAR_GRAL_TBL WHERE PARAM_ID = 'DIAS_LMT_DIVISA'";

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


                    DatosUsuarioPNentity ofi = new DatosUsuarioPNentity()
                    {
                        VALUE_NBR = reader.GetValue(0).ToString(),


                    };

                    if (ofi.VALUE_NBR == " ")
                    {

                        ofi.VALUE_NBR = "365";

                    }



                    resultado.Add(ofi);
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



    public List<OperacionesInterPNEntity> CargosAutorizanDiv(string value)
    {

        var valor = double.Parse(value, CultureInfo.InvariantCulture);
        // respuesta de el metodo
        List<OperacionesInterPNEntity> parametros = new List<OperacionesInterPNEntity>();




        ContextOracle.Open();
        string sentencia = "SELECT distinct  LISTAGG(VALUE_DISPLAY, ', ') WITHIN GROUP (ORDER BY VALUE_DISPLAY) FROM CRWORK.PS_AV_PAR_GRAL_DTL";

        // validar parametro
        if (!string.IsNullOrEmpty(value))
        {
            sentencia = sentencia + " WHERE PARAM_ID='AUTORIZACION_DV' AND(to_number('" + valor + "') BETWEEN DESCR254_8 AND DESCR254_9)";

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




                    OperacionesInterPNEntity param = new OperacionesInterPNEntity()

                    {
                        DESCR = reader.GetValue(0).ToString(),

                    };
                    parametros.Add(param);

                }
            }
            else
            {


                OperacionesInterPNEntity param = new OperacionesInterPNEntity()

                {
                    DESCR = "N",

                };
                parametros.Add(param);



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










    public List<OperacionesInterPNEntity> puedeAutorizar(string compra, string venta, string jobcode)
    {
        // respuesta de el metodo
        List<OperacionesInterPNEntity> parametros = new List<OperacionesInterPNEntity>();


        if (venta == "")
        {
            venta = "100000000";

        }

        if (compra == "")
        {
            compra = "100000000";


        }
        //string valor1=  value.Replace(",",".");   
        //var valor = Double.Parse(valor1);
        // var valor22 = double.Parse(value2, CultureInfo.InvariantCulture);
        //var valor33 = double.Parse(value, CultureInfo.InvariantCulture);

        ContextOracle.Open();
        string sentencia = "SELECT 'Y' from CRWORK.PS_AV_PAR_GRAL_DTL";

        string sentencia2 = "SELECT 'Y' from CRWORK.PS_AV_PAR_GRAL_DTL";

        // validar parametro
        if (!string.IsNullOrEmpty(venta) || !string.IsNullOrEmpty(compra))
        {
            sentencia = sentencia + " WHERE PARAM_ID='AUTORIZACION_DV' AND DESCR_OTHER = '" + jobcode + "' AND(to_number('" + venta + "') BETWEEN DESCR254_8 AND DESCR254_9)";

            sentencia2 = sentencia2 + " WHERE PARAM_ID='AUTORIZACION_DV' AND DESCR_OTHER = '" + jobcode + "' AND(to_number('" + compra + "') BETWEEN DESCR254_8 AND DESCR254_9)";

        }
        OracleCommand command2 = new OracleCommand(sentencia2, ContextOracle);

        OracleCommand command = new OracleCommand(sentencia, ContextOracle);
        // llamado a los procedimientos
        OracleDataReader reader = null;
        OracleDataReader reader2 = null;


        try
        {
            reader2 = command2.ExecuteReader();

            reader = command.ExecuteReader();

            if (reader.HasRows && compra == "100000000")
            {
                while (reader.Read())
                {

                    OperacionesInterPNEntity param = new OperacionesInterPNEntity();

                    if (reader.HasRows)
                    {
                        param.puedeAutorizar = reader.GetValue(0).ToString();
                        param.puedeAutorizar2 = "N";


                    }

                    else
                    {
                        param.puedeAutorizar = "N";
                    }

                    parametros.Add(param);

                }
            }

            else if (reader2.HasRows && venta == "100000000")
            {
                while (reader2.Read())
                {
                    OperacionesInterPNEntity param = new OperacionesInterPNEntity();


                    if (reader2.HasRows)
                    {
                        param.puedeAutorizar2 = reader2.GetValue(0).ToString();
                        param.puedeAutorizar = "N";


                    }

                    else
                    {
                        param.puedeAutorizar2 = "N";
                    }




                    parametros.Add(param);

                }
            }


            else if (reader2.HasRows && reader.HasRows && compra != "100000000" && venta != "100000000")
            {
                while (reader2.Read() && reader.Read())
                {
                    OperacionesInterPNEntity param = new OperacionesInterPNEntity();


                    if (reader2.HasRows && reader.HasRows)
                    {
                        param.puedeAutorizar2 = reader2.GetValue(0).ToString();
                        param.puedeAutorizar = reader.GetValue(0).ToString();


                    }

                    else
                    {
                        param.puedeAutorizar = "N";

                        param.puedeAutorizar2 = "N";
                    }




                    parametros.Add(param);

                }
            }

            else if (reader2.HasRows && !reader.HasRows && compra != "100000000" && venta != "100000000")
            {
                while (reader2.Read())
                {
                    OperacionesInterPNEntity param = new OperacionesInterPNEntity();


                    if (reader2.HasRows)
                    {
                        param.puedeAutorizar2 = reader2.GetValue(0).ToString();
                        param.puedeAutorizar = "N";



                    }


                    parametros.Add(param);

                }
            }

            else if (!reader2.HasRows && reader.HasRows && compra != "100000000" && venta != "100000000")
            {
                while (reader.Read())
                {
                    OperacionesInterPNEntity param = new OperacionesInterPNEntity();


                    if (reader.HasRows)
                    {
                        param.puedeAutorizar = reader.GetValue(0).ToString();
                        param.puedeAutorizar2 = "N";



                    }


                    parametros.Add(param);

                }
            }
            else
            {


                OperacionesInterPNEntity param = new OperacionesInterPNEntity()

                {
                    puedeAutorizar = "N",
                    puedeAutorizar2 = "N",
                };
                parametros.Add(param);



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







    public List<OperacionesInterPNEntity> AutorizarMontoCompra(string value, string value2, string jobcode)
    {
        // respuesta de el metodo
        List<OperacionesInterPNEntity> parametros = new List<OperacionesInterPNEntity>();



        ContextOracle.Open();
        string sentencia = "SELECT 'Y' from DIVISAS.PS_AA_AUTORIDIVIZA";

        string sentencia2 = "SELECT 'Y' from DIVISAS.PS_AA_AUTORIDIVIZA";

        // validar parametro
        if (!string.IsNullOrEmpty(value) || !string.IsNullOrEmpty(value2))
        {
            sentencia = sentencia + " WHERE BO_TYPE_ID = 1 AND JOBCODE = '" + jobcode + "' AND('" + value + "' BETWEEN AA_VENTAS_DESDE AND AA_VENTAS_HASTA)";

            sentencia2 = sentencia2 + " WHERE BO_TYPE_ID = 1 AND JOBCODE = '" + jobcode + "' AND('" + value2 + "' BETWEEN AA_VENTAS_DESDE AND AA_VENTAS_HASTA)";

        }
        OracleCommand command2 = new OracleCommand(sentencia2, ContextOracle);

        OracleCommand command = new OracleCommand(sentencia, ContextOracle);
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




                    OperacionesInterPNEntity param = new OperacionesInterPNEntity()

                    {
                        puedeAutorizar = reader.GetValue(0).ToString(),
                        puedeAutorizar2 = reader2.GetValue(0).ToString(),


                    };
                    parametros.Add(param);

                }
            }
            else
            {


                OperacionesInterPNEntity param = new OperacionesInterPNEntity()

                {
                    puedeAutorizar = "N",
                    puedeAutorizar2 = "N",
                };
                parametros.Add(param);



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






    public bool ActualizarOperaciones(OperacionesInterPNEntity Value)
    {

        if (Value.AA_NRO_CUENTA == "" || Value.AA_TOTAL_COMPR_USD == null)
        {

            Value.AA_NRO_CUENTA = " ";

        }

        if (Value.AA_SALDO_FIN == "" || Value.AA_SALDO_FIN == null)
        {

            Value.AA_SALDO_FIN = "0";

        }

        if (Value.AA_TIPO_CUENTA == "" || Value.AA_TIPO_CUENTA == null)
        {

            Value.AA_TIPO_CUENTA = " ";

        }

        if (Value.CURRENCY_CD_BASE == "" || Value.CURRENCY_CD_BASE == null)
        {

            Value.CURRENCY_CD_BASE = " ";

        }

        if (Value.AA_CTA_EXTERIOR == "" || Value.AA_CTA_EXTERIOR == null)
        {

            Value.AA_CTA_EXTERIOR = "N";

        }

        if (Value.COUNTRY == "" || Value.COUNTRY == null)
        {

            Value.COUNTRY = " ";

        }

        if (Value.AA_ENTIDAD_OPR == "" || Value.AA_ENTIDAD_OPR == null)
        {

            Value.AA_ENTIDAD_OPR = " ";

        }


        if (Value.AA_TOTAL_COMPR_USD == "" || Value.AA_TOTAL_COMPR_USD == null)
        {

            Value.AA_TOTAL_COMPR_USD = "0";
        }


        if (Value.AA_TOTAL_COMPR_COP == "" || Value.AA_TOTAL_COMPR_COP == null)
        {

            Value.AA_TOTAL_COMPR_COP = "0";
        }

        if (Value.AA_TOTAL_VENTA_COP == "" || Value.AA_TOTAL_VENTA_COP == null)
        {

            Value.AA_TOTAL_VENTA_COP = "0";
        }


        if (Value.AA_MONTO_OPR == "" || Value.AA_MONTO_OPR == null)
        {

            Value.AA_MONTO_OPR = "0";
        }
        if (Value.AA_MONTO_VENTA_OPR == "" || Value.AA_MONTO_VENTA_OPR == null)
        {

            Value.AA_MONTO_VENTA_OPR = "0";
        }
        if (Value.AA_MONTO_PROMEDIO == "" || Value.AA_MONTO_PROMEDIO == null)
        {

            Value.AA_MONTO_PROMEDIO = "0";
        }

        string AA_MONTO_PROM_VENT2 = Value.AA_MONTO_PROM_VENT.Replace(",", "");
        string AA_TOTAL_VENTA_USD2 = Value.AA_TOTAL_VENTA_USD.Replace(".", "");
        string AA_TOTAL_VENTA_USD3 = AA_TOTAL_VENTA_USD2.Replace("$", "");


        string AA_MONTO_VENTA_OPR1 = Value.AA_MONTO_VENTA_OPR.Replace("$", "");
        string AA_MONTO_VENTA_OPR2 = AA_MONTO_VENTA_OPR1.Replace(".", "");


        // int AA_SALDO_FIN = Int32.Parse(Value.AA_SALDO_FIN);
        int AA_MONTO_PROM_VENT = Int32.Parse(AA_MONTO_PROM_VENT2);
        double AA_MONTO_PROMEDIO = Double.Parse(Value.AA_MONTO_PROMEDIO);
        //double AA_TOTAL_VENTA_USD = Double.Parse(AA_TOTAL_VENTA_USD3);
        //int AA_TOTAL_COMPR_USD = Int32.Parse(Value.AA_TOTAL_COMPR_USD);
        string AA_MONTO_OPR = Value.AA_MONTO_OPR;
        string AA_MONTO_VENTA_OPR = AA_MONTO_VENTA_OPR2;

        string AA_TOTAL_COMPR_USD = Value.AA_TOTAL_COMPR_USD.Replace("$", "").Replace(".", "");
        string AA_TOTAL_VENTA_USD = Value.AA_TOTAL_VENTA_USD.Replace("$", "").Replace(".", "");
        string AA_TOTAL_COMPR_COP = Value.AA_TOTAL_COMPR_COP.Replace("$", "").Replace(".", "");
        string AA_TOTAL_VENTA_COP = Value.AA_TOTAL_VENTA_COP.Replace("$", "").Replace(".", "");

        if (Value.AA_CIUDAD_OPR == "")
        {
            Value.AA_CIUDAD_OPR = " ";
        }


        if (Value.AA_IMPORTACION == "")
        {
            Value.AA_IMPORTACION = "N";
        }

        if (Value.AA_EXPORTACION == "")
        {
            Value.AA_EXPORTACION = "N";
        }

        if (Value.AA_INVERSION == "")
        {
            Value.AA_INVERSION = "N";
        }

        if (Value.AA_PRESTAMOS == "")
        {
            Value.AA_PRESTAMOS = "N";
        }
        if (Value.AA_GIROS == "")
        {
            Value.AA_GIROS = "N";
        }
        if (Value.AA_ENVIO_GIROS == "")
        {
            Value.AA_ENVIO_GIROS = "N";
        }
        if (Value.AA_SERVICIOS == "")
        {
            Value.AA_SERVICIOS = "N";
        }


        if (Value.AA_DIVISAS == "1000002")
        {

            Value.AA_DIVISAS = "N";

        }
        else if (Value.AA_DIVISAS == "1000001")
        {

            Value.AA_DIVISAS = "Y";

        }

        String ROW_LASTMANT_DTTM = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");



        bool respuesta = false;
        // respuesta de el metodo
        List<OperacionesInterPNEntity> parametros = new List<OperacionesInterPNEntity>();

        if (Value.TIPODOCUMENTO != "N")
        {

            ContextOracle.Open();
            string sentencia = " UPDATE DIVISAS.ps_aa_pn_opr_inter SET aa_ciudad_opr='" + Value.AA_CIUDAD_OPR + "', aa_cta_exterior='" + Value.AA_CTA_EXTERIOR + "', aa_divisas='" + Value.AA_DIVISAS + "'," +
                "aa_importacion='" + Value.AA_IMPORTACION + "',aa_exportacion='" + Value.AA_EXPORTACION + "',aa_inversion='" + Value.AA_INVERSION + "'" +
                ",aa_prestamos='" + Value.AA_PRESTAMOS + "',aa_giros='" + Value.AA_GIROS + "',aa_envio_giros='" + Value.AA_ENVIO_GIROS + "'," +
                "aa_servicios='" + Value.AA_SERVICIOS + "',AA_MONTO_PROM_VENT='" + Value.AA_MONTO_PROM_VENT + "'," +
                "AA_MONTO_PROMEDIO='" + Value.AA_MONTO_PROMEDIO + "', COUNTRY='" + Value.COUNTRY + "',AA_ENTIDAD_OPR='" + Value.AA_ENTIDAD_OPR + "'," +
                "AA_TIPO_CUENTA='" + Value.AA_TIPO_CUENTA + "',CURRENCY_CD_BASE='" + Value.CURRENCY_CD_BASE + "',AA_NRO_CUENTA='" + Value.AA_NRO_CUENTA + "'," +
                "AA_SALDO_FIN='" + Value.AA_SALDO_FIN + "',AA_TOTAL_VENTA_USD='" + AA_TOTAL_VENTA_USD + "',AA_TOTAL_COMPR_USD='" + AA_TOTAL_COMPR_USD + "'," +
                "AA_MONTO_OPR='" + AA_MONTO_OPR + "',AA_MONTO_VENTA_OPR='" + AA_MONTO_VENTA_OPR + "', ROW_LASTMANT_DTTM='" + ROW_LASTMANT_DTTM + "' , AA_TOTAL_COMPR_COP='" + AA_TOTAL_COMPR_COP + "' , " +
                "AA_TOTAL_VENTA_COP='" + AA_TOTAL_VENTA_COP + "',ROW_LASTMANT_OPRID='" + Value.ROW_LASTMANT_OPRID + "'  ";

            // validar parametro
            if (!string.IsNullOrEmpty(Value.NRODOCUMENTO))
            {
                sentencia = sentencia + " WHERE NRO_IDENTIDAD= " + Value.NRODOCUMENTO + "";
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
                    respuesta = true;

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
            string sentencia = " UPDATE DIVISAS.ps_aa_pj_opr_inter SET aa_ciudad_opr='" + Value.AA_CIUDAD_OPR + "', aa_cta_exterior='" + Value.AA_CTA_EXTERIOR + "', aa_divisas='" + Value.AA_DIVISAS + "'," +
                "aa_importacion='" + Value.AA_IMPORTACION + "',aa_exportacion='" + Value.AA_EXPORTACION + "',aa_inversion='" + Value.AA_INVERSION + "'" +
                ",aa_prestamos='" + Value.AA_PRESTAMOS + "',aa_giros='" + Value.AA_GIROS + "',aa_envio_giros='" + Value.AA_ENVIO_GIROS + "'," +
                "aa_servicios='" + Value.AA_SERVICIOS + "',AA_MONTO_PROM_VENT='" + Value.AA_MONTO_PROM_VENT + "'," +
                "AA_MONTO_PROMEDIO='" + Value.AA_MONTO_PROMEDIO + "',COUNTRY='" + Value.COUNTRY + "',AA_BANCO='" + Value.AA_ENTIDAD_OPR + "'," +
                "AA_TIPO_CUENTA='" + Value.AA_TIPO_CUENTA + "',CURRENCY_CD_BASE='" + Value.CURRENCY_CD_BASE + "',AA_NRO_CUENTA='" + Value.AA_NRO_CUENTA + "'," +
                "AA_SALDO_FIN='" + Value.AA_SALDO_FIN + "',AA_TOTAL_VENTA_USD='" + AA_TOTAL_VENTA_USD + "',AA_TOTAL_COMPR_USD='" + AA_TOTAL_COMPR_USD + "'," +
                "AA_MONTO_OPR='" + AA_MONTO_OPR + "',AA_MONTO_VENTA_OPR='" + AA_MONTO_VENTA_OPR + "', ROW_LASTMANT_DTTM='" + ROW_LASTMANT_DTTM + "', AA_TOTAL_COMPR_COP='" + AA_TOTAL_COMPR_COP + "' ," +
                " AA_TOTAL_VENTA_COP='" + AA_TOTAL_VENTA_COP + "',ROW_LASTMANT_OPRID='" + Value.ROW_LASTMANT_OPRID + "'";

            // validar parametro
            if (!string.IsNullOrEmpty(Value.NRODOCUMENTO))
            {
                sentencia = sentencia + " WHERE NRO_IDENTIDAD= " + Value.NRODOCUMENTO + "";
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
                    respuesta = true;

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






    public bool ActualizarAutorizacionDivisa(ParamaetroOpDivisas Value)
    {
        /*
        if (Value.AA_ESTADO_AUTORIZA == "Pendiente")
        {

            Value.AA_ESTADO_AUTORIZA = "Aprobar";
        }
        */

        /*
        if (Value.AA_ESTADO_AUTORIZA == "Pendiente")
        {

            Value.AA_ESTADO_AUTORIZA = "Vigente";

        }
      
        */

        if (Value.AA_AUTORIZACION_DIV == "SI" || Value.AA_AUTORIZACION_DIV == "True")
        {

            Value.AA_AUTORIZACION_DIV = "SI";
        }

        if (Value.AA_AUTORIZACION_DIV == "NO" || Value.AA_AUTORIZACION_DIV == "False")
        {

            Value.AA_AUTORIZACION_DIV = "NO";
        }



        bool respuesta = false;
        // respuesta de el metodo
        List<ParamaetroOpDivisas> parametros = new List<ParamaetroOpDivisas>();


        if (Value.TIPODOCUMENTO != "N")
        {


            ContextOracle.Open();
            string sentencia = " UPDATE DIVISAS.PS_AA_PN_OPR_INTDT SET AA_AUTORIZA_DIVISA='" + Value.AA_AUTORIZACION_DIV + "', AA_ESTADO_AUTORIZA='" + Value.AA_ESTADO_AUTORIZA + "', ROW_LASTMANT_OPRID='" + Value.ROWLASTMANOPPRID + "'   ";

            // validar parametro
            if (!string.IsNullOrEmpty(Value.NRODOCUMENTO))
            {
                sentencia = sentencia + " WHERE NRO_IDENTIDAD= " + Value.NRODOCUMENTO + "  and SEQ_NBR=" + Value.SEQNBR + " ";
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
            string sentencia = " UPDATE DIVISAS.PS_AA_PJ_OPR_INTDT SET AA_AUTORIZA_DIVISA='" + Value.AA_AUTORIZACION_DIV + "',AA_ESTADO_AUTORIZA='" + Value.AA_ESTADO_AUTORIZA + "'  ";

            // validar parametro
            if (!string.IsNullOrEmpty(Value.NRODOCUMENTO))
            {
                sentencia = sentencia + " WHERE NRO_IDENTIDAD= " + Value.NRODOCUMENTO + "  and SEQ_NBR=" + Value.SEQNBR + " ";
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






    public List<PaisesProductoExterior> ListaPaises()
    {
        List<PaisesProductoExterior> resultado = new List<PaisesProductoExterior>();

        ContextOracle.Open();
        string sentencia = "SELECT * FROM CRWORK.PS_COUNTRY_TBL";

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


                    PaisesProductoExterior ofi = new PaisesProductoExterior()
                    {
                        COUNTRY = reader.GetValue(0).ToString(),
                        DESCR = reader.GetValue(1).ToString(),

                    };



                    resultado.Add(ofi);
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



    public List<TiposMonedaExterior> ListaMonedasExtranjero()
    {
        List<TiposMonedaExterior> resultado = new List<TiposMonedaExterior>();

        ContextOracle.Open();
        string sentencia = "SELECT * FROM CRWORK.PS_CURRENCY_CD_TBL";

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


                    TiposMonedaExterior ofi = new TiposMonedaExterior()
                    {

                        CODIGO = reader.GetValue(0).ToString(),
                        DESCRIPCION = reader.GetValue(3).ToString(),

                    };



                    resultado.Add(ofi);
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






    public List<OperacionesMesaDinero> Monto_op_mesa_dinero(string NroDocumento, string TipoDocumento)
    {
        List<OperacionesMesaDinero> resultado = new List<OperacionesMesaDinero>();


        if (TipoDocumento != "N")
        {



            ContextOracle.Open();
            string sentencia = "select sum(AA_MONTO_OPR) AS MontoOperaciones,AA_TIPO_DIVISA,AA_FECHA_TX from DIVISAS.PS_AA_DIV_MOV_PN where NRO_IDENTIDAD='" + NroDocumento + "' and TIPO_IDENTIDAD='" + TipoDocumento + "' group by AA_TIPO_DIVISA,AA_FECHA_TX";

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



                        OperacionesMesaDinero ofi = new OperacionesMesaDinero()




                        {

                            AA_TIPO_DIVISA = reader.GetValue(1).ToString(),



                        };

                        string Date = DateTime.Now.ToString("MM/yyyy");
                        ofi.AA_FECHA_TX = Convert.ToDateTime(reader.GetValue(2)).Month.ToString("00") + "/" + Convert.ToDateTime(reader.GetValue(2)).Year;



                        if (ofi.AA_FECHA_TX == Date)
                        {
                            if (ofi.AA_TIPO_DIVISA == "V")
                            {
                                ofi.TOTAL_VENTA_USD = reader.GetValue(0).ToString();
                            }
                            else
                            {
                                ofi.TOTAL_COMPR_USD = reader.GetValue(0).ToString();
                            }

                            resultado.Add(ofi);

                        }








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
        else
        {


            ContextOracle.Open();
            string sentencia = "select sum(AA_MONTO_OPR) AS MontoOperaciones,AA_TIPO_DIVISA from DIVISAS.PS_AA_DIV_MOV_PJ where NRO_IDENTIDAD='" + NroDocumento + "' and TIPO_IDENTIDAD='" + TipoDocumento + "' group by AA_TIPO_DIVISA";

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



                        OperacionesMesaDinero ofi = new OperacionesMesaDinero()




                        {

                            AA_TIPO_DIVISA = reader.GetValue(1).ToString(),



                        };


                        if (ofi.AA_TIPO_DIVISA == "V")
                        {
                            ofi.TOTAL_VENTA_USD = reader.GetValue(0).ToString();
                        }
                        else
                        {
                            ofi.TOTAL_COMPR_USD = reader.GetValue(0).ToString();
                        }






                        resultado.Add(ofi);
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


    }






    public List<OperacionesInterPNEntity> TraerPaisDescr(string value)
    {
        // respuesta de el metodo
        List<OperacionesInterPNEntity> parametros = new List<OperacionesInterPNEntity>();

        ContextOracle.Open();

        string sentencia = "select DESCR from CRWORK.PS_COUNTRY_TBL"; // tabla ....tbl

        // validar parametro
        if (!string.IsNullOrEmpty(value))
        {
            sentencia = sentencia + " where UPPER(COUNTRY) LIKE UPPER('" + value + "')  ";
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


                    OperacionesInterPNEntity param = new OperacionesInterPNEntity()
                    {

                        DESCRPAIS = reader.GetValue(0).ToString(),


                    };

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







    public List<OperacionesInterPNEntity> TraerMonedaDescr(string value)
    {
        // respuesta de el metodo
        List<OperacionesInterPNEntity> parametros = new List<OperacionesInterPNEntity>();

        ContextOracle.Open();

        string sentencia = "select DESCR from CRWORK.PS_CURRENCY_CD_TBL"; // tabla ....tbl

        // validar parametro
        if (!string.IsNullOrEmpty(value))
        {
            sentencia = sentencia + " where UPPER(CURRENCY_CD) LIKE UPPER('" + value + "')  ";
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


                    OperacionesInterPNEntity param = new OperacionesInterPNEntity()
                    {

                        DESCRMONEDA = reader.GetValue(0).ToString(),


                    };

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







    public List<OperacionesMesaDineroVenta> Monto_op_mesa_dinero_venta(string NroDocumento, string TipoDocumento)
    {
        List<OperacionesMesaDineroVenta> resultado = new List<OperacionesMesaDineroVenta>();


        if (TipoDocumento != "N")
        {



            ContextOracle.Open();
            string sentencia = "select sum(AA_MONTO_OPR) AS MontoOperaciones,AA_TIPO_DIVISA,AA_FECHA_TX from DIVISAS.PS_AA_DIV_MOV_PN where NRO_IDENTIDAD='" + NroDocumento + "' and TIPO_IDENTIDAD='" + TipoDocumento + "' and AA_TIPO_DIVISA='V' group by AA_TIPO_DIVISA,AA_FECHA_TX";

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



                        OperacionesMesaDineroVenta ofi = new OperacionesMesaDineroVenta()




                        {

                            AA_TIPO_DIVISA = reader.GetValue(1).ToString(),



                        };

                        string Date = DateTime.Now.ToString("MM/yyyy");
                        ofi.AA_FECHA_TX = Convert.ToDateTime(reader.GetValue(2)).Month.ToString("00") + "/" + Convert.ToDateTime(reader.GetValue(2)).Year;



                        if (ofi.AA_FECHA_TX == Date)
                        {
                           
                                ofi.TOTAL_VENTA_USD = reader.GetValue(0).ToString();
                            
                            

                            resultado.Add(ofi);

                        }








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
        else
        {


            ContextOracle.Open();
            string sentencia = "select sum(AA_MONTO_OPR) AS MontoOperaciones,AA_TIPO_DIVISA,AA_FECHA_TX from DIVISAS.PS_AA_DIV_MOV_PJ where NRO_IDENTIDAD='" + NroDocumento + "' and TIPO_IDENTIDAD='" + TipoDocumento + "' and AA_TIPO_DIVISA='V' group by AA_TIPO_DIVISA,AA_FECHA_TX";

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



                        OperacionesMesaDineroVenta ofi = new OperacionesMesaDineroVenta()




                        {

                            AA_TIPO_DIVISA = reader.GetValue(1).ToString(),



                        };


                        string Date = DateTime.Now.ToString("MM/yyyy");
                        ofi.AA_FECHA_TX = Convert.ToDateTime(reader.GetValue(2)).Month.ToString("00") + "/" + Convert.ToDateTime(reader.GetValue(2)).Year;



                        if (ofi.AA_FECHA_TX == Date)
                        {

                            ofi.TOTAL_VENTA_USD = reader.GetValue(0).ToString();



                            resultado.Add(ofi);

                        }





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


    }




    public List<OperacionesMesaDineroCopVenta> Monto_op_mesa_dinero_cop_venta(string NroDocumento, string TipoDocumento)
    {
        List<OperacionesMesaDineroCopVenta> resultado = new List<OperacionesMesaDineroCopVenta>();


        if (TipoDocumento != "N")
        {



            ContextOracle.Open();
            string sentencia = "select sum(AA_MONTO_TASA) AS MontoOperaciones,AA_TIPO_DIVISA,AA_FECHA_TX from DIVISAS.PS_AA_DIV_MOV_PN where NRO_IDENTIDAD='" + NroDocumento + "' and TIPO_IDENTIDAD='" + TipoDocumento + "' and AA_TIPO_DIVISA='V' group by AA_TIPO_DIVISA,AA_FECHA_TX";

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



                        OperacionesMesaDineroCopVenta ofi = new OperacionesMesaDineroCopVenta()




                        {

                            AA_TIPO_DIVISA = reader.GetValue(1).ToString(),



                        };

                        string Date = DateTime.Now.ToString("MM/yyyy");
                        ofi.AA_FECHA_TX = Convert.ToDateTime(reader.GetValue(2)).Month.ToString("00") + "/" + Convert.ToDateTime(reader.GetValue(2)).Year;



                        if (ofi.AA_FECHA_TX == Date)
                        {

                            ofi.TOTAL_VENTA_USD = reader.GetValue(0).ToString();



                            resultado.Add(ofi);

                        }








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
        else
        {


            ContextOracle.Open();
            string sentencia = "select sum(AA_MONTO_TASA) AS MontoOperaciones,AA_TIPO_DIVISA,AA_FECHA_TX from DIVISAS.PS_AA_DIV_MOV_PJ where NRO_IDENTIDAD='" + NroDocumento + "' and TIPO_IDENTIDAD='" + TipoDocumento + "' and AA_TIPO_DIVISA='V' group by AA_TIPO_DIVISA,AA_FECHA_TX";

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



                        OperacionesMesaDineroCopVenta ofi = new OperacionesMesaDineroCopVenta()




                        {

                            AA_TIPO_DIVISA = reader.GetValue(1).ToString(),



                        };


                        string Date = DateTime.Now.ToString("MM/yyyy");
                        ofi.AA_FECHA_TX = Convert.ToDateTime(reader.GetValue(2)).Month.ToString("00") + "/" + Convert.ToDateTime(reader.GetValue(2)).Year;



                        if (ofi.AA_FECHA_TX == Date)
                        {

                            ofi.TOTAL_VENTA_USD = reader.GetValue(0).ToString();



                            resultado.Add(ofi);

                        }





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


    }







    public List<OperacionesMesaDineroCopCompra> Monto_op_mesa_dinero_cop_compra(string NroDocumento, string TipoDocumento)
    {
        List<OperacionesMesaDineroCopCompra> resultado = new List<OperacionesMesaDineroCopCompra>();


        if (TipoDocumento != "N")
        {



            ContextOracle.Open();
            string sentencia = "select sum(AA_MONTO_TASA) AS MontoOperaciones,AA_TIPO_DIVISA,AA_FECHA_TX from DIVISAS.PS_AA_DIV_MOV_PN where NRO_IDENTIDAD='" + NroDocumento + "' and TIPO_IDENTIDAD='" + TipoDocumento + "' and AA_TIPO_DIVISA='C' group by AA_TIPO_DIVISA,AA_FECHA_TX";

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



                        OperacionesMesaDineroCopCompra ofi = new OperacionesMesaDineroCopCompra()




                        {

                            AA_TIPO_DIVISA = reader.GetValue(1).ToString(),



                        };

                        string Date = DateTime.Now.ToString("MM/yyyy");
                        ofi.AA_FECHA_TX = Convert.ToDateTime(reader.GetValue(2)).Month.ToString("00") + "/" + Convert.ToDateTime(reader.GetValue(2)).Year;



                        if (ofi.AA_FECHA_TX == Date)
                        {

                            ofi.TOTAL_COMPR_USD = reader.GetValue(0).ToString();



                            resultado.Add(ofi);

                        }








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
        else
        {


            ContextOracle.Open();
            string sentencia = "select sum(AA_MONTO_TASA) AS MontoOperaciones,AA_TIPO_DIVISA,AA_FECHA_TX from DIVISAS.PS_AA_DIV_MOV_PJ where NRO_IDENTIDAD='" + NroDocumento + "' and TIPO_IDENTIDAD='" + TipoDocumento + "' and AA_TIPO_DIVISA='C' group by AA_TIPO_DIVISA,AA_FECHA_TX";

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



                        OperacionesMesaDineroCopCompra ofi = new OperacionesMesaDineroCopCompra()




                        {

                            AA_TIPO_DIVISA = reader.GetValue(1).ToString(),



                        };


                        string Date = DateTime.Now.ToString("MM/yyyy");
                        ofi.AA_FECHA_TX = Convert.ToDateTime(reader.GetValue(2)).Month.ToString("00") + "/" + Convert.ToDateTime(reader.GetValue(2)).Year;



                        if (ofi.AA_FECHA_TX == Date)
                        {

                            ofi.TOTAL_COMPR_USD = reader.GetValue(0).ToString();



                            resultado.Add(ofi);

                        }





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


    }





    public List<OperacionesMesaDineroCompra> Monto_op_mesa_dinero_compra(string NroDocumento, string TipoDocumento)
    {
        List<OperacionesMesaDineroCompra> resultado = new List<OperacionesMesaDineroCompra>();


        if (TipoDocumento != "N")
        {



            ContextOracle.Open();
            string sentencia = "select sum(AA_MONTO_OPR) AS MontoOperaciones,AA_TIPO_DIVISA,AA_FECHA_TX from DIVISAS.PS_AA_DIV_MOV_PN where NRO_IDENTIDAD='" + NroDocumento + "' and TIPO_IDENTIDAD='" + TipoDocumento + "' and AA_TIPO_DIVISA='C' group by AA_TIPO_DIVISA,AA_FECHA_TX";

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



                        OperacionesMesaDineroCompra ofi = new OperacionesMesaDineroCompra()




                        {

                            AA_TIPO_DIVISA = reader.GetValue(1).ToString(),



                        };

                        string Date = DateTime.Now.ToString("MM/yyyy");
                        ofi.AA_FECHA_TX = Convert.ToDateTime(reader.GetValue(2)).Month.ToString("00") + "/" + Convert.ToDateTime(reader.GetValue(2)).Year;



                        if (ofi.AA_FECHA_TX == Date)
                        {

                            ofi.TOTAL_COMPR_USD = reader.GetValue(0).ToString();



                            resultado.Add(ofi);

                        }








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
        else
        {


            ContextOracle.Open();
            string sentencia = "select sum(AA_MONTO_OPR) AS MontoOperaciones,AA_TIPO_DIVISA,AA_FECHA_TX from DIVISAS.PS_AA_DIV_MOV_PJ where NRO_IDENTIDAD='" + NroDocumento + "' and TIPO_IDENTIDAD='" + TipoDocumento + "' and AA_TIPO_DIVISA='C' group by AA_TIPO_DIVISA,AA_FECHA_TX";

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



                        OperacionesMesaDineroCompra ofi = new OperacionesMesaDineroCompra()




                        {

                            AA_TIPO_DIVISA = reader.GetValue(1).ToString(),



                        };


                        string Date = DateTime.Now.ToString("MM/yyyy");
                        ofi.AA_FECHA_TX = Convert.ToDateTime(reader.GetValue(2)).Month.ToString("00") + "/" + Convert.ToDateTime(reader.GetValue(2)).Year;



                        if (ofi.AA_FECHA_TX == Date)
                        {

                            ofi.TOTAL_COMPR_USD = reader.GetValue(0).ToString();



                            resultado.Add(ofi);

                        }





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


    }





    public List<OperacionesMesaDinero> Monto_op_mesa_dinero_cop(string NroDocumento, string TipoDocumento)
    {
        List<OperacionesMesaDinero> resultado = new List<OperacionesMesaDinero>();


        if (TipoDocumento != "N")
        {



            ContextOracle.Open();
            string sentencia = "select sum(AA_MONTO_TASA) AS MontoOperaciones,AA_TIPO_DIVISA,AA_FECHA_TX from DIVISAS.PS_AA_DIV_MOV_PN where NRO_IDENTIDAD='" + NroDocumento + "' and TIPO_IDENTIDAD='" + TipoDocumento + "' group by AA_TIPO_DIVISA,AA_FECHA_TX";

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



                        OperacionesMesaDinero ofi = new OperacionesMesaDinero()




                        {

                            AA_TIPO_DIVISA = reader.GetValue(1).ToString(),



                        };

                        string Date = DateTime.Now.ToString("MM/yyyy");
                        ofi.AA_FECHA_TX = Convert.ToDateTime(reader.GetValue(2)).Month.ToString("00") + "/" + Convert.ToDateTime(reader.GetValue(2)).Year;



                        if (ofi.AA_FECHA_TX == Date)
                        {
                            if (ofi.AA_TIPO_DIVISA == "V")
                            {
                                ofi.TOTAL_VENTA_USD = reader.GetValue(0).ToString();
                                ofi.TOTAL_COMPR_USD = "0.0";

                            }
                            else
                            {
                                ofi.TOTAL_COMPR_USD = reader.GetValue(0).ToString();
                                ofi.TOTAL_VENTA_USD = "0.0";

                            }

                            resultado.Add(ofi);

                        }








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
        else
        {


            ContextOracle.Open();
            string sentencia = "select sum(AA_MONTO_TASA) AS MontoOperaciones,AA_TIPO_DIVISA,AA_FECHA_TX from DIVISAS.PS_AA_DIV_MOV_PJ where NRO_IDENTIDAD='" + NroDocumento + "' and TIPO_IDENTIDAD='" + TipoDocumento + "' group by AA_TIPO_DIVISA,AA_FECHA_TX";



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



                        OperacionesMesaDinero ofi = new OperacionesMesaDinero()




                        {

                            AA_TIPO_DIVISA = reader.GetValue(1).ToString(),



                        };


                        string Date = DateTime.Now.ToString("MM/yyyy");
                        ofi.AA_FECHA_TX = Convert.ToDateTime(reader.GetValue(2)).Month.ToString("00") + "/" + Convert.ToDateTime(reader.GetValue(2)).Year;



                        if (ofi.AA_FECHA_TX == Date)
                        {
                            if (ofi.AA_TIPO_DIVISA == "V")
                            {
                                ofi.TOTAL_VENTA_USD = reader.GetValue(0).ToString();
                            }
                            else
                            {
                                ofi.TOTAL_COMPR_USD = reader.GetValue(0).ToString();
                            }

                            resultado.Add(ofi);

                        }






                        resultado.Add(ofi);
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


    }


    public List<UsuarioSesion> TraerUsuarioSesion(string value, string value2)
    {
        // respuesta de el metodo
        List<UsuarioSesion> parametros = new List<UsuarioSesion>();




        ContextOracle.Open();



        string sentencia = "select NOMBRES,COD_CARGO from CRWORK.AV_FUNCIONARIOS"; // tabla ....tbl






        // validar parametro
        if (!string.IsNullOrEmpty(value))
        {
            sentencia = sentencia + " where UPPER(IDENTIFICACION) LIKE UPPER('" + value + "')  ";
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


                    UsuarioSesion param = new UsuarioSesion()
                    {

                        NOMBRECOMPLETO = reader.GetValue(0).ToString(),
                        JOBCODE = reader.GetValue(1).ToString(),







                    };





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




    public bool CrerOperacionesInt(OperacionesInterPNEntity Value)
    {
        bool respuesta = false;
        //string NuevoValor = Value.MONTOPROMVENT.Replace(".", ",");
        //string[] dateAdded = Value.DATEEND.Split(' ');
        //string[] FechaLastmant = Value.ROWLASTMANDTTM.Split(' ');
        DateTime FechaHoy = DateTime.Now;


        if (Value.AA_TOTAL_COMPR_USD == "" || Value.AA_TOTAL_COMPR_USD == null)
        {

            Value.AA_TOTAL_COMPR_USD = "0";
        }
        if (Value.AA_MONTO_OPR == "" || Value.AA_MONTO_OPR == null)
        {

            Value.AA_MONTO_OPR = "0";
        }
        if (Value.AA_MONTO_VENTA_OPR == "" || Value.AA_MONTO_VENTA_OPR == null)
        {

            Value.AA_MONTO_VENTA_OPR = "0";
        }

        string AA_MONTO_PROM_VENT2 = Value.AA_MONTO_PROM_VENT.Replace(",", "");
        // string AA_TOTAL_VENTA_USD2 = Value.AA_TOTAL_VENTA_USD.Replace(".", "");
        // string AA_TOTAL_VENTA_USD3 = AA_TOTAL_VENTA_USD2.Replace("$", "");

        string AA_SALDO_FIN1 = Value.AA_SALDO_FIN.Replace(",", "");

        double AA_SALDO_FIN = double.Parse(AA_SALDO_FIN1);
        double AA_MONTO_PROM_VENT = double.Parse(AA_MONTO_PROM_VENT2);
        double AA_MONTO_PROMEDIO = double.Parse(Value.AA_MONTO_PROMEDIO);
        //int AA_TOTAL_VENTA_USD = Int32.Parse(AA_TOTAL_VENTA_USD3);


        string AA_TOTAL_VENTA_USD2 = Value.AA_TOTAL_VENTA_USD.Replace("$", "").Replace(".","").Replace(",",".");
        double AA_TOTAL_VENTA_USD = double.Parse(AA_TOTAL_VENTA_USD2);

        string AA_TOTAL_COMPR_USD2 = Value.AA_TOTAL_COMPR_USD.Replace("$", "").Replace(".", "").Replace(",", ".");

        double AA_TOTAL_COMPR_USD = double.Parse(AA_TOTAL_COMPR_USD2);
        double AA_MONTO_OPR = double.Parse(Value.AA_MONTO_OPR);


        string AA_MONTO_VENTA_OPR2 = Value.AA_MONTO_VENTA_OPR.Replace("$", "").Replace(".", "").Replace(",", ".");

        double AA_MONTO_VENTA_OPR = double.Parse(AA_MONTO_VENTA_OPR2);












        try
        {
            // instancia del procedimiento
            OracleCommand command = new OracleCommand("DIVISAS.av_crear_operaciones_int_div", ContextOracle);
            command.CommandType = CommandType.StoredProcedure;

            // parametros de entrada
            command.Parameters.Add(new OracleParameter("tipo_documento", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.TIPODOCUMENTO) ? " " : Value.TIPODOCUMENTO;

            command.Parameters.Add(new OracleParameter("nro_documento", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.NRODOCUMENTO) ? " " : Value.NRODOCUMENTO;

            command.Parameters.Add(new OracleParameter("aa_divisas", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.AA_DIVISAS) ? " " : Value.AA_DIVISAS;

            command.Parameters.Add(new OracleParameter("aa_importacion", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.AA_IMPORTACION) ? "N" : Value.AA_IMPORTACION;

            command.Parameters.Add(new OracleParameter("aa_exportacion", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.AA_EXPORTACION) ? "N" : Value.AA_EXPORTACION;

            command.Parameters.Add(new OracleParameter("aa_inversion", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.AA_INVERSION) ? "N" : Value.AA_INVERSION;

            command.Parameters.Add(new OracleParameter("aa_prestamos", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.AA_PRESTAMOS) ? "N" : Value.AA_PRESTAMOS;

            command.Parameters.Add(new OracleParameter("aa_giros", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.AA_GIROS) ? "N" : Value.AA_GIROS;

            command.Parameters.Add(new OracleParameter("aa_envio_giros", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.AA_ENVIO_GIROS) ? "N" : Value.AA_ENVIO_GIROS;

            command.Parameters.Add(new OracleParameter("aa_servicios", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.AA_SERVICIOS) ? "N" : Value.AA_SERVICIOS;

            command.Parameters.Add(new OracleParameter("aa_cta_exterior", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.AA_CTA_EXTERIOR) ? "N" : Value.AA_CTA_EXTERIOR;

            command.Parameters.Add(new OracleParameter("aa_ciudad_opr", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.AA_CIUDAD_OPR) ? " " : Value.AA_CIUDAD_OPR;

            command.Parameters.Add(new OracleParameter("country", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.COUNTRY) ? " " : Value.COUNTRY;

            command.Parameters.Add(new OracleParameter("aa_entidad_opr", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.AA_ENTIDAD_OPR) ? " " : Value.AA_ENTIDAD_OPR;

            command.Parameters.Add(new OracleParameter("aa_tipo_cuenta", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.AA_TIPO_CUENTA) ? " " : Value.AA_TIPO_CUENTA;

            command.Parameters.Add(new OracleParameter("currency_cd_base", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.CURRENCY_CD_BASE) ? " " : Value.CURRENCY_CD_BASE;

            command.Parameters.Add(new OracleParameter("aa_nro_cuenta", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.AA_NRO_CUENTA) ? " " : Value.AA_NRO_CUENTA;

            command.Parameters.Add(new OracleParameter("aa_saldo_fin", OracleType.Int32)).Value = AA_SALDO_FIN;

            command.Parameters.Add(new OracleParameter("aa_monto_prom_vent", OracleType.Int32)).Value = AA_MONTO_PROM_VENT;

            command.Parameters.Add(new OracleParameter("aa_monto_promedio", OracleType.Int32)).Value = AA_MONTO_PROMEDIO;



            command.Parameters.Add(new OracleParameter("aa_total_venta_usd", OracleType.Int32)).Value = AA_TOTAL_VENTA_USD;

            command.Parameters.Add(new OracleParameter("aa_total_compr_usd", OracleType.Int32)).Value = AA_TOTAL_COMPR_USD;

            command.Parameters.Add(new OracleParameter("aa_monto_opr", OracleType.Int32)).Value = AA_MONTO_OPR;
            command.Parameters.Add(new OracleParameter("aa_monto_venta_opr", OracleType.Int32)).Value = AA_MONTO_VENTA_OPR;


            command.Parameters.Add(new OracleParameter("row_lastmant_oprid", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.ROW_LASTMANT_OPRID) ? " " : Value.ROW_LASTMANT_OPRID;

            command.Parameters.Add(new OracleParameter("row_added_oprid", OracleType.VarChar)).Value = string.IsNullOrEmpty(Value.ROW_ADDED_OPRID) ? " " : Value.ROW_ADDED_OPRID;




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



    public List<OperacionesInterPNEntity> ValidacionListaBloqueo(string value)
    {

        int valor = Int32.Parse(value);
        // respuesta de el metodo
        List<OperacionesInterPNEntity> parametros = new List<OperacionesInterPNEntity>();



        ContextOracle.Open();
        string sentencia = "select 'SI' from CRWORK.PS_AA_LISTA_BLOQ ";

        // validar parametro
        if (!string.IsNullOrEmpty(value))
        {
            sentencia = sentencia + " where  AA_NRO_DOCUMENTO = '" + value + "' AND AA_ESTADO_BLQ='A' ";

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




                    OperacionesInterPNEntity param = new OperacionesInterPNEntity()

                    {
                        LISTA_BLOQUEO = "SI",

                    };




                    parametros.Add(param);

                }
            }
            else
            {


                OperacionesInterPNEntity param = new OperacionesInterPNEntity()

                {
                    LISTA_BLOQUEO = "NO",

                };
                parametros.Add(param);



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











    public List<OperacionesInterPNEntity> ValidacionDivisasVigentes(string value)
    {

        int valor = Int32.Parse(value);
        // respuesta de el metodo
        List<OperacionesInterPNEntity> parametros = new List<OperacionesInterPNEntity>();



        ContextOracle.Open();
        string sentencia = "select distinct 'SI' from DIVISAS.PS_AA_PN_OPR_INTDT ";

        // validar parametro
        if (!string.IsNullOrEmpty(value))
        {
            sentencia = sentencia + " where  NRO_IDENTIDAD = '" + value + "' AND AA_ESTADO_AUTORIZA='Vigente'  ";

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

                    OperacionesInterPNEntity param = new OperacionesInterPNEntity()

                    {
                        DIVISASVIGENTE = reader.GetValue(0).ToString(),

                    };

                    parametros.Add(param);

                }
            }
            else
            {


                OperacionesInterPNEntity param = new OperacionesInterPNEntity()

                {
                    DIVISASVIGENTE = "NO",

                };
                parametros.Add(param);



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











public class OperacionesInterPNEntity
{


    public OperacionesInterPNEntity()
    {


        this.ID = 0;

    }

    private string _VALUE_NBR;
    private string _puedeAutorizar;
    private string _puedeAutorizar2;

    private string _DESCR;
    private string _USUARIOAPLICACION;
    private string _NRODOCUMENTO;
    private string _TIPODOCUMENTO;
    private string _AA_IMPORTACION;
    private string _AA_EXPORTACION;
    private string _AA_INVERSION;
    private string _AA_PRESTAMOS;
    private string _AA_GIROS;
    private string _AA_ENVIO_GIROS;
    private string _AA_SERVICIOS;
    private string _AA_CTA_EXTERIOR;
    private string _COUNTRY;
    private string _AA_CIUDAD_OPR;
    private string _AA_ENTIDAD_OPR;
    private string _AA_TIPO_CUENTA;
    private string _CURRENCY_CD_BASE;
    private string _AA_NRO_CUENTA;
    private string _AA_SALDO_FIN;
    private string _AA_MONTO_PROM_VENT;
    private string _CURRENCY_CD;
    private string _AA_MONTO_PROMEDIO;

    private string _AA_TOTAL_VENTA_USD;
    private string _AA_TOTAL_COMPR_USD;
    private string _AA_TOTAL_VENTA_COP;
    private string _AA_TOTAL_COMPR_COP;
    private string _AA_MONTO_OPR;

    private string _AA_MONTO_VENTA_OPR;

    private string _ROW_LASTMANT_DTTM;
    private string _ROW_LASTMANT_OPRID;
    private string _AA_DIVISAS;
    private string _DATE_END;
    private string _AA_AUTORIZA_DIVISA;
    private string _LISTA_BLOQUEO;
    private string _DIVISASVIGENTE;
    private string _DESCRPAIS;
    private string _DESCRMONEDA;
    private string _ROW_ADDED_OPRID;


    public string ROW_ADDED_OPRID


    {
        get
        {
            return _ROW_ADDED_OPRID;
        }

        set
        {
            _ROW_ADDED_OPRID = value;
        }
    }


    public string LISTA_BLOQUEO


    {
        get
        {
            return _LISTA_BLOQUEO;
        }

        set
        {
            _LISTA_BLOQUEO = value;
        }
    }




    public string DESCRPAIS


    {
        get
        {
            return _DESCRPAIS;
        }

        set
        {
            _DESCRPAIS = value;
        }
    }



    public string DESCRMONEDA


    {
        get
        {
            return _DESCRMONEDA;
        }

        set
        {
            _DESCRMONEDA = value;
        }
    }







    public string DIVISASVIGENTE


    {
        get
        {
            return _DIVISASVIGENTE;
        }

        set
        {
            _DIVISASVIGENTE = value;
        }
    }
    public string AA_AUTORIZA_DIVISA


    {
        get
        {
            return _AA_AUTORIZA_DIVISA;
        }

        set
        {
            _AA_AUTORIZA_DIVISA = value;
        }
    }


    public string puedeAutorizar


    {
        get
        {
            return _puedeAutorizar;
        }

        set
        {
            _puedeAutorizar = value;
        }
    }

    public string puedeAutorizar2


    {
        get
        {
            return _puedeAutorizar2;
        }

        set
        {
            _puedeAutorizar2 = value;
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

    public string DATE_END


    {
        get
        {
            return _DATE_END;
        }

        set
        {
            _DATE_END = value;
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



    public string AA_DIVISAS


    {
        get
        {
            return _AA_DIVISAS;
        }

        set
        {
            _AA_DIVISAS = value;
        }
    }




    public string DESCR


    {
        get
        {
            return _DESCR;
        }

        set
        {
            _DESCR = value;
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



    public string AA_IMPORTACION
    {
        get
        {
            return _AA_IMPORTACION;
        }

        set
        {
            _AA_IMPORTACION = value;
        }
    }

    public string AA_EXPORTACION
    {
        get
        {
            return _AA_EXPORTACION;
        }

        set
        {
            _AA_EXPORTACION = value;
        }
    }

    public string AA_INVERSION
    {
        get
        {
            return _AA_INVERSION;
        }

        set
        {
            _AA_INVERSION = value;
        }
    }

    public string AA_PRESTAMOS
    {
        get
        {
            return _AA_PRESTAMOS;
        }

        set
        {
            _AA_PRESTAMOS = value;
        }
    }

    public string AA_GIROS
    {
        get
        {
            return _AA_GIROS;
        }

        set
        {
            _AA_GIROS = value;
        }
    }

    public string AA_ENVIO_GIROS
    {
        get
        {
            return _AA_ENVIO_GIROS;
        }

        set
        {
            _AA_ENVIO_GIROS = value;
        }
    }

    public string AA_SERVICIOS
    {
        get
        {
            return _AA_SERVICIOS;
        }

        set
        {
            _AA_SERVICIOS = value;
        }
    }

    public string AA_CTA_EXTERIOR
    {
        get
        {
            return _AA_CTA_EXTERIOR;
        }

        set
        {
            _AA_CTA_EXTERIOR = value;
        }
    }

    public string COUNTRY
    {
        get
        {
            return _COUNTRY;
        }

        set
        {
            _COUNTRY = value;
        }
    }

    public string AA_CIUDAD_OPR
    {
        get
        {
            return _AA_CIUDAD_OPR;
        }

        set
        {
            _AA_CIUDAD_OPR = value;
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



    public string AA_ENTIDAD_OPR
    {
        get
        {
            return _AA_ENTIDAD_OPR;
        }

        set
        {
            _AA_ENTIDAD_OPR = value;
        }
    }



    public string AA_TIPO_CUENTA
    {
        get
        {
            return _AA_TIPO_CUENTA;
        }

        set
        {
            _AA_TIPO_CUENTA = value;
        }
    }




    public string CURRENCY_CD_BASE
    {
        get
        {
            return _CURRENCY_CD_BASE;
        }

        set
        {
            _CURRENCY_CD_BASE = value;
        }
    }


    public string AA_NRO_CUENTA
    {
        get
        {
            return _AA_NRO_CUENTA;
        }

        set
        {
            _AA_NRO_CUENTA = value;
        }
    }



    public string AA_SALDO_FIN
    {
        get
        {
            return _AA_SALDO_FIN;
        }

        set
        {
            _AA_SALDO_FIN = value;
        }
    }




    public string AA_MONTO_PROM_VENT
    {
        get
        {
            return _AA_MONTO_PROM_VENT;
        }

        set
        {
            _AA_MONTO_PROM_VENT = value;
        }
    }


    public string CURRENCY_CD
    {
        get
        {
            return _CURRENCY_CD;
        }

        set
        {
            _CURRENCY_CD = value;
        }
    }

    public string AA_MONTO_PROMEDIO
    {
        get
        {
            return _AA_MONTO_PROMEDIO;
        }

        set
        {
            _AA_MONTO_PROMEDIO = value;
        }
    }

    public string AA_TOTAL_VENTA_USD
    {
        get
        {
            return _AA_TOTAL_VENTA_USD;
        }

        set
        {
            _AA_TOTAL_VENTA_USD = value;
        }
    }


    public string AA_TOTAL_COMPR_USD
    {
        get
        {
            return _AA_TOTAL_COMPR_USD;
        }

        set
        {
            _AA_TOTAL_COMPR_USD = value;
        }
    }



    public string AA_TOTAL_VENTA_COP
    {
        get
        {
            return _AA_TOTAL_VENTA_COP;
        }

        set
        {
            _AA_TOTAL_VENTA_COP = value;
        }
    }




    public string AA_TOTAL_COMPR_COP
    {
        get
        {
            return _AA_TOTAL_COMPR_COP;
        }

        set
        {
            _AA_TOTAL_COMPR_COP = value;
        }
    }


    public string AA_MONTO_OPR
    {
        get
        {
            return _AA_MONTO_OPR;
        }

        set
        {
            _AA_MONTO_OPR = value;
        }
    }



    public string AA_MONTO_VENTA_OPR
    {
        get
        {
            return _AA_MONTO_VENTA_OPR;
        }

        set
        {
            _AA_MONTO_VENTA_OPR = value;
        }
    }






    public string ROW_LASTMANT_DTTM
    {
        get
        {
            return _ROW_LASTMANT_DTTM;
        }

        set
        {
            _ROW_LASTMANT_DTTM = value;
        }
    }





    public string ROW_LASTMANT_OPRID
    {
        get
        {
            return _ROW_LASTMANT_OPRID;
        }

        set
        {
            _ROW_LASTMANT_OPRID = value;
        }
    }









}


public class DatosUsuarioPNentity
{


    public DatosUsuarioPNentity()
    {


        this.ID = 0;

    }


    private string _ROLENAME;
    private string _puedeAutorizar;
    private string _puedeAutorizarDetalle;
    private string _puedeAdicionarDetalle;
    private string _VALUE_NBR;






    public int ID { get; set; }




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





    public string ROLENAME
    {
        get
        {
            return _ROLENAME;
        }

        set
        {
            _ROLENAME = value;
        }
    }

    public string puedeAutorizarDetalle
    {
        get
        {
            return _puedeAutorizarDetalle;
        }

        set
        {
            _puedeAutorizarDetalle = value;
        }
    }



    public string puedeAdicionarDetalle
    {
        get
        {
            return _puedeAdicionarDetalle;
        }

        set
        {
            _puedeAdicionarDetalle = value;
        }
    }


    public string puedeAutorizar
    {
        get
        {
            return _puedeAutorizar;
        }

        set
        {
            _puedeAutorizar = value;
        }
    }
}







public class EntidadesFinancierasentity
{


    public EntidadesFinancierasentity()
    {


        this.ID = 0;

    }


    private string _CodEntidad;
    private string _EntidadFinanciera;
    private string _NroDocumento;
    private string _TIPODOCUMENTO;
    private string _ROWADDEDOPRID;






    public int ID { get; set; }


    public string ROWADDEDOPRID
    {
        get
        {
            return _ROWADDEDOPRID;
        }

        set
        {
            _ROWADDEDOPRID = value;
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


    public string CodEntidad
    {
        get
        {
            return _CodEntidad;
        }

        set
        {
            _CodEntidad = value;
        }
    }


    public string Nrodocumento
    {
        get
        {
            return _NroDocumento;
        }

        set
        {
            _NroDocumento = value;
        }
    }

    public string EntidadFinanciera
    {
        get
        {
            return _EntidadFinanciera;
        }

        set
        {
            _EntidadFinanciera = value;
        }
    }


}






public class PaisesProductoExterior
{


    public PaisesProductoExterior()
    {


        this.ID = 0;

    }


    private string _COUNTRY;
    private string _DESCR;






    public int ID { get; set; }


    public string DESCR
    {
        get
        {
            return _DESCR;
        }

        set
        {
            _DESCR = value;
        }
    }



    public string COUNTRY
    {
        get
        {
            return _COUNTRY;
        }

        set
        {
            _COUNTRY = value;
        }
    }

}





public class OperacionesMesaDinero
{


    public OperacionesMesaDinero()
    {


        this.ID = 0;

    }


    private string _TOTAL_VENTA_USD;
    private string _TOTAL_COMPR_USD;
    private string _AA_TIPO_DIVISA;
    private string _AA_FECHA_TX;




    public int ID { get; set; }


    public string AA_TIPO_DIVISA
    {
        get
        {
            return _AA_TIPO_DIVISA;
        }

        set
        {
            _AA_TIPO_DIVISA = value;
        }
    }


    public string AA_FECHA_TX
    {
        get
        {
            return _AA_FECHA_TX;
        }

        set
        {
            _AA_FECHA_TX = value;
        }
    }



    public string TOTAL_VENTA_USD
    {
        get
        {
            return _TOTAL_VENTA_USD;
        }

        set
        {
            _TOTAL_VENTA_USD = value;
        }
    }



    public string TOTAL_COMPR_USD
    {
        get
        {
            return _TOTAL_COMPR_USD;
        }

        set
        {
            _TOTAL_COMPR_USD = value;
        }
    }

}



public class OperacionesMesaDineroCompra
{


    public OperacionesMesaDineroCompra()
    {


        this.ID = 0;

    }


   
    private string _TOTAL_COMPR_USD;
    private string _AA_TIPO_DIVISA;
    private string _AA_FECHA_TX;




    public int ID { get; set; }


    public string AA_TIPO_DIVISA
    {
        get
        {
            return _AA_TIPO_DIVISA;
        }

        set
        {
            _AA_TIPO_DIVISA = value;
        }
    }


    public string AA_FECHA_TX
    {
        get
        {
            return _AA_FECHA_TX;
        }

        set
        {
            _AA_FECHA_TX = value;
        }
    }



   


    public string TOTAL_COMPR_USD
    {
        get
        {
            return _TOTAL_COMPR_USD;
        }

        set
        {
            _TOTAL_COMPR_USD = value;
        }
    }

}




public class OperacionesMesaDineroVenta
{


    public OperacionesMesaDineroVenta()
    {


        this.ID = 0;

    }


    private string _TOTAL_VENTA_USD;
    private string _AA_TIPO_DIVISA;
    private string _AA_FECHA_TX;




    public int ID { get; set; }


    public string AA_TIPO_DIVISA
    {
        get
        {
            return _AA_TIPO_DIVISA;
        }

        set
        {
            _AA_TIPO_DIVISA = value;
        }
    }


    public string AA_FECHA_TX
    {
        get
        {
            return _AA_FECHA_TX;
        }

        set
        {
            _AA_FECHA_TX = value;
        }
    }



    public string TOTAL_VENTA_USD
    {
        get
        {
            return _TOTAL_VENTA_USD;
        }

        set
        {
            _TOTAL_VENTA_USD = value;
        }
    }


}





public class OperacionesMesaDineroCopCompra
{


    public OperacionesMesaDineroCopCompra()
    {


        this.ID = 0;

    }



    private string _TOTAL_COMPR_USD;
    private string _AA_TIPO_DIVISA;
    private string _AA_FECHA_TX;




    public int ID { get; set; }


    public string AA_TIPO_DIVISA
    {
        get
        {
            return _AA_TIPO_DIVISA;
        }

        set
        {
            _AA_TIPO_DIVISA = value;
        }
    }


    public string AA_FECHA_TX
    {
        get
        {
            return _AA_FECHA_TX;
        }

        set
        {
            _AA_FECHA_TX = value;
        }
    }






    public string TOTAL_COMPR_USD
    {
        get
        {
            return _TOTAL_COMPR_USD;
        }

        set
        {
            _TOTAL_COMPR_USD = value;
        }
    }

}










public class OperacionesMesaDineroCopVenta
{


    public OperacionesMesaDineroCopVenta()
    {


        this.ID = 0;

    }


    private string _TOTAL_VENTA_USD;
    private string _AA_TIPO_DIVISA;
    private string _AA_FECHA_TX;




    public int ID { get; set; }


    public string AA_TIPO_DIVISA
    {
        get
        {
            return _AA_TIPO_DIVISA;
        }

        set
        {
            _AA_TIPO_DIVISA = value;
        }
    }


    public string AA_FECHA_TX
    {
        get
        {
            return _AA_FECHA_TX;
        }

        set
        {
            _AA_FECHA_TX = value;
        }
    }



    public string TOTAL_VENTA_USD
    {
        get
        {
            return _TOTAL_VENTA_USD;
        }

        set
        {
            _TOTAL_VENTA_USD = value;
        }
    }


}




public class TiposMonedaExterior
{


    public TiposMonedaExterior()
    {


        this.ID = 0;

    }


    private string _CODIGO;
    private string _DESCRIPCION;






    public int ID { get; set; }


    public string CODIGO
    {
        get
        {
            return _CODIGO;
        }

        set
        {
            _CODIGO = value;
        }
    }



    public string DESCRIPCION
    {
        get
        {
            return _DESCRIPCION;
        }

        set
        {
            _DESCRIPCION = value;
        }
    }

}