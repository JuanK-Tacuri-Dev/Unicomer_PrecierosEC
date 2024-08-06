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

        public CambioPrecio CambioPrecioQuery(CambioPrecioRequest model, ref string mensaje) =>  UnitOfWork.CambioPrecioQuery(model,ref mensaje);
        public ItemService ItemServiceQuery(ItemServiceRequest model, ref string mensaje) =>  UnitOfWork.ItemServiceQuery(model, ref mensaje);
        public PlanCredito PlanCreditoQuery(PlanCreditoRequest model, ref string mensaje) =>  UnitOfWork.PlanCreditoQuery(model, ref mensaje);


    }
}
