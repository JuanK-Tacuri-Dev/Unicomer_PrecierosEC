﻿using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace PrecierosEC.Core.Models
{
    public class CambioPrecio
    {
        public CambioPrecio()
        {
            this.Producto = new List<Producto>(); 
        }
        public List<Producto> Producto { get; set; }
    }
    public class Producto
    {
        public Producto()
        {
            this.garantias = new List<Garantia>();
        }
        public string storeCode { get; set; }
        public string onHand { get; set; }
        public string upc { get; set; }
        public string sku { get; set; }
        public string description { get; set; }
        public string fellDescription { get; set; }
        public string brandCode { get; set; }
        public string brandDescription { get; set; }
        public string model { get; set; }
        public string category { get; set; }
        public string warrDuraction { get; set; }
        public string tipoCambio { get; set; }
        public string fechaDesde { get; set; }
        public string fechaHasta { get; set; }
        public string precioAntes { get; set; }
        public string precioAhora { get; set; }
        public string regularPrice { get; set; }
        public string initialPrice { get; set; }
        public string deparmentCode { get; set; }
        public string deparmentDesc { get; set; }
        public string classCode { get; set; }
        public string classDesc { get; set; }
        public string skuType { get; set; }
        [JsonIgnore]
        public string id { get; set; }
        public List<Garantia> garantias { get; set; }
    }

    public class Garantia
    {
        public string csrUpc { get; set; }
        public string csrDescription { get; set; }
        public string csrDuration { get; set; }
        public string csrPrice { get; set; }
        public string csrpricepercent { get; set; }
        [JsonIgnore]
        public string id { get; set; }
    }


}
