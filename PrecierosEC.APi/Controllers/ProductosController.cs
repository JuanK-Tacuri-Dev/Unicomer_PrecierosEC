using Microsoft.AspNetCore.Mvc;
using PrecierosEC.Core.Extensions;
using PrecierosEC.Core.Interface.Service;
using PrecierosEC.Core.Models;
using PrecierosEC.Core.Models.Request;

namespace PrecierosEC.APi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : BaseController
    {
        private readonly IPrecierosService PrecierosService;
        ResponseData ResponseData;
        private string mensaje = "";

        public ProductosController(IPrecierosService _PrecierosService, IServiceErrorLog _ServiceErrorLog) : base(_ServiceErrorLog)
        {
            PrecierosService = _PrecierosService;
        }

        [HttpPost("CambioPrecioQuery")]
        public IActionResult QueryCambioPrecio(CambioPrecioRequest body)
        {

            var Response = new Response<CambioPrecio>();
            try
            {
                var model = PrecierosService.CambioPrecioQuery(body, ref mensaje);
                Response = ResponseData.GetResponse<CambioPrecio>(model, mensaje, string.IsNullOrEmpty(mensaje));
                return string.IsNullOrEmpty(mensaje) ? Ok(Response) : BadRequest(Response);
            }
            catch (Exception ex)
            {
                var Mensaje = this.SaveErrorLog(ex)?.Mensaje;
                Response = ResponseData.GetResponse<CambioPrecio>(Mensaje, false);
                return BadRequest(Response);
            }
        }


        [HttpPost("ItemServiceQuery")]
        public IActionResult ItemServiceQuery(ItemServiceRequest body)
        {
            var Response = new Response<ItemService>();
            try
            {
                var model = PrecierosService.ItemServiceQuery(body, ref mensaje);
                Response = ResponseData.GetResponse<ItemService>(model, mensaje, string.IsNullOrEmpty(mensaje));
                return string.IsNullOrEmpty(mensaje) ? Ok(Response) : BadRequest(Response);
            }
            catch (Exception ex)
            {
                var Mensaje = this.SaveErrorLog(ex)?.Mensaje;
                Response = ResponseData.GetResponse<ItemService>(Mensaje, false);
                return BadRequest(Response);
            }
        }

        [HttpPost("PlanCreditoQuery")]
        public IActionResult PlanCreditoQuery(PlanCreditoRequest body)
        {

            var Response = new Response<PlanCredito>();
            try
            {
                var model = PrecierosService.PlanCreditoQuery(body, ref mensaje);
                Response = ResponseData.GetResponse<PlanCredito>(model, mensaje, string.IsNullOrEmpty(mensaje));
                return string.IsNullOrEmpty(mensaje) ? Ok(Response) : BadRequest(Response);
            }
            catch (Exception ex)
            {
                var Mensaje = this.SaveErrorLog(ex)?.Mensaje;
                Response = ResponseData.GetResponse<PlanCredito>(Mensaje, false);
                return BadRequest(Response);
            }
        }


    }
}
