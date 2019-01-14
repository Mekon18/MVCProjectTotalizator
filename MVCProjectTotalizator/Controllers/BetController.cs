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
    public class BetController : BaseController
    {

        public BetController(IBusinessLayer businessLayer) : base(businessLayer)
        {
        }

        // GET: Bet
        [Authorize]
        [HttpGet]
        public ActionResult MakeBet(int sportEventId = 1)
        {
            User.Identity.GetUserId();
            ViewBag.Money = _businessLayer.GetUsersMoney(User.Identity.GetUserId());
            var Event = _businessLayer.GetSportEvent(sportEventId);
            Models.BetsViewModel betsViewModel = new Models.BetsViewModel() { Bets = new List<Bet>() { new Bet() }, SportEventId = sportEventId, SportEvent = Event };
            return View(betsViewModel);
        }
        [Authorize]
        [HttpPost]
        public ActionResult MakeBet(Models.BetsViewModel betsViewModel)
        {
            int money = 0;
            foreach (var bet in betsViewModel.Bets)
            {
                money += bet.Money;
            }
            var userId = User.Identity.GetUserId();
            _businessLayer.TakeUsersMoney(userId, money);

            var rate = new Rate()
            {
                DateTime = DateTime.Now,
                Event = new SportEvent() { Id = betsViewModel.SportEventId },
                UserId = userId
            };
            _businessLayer.SetBets(rate, betsViewModel.Bets);

            return RedirectToAction("Index", "Home");
        }
    }
}