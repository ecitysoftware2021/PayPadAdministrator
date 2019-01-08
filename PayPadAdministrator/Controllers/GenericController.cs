using PayPadAdministrator.Classes;
using PayPadAdministrator.Helpers;
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
    public class GenericController : Controller
    {
        static ApiService apiService = new ApiService();
        // GET: Generic
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> AssingCustomerToSponsor(int sponsor, int client)
        {
            var request = new Sponsor
            {
                CUSTOMER_ID = client,
                SPONSOR_CUSTOMER_ID = sponsor
            };

            var response = await apiService.InsertPost(request, "AssingCustomerToSponsor");
            return Json(response);
        }

        public async Task<JsonResult> GetOffice(int customerId)
        {
            try
            {
                var offices = await ComboHelper.GetOffices(customerId);
                return Json(new Response
                {
                    CodeError = 200,
                    Data = offices
                });
            }
            catch (Exception ex)
            {
                return Json(new Response
                {
                    CodeError = 300,
                    Message = ex.Message
                });
            }
        }
    }
}