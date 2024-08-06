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
        CambioPrecio CambioPrecioQuery(CambioPrecioRequest model, ref string mensaje);
        ItemService ItemServiceQuery(ItemServiceRequest model, ref string mensaje);
        PlanCredito PlanCreditoQuery(PlanCreditoRequest model, ref string mensaje);
    }
}
