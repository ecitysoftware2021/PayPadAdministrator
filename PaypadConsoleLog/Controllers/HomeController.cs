using Newtonsoft.Json;
using PaypadConsoleLog.Services;
using PayPlusModels;
using PayPlusModels.Classes;
using PayPlusModels.CustomAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PaypadConsoleLog.Controllers
{
    [CustomAuthorize]
    public class HomeController : Controller
    {
        static ApiService apiService = new ApiService();

        public async  Task<ActionResult> Index()
        {
            List<PayPad> payPads = new List<PayPad>();

            if (User.IsInRole("SuperAdmin"))
            {
                payPads = await GetAllsPaypads();
                return View(payPads);
            }

            var userCurrent = apiService.ValidateUser(this, User.Identity.Name);
            if (userCurrent == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (User.IsInRole("Admin"))
            {
                payPads = await GetAllsPaypadsForCustomer(userCurrent.CUSTOMER_ID);
                return View(payPads);
            }

            if (User.IsInRole("Director"))
            {
                payPads = await GetAllsPaypadsForSponsor(userCurrent.CUSTOMER_ID);
                return View(payPads);
            }

            payPads = await GetAllsPaypadsForUser(userCurrent.USER_ID);
            return View(payPads.Where(p => p.CUSTOMER_ID == userCurrent.CUSTOMER_ID).ToList());            
        }

        private async Task<List<PayPad>> GetAllsPaypads()
        {
            List<PayPad> payPads = new List<PayPad>();
            var response = await apiService.GetData(this, "GetAllPayPads");
            if (response.CodeError == 200 && !string.IsNullOrEmpty(response.Data.ToString()))
            {
                payPads = JsonConvert.DeserializeObject<List<PayPad>>(response.Data.ToString());
            }

            return payPads;
        }

        private async Task<List<PayPad>> GetAllsPaypadsForCustomer(int customerId)
        {
            List<PayPad> payPads = new List<PayPad>();
            var response = await apiService.GetDataV2(this, string.Concat(Utilities.GetConfiguration("GetAllPayPadsForCustomer"), customerId));
            if (response.CodeError == 200 && !string.IsNullOrEmpty(response.Data.ToString()))
            {
                payPads = JsonConvert.DeserializeObject<List<PayPad>>(response.Data.ToString());
            }

            return payPads;
        }

        private async Task<List<PayPad>> GetAllsPaypadsForSponsor(int customerId)
        {
            List<PayPad> payPads = new List<PayPad>();
            var response = await apiService.GetDataV2(this, string.Concat(Utilities.GetConfiguration("GetAllPayPadsForSponsor"), customerId));
            if (response.CodeError == 200 && !string.IsNullOrEmpty(response.Data.ToString()))
            {
                payPads = JsonConvert.DeserializeObject<List<PayPad>>(response.Data.ToString());
            }

            return payPads;
        }

        private async Task<List<PayPad>> GetAllsPaypadsForUser(int userId)
        {
            List<PayPad> payPads = new List<PayPad>();
            var response = await apiService.GetDataV2(this, string.Concat(Utilities.GetConfiguration("GetAllPayPadsForUserOffice"), userId));
            if (response.CodeError == 200 && !string.IsNullOrEmpty(response.Data.ToString()))
            {
                payPads = JsonConvert.DeserializeObject<List<PayPad>>(response.Data.ToString());
            }

            return payPads;
        }
    }
}