using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Business;
using Common;
using MVCProjectTotalizator.Models;

namespace MVCProjectTotalizator.Controllers
{
    public class HomeController : BaseController
    {


        public HomeController(IBusinessLayer businessLayer) : base(businessLayer)
        {
        }


        public ActionResult Index()
        {
            var events = _businessLayer.GetNearSportEvents();
            if (User.Identity.IsAuthenticated)
            {
                //ViewBag.money = _businessLayer.GetUsersMoney(User.Identity.GetUserId());
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
            return RedirectToAction("MakeBet", "Bet", new { sportEventId = sportEvent.Id });
        }

        [Authorize]
        public ActionResult MyBets()
        {
            var userId = User.Identity.GetUserId();
            ViewBag.money = _businessLayer.GetUsersMoney(userId);

            var rates = _businessLayer.GetUsersRates(userId);
            return View(rates);
        }

        

        
    }
}