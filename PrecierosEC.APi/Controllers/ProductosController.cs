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

        public ProductosController(IPrecierosService _PrecierosService, IServiceErrorLog _ServiceErrorLog) :base(_ServiceErrorLog)
        {
            PrecierosService = _PrecierosService;
        }

        [HttpPost]
        public async Task<IActionResult> QueryCambioPrecio(CambioPrecioRequest body)
        {
            try
            {
                await PrecierosService.ReceivedMessage(body);
                return Ok("EVENT_RECEIVED");
            }
            catch (Exception ex)
            {
                var Mensaje = this.SaveErrorLog(ex)?.Mensaje;
                var NumberPhone = body.Entry[0]?.Changes[0]?.Value?.Messages[0].From;
                if (await SendMessageError(Mensaje, NumberPhone))
                    return Ok("EVENT_RECEIVED");
                else
                    return BadRequest("EVENT_NOT_RECEIVED");
            }
        }
        
        
        [HttpPost]
        public async Task<IActionResult> QueryItemService(ItemServiceRequest body)
        {
            try
            {
                await WhatsAppService.ReceivedMessage(body);
                return Ok("EVENT_RECEIVED");
            }
            catch (Exception ex)
            {
                var Mensaje = this.SaveErrorLog(ex)?.Mensaje;
                var NumberPhone = body.Entry[0]?.Changes[0]?.Value?.Messages[0].From;
                if (await SendMessageError(Mensaje, NumberPhone))
                    return Ok("EVENT_RECEIVED");
                else
                    return BadRequest("EVENT_NOT_RECEIVED");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> QueryPlanCredito(PlanCreditoRequest body)
        {
            try
            {
                await WhatsAppService.ReceivedMessage(body);
                return Ok("EVENT_RECEIVED");
            }
            catch (Exception ex)
            {
                var Mensaje = this.SaveErrorLog(ex)?.Mensaje;
                var NumberPhone = body.Entry[0]?.Changes[0]?.Value?.Messages[0].From;
                if (await SendMessageError(Mensaje, NumberPhone))
                    return Ok("EVENT_RECEIVED");
                else
                    return BadRequest("EVENT_NOT_RECEIVED");
            }
        }

        
    }
}
