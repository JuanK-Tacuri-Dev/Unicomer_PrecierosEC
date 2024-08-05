
using PrecierosEC.Core.Interface.Service;
using PrecierosEC.Core.Models;
using PrecierosEC.Core.Models.Request;
using PrecierosEC.Core.Utiliies;
using System.Data;
using System.Data.Odbc;
using System.Runtime.Versioning;

namespace PrecierosEC.Core.Repositories
{
    [SupportedOSPlatform("windows")]
    public class UnitOfWork : IUnitOfWork
    {
        protected OdbcConnection conn;
        private string xmlinfo = "";
        
        public void SetearConexion()
        {
            conn = new OdbcConnection(AppConfiguration.ConnectionString);
                conn.Open();
        }
        
        public void CerrarConexion()
        {

            if (conn is not null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close(); // cierro la conexión
                    
                }

                conn.Dispose();
                conn = null;
            }

        }

      

        public async Task<CambioPrecio> CambioPrecioQuery(CambioPrecioRequest model)
        {
            


            this.xmlinfo = Utilities.ConvertObjectToXml<CambioPrecioRequest>(model);

            CambioPrecio result = new CambioPrecio();
            List<Producto> producto = new List<Producto>();
            List<Garantia> garantia = new List<Garantia>();

            try
            {
                SetearConexion();

                var cmd = conn.CreateCommand();
                var exec = String.Format("DBA.SP_CambioPrecioQuery @xmlInfo='{0}'", this.xmlinfo);
                cmd.CommandText = exec;

                using IDataReader reader = await cmd.ExecuteReaderAsync();
                result.Producto = Utilities.Serialize_DataReader_To_string<Producto>(reader).ToList();

                if (result != null)
                    garantia = Utilities.Serialize_DataReader_To_string<Garantia>(reader).ToList();

                reader.Close();


                if (result != null)
                {
                    result.Producto.ForEach(x => x.garantias = garantia ?? null);
                }

            }
            finally
            {
                CerrarConexion();
            }
            return result;
        }
        public async Task<ItemService> ItemServiceQuery(ItemServiceRequest model)
        {
            this.xmlinfo = Utilities.ConvertObjectToXml<ItemServiceRequest>(model);
            ItemService result = new ItemService();
            //Product product = new Product();
            //Item item = new Item();
            List<Aggregateditem> aggregatedItem = new List<Aggregateditem>();
            LocationItemService location = new LocationItemService();
            Itemsellingprices itemSellingPrices = new Itemsellingprices();
            Itemattributes itemAttributes = new Itemattributes();
            Itemsellingprices1 itemSellingPrices1 = new Itemsellingprices1();
            Stockitemattributes stockItemAttributes = new Stockitemattributes();

            try
            {
                SetearConexion();
                var cmd = conn.CreateCommand();
                var exec = String.Format("DBA.SP_ItemServiceQuery @xmlInfo='{0}'", this.xmlinfo);
                cmd.CommandText = exec;

                using IDataReader reader = await cmd.ExecuteReaderAsync();
                result.product = Utilities.Serialize_DataReader_To_string<Product>(reader).FirstOrDefault();

                if (result != null)
                {
                    result.product.item = Utilities.Serialize_DataReader_To_string<Item>(reader).FirstOrDefault();

                    aggregatedItem = Utilities.Serialize_DataReader_To_string<Aggregateditem>(reader).ToList();

                    location = Utilities.Serialize_DataReader_To_string<LocationItemService>(reader).FirstOrDefault();

                    itemSellingPrices = Utilities.Serialize_DataReader_To_string<Itemsellingprices>(reader).FirstOrDefault();

                    itemAttributes = Utilities.Serialize_DataReader_To_string<Itemattributes>(reader).FirstOrDefault();

                    itemSellingPrices1 = Utilities.Serialize_DataReader_To_string<Itemsellingprices1>(reader).FirstOrDefault();

                    stockItemAttributes = Utilities.Serialize_DataReader_To_string<Stockitemattributes>(reader).FirstOrDefault();
                }
                reader.Close();

            }
            finally
            {
                CerrarConexion();
            }
            return result;
        }
        public async Task<PlanCredito> PlanCreditoQuery(PlanCreditoRequest model)
        {
            this.xmlinfo = Utilities.ConvertObjectToXml<PlanCreditoRequest>(model);
            PlanCredito result = new PlanCredito();
            List<Creditplan> creditPlan = new List<Creditplan>();
            List<Installmentdetail> installmentDetail = new List<Installmentdetail>();
            Location location = new Location();
            List<Installmentrange> installmentRange = new List<Installmentrange>();

            try
            {
                SetearConexion();
                var cmd = conn.CreateCommand();
                var exec = String.Format("DBA.SP_PlanCreditoQuery @xmlInfo='{0}'", this.xmlinfo);
                cmd.CommandText = exec;

                using IDataReader reader = await cmd.ExecuteReaderAsync();
                result.creditPlan = Utilities.Serialize_DataReader_To_string<Creditplan>(reader).ToList();

                if (result != null)
                {
                    installmentDetail = Utilities.Serialize_DataReader_To_string<Installmentdetail>(reader).ToList();

                    location = Utilities.Serialize_DataReader_To_string<Location>(reader).FirstOrDefault();

                    installmentRange = Utilities.Serialize_DataReader_To_string<Installmentrange>(reader).ToList();
                }
                reader.Close();

            }
            finally
            {
                CerrarConexion();
            }
            return result;
        }



    }
}
