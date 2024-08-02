namespace PrecierosEC.Core.Models
{
    public class ItemService
    {
        public Product product { get; set; }
    }

    public class Product
    {
        public Item item { get; set; }
        public string id { get; set; }
    }

    public class Item
    {
        public string departmentName { get; set; }
        public string brandName { get; set; }
        public string itemType { get; set; }
        public string departmentCode { get; set; }
        public long upc { get; set; }
        public string description { get; set; }
        public string fullDescription { get; set; }
        public object skuType { get; set; }
        public Aggregateditem[] aggregatedItem { get; set; }
        public string productCategory { get; set; }
        public int warrantyDuration { get; set; }
        public string modelName { get; set; }
        public string modelCode { get; set; }
        public LocationItemService location { get; set; }
        public string id { get; set; }
        public Itemsellingprices itemSellingPrices { get; set; }
        public int sku { get; set; }
        public string brandCode { get; set; }
    }

    public class LocationItemService
    {
        public string locationId { get; set; }
        public int locationType { get; set; }
    }

    public class Itemsellingprices
    {
        public int permanentSaleUnitRetailPriceAmount { get; set; }
        public object temporarySaleUnitRetailPriceExpirationDate { get; set; }
        public string currentSaleUnitRetailPriceTypeCode { get; set; }
        public string currentSaleUnitRetailPriceEffectiveDate { get; set; }
    }

    public class Aggregateditem
    {
        public string modelName { get; set; }
        public string brandName { get; set; }
        public string itemType { get; set; }
        public string modelCode { get; set; }
        public Itemattributes itemAttributes { get; set; }
        public string description { get; set; }
        public long upc { get; set; }
        public string id { get; set; }
        public Itemsellingprices1 itemSellingPrices { get; set; }
        public int sku { get; set; }
        public string fullDescription { get; set; }
        public string brandCode { get; set; }
    }

    public class Itemattributes
    {
        public string stockItemType { get; set; }
        public Stockitemattributes stockItemAttributes { get; set; }
    }

    public class Stockitemattributes
    {
        public int warrantyDuration { get; set; }
    }

    public class Itemsellingprices1
    {
        public int permanentSaleUnitRetailPriceAmount { get; set; }
        public string currentSaleUnitRetailPriceTypeCode { get; set; }
    }

}

