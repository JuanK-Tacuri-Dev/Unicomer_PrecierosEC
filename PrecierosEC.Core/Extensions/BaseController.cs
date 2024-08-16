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
        protected void SaveErrorLog(Exception ex) => this.message = string.Format(MensaggeErrorLog.ErrorGeneral,ErrorLog.SaveErrorlog(ex));

        protected Response<T> addResponse<T>(T info)
        {
            return new Response<T>
            {
                Mensaje = string.IsNullOrEmpty(this.message) ? "OK" : this.message,
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
        protected IActionResult BadRequestResult()=> BadRequest(addResponse<string>());
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
