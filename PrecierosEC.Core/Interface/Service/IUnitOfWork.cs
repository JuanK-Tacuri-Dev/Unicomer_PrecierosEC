using PrecierosEC.Core.Models.Request;
using PrecierosEC.Core.Models;
using PrecierosEC.Core.Utiliies;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrecierosEC.Core.Interface.Service
{
    public interface IUnitOfWork
    {
        Task<CambioPrecio> CambioPrecioQuery(CambioPrecioRequest model);
        Task<ItemService> ItemServiceQuery(ItemServiceRequest model);
        Task<PlanCredito> PlanCreditoQuery(PlanCreditoRequest model);
    }
}
