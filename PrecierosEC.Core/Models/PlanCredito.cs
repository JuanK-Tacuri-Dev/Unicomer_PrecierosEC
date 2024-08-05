﻿namespace PrecierosEC.Core.Models
{
    public class PlanCredito
    {
        public PlanCredito()
        {
            this.creditPlan = new List<Creditplan>();
        }
        public List<Creditplan> creditPlan { get; set; }
    }
    public class Creditplan
    {
        public Creditplan()
        {
            this.installmentDetail = new List<Installmentdetail>();
            this.location = new Location();
        }
        public string countryISOCode { get; set; }
        public string planName { get; set; }
        public Location location { get; set; }
        public int planid { get; set; }
        public string id { get; set; }
        public string assistanceFlag { get; set; }
        public string paymentCycleID { get; set; }
        public int minimunDepositAmount { get; set; }
        public int minimunDepositPercent { get; set; }
        public int paymentHoliday { get; set; }
        public List<Installmentdetail> installmentDetail { get; set; }
    }

    public class Location
    {
        public int locationId { get; set; }
        public string locationType { get; set; }
    }

    public class Installmentdetail
    {
        public Installmentdetail()
        {
            this.installmentRange = new List<Installmentrange>();
        }
        public int numberInstallment { get; set; }
        public List<Installmentrange> installmentRange { get; set; }
    }

    public class Installmentrange
    {
        public float annualInterestRate { get; set; }
        public int finalRange { get; set; }
        public int initialRange { get; set; }
    }


}

