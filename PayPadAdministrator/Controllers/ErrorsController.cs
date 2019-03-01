using PayPadAdministrator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PayPadAdministrator.Controllers
{
    public class ErrorsController : Controller
    {
        public ErrorsController()
        {
            ComboHelper.Controller = this;
        }
        // GET: Errors
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error500()
        {
            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        public ActionResult NotExistData(string message)
        {
            ViewBag.Title = message;
            return View();
        }
    }
}