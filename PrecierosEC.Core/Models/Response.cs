using Newtonsoft.Json;

namespace PrecierosEC.Core.Models
{
    public class Response<T>
    {
        public Response()
        {
            this.Resultado = new Result();
        }

        [JsonProperty("resultado")]
        public Result Resultado { get; set; }

        [JsonProperty("detalle")]
        public T Detalle { get; set; }

    }

    public class Result
    {
        public Result()
        {
            
        }
        public Result(string mensaje, bool estado)
        {
            this.Estado = estado;
            this.Mensaje = mensaje;
        }
        [JsonProperty("mensaje")]
        public string Mensaje { get; set; }
        [JsonProperty("estado")]
        public bool Estado { get; set; }


    }

}
