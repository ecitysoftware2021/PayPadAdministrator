using Newtonsoft.Json;
using PayPadAdministrator.Classes;
using PayPadAdministrator.Helpers;
using PayPlusModels;
using PayPlusModels.CustomAuthentication;
using PayPadAdministrator.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PayPadAdministrator.Controllers
{
    [CustomAuthorize]
    public class NotificationsController : Controller
    {
        static ApiService apiService = new ApiService();

        public NotificationsController()
        {
            ComboHelper.Controller = this;
        }

        // GET: Notifications
        public async Task<ActionResult> Index()
        {
            List<Notifications> notifications = new List<Notifications>();
            var userCurrent = apiService.ValidateUser(this,User.Identity.Name);
            if (userCurrent == null)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            var url = string.Concat(Utilities.GetConfiguration("GetNotficationsForCustomer"), userCurrent.CUSTOMER_ID);
            var response = await apiService.GetDataV2(this,url);
            if (response.CodeError == 200)
            {
                notifications = JsonConvert.DeserializeObject<List<Notifications>>(response.Data.ToString());
            }

            return View(notifications);
        }

        [ChildActionOnly]
        public ActionResult GetTenNotificstions()
        {
            List<Notifications> notifications = new List<Notifications>();
            var userCurrent = apiService.ValidateUser(this,User.Identity.Name);
            if (userCurrent == null)
            {
                return PartialView(notifications);
            }

            var url = string.Concat(Utilities.GetConfiguration("GetTenNotficationsForCustomer"), userCurrent.CUSTOMER_ID);
            var response = apiService.GetDataRest(this, url);
            if (response.CodeError == 200)
            {
                notifications = JsonConvert.DeserializeObject<List<Notifications>>(response.Data.ToString());
            }

            return PartialView(notifications);
        }
    }
}