using System.Web;
using System.Web.Mvc;

namespace SignalRUsingPersistentConnectionsDemo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
