using PayPadAdministrator.Helpers;
using PayPadAdministrator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PayPadAdministrator.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult Login()
        {
            if (Session["User"] != null)
            {
                Session["User"] = null;
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserSession userSession = new UserSession
            {
                Customer_ID = 1,
                Email = "brandon-377@hotmail.com",
                Identification = "1036660391",
                Phone = "3126104754",
                Role_ID = 1,
                StateId = 1,
                UserName = model.UserName,
                User_ID = 1
            };

            Session["User"] = SessionHelper.SaveUser(userSession);
            return RedirectToAction("index", "Home");
        }
    }
}
