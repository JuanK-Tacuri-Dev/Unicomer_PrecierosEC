using Microsoft.AspNetCore.Mvc;
using PrecierosEC.Core.Extensions;
using PrecierosEC.Core.Interface.Service;
using PrecierosEC.Core.Models;
using PrecierosEC.Core.Models.Request;

namespace PrecierosEC.APi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IPrecierosService PrecierosService;
        ResponseData ResponseData;

        public ProductosController(IPrecierosService _PrecierosService )//, IServiceErrorLog _ServiceErrorLog) //:base(_ServiceErrorLog)
        {
            PrecierosService = _PrecierosService;
        }

        [HttpPost("QueryCambioPrecio")]
        public async Task<IActionResult> QueryCambioPrecio(CambioPrecioRequest body)
        {
            var model = new CambioPrecio();
            var Response = new Response<CambioPrecio>();
            try
            {
                model= await PrecierosService.QueryCambioPrecio(body);
                Response = ResponseData.GetResponse<CambioPrecio>("OK",model);
                return Ok(Response);
            }
            catch (Exception ex)
            {
                var Mensaje = "";// this.SaveErrorLog(ex)?.Mensaje;
                Response = ResponseData.GetResponse<CambioPrecio>(Mensaje, model,false);
                return BadRequest(Response);
            }
        }
        
        
        [HttpPost("QueryItemService")]
        public async Task<IActionResult> QueryItemService(ItemServiceRequest body)
        {
            var model = new ItemService();
            var Response = new Response<ItemService>();
            try
            {
                model= await PrecierosService.QueryItemService(body);
                Response = ResponseData.GetResponse<ItemService>("OK", model);
                return Ok(Response);
            }
            catch (Exception ex)
            {
                var Mensaje = "";// this.SaveErrorLog(ex)?.Mensaje;
                Response = ResponseData.GetResponse<ItemService>(Mensaje, model, false);
                return BadRequest(Response);
            }
        }
        
        [HttpPost("QueryPlanCredito")]
        public async Task<IActionResult> QueryPlanCredito(PlanCreditoRequest body)
        {
            var model = new PlanCredito();
            var Response = new Response<PlanCredito>();
            try
            {
                model= await PrecierosService.QueryPlanCredito(body);
                Response = ResponseData.GetResponse<PlanCredito>("OK", model);
                return Ok(Response);
            }
            catch (Exception ex)
            {
                var Mensaje = "";// this.SaveErrorLog(ex)?.Mensaje;
                Response = ResponseData.GetResponse<PlanCredito>(Mensaje, model, false);
                return BadRequest(Response);
            }
        }

        
    }
}
