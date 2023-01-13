using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;

namespace StoreApp.Util
{
    public class DenyAttribute : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; } = default!;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.IsInRole(Roles))
                context.Result = new ForbidResult();
        }
    }
}
