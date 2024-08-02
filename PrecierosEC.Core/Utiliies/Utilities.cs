using Microsoft.Extensions.Configuration;
using NLog;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

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
    }

}

