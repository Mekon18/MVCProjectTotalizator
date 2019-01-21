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


        //public ActionResult Index()
        //{
        //    var events = _businessLayer.GetNearSportEvents();           
        //    return View(events);
        //}

        public ActionResult Index(int? id)
        {
            List<SportEvent> events;
            List<KindOfSport> kinds = _businessLayer.GetAllKindsOfSport();
            if (id == null)
            {
                events = _businessLayer.GetNearSportEvents();
            }
            else
            {
                events = _businessLayer.GetNearSportEventsByKindOfSport(id ?? default(int));
            }
            var ad = _businessLayer.GetAdvertisement();
            HomeViewModel viewModel = new HomeViewModel() { KindsOfSport = kinds, SportEvents = events, Advertisements=new List<string>() { ad} };
            return View(viewModel);
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

        //public ActionResult MakeBet(SportEvent sportEvent)
        //{
        //    return RedirectToAction("MakeBet", "Bet", new { sportEventId = sportEvent.Id });
        //}

        //[Authorize]
        //public ActionResult MyBets()
        //{
        //    var userId = User.Identity.GetUserId();
        //    ViewBag.money = _businessLayer.GetUsersMoney(userId);

        //    var rates = _businessLayer.GetUsersRates(userId);
        //    return View(rates);
        //}

        [HttpGet]
        [Authorize]
        public ActionResult TopUpBalance()
        {
            var userid = User.Identity.GetUserId();
            var money = _businessLayer.GetUsersMoney(userid);
            return View(money);
        }

        [HttpPost]
        [Authorize]
        public ActionResult TopUpBalance(int money)
        {
            var userid = User.Identity.GetUserId();
            _businessLayer.GiveUserMoney(userid, money);
            return RedirectToAction("Index");
        }        
    }
}