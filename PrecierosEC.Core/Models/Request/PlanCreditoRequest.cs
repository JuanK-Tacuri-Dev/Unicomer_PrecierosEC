namespace PrecierosEC.Core.Models.Request
{
    public class PlanCreditoRequest
    {

        public string Country { get; set; }
        public int CompanyId { get; set; }
        public decimal AmountToFinance { get; set; }
        public int Installments { get; set; }
        public decimal InterestRate { get; set; }
        public int PaymentCycle { get; set; }
        public int DefferedPeriods { get; set; }
        public string Treatment { get; set; }
        public int storeId { get; set; }
        public string SKU { get; set; }
        public int warrantyid { get; set; }



    }
}
    