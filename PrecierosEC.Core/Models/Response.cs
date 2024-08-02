using Newtonsoft.Json;

namespace PrecierosEC.Core.Models
{
    public class Response<T>
    {

        [JsonProperty("info")]
        public T Info { get; set; }
        [JsonProperty("mensaje")]
        public string Mensaje { get; set; }
        [JsonProperty("exito")]
        public bool Exito { get; set; }
    }

    public class ResponseData
    {
        public static Response<T> GetResponse<T>( string mensaje, T info, bool exito = true)
        {
            return new Response<T>
            {
                Info = info,
                Mensaje = mensaje,
                Exito = exito
            };
        }

        public static Response<T> GetResponse<T>(bool exito, string mensaje)
        {
            return new Response<T>
            {
                Mensaje = mensaje,
                Exito = exito
            };
        }
        public static Response<T> GetResponseT<T>(T info)
        {
            return new Response<T>
            {
                Info = info
            };
        }
    }
}
