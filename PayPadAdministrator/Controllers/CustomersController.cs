using Newtonsoft.Json;
using PayPadAdministrator.CustomAuthentication;
using PayPadAdministrator.Models;
using PayPadAdministrator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PayPadAdministrator.Controllers
{
    [CustomAuthorize]
    public class CustomersController : Controller
    {
        ApiService apiService = new ApiService();
        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SponsorCustomers()
        {
            return View();
        }

        public async Task<ActionResult> TypeCustomers()
        {
            List<CustomerType> customerTypes = new List<CustomerType>();
            var request = new GetRequest
            {
                Parameter = "",
                Type = 1
            };

            var response = await apiService.InsertPost(request, "CustomerGetType");
            if (response.CodeError == 200)
            {
                customerTypes = JsonConvert.DeserializeObject<List<CustomerType>>(response.Data.ToString());
            }

            return View(customerTypes);
        }

        public ActionResult CreateTypeCustomers()
        {
            return View();
        }
    }
}