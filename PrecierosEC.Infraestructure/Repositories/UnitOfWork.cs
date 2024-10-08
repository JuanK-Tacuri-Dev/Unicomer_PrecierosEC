﻿using PrecierosEC.Core.Interface.Service;
using PrecierosEC.Core.Models;
using PrecierosEC.Core.Models.Request;
using PrecierosEC.Core.Utiliies;
using System.Data;
using System.Data.Odbc;
using System.Runtime.Versioning;

namespace PrecierosEC.Infraestructure.Repositories
{
    [SupportedOSPlatform("windows")]
    public class UnitOfWork : IUnitOfWork
    {
        protected OdbcConnection conn;
        private string xmlinfo = "";

        DataSet query = new();

        public void SetearConexion()
        {
            conn = new OdbcConnection(AppConfiguration.ConnectionString);
            conn.OpenAsync();
        }

        public void CerrarConexion()
        {
            if (conn is not null)
            {
                if (conn.State == ConnectionState.Open)
                    conn.CloseAsync();

                conn.Dispose();
                conn = null;
            }
        }

        public ItemService ItemServiceQuery(ItemServiceRequest model, ref string mensaje)
        {
            xmlinfo = Utilities.ConvertObjectToXml(model);
            ItemService result = new();


            var exec = string.Format("DBA.SP_ItemServiceQuery @xmlInfo='{0}'", xmlinfo);
            Execute(exec, ref mensaje);
            if (!string.IsNullOrEmpty(mensaje))
                return null;


            result.product = Utilities.Serialize_DataTable_To_Object<Product>(query.Tables[0]).FirstOrDefault();

            if (result != null && !string.IsNullOrEmpty(result?.product?.id ?? ""))
            {
                result.product.item = Utilities.Serialize_DataTable_To_Object<Item>(query.Tables[1]).FirstOrDefault();

                result.product.item.location = Utilities.Serialize_DataTable_To_Object<LocationItemService>(query.Tables[2]).FirstOrDefault();

                result.product.item.itemSellingPrices = Utilities.Serialize_DataTable_To_Object<Itemsellingprices>(query.Tables[3]).FirstOrDefault();

                result.product.item.aggregatedItem = Utilities.Serialize_DataTable_To_Object<Aggregateditem>(query.Tables[4]).ToList();

                if (result.product?.item?.aggregatedItem != null)
                {
                    var itemSellingPrices1 = Utilities.Serialize_DataTable_To_Object<Itemsellingprices1>(query.Tables[5]).ToList();
                    var itemAttributes = Utilities.Serialize_DataTable_To_Object<Itemattributes>(query.Tables[6]).ToList();

                    if (itemAttributes != null)
                    {
                        var stockItemAttributes = Utilities.Serialize_DataTable_To_Object<Stockitemattributes>(query.Tables[7]).ToList();
                        itemAttributes.ForEach(x => x.stockItemAttributes = stockItemAttributes.Where(y => y.id == x.id).FirstOrDefault());
                    }
                    result.product?.item?.aggregatedItem.ForEach(x => x.itemSellingPrices = itemSellingPrices1.Where(y => y.id == x.id).FirstOrDefault());
                    result.product?.item?.aggregatedItem.ForEach(x => x.itemAttributes = itemAttributes.Where(y => y.id == x.id).FirstOrDefault());
                }
            }
            return result;
        }
        public PlanCredito PlanCreditoQuery(PlanCreditoRequest model, ref string mensaje)
        {
            xmlinfo = Utilities.ConvertObjectToXml(model);
            PlanCredito result = new();
            Location location = new();
            List<Installmentdetail> installmentDetail = new();
            List<Installmentrange> installmentRange = new();


            var exec = string.Format("DBA.SP_PlanCreditoQuery @xmlInfo='{0}'", xmlinfo);
            Execute(exec, ref mensaje);

            if (!string.IsNullOrEmpty(mensaje))
                return null;

            result.creditPlan = Utilities.Serialize_DataTable_To_Object<Creditplan>(query.Tables[0]).ToList();

            if (result.creditPlan != null)
            {
                location = Utilities.Serialize_DataTable_To_Object<Location>(query.Tables[1]).FirstOrDefault();
                installmentDetail = Utilities.Serialize_DataTable_To_Object<Installmentdetail>(query.Tables[2]).ToList();
                if (installmentDetail != null)
                {
                    installmentRange = Utilities.Serialize_DataTable_To_Object<Installmentrange>(query.Tables[3]).ToList();
                    installmentDetail.ForEach(x => x.installmentRange = installmentRange.Where(y => y.planid == x.planid).ToList());
                }
                result.creditPlan?.ForEach(x => x.location = location);
                result.creditPlan?.ForEach(x => x.installmentDetail = installmentDetail);
            }

            return result;
        }
        public CambioPrecio CambioPrecioQuery(CambioPrecioRequest model, ref string mensaje)
        {
            xmlinfo = Utilities.ConvertObjectToXml(model);
            CambioPrecio result = new();
            List<Garantia> garantia = new();

            var exec = string.Format("DBA.SP_CambioPrecioQuery @xmlInfo='{0}'", xmlinfo);

            Execute(exec, ref mensaje);
            if (!string.IsNullOrEmpty(mensaje))
                return null;


            result.Producto = Utilities.Serialize_DataTable_To_Object<Producto>(query.Tables[0]).ToList();

            if (result != null)
                garantia = Utilities.Serialize_DataTable_To_Object<Garantia>(query.Tables[1]).ToList();

            result?.Producto.ForEach(x => x.garantias = garantia.Where(y => y.id == x.id).ToList());

            return result;
        }

        private void Execute(string command, ref string mensaje)
        {
            try
            {
                SetearConexion();
                var cmd = conn.CreateCommand();
                cmd.CommandText = command;
                OdbcDataAdapter da = new(cmd);
                da.Fill(query);
            }
            catch (OdbcException ex)
            {
                foreach (OdbcError error in ex.Errors)
                {
                    if (error.SQLState == "HY000" && error.NativeError == -99999)
                    {
                        mensaje = Utilities.ErrorDatabase(error.Message);
                        break;
                    }
                    else
                        throw;
                }
            }
            finally
            {
                CerrarConexion();
            }
        }

    }
}
