using DevExpress.LookAndFeel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oms.Model
{
    public class BasicModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public float PerUnitFees { get; set; }
        public float FlatFees { get; set; }

        public void Read(SqlDataReader reader)
        {
            this.Id = CommonFunctions.GetIntSafely((reader["id"]));
            this.Name = CommonFunctions.GetStringSafely(reader["name"]);
            this.Description = CommonFunctions.GetStringSafely(reader["description"]);
            this.Active = CommonFunctions.GetBoolSafely(reader["active"]);
            this.PerUnitFees = CommonFunctions.GetFloatSafely(reader["perunitfees"]);
            this.FlatFees = CommonFunctions.GetFloatSafely(reader["flatfees"]);
        }
        public static BasicModel Create(SqlDataReader reader)
        {
            BasicModel model = new BasicModel();
            model.Read(reader);

            return model;
        }
    }
}
