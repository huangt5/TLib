using System.Xml.Linq;

namespace TLib.Core.Xml.LinqToXml
{
    public static class XAttributeExtensions
    {

        public static string GetString(this XAttribute attr)
        {
            if (attr == null)
            {
                return "";
            }
            return attr.Value;
        }

        public static bool GetBool(this XAttribute attr)
        {
            if (attr == null)
            {
                return false;
            }
            return bool.Parse(attr.Value);
        }

        public static int? GetNullableInt(this XAttribute attr)
        {
            if (attr == null || attr.Value == "")
            {
                return null;
            }
            return int.Parse(attr.Value);
        }
    } 
}
