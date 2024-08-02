using PrecierosEC.Core.Interface.Service;
using PrecierosEC.Core.Models;
using PrecierosEC.Core.Models.Request;

namespace PrecierosEC.Core.Service
{
    public class PrecierosService : IPrecierosService
    {
        private readonly IUnitOfWork UnitOfWork;

        public PrecierosService(IUnitOfWork _UnitOfWork)
        {
            UnitOfWork = _UnitOfWork;
        }


        public async Task<CambioPrecio> QueryCambioPrecio(CambioPrecioRequest model)
        {
            return new CambioPrecio();
        }
        public async Task<ItemService> QueryItemService(ItemServiceRequest model)
        {
            return new ItemService();
        }
        public async Task<PlanCredito> QueryPlanCredito(PlanCreditoRequest model)
        {
            return new PlanCredito();
        }

    }
}
