using PayPadAdministrator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PayPadAdministrator.CustomAuthentication;
using System.Threading.Tasks;
using PayPadAdministrator.Services;
using PayPadAdministrator.Models;

namespace PayPadAdministrator.Controllers
{
    public class HomeController : Controller
    {
        ApiService apiService = new ApiService();

        [CustomAuthorize]
        public async Task<ActionResult> Index()
        {
            var userCurrent = apiService.ValidateUser(User.Identity.Name);
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