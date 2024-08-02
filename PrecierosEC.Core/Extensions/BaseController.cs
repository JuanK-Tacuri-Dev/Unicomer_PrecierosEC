using Microsoft.AspNetCore.Mvc;
using PrecierosEC.Core.Interface.Service;
using PrecierosEC.Core.Models;
using PrecierosEC.Core.Utiliies;

namespace PrecierosEC.Core.Extensions
{
    public class BaseController : Controller
    {
        readonly IServiceErrorLog ServiceErrorLog;
        public BaseController(IServiceErrorLog _SqlServiceErrorLog)
        {
            ServiceErrorLog = _SqlServiceErrorLog;
        }
        protected Response<string> SaveErrorLog(Exception ex)
        {
            var Codigo = ServiceErrorLog.SaveErrorlog(ex);
            return ResponseData.GetResponse<string>(false, string.Format(MensaggeErrorLog.ErrorGeneral, Codigo));
        }
    }
}
