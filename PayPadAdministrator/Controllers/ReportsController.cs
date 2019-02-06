using Newtonsoft.Json;
using PayPadAdministrator.Classes;
using PayPadAdministrator.Helpers;
using PayPadAdministrator.Models;
using PayPadAdministrator.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PayPadAdministrator.Controllers
{
    public class ReportsController : Controller
    {
        static ApiService apiService = new ApiService();

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetTransactionForTransact()
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
            ViewBag.TransactId = new SelectList(await ComboHelper.GetTransact(0), nameof(TransactPaypadViewModel.TRANSACTION_TYPE_ID), nameof(TransactPaypadViewModel.DESCRIPTION), 0);

            return View();
        }

        public async Task<ActionResult> GetTransactionCM()
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
            else
            {
                payPads = await ComboHelper.GetAllsPaypadsForCustomer(userCurrent.USER_ID);
            }

            ViewBag.PayPadId = new SelectList(payPads, nameof(PayPad.PAYPAD_ID), nameof(PayPad.NAME), 0);            
            return View();
        }

        public async Task<ActionResult> GetDetailsFromTransactions(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            var url = string.Concat(Utilities.GetConfiguration("GetTransactionDescriptionsForId"), id);
            var response = await apiService.GetDataV2(url);
            if (response.CodeError != 200)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            var transactions = JsonConvert.DeserializeObject<TransactionViewModel>(response.Data.ToString());
            return PartialView(transactions);
        }
    }
}