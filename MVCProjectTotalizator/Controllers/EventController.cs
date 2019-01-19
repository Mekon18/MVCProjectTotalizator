using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business;
using MVCProjectTotalizator.Models;
using Common;

namespace MVCProjectTotalizator.Controllers
{
    public class EventController : BaseController
    {
        public EventController(IBusinessLayer businessLayer) : base(businessLayer)
        {
        }

        #region for Admins and Moders
        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult ShowEvents()
        {
            var events = _businessLayer.GetAllSportEvents();
            return View(events);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult MakeEvent()
        {
            var kinds = _businessLayer.GetAllKindsOfSport();
            var teams = _businessLayer.GetAllTeams();
            EventViewModel viewModel = new EventViewModel() { Teams = teams, KindsOfSport = kinds };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult MakeEvent(EventViewModel viewModel)
        {
            if (viewModel.SportEvent.DateTime == new DateTime(1, 1, 1))
            {
                viewModel.SportEvent.DateTime = new DateTime(2000, 1, 1);
            }
            viewModel.SportEvent.Status = GetStatus(viewModel.SportEvent.DateTime);
            _businessLayer.AddSportEvent(viewModel.SportEvent);

            return RedirectToAction("ShowEvents");
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult EditEvent(int id)
        {
            var teams = _businessLayer.GetAllTeams();
            var sportEvent = _businessLayer.GetSportEvent(id);
            var kinds = _businessLayer.GetAllKindsOfSport();
            EventViewModel viewModel = new EventViewModel() { Teams = teams, SportEvent = sportEvent, KindsOfSport = kinds };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult EditEvent(EventViewModel viewModel)
        {
            if (viewModel.SportEvent.DateTime == new DateTime(1, 1, 1))
            {
                viewModel.SportEvent.DateTime = new DateTime(2000, 1, 1);
            }
            viewModel.SportEvent.Status = GetStatus(viewModel.SportEvent.DateTime);
            _businessLayer.EditEvent(viewModel.SportEvent);

            return RedirectToAction("ShowEvents");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public void DeleteEvent(int id)
        {
            _businessLayer.DeleteEvent(id);
        }
        #endregion

        #region for Guests

        [HttpGet]
        public ActionResult Search()
        {
            var kinds = _businessLayer.GetAllKindsOfSport();
            var viewModel = new SearchViewModel() { KindsOfSport = kinds };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Search(SearchViewModel viewModel)
        {
            var kinds = _businessLayer.GetAllKindsOfSport();
            viewModel.SportEvents = _businessLayer.SearchSportEvents(viewModel.Status, viewModel.Date, viewModel.KindId);
            viewModel.KindsOfSport = kinds;
            return View(viewModel);
        }
        #endregion


        public string GetStatus(DateTime date)
        {
            var result = "Passed";
            if (date - DateTime.Now < new TimeSpan(0, 0, 0))
            {
                result = "Going";
            }
            if (date - DateTime.Now < new TimeSpan(-2, 0, 0))
            {
                result = "Passed";
            }
            if (date - DateTime.Now > new TimeSpan(0, 0, 0))
            {
                result = "Coming";
            }
            return result;
        }
    }
}