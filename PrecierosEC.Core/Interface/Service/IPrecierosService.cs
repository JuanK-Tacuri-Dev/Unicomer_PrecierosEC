using PrecierosEC.Core.Models;
using PrecierosEC.Core.Models.Request;

namespace PrecierosEC.Core.Interface.Service
{
    public interface IPrecierosService
    {
        Task<CambioPrecio> CambioPrecioQuery(CambioPrecioRequest model);
        Task<ItemService> ItemServiceQuery(ItemServiceRequest model);
        Task<PlanCredito> PlanCreditoQuery(PlanCreditoRequest model);
        
    }
}
