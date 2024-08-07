namespace PrecierosEC.Core.Models
{
    public class ItemService
    {
        public ItemService()
        {
            this.product = new Product();
        }
        public Product product { get; set; }
    }

    public class Product
    {
        public Product()
        {
            this.item = new Item();
        }
        public Item item { get; set; }
        public string id { get; set; }
    }

    public class Item
    {
        public Item()
        {
            this.aggregatedItem = new List<Aggregateditem>();
            this.location = new LocationItemService();
            this.itemSellingPrices = new Itemsellingprices();
        }
        public string departmentName { get; set; }
        public string brandName { get; set; }
        public string itemType { get; set; }
        public string departmentCode { get; set; }
        public long? upc { get; set; }
        public string description { get; set; }
        public string fullDescription { get; set; }
        public string skuType { get; set; }
        public string productCategory { get; set; }
        public int? warrantyDuration { get; set; }
        public string modelName { get; set; }
        public string modelCode { get; set; }
        public string id { get; set; }
        public int? sku { get; set; }
        public string brandCode { get; set; }
        public LocationItemService location { get; set; }
        public Itemsellingprices itemSellingPrices { get; set; }
        public List<Aggregateditem> aggregatedItem { get; set; }
    }

    public class LocationItemService
    {
        public string locationId { get; set; }
        public int? locationType { get; set; }
    }

    public class Itemsellingprices
    {
        public int? permanentSaleUnitRetailPriceAmount { get; set; }
        public DateTime? temporarySaleUnitRetailPriceExpirationDate { get; set; }
        public string currentSaleUnitRetailPriceTypeCode { get; set; }
        public string currentSaleUnitRetailPriceEffectiveDate { get; set; }
       
    }

    public class Aggregateditem
    {
        public Aggregateditem()
        {
            this.itemAttributes = new Itemattributes();
            this.itemSellingPrices = new Itemsellingprices1();
        }
        public string modelName { get; set; }
        public string brandName { get; set; }
        public string itemType { get; set; }
        public string modelCode { get; set; }
        
        public string description { get; set; }
        public long? upc { get; set; }
        public string id { get; set; }
        public int? sku { get; set; }
        public string fullDescription { get; set; }
        public string brandCode { get; set; }
        public Itemsellingprices1 itemSellingPrices { get; set; }
        public Itemattributes itemAttributes { get; set; }
        
    }
    public class Itemsellingprices1
    {
        public int? permanentSaleUnitRetailPriceAmount { get; set; }
        public string currentSaleUnitRetailPriceTypeCode { get; set; }
        public decimal? pricepercent { get; set; }
    }
    public class Itemattributes
    {
        public Itemattributes()
        {
            this.stockItemAttributes = new Stockitemattributes();
        }
        public string stockItemType { get; set; }
        public Stockitemattributes stockItemAttributes { get; set; }
    }

    public class Stockitemattributes
    {
        public int? warrantyDuration { get; set; }
    }

    

}

