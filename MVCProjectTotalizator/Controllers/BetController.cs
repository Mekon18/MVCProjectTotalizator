using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Business;

namespace MVCProjectTotalizator.Controllers
{
    public class BetController : Controller
    {
        private IBusinessLayer _businessLayer;

        public BetController(IBusinessLayer businessLayer)
        {
            _businessLayer = businessLayer;
        }

        // GET: Bet
        [Authorize]
        [HttpGet]
        public ActionResult MakeBet(SportEvent sportEvent)
        {
            var Event = _businessLayer.GetSportEvent(sportEvent.Id);

            return View(Event);
        }
        [Authorize]
        [HttpPost]
        public ActionResult MakeBet(int teamId, double money, int sportEventId)
        {
            var bet = new Bet() { Money = money, TeamId = teamId };
            var rate = new Rate()
            {
                BetsId = new List<int>(new[] { bet.Id }),
                DateTime = DateTime.Now,
                EventId = sportEventId,
                UserId = User.Identity.GetUserId()
            };
            return RedirectToAction("Index", "Home");
        }
    }
}