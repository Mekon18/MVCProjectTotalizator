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
    public class TeamController : BaseController
    {
        public TeamController(IBusinessLayer businessLayer) : base(businessLayer)
        {
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult ShowTeams()
        {
            var teams = _businessLayer.GetAllTeams();
            return View(teams);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult AddTeam()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult AddTeam(Team team)
        {
            _businessLayer.AddTeam(team);
            return RedirectToAction("ShowTeams");
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult EditTeam(int id)
        {
            var team = _businessLayer.GetTeam(id);
            return View(team);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult EditTeam(Team team)
        {
            _businessLayer.EditTeam(team);
            return RedirectToAction("ShowTeams");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public void DeleteTeam(int id)
        {
            _businessLayer.DeleteTeam(id);
        }
    }
}