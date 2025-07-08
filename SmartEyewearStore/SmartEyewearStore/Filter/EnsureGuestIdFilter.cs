using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System;

namespace SmartEyewearStore.Filters
{
    public class EnsureGuestIdFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            if (string.IsNullOrEmpty(session.GetString("GuestId")))
            {
                var guestId = Guid.NewGuid().ToString();
                session.SetString("GuestId", guestId);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}