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

        private string AddResponse()
        {
            this.Data = new Response<T>
            {
                Mensaje = string.IsNullOrEmpty(this.message) ? "OK" : this.message,
                Info = this.Model,
                Exito = string.IsNullOrEmpty(this.message)
            };

            return this.message;

        }
        //protected Response<T> addResponse<T>()
        //{
        //    return new Response<T>
        //    {
        //        Mensaje = this.message,
        //        Exito = string.IsNullOrEmpty(this.message)
        //    };  
        //}
        // protected IActionResult BadRequestResult()=> BadRequest(addResponse<string>());
        protected IActionResult HttpResult() => string.IsNullOrEmpty(AddResponse()) ? Ok(this.Data) : BadRequest(this.Data);
    }
}
