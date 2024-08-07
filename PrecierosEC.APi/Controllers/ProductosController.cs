using Microsoft.AspNetCore.Mvc;
using PrecierosEC.Core.Extensions;
using PrecierosEC.Core.Interface.Service;
using PrecierosEC.Core.Models.Request;

namespace PrecierosEC.APi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : BaseController
    {
        private readonly IPrecierosService PrecierosService;
        public ProductosController(IPrecierosService _PrecierosService, IServiceErrorLog _ServiceErrorLog) : base(_ServiceErrorLog)
        {
            PrecierosService = _PrecierosService;
        }

        [HttpPost("ItemServiceQuery")]
        public IActionResult ItemServiceQuery(ItemServiceRequest body)
        {
            try
            {
                var model = PrecierosService.ItemServiceQuery(body, ref message);
                return OkResult(model);
            }
            catch (Exception ex)
            {
                this.message = this.SaveErrorLog(ex)?.Mensaje;
                return BadRequestResult();
            }
        }

        [HttpPost("PlanCreditoQuery")]
        public IActionResult PlanCreditoQuery(PlanCreditoRequest body)
        {
            try
            {
                var model = PrecierosService.PlanCreditoQuery(body, ref message);
                return OkResult(model);
            }
            catch (Exception ex)
            {
                this.message = this.SaveErrorLog(ex)?.Mensaje;
                return BadRequestResult();
            }
        }

        [HttpPost("CambioPrecioQuery")]
        public IActionResult CambioPrecioQuery(CambioPrecioRequest body)
        {
            try
            {
                var model = PrecierosService.CambioPrecioQuery(body, ref message);
                return OkResult(model);
            }
            catch (Exception ex)
            {
                this.message = this.SaveErrorLog(ex)?.Mensaje;
                return BadRequestResult();
            }
        }

    }
}
