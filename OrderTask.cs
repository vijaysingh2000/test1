using oms.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace oms.Model
{
    public class OrderTask
    {
        public OrderTask()
        {
            this.OrderId = Guid.Empty;
            this.TaskCode = string.Empty;
            this.TaskName = string.Empty;
            this.TaskStatus = E_TaskStatus.None;

            this.Notes = String.Empty;
            this.LastUpdatedDate = DateTime.MinValue;
            this.LastUpdatedBy = Guid.Empty;
            this.LastUpdatedByName = String.Empty;
        }

        public Guid OrderId { get; set; }
        public string TaskCode { get; set; }
        public string TaskName { get; set; }
        public int Index { get; set; }
        public E_TaskStatus TaskStatus { get; set; }
        public string Notes { get; set; }
        public Guid LastUpdatedBy { get; set; }
        public string LastUpdatedByName { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public void Read(SqlDataReader reader)
        {
            this.OrderId = CommonFunctions.GetGuidSafely(reader["oid"]);
            this.TaskCode = CommonFunctions.GetStringSafely(reader["taskcode"]);
            this.Index = CommonFunctions.GetIntSafely(reader["idx"]);
            this.TaskStatus = (E_TaskStatus)Enum.Parse(typeof(E_TaskStatus), reader["taskstatus"].ToString(), true);
            this.Notes = CommonFunctions.GetStringSafely(reader["notes"]);
            this.LastUpdatedDate = CommonFunctions.GetDateTimeSafely(reader["lastupdatedttm"]);
            this.LastUpdatedBy = CommonFunctions.GetGuidSafely(reader["lastupdatedby"]);
            this.LastUpdatedByName = CommonFunctions.GetStringSafely(reader["lastname"]) + ", " + CommonFunctions.GetStringSafely(reader["firstname"]);
        }

        public static OrderTask Create(SqlDataReader sqlDataReader)
        {
            OrderTask orderTask = new OrderTask();
            orderTask.Read(sqlDataReader);
            return orderTask;
        }

        public static OrderTask Create()
        {
            return new OrderTask();
        }
    }
}
