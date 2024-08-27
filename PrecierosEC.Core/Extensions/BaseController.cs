using Microsoft.AspNetCore.Mvc;
using PrecierosEC.Core.Interface.Service;
using PrecierosEC.Core.Models;
using PrecierosEC.Core.Utiliies;

namespace PrecierosEC.Core.Extensions
{

    public class BaseController<T> : Controller
    {
        readonly IServiceErrorLog ErrorLog;
        protected string message = "";

        protected T Model { get; set; }
        private Response<T> Data { get; set; }
        public BaseController(IServiceErrorLog _ErrorLog) => ErrorLog = _ErrorLog;

        protected void SaveErrorLog(Exception ex) => this.message = string.Format(MensaggeErrorLog.ErrorGeneral, ErrorLog.SaveErrorlog(ex));

        private void AddResponse()
        {
            this.Data = new Response<T>
            {
                Resultado = new Result(this.message, string.IsNullOrEmpty(this.message)),
                Detalle = this.Model,
                
            };

        }

        protected IActionResult HttpResult()
        {
            this.AddResponse();
            if (string.IsNullOrEmpty(this.message)) 
                return Ok(this.Data);
            else
                return BadRequest(this.Data);

        }
    }
}
