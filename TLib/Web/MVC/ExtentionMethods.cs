using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace TLib.Web.MVC
{
    public static class ExtentionMethods
    {
        public static string ToJsonString(this JsonResult result)
        {
            return new JavaScriptSerializer().Serialize(result.Data);
        }
    }
}
