using Microsoft.AspNetCore.Mvc.Rendering;

namespace StoreApp.Util
{
    public static class HtmlHelpers
    {
        public static string IsActive(this IHtmlHelper htmlHelper, string page)
        {
            // TODO
            var currentPage = htmlHelper.ViewContext.RouteData.Values["page"] as string;
            return currentPage == page ? "active" : string.Empty;
        }
    }
}
