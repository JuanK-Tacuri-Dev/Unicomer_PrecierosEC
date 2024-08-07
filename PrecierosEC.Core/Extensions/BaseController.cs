using Microsoft.AspNetCore.Mvc;
using PrecierosEC.Core.Interface.Service;
using PrecierosEC.Core.Models;
using PrecierosEC.Core.Utiliies;

namespace PrecierosEC.Core.Extensions
{

    public class BaseController : Controller
    {
        readonly IServiceErrorLog ErrorLog;
        public string message = "";
        public BaseController(IServiceErrorLog _ErrorLog)
        {
            ErrorLog = _ErrorLog;
        }
        protected Response<string> SaveErrorLog(Exception ex)
        {
            var Codigo = ErrorLog.SaveErrorlog(ex);
            return ResponseData.GetResponse<string>(string.Format(MensaggeErrorLog.ErrorGeneral, Codigo), false);
        }
        protected Response<T> addResponse<T>(T info, string mensaje = "OK")
        {
            return new Response<T>
            {
                Mensaje = mensaje,
                Info = info,
                Exito = string.IsNullOrEmpty(this.message)
            };
        }
        protected Response<T> addResponse<T>()
        {
            return new Response<T>
            {
                Mensaje = this.message,
                Exito = string.IsNullOrEmpty(this.message)
            };  
        }
        protected IActionResult BadRequestResult()
        {
            var Response = addResponse<string>();
            return BadRequest(Response);
        }
        protected IActionResult OkResult(object info)
        {
            var Response = addResponse(info);

            if (string.IsNullOrEmpty(this.message))
                return Ok(Response);
            else
                return BadRequest(Response);

        }
    }
}
