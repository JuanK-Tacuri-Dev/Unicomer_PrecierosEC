using PrecierosEC.Core.Models;
using PrecierosEC.Core.Models.Request;

namespace PrecierosEC.Core.Interface.Service
{
    public interface IPrecierosService
    {
        ItemService ItemServiceQuery(string country, string storeId, string SKU, ref string mensaje);
        PlanCredito PlanCreditoQuery(string country, int companyId, decimal amountToFinance, int installments, decimal interestRate, int paymentCycle, int defferedPeriods, string treatment, int storeId, string sku, int warrantyid, ref string mensaje);
        CambioPrecio CambioPrecioQuery(CambioPrecioRequest model, ref string mensaje);
    }
}
