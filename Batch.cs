using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace oms.Model
{
    public class Batch
    {
        public Batch()
        {
            this.Id = Guid.Empty;
            this.Name = String.Empty;
            this.Notes = String.Empty;
            this.CreatedBy = String.Empty;
            this.LastUpdatedBy = String.Empty;
            this.CreatedDate = DateTime.Now;
            this.EmailDate = DateTime.Now;
            this.ReportDate = DateTime.Now;
            this.LastUpdatedDate = DateTime.Now;
            this.TotalAmount = 0;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public float TotalAmount { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EmailDate { get; set; }
        public DateTime ReportDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }       

        public void Read(SqlDataReader reader)
        {
            this.Id = CommonFunctions.GetGuidSafely((reader["bid"]));
            this.Name = CommonFunctions.GetStringSafely(reader["name"]);
            this.Notes = CommonFunctions.GetStringSafely(reader["notes"]);
            this.CreatedBy = CommonFunctions.GetStringSafely(reader["createdby"]);
            this.LastUpdatedBy = CommonFunctions.GetStringSafely(reader["lastname"]) + ", " + CommonFunctions.GetStringSafely(reader["firstname"]);
            this.CreatedDate = CommonFunctions.GetDateTimeSafely(reader["createddate"]);
            this.EmailDate = CommonFunctions.GetDateTimeSafely(reader["emaildate"]);
            this.ReportDate = CommonFunctions.GetDateTimeSafely(reader["reportdate"]);
            this.LastUpdatedDate = CommonFunctions.GetDateTimeSafely(reader["lastupdateddate"]);
        }
        public static Batch Create(SqlDataReader reader)
        {
            Batch batchPayment = new Batch();
            batchPayment.Read(reader);
            return batchPayment;
        }

        public static Batch Create()
        {
            return new Batch();
        }
    }
}
