using oms.DataAccessLayer;
using oms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace oms
{
    public static class ApplicationVariables
    {
        public static string ClientId = "allcare";
        public static User? LoggedInUser { get; set; }
        public static BasicModel? WorkingClient { get; set; }
        public static string? ConfigFilePath { get; set; }
        public static string? ConnectionString { get; set; }
        public static string? StaticListFile { get; set; }
        public static string? TaskListFile { get; set; }
        public static string? ImportDataFile { get; set; }
        public static string? DataFolder { get; set; }
        public static string? TemplateFolder { get; set; }

        public static bool SetApplicationVariables()
        {
            try
            {
                string configXml = Path.Combine(ConfigFilePath, "config.xml");
                XElement configXmlElement = XElement.Load(configXml);

                ConnectionString = configXmlElement.Element("connectionstring").Value.ToString();
                StaticListFile = configXmlElement.Element("staticlistfile").Value.ToString();
                TaskListFile = configXmlElement.Element("tasklistfile").Value.ToString();
                ImportDataFile = configXmlElement.Element("importdatafile").Value.ToString();
                DataFolder = configXmlElement.Element("datafolder").Value.ToString();
                TemplateFolder = configXmlElement.Element("templatefolder").Value.ToString();

                ConnectionString = CommonFunctions.ReplacePlaceHolders(ConnectionString);
                StaticListFile = CommonFunctions.ReplacePlaceHolders(StaticListFile);
                TaskListFile = CommonFunctions.ReplacePlaceHolders(TaskListFile);
                ImportDataFile = CommonFunctions.ReplacePlaceHolders(ImportDataFile);
                DataFolder = CommonFunctions.ReplacePlaceHolders(DataFolder);
                TemplateFolder = CommonFunctions.ReplacePlaceHolders(TemplateFolder);

                WorkingClient = ClienttDL.GetActive().FirstOrDefault();

                if (WorkingClient == null)
                {
                    CommonFunctions.ShowErrorMessage("Unable to find CLIENT Application Variable");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
