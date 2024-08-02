using Microsoft.AspNetCore.Mvc;
using PrecierosEC.Core.Interface.Service;
using PrecierosEC.Core.Models;
using PrecierosEC.Core.Utiliies;
using System.Net;

namespace PrecierosEC.Core.Extensions
{
    public class BaseController : Controller
    {
        readonly IServiceErrorLog ServiceErrorLog;
        ResponseData responseData = new();
        public BaseController(IServiceErrorLog _SqlServiceErrorLog)
        {
            ServiceErrorLog = _SqlServiceErrorLog;
        }

        
       
        
        private Response data = new();
        private HttpStatusCode status;


        protected Response<string> SaveErrorLog(Exception ex)
        {
            var Codigo = ServiceErrorLog.SaveErrorlog(ex);
            return ResponseData.GetResponse<string>(false, string.Format(MensaggeErrorLog.ErrorGeneral, Codigo));
        }

        


        protected void Status(HttpStatusCode Status)
        {
            this.status = Status;
        }
        protected void Data<T>(T Status)
        {
            this.data.Info = Status.ToString();
        }


        protected IActionResult OkModelResult()
        {
            if (this.status == HttpStatusCode.BadRequest)
                return BadRequest();
            if (this.status == HttpStatusCode.Unauthorized)
                return Unauthorized();
            return Ok(this.data);
        }
        protected IActionResult OkResult()
        {
            if (this.status == HttpStatusCode.BadRequest)
                return BadRequest(this.data.Info);
            if (this.status == HttpStatusCode.Unauthorized)
                return Unauthorized();
            if (this.status == HttpStatusCode.NotFound)
                return NotFound();
            if (this.status == HttpStatusCode.InternalServerError)
                return NotFound();
            return Ok(this.data.Info);
        }
    }
}
