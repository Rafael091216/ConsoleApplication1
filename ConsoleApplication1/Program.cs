using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Collections;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateWebRequest();
        }
        public static void CreateWebRequest()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"http://apps.zeekgps.com/cmovilapi3ws/cmovilapi3ws.asmx?WSDL");
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";

            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                    <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:cmov=""http://cmovil.coordenada.info/CMovilApi3WS/"">
                    <soapenv:Header/>
                    <soapenv:Body>
                    <cmov:AutentificaUsuario>
                    <!--Optional:-->
                    <cmov:usuario>Panalpina</cmov:usuario>
                    <!--Optional:-->
                    <cmov:contrasenya>s1HJFjEH</cmov:contrasenya>
                    <!--Optional:-->
                    <cmov:licencia>iPinssoMperPogm</cmov:licencia>
                    <cmov:licencia>?</cmov:licencia>
                    </cmov:AutentificaUsuario>
                    </soapenv:Body>
                    </soapenv:Envelope>");

            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = webRequest.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();


                    var rawXML = XDocument.Parse(soapResult);
                    Console.WriteLine(rawXML);
                    Console.ReadLine();

                    ////Agregar codigo para convertir de xml a json o bien trabajar directamente con xml


                    //Se convierte a JSON ----------
                    string json1 = JsonConvert.SerializeObject(rawXML);
                    Console.WriteLine("Convirtiendo el XDocument a JSON: ");
                    Console.WriteLine(json1);
                    Console.ReadKey();


                    //var numero = jsonObj
                    //Console.WriteLine(numero);
                    //Console.ReadKey();
                    //foreach (SearchResult model in models) //itera el modelo y muestra lo que hay en el JSON
                    //{

                    //    Console.WriteLine(model.Capacidad);
                    //    Console.ReadKey();
                    //    Console.WriteLine(model.title);
                    //    Console.ReadKey();

                    //}


                    //JsonTextReader reader = new JsonTextReader(new StringReader(json1));
                    //while (reader.Read())
                    //{
                    //    if (reader.Value != null)
                    //    {
                    //        Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
                    //        Console.ReadKey();
                    //    }

                    //    else
                    //    {
                    //        Console.WriteLine("Token: {0}", reader.TokenType);
                    //        Console.ReadKey();
                    //    }

                    //}

                }

            }
        }
    }
}





//        HttpWebRequest request = CreateWebRequest();
//        XmlDocument soapEnvelopeXml = new XmlDocument();
//        soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
//            <soap:Envelope xmlns:xsi= ""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd = ""http://www.w3.org/2001/XMLSchema"" xmlns:soap =""http://schemas.xmlsoap.org/soap/envelope/"">
//              <soap:Body>
//            <UltimasUbicaciones xmlns=""http://cmovil.coordenada.info/CMovilApi3WS/"">
//            <token>370690</token>
//            <licencia>iPinssoMperPogm</licencia>
//            <cliente>CARGASEGURA</cliente >
//            <unidades>
//            <string>008076377st</string >
//            <string>008063285st</string >
//            </unidades>
//            </UltimasUbicaciones>
//            </soap:Body>
//            </soap:Envelope>");


//        using (Stream stream = request.GetRequestStream())
//        {
//            soapEnvelopeXml.Save(stream);
//        }

//        using (WebResponse response = request.GetResponse())
//        {
//            using (StreamReader rd = new StreamReader(response.GetResponseStream()))
//            {
//                string soapResult = rd.ReadToEnd();
//                Console.WriteLine(soapResult);
//                Console.ReadKey();
//            }
//        }
//    }
//    /// <summary>
//    /// Create a soap webrequest to [Url]
//    /// </summary>
//    /// <returns></returns>
//    public static HttpWebRequest CreateWebRequest()
//    {
//        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"http://apps.zeekgps.com/cmovilapi3ws/cmovilapi3ws.asmx?wsdl");
//        webRequest.Headers.Add(@"SOAP:Action");
//        webRequest.ContentType = "text/xml;charset=\"utf-8\"";
//        webRequest.Accept = "text/xml";
//        webRequest.Method = "POST";
//        return webRequest;
//    }

