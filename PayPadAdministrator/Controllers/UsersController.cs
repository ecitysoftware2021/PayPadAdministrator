using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PayPadAdministrator.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SuperAdministrators()
        {
            return View();
        }

        public ActionResult Administrators()
        {
            return View();
        }

        public ActionResult Responsible()
        {
            return View();
        }
    }
}