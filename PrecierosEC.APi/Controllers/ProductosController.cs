using Microsoft.AspNetCore.Components.Forms;
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

        [HttpGet("ItemServiceQuery")]
        public IActionResult ItemServiceQuery(string country, string storeId, string SKU)
        {
            try
            {
                return OkResult(PrecierosService.ItemServiceQuery(country, storeId, SKU, ref message));
            }
            catch (Exception ex)
            {
                this.SaveErrorLog(ex);
                return BadRequestResult();
            }
        }

        [HttpGet("PlanCreditoQuery")]
        public IActionResult PlanCreditoQuery(string country, int companyId, decimal amountToFinance, int installments, decimal interestRate, int paymentCycle, int defferedPeriods, string treatment, int storeId, string sku, int warrantyid)
        {
            try
            {
                return OkResult(PrecierosService.PlanCreditoQuery(country, companyId, amountToFinance, installments, interestRate, paymentCycle, defferedPeriods, treatment, storeId, sku, warrantyid, ref message));
            }
            catch (Exception ex)
            {
                this.SaveErrorLog(ex);
                return BadRequestResult();
            }
        }

        [HttpPost("CambioPrecioQuery")]
        public IActionResult CambioPrecioQuery(CambioPrecioRequest body)
        {
            try
            {
                return OkResult(PrecierosService.CambioPrecioQuery(body, ref message));
            }
            catch (Exception ex)
            {
                this.SaveErrorLog(ex);
                return BadRequestResult();
            }
        }

    }
}


