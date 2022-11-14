using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
namespace oms
{
    public static class Extensions
    {
        public static string GetAttribute(this XElement element, string attributeName)
        {
            string str = string.Empty;
            if ((element == null) || string.IsNullOrEmpty(attributeName))
            {
                str = string.Empty;
            }
            else
            {
                XAttribute attribute = element.Attribute(attributeName);
                str = (attribute == null) ? string.Empty : attribute.Value;
            }
            return str;
        }

        public static string GetItemValue(this IEnumerable<XElement> items, string key, string keyAttributeName = "key", string valueAttributeName = "value")
        {
            XElement element = items.FirstOrDefault(x => x.GetAttribute(keyAttributeName).IsStringEqual(key));
            return ((element == null) ? string.Empty : element.GetAttribute(valueAttributeName));
        }

        public static void SetItemValue(this XElement items, string key, string value, string keyAttributeName = "key", string valueAttributeName = "value")
        {
            XElement element = items.Elements().FirstOrDefault(x => x.GetAttribute(keyAttributeName).IsStringEqual(key));
            if (element == null)
            {
                element = new XElement("item", new XAttribute(keyAttributeName, key), new XAttribute(valueAttributeName, value));
                items.Add(element);
            }
            else
            {
                element.SetAttributeValue(valueAttributeName, value);
            }
        }

        public static bool IsStringContains(this string compareText1, string comparetext2) =>
            compareText1.ToUpper().Trim().Contains(comparetext2.Trim().ToUpper());

        public static bool IsStringContainsAny(this string compareText1, params string[] compareTexts)
        {
            string[] strArray = compareTexts;
            int index = 0;
            while (true)
            {
                bool flag2;
                if (index >= strArray.Length)
                {
                    flag2 = false;
                }
                else
                {
                    string str = strArray[index];
                    if (!compareText1.IsStringContains(str))
                    {
                        index++;
                        continue;
                    }
                    flag2 = true;
                }
                return flag2;
            }
        }

        public static bool IsStringEqual(this string compareText1, string compareText2)
        {
            if (ReferenceEquals(compareText2, null))
            {
                compareText2 = string.Empty;
            }
            return (string.Compare(compareText1.Trim(), compareText2.Trim(), StringComparison.OrdinalIgnoreCase) == 0);
        }

        public static string ReplaceAll(this string sourceText, params string[] replacedTexts)
        {
            int index = 0;
            string oldValue = string.Empty;
            int num2 = replacedTexts.Count<string>();
            while (index < num2)
            {
                oldValue = replacedTexts[index];
                if ((index + 1) < num2)
                {
                    sourceText = sourceText.Replace(oldValue, replacedTexts[index + 1]);
                }
                index += 2;
            }
            return sourceText;
        }
    }
}
