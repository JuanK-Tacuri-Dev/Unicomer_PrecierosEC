﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System.Data;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace PrecierosEC.Core.Utiliies
{

    public class Utilities
    {

        public static T LeerAppSettings<T>(Type type, ref string mensaje, string nameFile = "appsettings.json")
        {
            string directoryName = Path.GetDirectoryName(type.Assembly.Location);
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(directoryName).AddJsonFile(nameFile).Build();
            T val = configuration.Get<T>();
            if (val == null)
            {
                mensaje = "Archivo appsettings.json inválido";
            }

            return val;
        }


        public static string NothingToTexto(object texto)
        {
            if (texto == null)
                return "";
            else
                return texto.ToString();
        }


        [Obsolete]
        public static string GenerateLineLog(string mensaje, ref string CodigoSeguimiento)
        {
            var sf = new StackFrame(2, true);
            var Origen_lo = $"{sf.GetMethod().DeclaringType.FullName}/{sf.GetMethod().Name}";
            MappedDiagnosticsLogicalContext.Set("Origen", Origen_lo);
            MappedDiagnosticsLogicalContext.Set("Linea", sf.GetFileLineNumber());


            var Codigo = $"E{Guid.NewGuid().ToString("N").ToUpper().Substring(0, 5)}{DateTime.Now.ToString("hhmmssffffff")}";
            MappedDiagnosticsLogicalContext.Set("CodigoSeguimiento", Codigo);
            CodigoSeguimiento = MappedDiagnosticsLogicalContext.Get("CodigoSeguimiento");
            var errorLog = Conversions.DBNullToString(mensaje).Replace(Environment.NewLine, " ");
            return errorLog;
        }

        [Obsolete]
        public static string GenerateLineLog(Exception ex, ref string CodigoSeguimiento)
        {
            var sf = new StackFrame(2, true);
            var Origen_lo = $"{sf.GetMethod().DeclaringType.FullName}/{sf.GetMethod().Name}";
            MappedDiagnosticsLogicalContext.Set("Origen", Origen_lo);
            MappedDiagnosticsLogicalContext.Set("Linea", sf.GetFileLineNumber());

            var IdCompania = Conversions.DBNullToString(MappedDiagnosticsLogicalContext.Get("IdCompania"));

            var Codigo = $"E{Guid.NewGuid().ToString("N").ToUpper().Substring(0, 5)}{DateTime.Now.ToString("hhmmssffffff")}";
            MappedDiagnosticsLogicalContext.Set("CodigoSeguimiento", Codigo);
            CodigoSeguimiento = MappedDiagnosticsLogicalContext.Get("CodigoSeguimiento");
            var errorLog = Conversions.DBNullToString(Conversions.ExceptionToString(ex)).Replace(Environment.NewLine, " ");

            return errorLog;
        }

        public static List<T> Serialize_DataReader_To_string<T>(IDataReader reader)
        {

            var dataTable = new DataTable();
            var ModelReturn = new List<T>();
            dataTable.Load(reader);
            if (dataTable.Rows.Count > 0)
                ModelReturn = JsonConvert.DeserializeObject<List<T>>(JsonConvert.SerializeObject(dataTable));

            return ModelReturn;
        }

        public static string ConvertJsonToXml(string jsonString)
        {
            var jsonToken = JsonConvert.DeserializeObject<JToken>(jsonString);
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<root></root>");

            CreateXmlNodes(xmlDoc.DocumentElement, "element", jsonToken);

            return xmlDoc.OuterXml;
        }
        public static void CreateXmlNodes(XmlNode parentNode, string nodeName, JToken token)
        {
            if (token is JValue)
            {
                XmlElement element = parentNode.OwnerDocument.CreateElement(nodeName);
                element.InnerText = token.ToString();
                parentNode.AppendChild(element);
            }
            else if (token is JObject)
            {
                XmlElement objectElement = parentNode.OwnerDocument.CreateElement(nodeName);
                parentNode.AppendChild(objectElement);

                foreach (var property in ((JObject)token).Properties())
                {
                    CreateXmlNodes(objectElement, property.Name, property.Value);
                }
            }
            else if (token is JArray array)
            {
                foreach (var item in array)
                {
                    CreateXmlNodes(parentNode, nodeName, item);
                }
            }
        }


        public static string ConvertObjectToXml<T>(T model)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringWriter stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, model);
                return  stringWriter.ToString();
            }
        }
    }

}

