using DevExpress.XtraLayout.Filtering.Templates;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oms.Model
{
    public class OrderAssay
    {
        public OrderAssay()
        {
            this.OrderId = Guid.Empty;
            this.AssayId = String.Empty;
            this.Assay = -0;
            this.NDC = string.Empty;
            this.Qty = 0;
            this.ExpDate = DateTime.Now;
            this.Lot = string.Empty;
            this.RxNumber = string.Empty;
        }

        public Guid OrderId { get; set; }
        public string AssayId { get; set; }
        public float Assay { get; set; }
        public string NDC { get; set; }
        public float Qty { get; set; }
        public DateTime ExpDate { get; set; }
        public string Lot { get; set; }
        public string RxNumber { get; set; }

        public void Read(SqlDataReader reader)
        {
            this.OrderId = CommonFunctions.GetGuidSafely((reader["oid"]));
            this.AssayId = CommonFunctions.GetStringSafely(reader["assayid"]);
            this.Assay = CommonFunctions.GetFloatSafely(reader["assay"]);
            this.NDC= CommonFunctions.GetStringSafely(reader["ndc"]);
            this.Qty = CommonFunctions.GetFloatSafely(reader["qty"]);
            this.ExpDate = CommonFunctions.GetDateTimeSafely(reader["expdate"]);
            this.Lot = CommonFunctions.GetStringSafely(reader["lot"]);
            this.RxNumber = CommonFunctions.GetStringSafely(reader["rxnumber"]);
        }
        public static OrderAssay Create(SqlDataReader reader)
        {
            OrderAssay model = new OrderAssay();
            model.Read(reader);

            return model;
        }
    }
}
