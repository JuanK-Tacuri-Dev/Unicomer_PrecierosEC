using PrecierosEC.Core.Models;
using PrecierosEC.Core.Models.Request;

namespace PrecierosEC.Core.Interface.Service
{
    public interface IPrecierosService
    {
        Task<CambioPrecio> QueryCambioPrecio(CambioPrecioRequest model);
        Task<ItemService> QueryItemService(ItemServiceRequest model);
        Task<PlanCredito> QueryPlanCredito(PlanCreditoRequest model);
        
    }
}
