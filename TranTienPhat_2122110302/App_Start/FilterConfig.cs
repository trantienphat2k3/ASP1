using System.Web;
using System.Web.Mvc;

namespace TranTienPhat_2122110302
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
