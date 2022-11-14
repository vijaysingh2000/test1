using DevExpress.Data.Mask.Internal;
using oms.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oms.Model
{
    public class OrderWithPatient : Order
    {
        public string MRN { get; set; }
        public DateTime DOB { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string LastTaskCode { get; set; }
        public string LastState { get; set; }
        public int InsuranceId { get; set; }
        public string InsuranceName { get; set; }
        public double TotalAmountBilled { get; set; }
        public double TotalBalance { get; set; }

        public void Read(SqlDataReader reader)
        {
            base.Read(reader);

            this.MRN = CommonFunctions.GetStringSafely(reader["mrn"]);
            this.DOB = CommonFunctions.GetDateTimeSafely(reader["dob"]);
            this.FirstName = CommonFunctions.GetStringSafely(reader["firstname"]);
            this.LastName = CommonFunctions.GetStringSafely(reader["lastname"]);
            this.Email = CommonFunctions.GetStringSafely(reader["email"]);
            this.LastTaskCode = CommonFunctions.GetStringSafely(reader["lasttaskcode"]);
            this.InsuranceId = CommonFunctions.GetIntSafely(reader["insuranceid"]);
            this.InsuranceName = CommonFunctions.GetStringSafely(reader["insurancename"]);
            this.TotalAmountBilled = this.TotalUnitsBilled * this.BillPerUnit;
            this.TotalBalance = Math.Round((this.TotalAmountBilled - this.TotalPayments), 2);
            if (string.IsNullOrEmpty(this.LastTaskCode))
            {
                this.LastState = "Complete";
            }
            else
            {
                Tasks states = StaticListDL.GetAllTasks().FirstOrDefault(x => x.Code.IsStringEqual(this.LastTaskCode));
                if (states != null)
                    this.LastState = states.Name + " In Progress";
            }
        }

        public new static OrderWithPatient Create(SqlDataReader sqlDataReader)
        {
            OrderWithPatient order = new OrderWithPatient();
            order.Read(sqlDataReader);
            return order;
        }

        public new static OrderWithPatient Create()
        {
            return new OrderWithPatient();
        }
    }
}
