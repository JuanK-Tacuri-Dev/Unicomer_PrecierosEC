namespace PrecierosEC.Core.Models.Request
{
    public class CambioPrecioRequest
    {

        public int limit { get; set; }
        public int offset { get; set; }
        public int country { get; set; }
        public int fecha { get; set; }
        public int clase { get; set; }
        public int departamento { get; set; }
    }
}
