using PrecierosEC.Core.Models;
using PrecierosEC.Core.Models.Request;

namespace PrecierosEC.Core.Interface.Service
{
    public interface IPrecierosService
    {

        ItemService ItemServiceQuery(ItemServiceRequest model, ref string mensaje);
        PlanCredito PlanCreditoQuery(PlanCreditoRequest model, ref string mensaje);
        CambioPrecio CambioPrecioQuery(CambioPrecioRequest model, ref string mensaje);
       

    }
}
