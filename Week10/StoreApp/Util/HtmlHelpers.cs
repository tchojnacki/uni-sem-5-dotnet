using Microsoft.AspNetCore.Mvc.Rendering;

namespace StoreApp.Util
{
    public static class HtmlHelpers
    {
        public static string IsActive(this IHtmlHelper htmlHelper, string controller)
        {
            var currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;
            var currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;

            return currentAction == "Index" && currentController == controller
                ? "active"
                : string.Empty;
        }
    }
}
