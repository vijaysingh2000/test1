using DevExpress.XtraExport.Helpers;
using oms.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
namespace oms.Model
{
    public class Order
    {
        public Order()
        {
            this.PatientId = Guid.Empty;
            this.OrderNumber = string.Empty;
            this.DateOrdered = DateTime.Now;
            this.DOS = DateTime.Now;
            this.ConfirmedDOS = DateTime.Now;
            this.ConfirmedDeliveryDate = DateTime.Now;
            this.EstimatedDeliveryDate = DateTime.Now;
            this.OrderStatus = E_TaskStatus.None;
            this.DeliveryAddress = string.Empty;
            this.NextCallDate = DateTime.Now;
            this.ProphyOrPRN = "Prophy";
            this.DrugId = 0;
            this.ManufacturerId = 0;
            this.InsuranceId = 0;
            this.ConfirmationNumber = string.Empty;
            this.ProviderId = 0;
            this.AcceptableOutdatesId = 0;
            this.Id340B = 0;
            this.CreatedDate = DateTime.Now;
            this.CreatedBy = 0;
            this.LastUpdatedDate = DateTime.Now;
            this.LastUpdateBy = 0;
            this.Details = new XElement("details");
        }

        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime DOS { get; set; }
        public DateTime ConfirmedDOS { get; set; }
        public DateTime ConfirmedDeliveryDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public E_TaskStatus OrderStatus { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime NextCallDate { get; set; }
        public DateTime DateOrdered { get; set; }
        public string ProphyOrPRN { get; set; }
        public int DrugId { get; set; }
        public string DrugName { get; set; }
        public int ManufacturerId { get; set; }
        public int InsuranceId { get; set; }
        public string ManufacturerName { get; set; }
        public string InsuranceName { get; set; }
        public string ConfirmationNumber { get; set; }
        public float TotalPrescribedUnit { get; set; }
        public float DoseCount { get; set; }
        public float CogPerUnit { get; set; }
        public float BillPerUnit { get; set; }
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        public int AcceptableOutdatesId { get; set; }
        public string AcceptableOutdatesName { get; set; }
        public int Id340B { get; set; }
        public string Id340BName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int LastUpdateBy { get; set; }
        public XElement Details { get; set; }
        public double TotalUnitsBilled { get; set; }
        public double TotalPayments { get; set; }

        public void Read(SqlDataReader reader)
        {
            this.Id = CommonFunctions.GetGuidSafely(reader["oid"]);
            this.PatientId = CommonFunctions.GetGuidSafely(reader["pid"]);
            this.OrderNumber = CommonFunctions.GetStringSafely(reader["ordernumber"]); 
            this.DOS = CommonFunctions.GetDateTimeSafely(reader["dos"]);
            this.ConfirmedDOS = CommonFunctions.GetDateTimeSafely(reader["confirmeddos"]);
            this.ConfirmedDeliveryDate = CommonFunctions.GetDateTimeSafely(reader["confirmeddeliverydate"]);
            this.EstimatedDeliveryDate = CommonFunctions.GetDateTimeSafely(reader["estimateddeliverydate"]);
            this.EstimatedDeliveryDate = CommonFunctions.GetDateTimeSafely(reader["estimateddeliverydate"]);
            this.OrderStatus = (E_TaskStatus)Enum.Parse(typeof(E_TaskStatus), CommonFunctions.GetStringSafely(reader["orderstatus"], E_TaskStatus.None.ToString()), true);
            this.DeliveryAddress = CommonFunctions.GetStringSafely(reader["deliveryaddress"]);
            this.NextCallDate = CommonFunctions.GetDateTimeSafely(reader["nextcalldate"]);
            this.ProphyOrPRN = CommonFunctions.GetStringSafely(reader["prophyorprn"]);
            this.DrugId = CommonFunctions.GetIntSafely(reader["drugid"]);
            this.DrugName = CommonFunctions.GetStringSafely(reader["drugname"]);
            this.ManufacturerId = CommonFunctions.GetIntSafely(reader["manufacturerid"]);
            this.ManufacturerName = CommonFunctions.GetStringSafely(reader["manufacturername"]);
            this.InsuranceId = CommonFunctions.GetIntSafely(reader["insuranceid"]);
            this.InsuranceName = CommonFunctions.GetStringSafely(reader["insurancename"]);
            this.ConfirmationNumber = CommonFunctions.GetStringSafely(reader["confirmationnumber"]);
            this.TotalPrescribedUnit = CommonFunctions.GetFloatSafely(reader["totalunitprescribed"]);
            this.DoseCount = CommonFunctions.GetFloatSafely(reader["dosecount"]);
            this.CogPerUnit = CommonFunctions.GetFloatSafely(reader["cogperunit"]);
            this.BillPerUnit = CommonFunctions.GetFloatSafely(reader["billperunit"]);
            this.ProviderId = CommonFunctions.GetIntSafely(reader["providerid"]);
            this.ProviderName = CommonFunctions.GetStringSafely(reader["providername"]);
            this.AcceptableOutdatesId = CommonFunctions.GetIntSafely(reader["acceptableoutdatesid"]);
            this.AcceptableOutdatesName = StaticListDL.GetName(Constants.TableName_Acceptableoutdates, this.AcceptableOutdatesId);
            this.Id340B = CommonFunctions.GetIntSafely(reader["id340B"]);
            this.Id340BName = StaticListDL.GetName(Constants.TableName_id340B, this.Id340B);
            this.CreatedDate = CommonFunctions.GetDateTimeSafely(reader["createddttm"]);
            this.CreatedBy = CommonFunctions.GetIntSafely(reader["createdby"]);
            this.LastUpdatedDate = CommonFunctions.GetDateTimeSafely(reader["lastupdatedttm"]);
            this.LastUpdateBy = CommonFunctions.GetIntSafely(reader["lastupdatedby"]);
            this.TotalUnitsBilled = CommonFunctions.GetFloatSafely(reader["totalunitsbilled"]);
            this.TotalPayments = CommonFunctions.GetFloatSafely(reader["totalpayments"]);
            this.Details = CommonFunctions.GetXmlSafely(reader["otherdetails"]);
        }

        public static Order Create(SqlDataReader reader)
        {
            Order order = new Order();
            order.Read(reader);

            return order;
        }

        public static Order Create()
        {
            return new Order();
        }
    }
}
