using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business;
using Common;
using Microsoft.AspNet.Identity;

namespace MVCProjectTotalizator.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IBusinessLayer businessLayer) : base(businessLayer)
        {
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ShowUsers()
        {
            var users = _businessLayer.GetAllUsers();
            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditUser(string id)
        {
            var user = _businessLayer.GetUser(id);
            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditUser(User user)
        {
            _businessLayer.EditUser(user);
            return RedirectToAction("ShowUsers");
        }

        public void DeleteUser(string id)
        {
            _businessLayer.DeleteUser(id);
        }
    }
}