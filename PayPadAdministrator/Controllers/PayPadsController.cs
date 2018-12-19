using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PayPadAdministrator.Controllers
{
    public class PayPadsController : Controller
    {
        // GET: PayPads
        public ActionResult Index()
        {
            return View();
        }
    }
}