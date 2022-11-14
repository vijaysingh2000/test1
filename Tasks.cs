using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace oms.Model
{
    public class Tasks
    {
        public Tasks()
        {
            this.Code = String.Empty;
            this.Name = string.Empty;
            this.Steps = new XElement("items");
        }

        public string Code { get; set; }
        public string Name { get; set; }      
        public XElement Steps { get; set; }
    }
}
