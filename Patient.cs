using DocumentFormat.OpenXml.Office.CoverPageProps;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oms.Model
{
    public class Patient
    {
        public Patient()
        {
            this.MRN = string.Empty;
            this.DOB = DateTime.Now;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Email = string.Empty;
            this.Phone1 = String.Empty;
            this.Phone2 = String.Empty;
            this.DefaultAddressType = 0;
            this.Address1 = string.Empty;
            this.Address2 = string.Empty;
            this.Address3 = string.Empty;
            this.InsuranceId = 0;
            this.Notes = string.Empty;
            this.GuardianDetails = string.Empty;
        }
        public Guid Id { get; set; }
        public string MRN { get; set; }
        public DateTime DOB { get; set; }
        public string FirstName { get; set;} 
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public int DefaultAddressType { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public int InsuranceId { get; set; }
        public string InsuranceName { get; set; }
        public string Notes { get; set; }
        public string GuardianDetails { get; set; }
        public long Completed { get; set; }
        public long InProgress { get; set; }

        public void Read(SqlDataReader reader)
        {
            this.Id = CommonFunctions.GetGuidSafely((reader["pid"]));
            this.MRN = CommonFunctions.GetStringSafely(reader["mrn"]);
            this.DOB = CommonFunctions.GetDateTimeSafely(reader["dob"]);
            this.FirstName = CommonFunctions.GetStringSafely(reader["firstname"]);
            this.LastName = CommonFunctions.GetStringSafely(reader["lastname"]);
            this.Email = CommonFunctions.GetStringSafely(reader["email"]);
            this.Phone1 = CommonFunctions.GetStringSafely(reader["phone1"]);
            this.Phone2 = CommonFunctions.GetStringSafely(reader["phone2"]);
            this.Address1 = CommonFunctions.GetStringSafely(reader["address1"]);
            this.Address2 = CommonFunctions.GetStringSafely(reader["address2"]);
            this.Address3 = CommonFunctions.GetStringSafely(reader["address3"]);
            this.DefaultAddressType = CommonFunctions.GetIntSafely(reader["defaultaddresstype"]);
            this.InsuranceId = CommonFunctions.GetIntSafely(reader["insuranceid"]);
            this.InsuranceName = CommonFunctions.GetStringSafely(reader["insurancename"]);
            this.GuardianDetails = CommonFunctions.GetStringSafely(reader["guardiandetails"]);
            this.Notes = CommonFunctions.GetStringSafely(reader["notes"]);
            this.Completed = CommonFunctions.GetLongSafely(reader["ordercompleted"]);
            this.InProgress = CommonFunctions.GetLongSafely(reader["orderinprogress"]);
        }
        public static Patient Create(SqlDataReader reader)
        {
            Patient patient = new Patient();
            patient.Read(reader);
            return patient;
        }

        public static Patient Create()
        {
            return new Patient();
        }
    }
}
