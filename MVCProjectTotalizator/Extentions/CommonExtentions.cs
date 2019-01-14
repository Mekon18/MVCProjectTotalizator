using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Principal;
using Business;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace MVCProjectTotalizator
{
    public static class CommonExtentions
    {
        public static string ReplaceSeporator(this double d)
        {
            return d.ToString().Replace(",", ".");
        }

        public static bool IsInAnyRoles(this IPrincipal principal, params string[] roles)
        {
            var manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var IsInRole = false;
            foreach (var role in roles)
            {
                IsInRole = manager.IsInRole(principal.Identity.GetUserId(), role);
                if (IsInRole)
                    break;
            }
            return IsInRole;
        }

    }
}