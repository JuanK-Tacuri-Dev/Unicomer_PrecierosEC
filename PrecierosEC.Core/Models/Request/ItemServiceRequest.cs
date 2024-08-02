using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecierosEC.Core.Models.Request
{
    public class ItemServiceRequest
    {
        public string country { get; set; }
        public string storeId { get; set; }
        public string SKU { get; set; }

    }
}
