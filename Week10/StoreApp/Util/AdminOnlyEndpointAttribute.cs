using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Microsoft.AspNetCore.Http;
using StoreApp.Data;

namespace StoreApp.Util
{
    public class AdminOnlyEndpointAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.IsInRole(Role.Admin))
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
        }
    }
}
