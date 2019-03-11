using Newtonsoft.Json;
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

            return View(consoleErrorLogs.OrderByDescending(c=>c.DATE).ToList());
        }
    }
}