using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business;
using MVCProjectTotalizator.Models;

namespace MVCProjectTotalizator.Controllers
{
    public class EventController : BaseController
    {
        public EventController(IBusinessLayer businessLayer) : base(businessLayer)
        {
        }
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
            var teams = _businessLayer.GetAllTeams();
            EventViewModel viewModel = new EventViewModel() { Teams = teams };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult MakeEvent(EventViewModel viewModel)
        {
            _businessLayer.AddSportEvent(viewModel.SportEvent);
            return RedirectToAction("ShowEvents");
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult EditEvent(int id)
        {
            var teams = _businessLayer.GetAllTeams();
            var sportEvent = _businessLayer.GetSportEvent(id);
            EventViewModel viewModel = new EventViewModel() { Teams = teams, SportEvent = sportEvent };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult EditEvent(EventViewModel viewModel)
        {
            _businessLayer.EditEvent(viewModel.SportEvent);
            return RedirectToAction("ShowEvents");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public void DeleteEvent(int id)
        {
            _businessLayer.DeleteEvent(id);
        }
    }
}