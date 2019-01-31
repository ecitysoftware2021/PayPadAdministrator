﻿using Newtonsoft.Json;
using PayPadAdministrator.Classes;
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
    public class NotificationsController : Controller
    {
        static ApiService apiService = new ApiService();

        // GET: Notifications
        public async Task<ActionResult> Index()
        {
            List<Notifications> notifications = new List<Notifications>();
            var userCurrent = apiService.ValidateUser(User.Identity.Name);
            if (userCurrent == null)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            var url = string.Concat(Utilities.GetConfiguration("GetNotficationsForCustomer"), userCurrent.CUSTOMER_ID);
            var response = await apiService.GetDataV2(url);
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
            var userCurrent = apiService.ValidateUser(User.Identity.Name);
            if (userCurrent == null)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            var url = string.Concat(Utilities.GetConfiguration("GetTenNotficationsForCustomer"), userCurrent.CUSTOMER_ID);
            var response = apiService.GetDataRest(url);
            if (response.CodeError == 200)
            {
                notifications = JsonConvert.DeserializeObject<List<Notifications>>(response.Data.ToString());
            }

            return PartialView(notifications);
        }
    }
}