using Microsoft.Extensions.Configuration;
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

    }

}

