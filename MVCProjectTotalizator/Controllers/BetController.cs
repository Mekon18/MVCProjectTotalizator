using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Business;
using MVCProjectTotalizator.Models;

namespace MVCProjectTotalizator.Controllers
{
    public class BetController : BaseController
    {

        public BetController(IBusinessLayer businessLayer) : base(businessLayer)
        {
        }

        [Authorize]
        public ActionResult MyBets()
        {
            var userId = User.Identity.GetUserId();
            ViewBag.money = _businessLayer.GetUsersMoney(userId);

            var rates = _businessLayer.GetUsersRates(userId);
            return View(rates);
        }

        // GET: Bet
        [Authorize]
        [HttpGet]
        public ActionResult MakeBet(int sportEventId)
        {
            User.Identity.GetUserId();
            ViewBag.Money = _businessLayer.GetUsersMoney(User.Identity.GetUserId());
            var Event = _businessLayer.GetSportEvent(sportEventId);
            BetsViewModel betsViewModel = new BetsViewModel() { Bets = new List<BetViewModel>() { new BetViewModel() }, SportEventId = sportEventId, SportEvent = Event };

            return View(betsViewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult MakeBet(BetsViewModel betsViewModel)
        {
            int money = 0;
            foreach (var bet in betsViewModel.Bets)
            {
                money += bet.Bet.Money;
            }

            var userId = User.Identity.GetUserId();
            _businessLayer.TakeUsersMoney(userId, money);

            foreach (var bet in betsViewModel.Bets.Where(b => b.Bet.ResultType == "Score"))
            {
                bet.Bet.ResultValue = bet.Score1.ToString() + ":" + bet.Score1.ToString();
            }

            var rate = new Rate()
            {
                DateTime = DateTime.Now,
                Event = new SportEvent() { Id = betsViewModel.SportEventId },
                UserId = userId
            };

            _businessLayer.SetBets(rate, betsViewModel.Bets.Select(x => x.Bet).ToList());

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditRate(int rateId, int eventId)
        {
            var bets = _businessLayer.GetBets(rateId);
            var betsmodel = new List<BetViewModel>();
            for (int i = 0; i < bets.Count; i++)
            {
                var model = new BetViewModel() { Bet = bets[i] };
                if(bets[i].ResultType == "Score")
                {
                    var score = bets[i].ResultValue.Split(':');
                    model.Score1 = int.Parse(score[0]);
                    model.Score2 = int.Parse(score[1]);
                }
                betsmodel.Add(model);
            }
            var sportEvent = _businessLayer.GetSportEvent(eventId);
            BetsViewModel viewModel = new BetsViewModel() { Bets = betsmodel, SportEvent = sportEvent, RateId = rateId };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditRate(BetsViewModel viewModel)
        {
            viewModel.Bets = viewModel.Bets.Where(b => b.Bet.ResultType != null).ToList();
            var oldBets = _businessLayer.GetBets(viewModel.RateId);
            var betsIdToDelete = oldBets.Select(b => b.Id).Except(viewModel.Bets.Select(b => b.Bet.Id));
            foreach (var betId in betsIdToDelete)
            {
                _businessLayer.DeleteBet(betId);
            }
            foreach (var bet in viewModel.Bets.Where(b => b.Bet.ResultType == "Score"))
            {
                bet.Bet.ResultValue = bet.Score1.ToString() + ":" + bet.Score1.ToString();
            }
            var betsToAdd = viewModel.Bets.Where(b => b.Bet.Id == 0).Select(b => b.Bet).ToList();
            _businessLayer.AddBets(betsToAdd, viewModel.RateId);

            _businessLayer.EditBets(viewModel.Bets.Where(b => b.Bet.Id != 0).Select(b => b.Bet).ToList());
            return RedirectToAction("MyBets");
        }

        [Authorize]
        public ActionResult DeleteRate(int id)
        {
            _businessLayer.DeleteRate(id);
            return RedirectToAction("MyBets");
        }
    }
}