using PayPadAdministrator.CustomAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PayPadAdministrator.Controllers
{
    [CustomAuthorize]
    public class ModulesController : Controller
    {
        // GET: Modules
        public ActionResult Index()
        {
            return View();
        }
    }
}