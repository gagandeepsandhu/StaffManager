using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StaffManager.Models;

namespace StaffManager.Controllers
{
    public class HomeController : Controller
    {
        public static bool auth = false;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login()
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];

            if (username.Equals("root")
                && password.Equals("root"))
            {

                auth = true;

                Response.Redirect("/Home/Panel");
                return View();
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Panel()
        {
            if (auth)
            {
                return View();
            }
            else
            {
                return JavaScript("ACCESS DENIED DUE TO AUTHENTICATION FAILURE");
            }
        }

        private ActionResult JavaScript(string v)
        {
            throw new NotImplementedException();
        }
    }
}
