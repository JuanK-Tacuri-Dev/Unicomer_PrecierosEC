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

        public async Task<CambioPrecio> CambioPrecioQuery(CambioPrecioRequest model) => await UnitOfWork.CambioPrecioQuery(model);
        public async Task<ItemService> ItemServiceQuery(ItemServiceRequest model) => await UnitOfWork.ItemServiceQuery(model);
        public async Task<PlanCredito> PlanCreditoQuery(PlanCreditoRequest model) => await UnitOfWork.PlanCreditoQuery(model);


    }
}
