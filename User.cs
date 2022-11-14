using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oms.Model
{
    public class User
    {
        public User()
        {
            this.Id = 0;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Email = string.Empty;
            this.LoginId = string.Empty;
            this.Password = string.Empty;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }

        public void Read(SqlDataReader reader)
        {
            this.Id = CommonFunctions.GetIntSafely((reader["id"]));
            this.FirstName = CommonFunctions.GetStringSafely(reader["firstname"]);
            this.LastName = CommonFunctions.GetStringSafely(reader["lastname"]);
            this.Email = CommonFunctions.GetStringSafely(reader["email"]);
            this.LoginId = CommonFunctions.GetStringSafely(reader["loginId"]);
            this.Password = CommonFunctions.GetStringSafely(reader["password"]);
        }

        public static User Create(SqlDataReader reader)
        {
            User user = new User();
            user.Read(reader);
            return user;
        }

        public static User Create()
        {
            return new User();
        }

        public override string ToString()
        {
            return this.LastName + ", " + this.LastName;
        }
    }
}
