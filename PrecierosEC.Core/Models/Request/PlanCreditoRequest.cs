namespace PrecierosEC.Core.Models.Request
{
    public class PlanCreditoRequest
    {

        public int Country { get; set; }
        public int CompanyId { get; set; }
        public int AmountToFinance { get; set; }
        public int Installments { get; set; }
        public int InterestRate { get; set; }
        public int PaymentCycle { get; set; }
        public int DefferedPeriods { get; set; }
        public int Treatment { get; set; }
        public int storeId { get; set; }
        public int SKU { get; set; }
        public int warrantyid { get; set; }



    }
}
