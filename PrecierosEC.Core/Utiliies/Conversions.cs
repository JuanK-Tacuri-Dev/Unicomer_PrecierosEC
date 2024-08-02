using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrecierosEC.Core.Utiliies
{
    public class Conversions
    {
        private class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }

        public static string DBNullToString(object Value)
        {
            if (Convert.IsDBNull(Value))
            {
                return string.Empty;
            }

            if (Value == null)
            {
                return string.Empty;
            }

            return Value.ToString();
        }
        public static string ExceptionToString(Exception Value)
        {
            try
            {
                string text = Value.Message;
                if (Value.InnerException != null)
                {
                    text = text + "-" + ExceptionToString(Value.InnerException);
                }

                return QuitarSaltosLinea(text);
            }
            catch (Exception)
            {
            }

            return string.Empty;
        }
        public static string QuitarSaltosLinea(string texto, string caracterReemplazar = " ")
        {
            if (texto != null)
            {
                if (texto != "")
                {
                    string text = texto.Replace('\n'.ToString(), caracterReemplazar).Replace('\r'.ToString(), caracterReemplazar);
                    text = text.Replace(Environment.NewLine, " ");
                    return Regex.Replace(text, " {2,}", " ");
                }

                return string.Empty;
            }

            return texto;
        }
        
       public static object NothingToDBNULL(string texto) => texto == null ? DBNull.Value : texto;
    }
}
