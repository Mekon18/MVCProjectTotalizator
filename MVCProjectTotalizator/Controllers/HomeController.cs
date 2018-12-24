using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Business;
using Common;

namespace MVCProjectTotalizator.Controllers
{
    public class HomeController : Controller
    {
        private IBusinessLayer _businessLayer;

        public HomeController(IBusinessLayer businessLayer)
        {
            _businessLayer = businessLayer;
        }


        public ActionResult Index()
        {
            var events = _businessLayer.GetNearSportEvents();
            if (User.Identity.IsAuthenticated)
            {
                User.Identity.GetUserId();
                ViewBag.money = _businessLayer.GetUsersMoney(User.Identity.GetUserId());
            }
            return View(events);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MakeBet(SportEvent sportEvent)
        {
            return RedirectToAction("MakeBet", "Bet", sportEvent);
        }
    }
}