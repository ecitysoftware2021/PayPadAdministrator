﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PayPlusModels.CustomAuthentication
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected virtual CustomPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (CurrentUser == null)
            {
                return false;
            }
            else if (HttpContext.Current.Request.Cookies["TokenDashboard"] == null)
            {
                return false;
            }
            else
            {
                if (!string.IsNullOrEmpty(Roles) && !CurrentUser.IsInRole(Roles))
                {
                    return false;
                }

                return true;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RedirectToRouteResult routeData = null;

            if (CurrentUser == null || HttpContext.Current.Request.Cookies["TokenDashboard"] == null)
            {
                routeData = new RedirectToRouteResult
                    (new System.Web.Routing.RouteValueDictionary
                    (new
                    {
                        controller = "Account",
                        action = "Login",
                    }
                    ));
            }
            else
            {
                routeData = new RedirectToRouteResult
                (new System.Web.Routing.RouteValueDictionary
                 (new
                 {
                     controller = "Errors",
                     action = "AccessDenied"
                 }
                 ));
            }

            filterContext.Result = routeData;
        }

    }
}