using Newtonsoft.Json;
using PayPadAdministrator.Classes;
using PayPadAdministrator.CustomAuthentication;
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
    [CustomAuthorize]
    public class AlarmsController : Controller
    {
        static ApiService apiService = new ApiService();

        public AlarmsController()
        {
            ComboHelper.Controller = this;
        }

        public async Task<ActionResult> GetAlarmForPaypad(int id)
        {
            var url = string.Concat(Utilities.GetConfiguration("GetAlarmForPaypad"), id);
            var response = await apiService.GetDataV2(this,url);
            if (response.CodeError != 200)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            if (response.Data == null)
            {
                return RedirectToAction("CreateAlarm", new { id = id });
            }

            var alarm = JsonConvert.DeserializeObject<Alarm>(response.Data.ToString());
            return View(alarm);
        }

        public ActionResult CreateAlarm(int id)
        {
            var alarm = new Alarm
            {
                PAYPAD_ID = id,
                STATE = true
            };

            return View(alarm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAlarm(Alarm alarm)
        {
            if (!ModelState.IsValid)
            {
                return View(alarm);
            }

            var response = await apiService.InsertPost(alarm, "CreateAlarmForPaypad");
            if (response.CodeError != 200)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View(alarm);
            }


            return RedirectToAction("Index", "PayPads");
        }

        public async Task<ActionResult> GetLogAlarm(int id)
        {
            var url = string.Concat(Utilities.GetConfiguration("GetLogAlarm"), id);
            var response = await apiService.GetDataV2(this,url);
            if (response.CodeError != 200)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            var logs = JsonConvert.DeserializeObject<List<LogAlarm>>(response.Data.ToString());
            ViewBag.ALarmId = id;
            return View(logs);
        }
    }
}