using oms.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace oms.Model
{
    public abstract class OrderPayment
    {
        public OrderPayment()
        {
            this.OrderId = Guid.Empty;
            this.ChequeDate = DateTime.Now;
            this.ChequeNumber = String.Empty;
            this.Amount = 0;
            this.Notes = String.Empty;
            this.Path = String.Empty;
            this.FileName = String.Empty;
        }

        public Guid OrderId { get; set; }
        public DateTime ChequeDate { get; set; }
        public string ChequeNumber { get; set; }
        public float Amount { get; set; }
        public string Notes { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }

        public virtual void Read(SqlDataReader reader)
        {
            this.OrderId = CommonFunctions.GetGuidSafely((reader["oid"]));
            this.ChequeDate = CommonFunctions.GetDateTimeSafely(reader["chequedate"]);
            this.ChequeNumber = CommonFunctions.GetStringSafely(reader["chequenumber"]);
            this.Amount = CommonFunctions.GetFloatSafely(reader["amount"]);
            this.Notes = CommonFunctions.GetStringSafely(reader["notes"]);
            this.Path = CommonFunctions.GetStringSafely(reader["path"]);
            this.FileName = CommonFunctions.GetStringSafely(reader["filename"]);
        }
    }

    public class OrderPaymentInsurance : OrderPayment
    {
        public OrderPaymentInsurance() : base()
        {
            this.BatchId = Guid.Empty;
            this.BatchName = string.Empty;
            this.RxNumber = string.Empty;
            this.PaymentTypeName = String.Empty;
        }

        public Guid BatchId { get; set; }
        public string BatchName { get; set; }
        public string RxNumber { get; set; }
        public string OrderNumber { get; set; }
        public float TotalAmountBilled { get; set; }
        public float TotalPayments { get; set; }
        public int PaymentType { get; set; }
        public string PaymentTypeName { get; set; }
        public bool IsPap { get; set; }

        public override void Read(SqlDataReader reader)
        {
            base.Read(reader);

            this.BatchId = CommonFunctions.GetGuidSafely((reader["batchid"]));
            this.BatchName = CommonFunctions.GetStringSafely((reader["batchname"]));
            this.RxNumber = CommonFunctions.GetStringSafely((reader["rxnumber"]));
            this.OrderNumber = CommonFunctions.GetStringSafely((reader["ordernumber"]));
            this.TotalAmountBilled = CommonFunctions.GetFloatSafely((reader["totalamountbilled"]));
            this.TotalPayments = CommonFunctions.GetFloatSafely((reader["totalpayments"]));
            this.PaymentType = CommonFunctions.GetIntSafely((reader["paymenttype"]));
            this.PaymentTypeName = StaticListDL.GetName(Constants.TableName_PaymentTypes, this.PaymentType);
            this.IsPap = CommonFunctions.GetBoolSafely((reader["pap"]));
        }

        public static OrderPaymentInsurance Create(SqlDataReader reader)
        {
            OrderPaymentInsurance model = new OrderPaymentInsurance();
            model.Read(reader);

            return model;
        }
    }

    public class OrderPaymentClient: OrderPayment
    {
        public static OrderPaymentClient Create(SqlDataReader reader)
        {
            OrderPaymentClient model = new OrderPaymentClient();
            model.Read(reader);

            return model;
        }
    }
}
