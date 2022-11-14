using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using DevExpress.Xpo.DB.Helpers;
using oms.DataAccessLayer;
using oms.Model;
using DevExpress.Utils.Extensions;
using System.Diagnostics;
using DevExpress.DataAccess.Native;

namespace oms
{
    public static class CommonFunctions
    {
        public static string GetConnectionString()
        {
            return ApplicationVariables.ConnectionString;

            //SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            //sqlConnectionStringBuilder.DataSource = "(local)\\vijaysqlinstance";
            //sqlConnectionStringBuilder.InitialCatalog = "test";
            //sqlConnectionStringBuilder.IntegratedSecurity = true;
            //sqlConnectionStringBuilder.PersistSecurityInfo = true;

            //return sqlConnectionStringBuilder.ConnectionString;

            //return @"server=PHSERVERN\SQLEXPRESS01;database=test;Trusted_Connection=True;Integrated Security=True";
        }

        public static Guid GetGuidSafely(object data)
        {
            if (data != null && Guid.TryParse(data.ToString(), out Guid returnValue))
                return returnValue;

            return Guid.Empty;
        }

        public static bool GetBoolSafely(object data, bool defaultValue = false)
        {
            if (data != null && Boolean.TryParse(data.ToString(), out bool returnVaue))
                return returnVaue;

            return defaultValue;
        }

        public static int GetBitSafely(bool data, int defaultValue = 0)
        {
            return data ? 1 : 0;
        }


        public static long GetLongSafely(object data)
        {
            if (data != null && long.TryParse(data.ToString(), out long returnVaue))
                return returnVaue;

            return 0;
        }

        public static int GetIntSafely(object data)
        {
            if (data != null && int.TryParse(data.ToString(), out int returnVaue))
                return returnVaue;

            return 0;
        }

        public static float GetFloatSafely(object data)
        {
            if (data != null && float.TryParse(data.ToString(), out float returnVaue))
                return returnVaue;

            return 0;
        }

        public static double GetDoubleSafely(object data)
        {
            if (data != null && double.TryParse(data.ToString(), out double returnVaue))
                return returnVaue;

            return 0;
        }

        public static string GetStringSafely(object data, string defaultValue = "")
        {
            if (data != null && data != DBNull.Value)
                return data.ToString();

            return defaultValue;
        }

        public static DateTime GetDateTimeSafely(object data)
        {
            if (data != null && DateTime.TryParse(data.ToString(), out DateTime returnValue))
                return returnValue;

            return DateTime.Now;
        }

        public static XElement GetXmlSafely(object data)
        {
            if (data != null && data.ToString() != String.Empty)
            {
                return XElement.Parse(data.ToString());
            }
            else
                return new XElement("nodata");
        }

        public static Exception GetNoObjectFoundException()
        {
            return new Exception(Constants.No_Object_Found);
        }

        public static void DeleteUnwantedFiles(string filePath, List<string> wantedFiles)
        {
            IEnumerable<string> unwantedFiles = Directory.GetFiles(filePath).Where(x => !wantedFiles.Contains(x.Trim().ToLower()));
            DeleteFilesSafely(unwantedFiles);
        }

        public static void DeleteFilesSafely(IEnumerable<string> files)
        {
            files.ForEach(x =>
            {
                try
                {
                    File.Delete(x);
                }
                catch
                {

                }
            });
        }

        public static void ShowErrorMessage(string errorMessage, string caption = "Error")
        {
            MessageBox.Show(errorMessage, GetDialogText(caption), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowInfomationMessage(string errorMessage, string caption = "Information")
        {
            MessageBox.Show(errorMessage, GetDialogText(caption), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static string GetDialogText(string suffix)
        {
            return (ApplicationVariables.WorkingClient != null ? ApplicationVariables.WorkingClient.Name : Constants.CompanyName) + " - " + suffix;
        }

        public static string GetDialogTextWithUser(string suffix = "")
        {
            if (ApplicationVariables.LoggedInUser != null)
                return $"{ApplicationVariables.WorkingClient.Name} - {suffix} [{ApplicationVariables.LoggedInUser.ToString()}]";
            else
                return $"{ApplicationVariables.WorkingClient.Name} - {suffix}";
        }

        public static string GetAddOrUpdateScript(string tableName, int idValue, string insertScript, string updateScript)
        {
            string returnVal = string.Empty;

            returnVal = $"IF EXISTS ( select 1 from {tableName} where id = {idValue}) " +
                $"BEGIN " +
                $"{updateScript} " +
                $"END " +
                $"ELSE " +
                $"BEGIN " +
                $"{insertScript}" +
                $"END ";

            return returnVal;
        }

        public static string GetAddOrUpdateScript(string existsScript, string insertScript, string updateScript)
        {
            string returnVal = string.Empty;

            returnVal = $"IF EXISTS ( {existsScript} ) " +
                $"BEGIN " +
                $"{updateScript} " +
                $"END " +
                $"ELSE " +
                $"BEGIN " +
                $"{insertScript}" +
                $"END ";

            return returnVal;
        }

        public static void RefreshHTMLData(string htmlFilename, Guid orderId)
        {
            string text = File.ReadAllText(htmlFilename);

            Order order = OrdersDL.Get(orderId, true);
            Patient patient = PatientsDL.Get(order.PatientId);
            List<OrderAssay> orderAssays = OrderAssayDL.Get(orderId);

            text = text.Replace("{mrn}", patient.MRN.FixHtmlText());
            text = text.Replace("{patientname}", patient.LastName.FixHtmlText() + ", " + patient.LastName.FixHtmlText());
            text = text.Replace("{date}", DateTime.Now.ToShortDateString().FixHtmlText());
            text = text.Replace("{deliveryaddress}", order.DeliveryAddress.FixHtmlText());
            text = text.Replace("{confirmeddos}", order.ConfirmedDOS.ToShortDateString().FixHtmlText());
            text = text.Replace("{drugname}", order.DrugName.FixHtmlText());
            text = text.Replace("{insurancename}", order.InsuranceName.FixHtmlText());
            text = text.Replace("{confirmeddeliverydate}", order.ConfirmedDeliveryDate.ToShortDateString().FixHtmlText());
            text = text.Replace("{nextcalldate}", order.NextCallDate.ToShortDateString().FixHtmlText());
            text = text.Replace("{estimateddeliverydate}", order.EstimatedDeliveryDate.ToShortDateString().FixHtmlText());
            text = text.Replace("{totalprescribedunit}", order.TotalPrescribedUnit.ToString().FixHtmlText());
            text = text.Replace("{totalunitsordered}", "0");
            text = text.Replace("{confirmation#}", order.ConfirmationNumber.FixHtmlText());
            text = text.Replace("{dateordered}", order.DateOrdered.ToShortDateString());
            text = text.Replace("{po#}", order.OrderNumber.ToString());
            text = text.Replace("{manufacturer}", order.ManufacturerName.FixHtmlText());
            text = text.Replace("{product1}", order.DrugName.FixHtmlText());
            text = text.Replace("{product2}", order.DrugName.FixHtmlText());
            text = text.Replace("{product3}", order.DrugName.FixHtmlText());
            text = text.Replace("{dateofservice}", order.DOS.ToShortDateString());
            text = text.Replace("{acceptableoutdate}", order.AcceptableOutdatesName.FixHtmlText());
            text = text.Replace("{340bid}", order.Id340BName.FixHtmlText());
            text = text.Replace("{providername}", order.ProviderName.FixHtmlText());
            text = text.Replace("{prophy}", (order.ProphyOrPRN.IsStringEqual("prophy") ? "X" : string.Empty).FixHtmlText());
            text = text.Replace("{prn}", (order.ProphyOrPRN.IsStringEqual("prn") ? "X" : string.Empty).FixHtmlText());
            text = text.Replace("{cogperunit}", order.CogPerUnit.ToString().FixHtmlText());
            text = text.Replace("{billedperunit}", order.BillPerUnit.ToString().FixHtmlText());
            text = text.Replace("{dosecount}", order.DoseCount.ToString().FixHtmlText());

            IEnumerable<XElement> items = order.Details.Elements("item");
            foreach (XElement item in items)
            {
                string key = item.GetAttribute("key");
                text = text.Replace("{" + $"{key}" + "}", item.GetAttribute("value").FixHtmlText());
            }

            List<XElement> supplies = order.Details.Element("supplies").Elements("item").ToList();
            StringBuilder stringBuilder = new StringBuilder();
            int itemNumber = 0;
            int maxCount = supplies.Count > 18 ? supplies.Count : 18;

            while (itemNumber < maxCount)
            {
                string rowHtml = $"<tr>" + Environment.NewLine;

                if (itemNumber < supplies.Count)
                {
                    XElement item = supplies[itemNumber];
                    rowHtml += $"<td>{item.GetAttribute("description").FixHtmlText()}</td>" + Environment.NewLine; ;
                    rowHtml += $"<td>{item.GetAttribute("itemnumber").FixHtmlText()}</td>" + Environment.NewLine; ;
                    rowHtml += $"<td>{item.GetAttribute("qty").FixHtmlText()}</td>" + Environment.NewLine; ;
                    rowHtml += $"<td>{item.GetAttribute("lot").FixHtmlText()}</td>" + Environment.NewLine; ;
                }
                else
                {
                    rowHtml += $"<td>{string.Empty.FixHtmlText()}</td>" + Environment.NewLine; ;
                    rowHtml += $"<td>{string.Empty.FixHtmlText()}</td>" + Environment.NewLine; ;
                    rowHtml += $"<td>{string.Empty.FixHtmlText()}</td>" + Environment.NewLine; ;
                    rowHtml += $"<td>{string.Empty.FixHtmlText()}</td>" + Environment.NewLine; ;
                }

                itemNumber++;

                if (itemNumber < supplies.Count)
                {
                    XElement item = supplies[itemNumber];
                    rowHtml += $"<td>{item.GetAttribute("description").FixHtmlText()}</td>" + Environment.NewLine; ;
                    rowHtml += $"<td>{item.GetAttribute("itemnumber").FixHtmlText()}</td>" + Environment.NewLine; ;
                    rowHtml += $"<td>{item.GetAttribute("qty").FixHtmlText()}</td>" + Environment.NewLine; ;
                    rowHtml += $"<td>{item.GetAttribute("lot").FixHtmlText()}</td>" + Environment.NewLine; ;
                }
                else
                {
                    rowHtml += $"<td>{string.Empty.FixHtmlText()}</td>" + Environment.NewLine; ;
                    rowHtml += $"<td>{string.Empty.FixHtmlText()}</td>" + Environment.NewLine; ;
                    rowHtml += $"<td>{string.Empty.FixHtmlText()}</td>" + Environment.NewLine; ;
                    rowHtml += $"<td>{string.Empty.FixHtmlText()}</td>" + Environment.NewLine; ;
                }

                rowHtml += $"</tr>" + Environment.NewLine;
                stringBuilder.Append(rowHtml);

                itemNumber++;
            }
            text = text.Replace("{supplies}", stringBuilder.ToString());


            double totalunitsordered = 0;
            for (int iloop = 0; iloop < orderAssays.Count; iloop++)
            {
                text = text.Replace("{" + "assay" + ((iloop + 1).ToString()) + "}", orderAssays[iloop].Assay.ToString().FixHtmlText());
                text = text.Replace("{" + "ndc" + ((iloop + 1).ToString()) + "}", orderAssays[iloop].NDC.ToString().FixHtmlText());
                text = text.Replace("{" + "qty" + ((iloop + 1).ToString()) + "}", orderAssays[iloop].Qty.ToString().FixHtmlText());
                text = text.Replace("{" + "totalunit" + ((iloop + 1).ToString()) + "}", Math.Round((orderAssays[iloop].Assay * orderAssays[iloop].Qty), 2).ToString().FixHtmlText());
                text = text.Replace("{" + "totalcog" + ((iloop + 1).ToString()) + "}", Math.Round((orderAssays[iloop].Assay * orderAssays[iloop].Qty * order.CogPerUnit), 2).ToString().FixHtmlText());
                text = text.Replace("{" + "totalbillcharge" + ((iloop + 1).ToString()) + "}", Math.Round((orderAssays[iloop].Assay * orderAssays[iloop].Qty * order.BillPerUnit), 2).ToString().FixHtmlText());
                text = text.Replace("{" + "expdate" + ((iloop + 1).ToString()) + "}", orderAssays[iloop].ExpDate.ToShortDateString().FixHtmlText());
                text = text.Replace("{" + "lot" + ((iloop + 1).ToString()) + "}", orderAssays[iloop].Lot.ToString().FixHtmlText());
                text = text.Replace("{" + "rx" + ((iloop + 1).ToString()) + "}", orderAssays[iloop].RxNumber.ToString().FixHtmlText());

                totalunitsordered += orderAssays[iloop].Assay;

            }

            text = text.Replace("{totalunitsprescribed}", Math.Round((order.TotalPrescribedUnit * order.DoseCount), 2).ToString().FixHtmlText());
            text = text.Replace("{totalunitstobill}", Math.Round((totalunitsordered * order.DoseCount), 2).ToString().FixHtmlText());
            text = text.Replace("{totalcogorder}", Math.Round((totalunitsordered * order.DoseCount * order.CogPerUnit), 2).ToString().FixHtmlText());
            text = text.Replace("{totalbillcharge}", Math.Round((totalunitsordered * order.DoseCount * order.BillPerUnit), 2).ToString().FixHtmlText());

            File.WriteAllText(htmlFilename, text);
        }

        public static void OpenFile(string fileName)
        {
            ProcessStartInfo pi = new ProcessStartInfo(fileName);
            pi.Arguments = Path.GetFileName(fileName);
            pi.UseShellExecute = true;
            pi.WorkingDirectory = Path.GetDirectoryName(fileName);
            pi.FileName = fileName;
            pi.Verb = "OPEN";
            Process.Start(pi);
        }

        public static string FixHtmlText(this string htmlText)
        {
            if (string.IsNullOrEmpty(htmlText))
                return "&nbsp;";

            return htmlText;
        }

        public static string BuildHeaderHtmlFile(Guid oid)
        {
            return BuildFileFromTemplate(oid, "Header.html");
        }

        public static string BuildOrderHtmlFile(Guid oid)
        {
            return BuildFileFromTemplate(oid, "Order.html");
        }

        public static string BuildFileFromTemplate(Guid orderId, string templateHtmlFileName)
        {
            string sourceFolder = ApplicationVariables.TemplateFolder;
            string sourceSupportFolder = Path.Combine(ApplicationVariables.TemplateFolder, @$"{Path.GetFileNameWithoutExtension(templateHtmlFileName)}_files");
            string sourceFileName = Path.Combine(sourceFolder, templateHtmlFileName);

            string destFolder = Path.Combine(Path.GetTempPath(), $"{orderId}");
            string destSupportFolder = Path.Combine(destFolder, $"{Path.GetFileNameWithoutExtension(templateHtmlFileName)}_files");
            string destFileName = Path.Combine(destFolder, templateHtmlFileName);

            Directory.CreateDirectory(destFolder);
            Directory.CreateDirectory(destSupportFolder);

            File.Copy(sourceFileName, destFileName, true);
            if (Directory.Exists(sourceSupportFolder))
            {
                string[] supporFiles = Directory.GetFiles(sourceSupportFolder);
                foreach (string supportFile in supporFiles)
                {
                    File.Copy(supportFile, Path.Combine(destSupportFolder, Path.GetFileName(supportFile)), true);
                }
            }

            CommonFunctions.RefreshHTMLData(destFileName, orderId);

            return destFileName;
        }

        public static void PrintReport(string path, Form form)
        {
            WebBrowser browser = new WebBrowser();
            browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(CommonFunctions.Wb_DocumentCompleted);
            browser.Visible = true;
            browser.ScrollBarsEnabled = false;
            browser.ScriptErrorsSuppressed = true;
            browser.AllowNavigation = false;
            browser.Parent = form;
            browser.Navigate(path);
        }

        private static void Wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = (WebBrowser)sender;
            if (browser.ReadyState.Equals(WebBrowserReadyState.Complete))
            {
                browser.ShowPrintPreviewDialog();
            }
        }

        public static string ReplacePlaceHolders(string text)
        {
            text = text.Replace("{appath}", Application.StartupPath);
            text = text.Replace("{configfilepath}", ApplicationVariables.ConfigFilePath);
            text = text.Replace("{clientname}", ApplicationVariables.WorkingClient != null ? ApplicationVariables.WorkingClient.Name : String.Empty);

            return text;
        }
    }
}
