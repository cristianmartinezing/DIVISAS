using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// Summary description for ArchivoLog
/// </summary>
class ArchivoLogDiv
{
    public void GuardaLog(CLSRespuestaDiv rtaMensaje, CLSClienteDiv cliente, string nombreArchivo)
    {
        string fecha = System.DateTime.Now.ToString("yyyyMMdd");
        string ruta = HttpContext.Current.Server.MapPath("/Aplicaciones/crm/Log/Log_" + nombreArchivo + "_" + fecha + ".txt");
       // string ruta = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @".\Log\Log_" + nombreArchivo + "_" + fecha +".txt");


        if (!File.Exists(ruta))
        {
            File.Create(ruta).Dispose();

            using (TextWriter tw = new StreamWriter(ruta))
            {
                tw.WriteLine("====" + System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "===================");
                tw.WriteLine("USUARIO APLICACION: " + cliente.usuarioAplicacion + "\n");
                tw.WriteLine("MENSAJE: " + rtaMensaje.ErrorLog +"\n");
                tw.WriteLine("DESCRIPCION: " + rtaMensaje.Descripcion + "\n");
                tw.WriteLine("CLIENTE: " + cliente.numeroDocumento);
                tw.WriteLine("TIPO DOCUMENTO: " + cliente.tipoDocumento);
                tw.WriteLine("ACCION: " + cliente.Accion);
                tw.WriteLine("================================================");
                tw.Close();
            }

        }
        else if (File.Exists(ruta))
        {
            using (StreamWriter tw = File.AppendText(ruta))
            {
                tw.WriteLine("====" + System.DateTime.Now + "===================");
                tw.WriteLine("USUARIO APLICACION: " + cliente.usuarioAplicacion + "\n");
                tw.WriteLine("MENSAJE: " + rtaMensaje.ErrorLog + "\n");
                tw.WriteLine("CLIENTE: " + cliente.numeroDocumento);
                tw.WriteLine("DESCRIPCION: " + rtaMensaje.Descripcion + "\n");
                tw.WriteLine("TIPO DOCUMENTO: " + cliente.tipoDocumento);
                tw.WriteLine("ACCION: " + cliente.Accion);
                tw.WriteLine("================================================");
                tw.Close();
            }
        }
    }

    public string GuardaXML(Object YourClassObjec, string nombre)
    {
        string fecha = System.DateTime.Now.ToString("yyyyMMdd");
        string ruta = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @".\Log\AppLog_" + nombre + "_" + fecha + ".xml");
        File.Create(ruta).Dispose();
        string xml = CreateXML(YourClassObjec);
        File.WriteAllText(ruta, xml);
        return xml;
    }

    public string CreateXML(Object YourClassObject)
    {
        XmlDocument xmlDoc = new XmlDocument();   //Represents an XML document, 
                                                  // Initializes a new instance of the XmlDocument class.          
        XmlSerializer xmlSerializer = new XmlSerializer(YourClassObject.GetType());
        // Creates a stream whose backing store is memory. 
        using (MemoryStream xmlStream = new MemoryStream())
        {
            xmlSerializer.Serialize(xmlStream, YourClassObject);
            xmlStream.Position = 0;
            //Loads the XML document from the specified string.
            xmlDoc.Load(xmlStream);

            return xmlDoc.InnerXml;
        }
    }

}
