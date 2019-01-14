using Business;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjectTotalizator.Controllers
{
    public class BaseController : Controller
    {
        protected IBusinessLayer _businessLayer { get; set; }
        public BaseController(IBusinessLayer businessLayer)
        {
            _businessLayer = businessLayer;
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Money = _businessLayer.GetUsersMoney(User.Identity.GetUserId());
            }
        }
    }
}