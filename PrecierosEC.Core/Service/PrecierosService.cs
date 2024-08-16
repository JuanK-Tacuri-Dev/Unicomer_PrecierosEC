using PrecierosEC.Core.Interface.Service;
using PrecierosEC.Core.Models;
using PrecierosEC.Core.Models.Request;
using System.Runtime.Versioning;

namespace PrecierosEC.Core.Service
{
    [SupportedOSPlatform("windows")]
    public class PrecierosService : IPrecierosService
    {
        private readonly IUnitOfWork UnitOfWork;

        public PrecierosService(IUnitOfWork _UnitOfWork)
        {
            UnitOfWork = _UnitOfWork;
        }
        public ItemService ItemServiceQuery(string country, string storeId, string SKU, ref string mensaje)
        {

            var model = new ItemServiceRequest()
            {
                country = country,
                storeId = storeId,
                SKU = SKU
            };
            return UnitOfWork.ItemServiceQuery(model, ref mensaje);

        }
        public PlanCredito PlanCreditoQuery(string country, int companyId, decimal amountToFinance, int installments, decimal interestRate, int paymentCycle, int defferedPeriods, string treatment, int storeId, string sku, int warrantyid, ref string mensaje)
        {

            var model = new PlanCreditoRequest()
            {
                Country = country,
                CompanyId = companyId,
                AmountToFinance = amountToFinance,
                Installments = installments,
                InterestRate = interestRate,
                PaymentCycle = paymentCycle,
                DefferedPeriods = defferedPeriods,
                Treatment = treatment,
                storeId = storeId,
                SKU = sku,
                warrantyid = warrantyid
            };

            return UnitOfWork.PlanCreditoQuery(model, ref mensaje);

        }
        public CambioPrecio CambioPrecioQuery(CambioPrecioRequest model, ref string mensaje) => UnitOfWork.CambioPrecioQuery(model, ref mensaje);





    }
}
