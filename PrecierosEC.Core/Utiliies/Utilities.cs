using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NLog;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
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
                mensaje = "Archivo appsettings.json inválido";

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
        public static string GenerateLineLog(Exception ex, ref string CodigoSeguimiento)
        {
            var sf = new StackFrame(2, true);
            var Origen_lo = $"{sf.GetMethod().DeclaringType.FullName}/{sf.GetMethod().Name}";
            MappedDiagnosticsLogicalContext.Set("Origen", Origen_lo);
            MappedDiagnosticsLogicalContext.Set("Linea", sf.GetFileLineNumber());

            var Codigo = $"E{Guid.NewGuid().ToString("N").ToUpper().Substring(0, 5)}{DateTime.Now.ToString("hhmmssffffff")}";
            MappedDiagnosticsLogicalContext.Set("CodigoSeguimiento", Codigo);
            CodigoSeguimiento = MappedDiagnosticsLogicalContext.Get("CodigoSeguimiento");
            var errorLog = Conversions.DBNullToString(Conversions.ExceptionToString(ex)).Replace(Environment.NewLine, " ");

            return errorLog;
        }

        public static List<T> Serialize_DataTable_To_Object<T>(DataTable dataTable)
        {
            var resultList = new List<T>();
            if (dataTable.Rows.Count > 0)
                resultList = JsonConvert.DeserializeObject<List<T>>(JsonConvert.SerializeObject(dataTable));
            return resultList;
        }


        public static string ErrorDatabase(string message)
        {
            // Ejemplo de expresión regular para extraer el mensaje (ajusta según el formato)
            var match = Regex.Match(message, @"RAISERROR executed: (.+)$");
            return match.Success ? match.Groups[1].Value.Trim() : message;
        }

        public static string GetExcepcion(Exception ex)
        {
            var excesion = "" + ex.ToString() + "---------" + Environment.NewLine;
            if (ex.InnerException != null)
                excesion += GetExcepcion(ex.InnerException);

            return excesion;
        }
        public static string ConvertObjectToXml<T>(T model)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using StringWriter stringWriter = new StringWriter();
            serializer.Serialize(stringWriter, model);
            return stringWriter.ToString();
        }

        public static void CreateDataBaseAuditIfNotExist()
        {

            if (!Directory.Exists(AppConfiguration.RutaAuditDatabase))
                Directory.CreateDirectory(AppConfiguration.RutaAuditDatabase);

            if (!File.Exists(Path.Combine(AppConfiguration.RutaAuditDatabase, AppConfiguration.NameDatabaseAudit)))
                using (File.Create(Path.Combine(AppConfiguration.RutaAuditDatabase, AppConfiguration.NameDatabaseAudit))) { };
        }

        public static void CreateDataBaseErrorLogIfNotExist()
        {

            if (!Directory.Exists(AppConfiguration.RutaLogDatabase))
                Directory.CreateDirectory(AppConfiguration.RutaLogDatabase);

            if (!File.Exists(Path.Combine(AppConfiguration.RutaLogDatabase, AppConfiguration.NameDatabaseLogs)))
                using (File.Create(Path.Combine(AppConfiguration.RutaLogDatabase, AppConfiguration.NameDatabaseLogs))) { };

        }
    }

}

