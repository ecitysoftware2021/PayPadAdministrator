using Newtonsoft.Json;
using PaypadConsoleLog.Helpers;
using PaypadConsoleLog.Services;
using PayPlusModels;
using PayPlusModels.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PaypadConsoleLog.Controllers
{
    public class ConsoleController : Controller
    {
        static ApiService apiService = new ApiService();
        // GET: Console
        public async Task<ActionResult> Index(int id)
        {
            List<ConsoleErrorLog> consoleErrorLogs = new List<ConsoleErrorLog>();
            var url = string.Concat(Utilities.GetConfiguration("GetConsoleLogForPayPad"), id);
            var response = await apiService.GetDataV2(this, url);
            if (response.CodeError == 200)
            {
                consoleErrorLogs = JsonConvert.DeserializeObject<List<ConsoleErrorLog>>(response.Data.ToString());
            }

            return View(consoleErrorLogs.OrderByDescending(c => c.DATE).ToList());
        }

        public async Task<ActionResult> GetPaypadActionLogForPaypad(int id)
        {
            List<PaypadActionLog> paypadActionLogs = new List<PaypadActionLog>();
            var url = string.Concat(Utilities.GetConfiguration("GetPaypadActionLogForPaypad"), id);
            var response = await apiService.GetDataV2(this, url);
            if (response.CodeError == 200)
            {
                paypadActionLogs = JsonConvert.DeserializeObject<List<PaypadActionLog>>(response.Data.ToString());
            }

            return View(paypadActionLogs);
        }

        public async Task<ActionResult> GetActionsForError(int id, int paypadId, int? deviceId)
        {
            List<PayPlusModels.Action> actions = new List<PayPlusModels.Action>();
            var url = string.Concat(Utilities.GetConfiguration("GetActionsForPayPad"), id);
            var response = await apiService.GetDataV2(this, url);
            if (response.CodeError == 200)
            {
                actions = JsonConvert.DeserializeObject<List<PayPlusModels.Action>>(response.Data.ToString());
            }

            PaypadActionLog paypadActionLog = new PaypadActionLog
            {
                PAYPAD_ID = paypadId,
                DEVICE_PAYPAD_ID = deviceId
            };

            var jsonPaypadActionLog = JsonConvert.SerializeObject(paypadActionLog);
            var cookies = CookiesHelper.CreateTokenCookie(jsonPaypadActionLog, "JsonPaypadActionLog");
            Response.Cookies.Add(cookies);

            return PartialView(actions);
        }

        public ActionResult CreatePaypadActionLog(int id)
        {
            string jsonPaypadActionLog = string.Empty;
            if (Request.Cookies["JsonPaypadActionLog"] != null)
            {
                jsonPaypadActionLog = Request.Cookies["JsonPaypadActionLog"].Value;
            }
            else
            {
                //TODO: Enviar a pagina 404
            }

            var user = apiService.ValidateUser(this, User.Identity.Name);

            var paypad = JsonConvert.DeserializeObject<PaypadActionLog>(jsonPaypadActionLog);
            paypad.ACTION_ID = id;
            paypad.USER_ID = user.USER_ID;
            paypad.DATE_CREATION = DateTime.Now;
            paypad.DATE_ACTION = DateTime.Now;
            return View(paypad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePaypadActionLog(PaypadActionLog paypadActionLog)
        {
            if (!ModelState.IsValid)
            {
                return View(paypadActionLog);
            }

            var response = await apiService.InsertPost(paypadActionLog, "InsertPaypadConsoleLog", this);
            if (response.CodeError != 200)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View(paypadActionLog);
            }

            return RedirectToAction("GetPaypadActionLogForPaypad", new { id = paypadActionLog.PAYPAD_ID });
        }

    }
}