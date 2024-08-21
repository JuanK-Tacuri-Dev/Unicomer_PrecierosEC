namespace PrecierosEC.Core.Models.Request
{
    public class CambioPrecioRequest
    {

        public string limit { get; set; }
        public string offset { get; set; }
        public string country { get; set; }
        public string fecha { get; set; }
        public string clase { get; set; }
        public string departamento { get; set; }
    }
}
