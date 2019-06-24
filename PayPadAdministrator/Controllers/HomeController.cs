using PayPadAdministrator.Helpers;
using PayPlusModels.CustomAuthentication;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Threading.Tasks;
using PayPadAdministrator.Services;
using PayPlusModels;

namespace PayPadAdministrator.Controllers
{
    [CustomAuthorize]
    public class HomeController : Controller
    {
        ApiService apiService = new ApiService();

        public HomeController()
        {
            ComboHelper.Controller = this;
        }

        [CustomAuthorize]
        public async Task<ActionResult> Index()
        {
            var userCurrent = apiService.ValidateUser(this, User.Identity.Name);

            if (userCurrent == null)
            {
                return RedirectToAction("Login", "Account");
            }

            List<PayPad> payPads = new List<PayPad>();
            if (User.IsInRole("SuperAdmin"))
            {
                payPads = await ComboHelper.GetAllsPaypads();
            }
            else if (User.IsInRole("Admin"))
            {
                payPads = await ComboHelper.GetAllsPaypadsForCustomer(userCurrent.CUSTOMER_ID);
            }
            else if (User.IsInRole("Director"))
            {
                payPads = await ComboHelper.GetAllsPaypadsForSponsor(userCurrent.CUSTOMER_ID);
            }
            else
            {
                payPads = await ComboHelper.GetAllsPaypadsForUser(userCurrent.USER_ID);
            }

            ViewBag.PayPadId = new SelectList(payPads, nameof(PayPad.PAYPAD_ID), nameof(PayPad.NAME), 0);

            return View();
        }
    }
}