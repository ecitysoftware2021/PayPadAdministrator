using Newtonsoft.Json;
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
    public class TransactController : Controller
    {
        static ApiService apiService = new ApiService();
        // GET: Transact
        public async Task<ActionResult> Index()
        {
            List<Transaction_Type> transaction_Types = new List<Transaction_Type>();
            var response = await apiService.GetDataV2(this,string.Concat(Utilities.GetConfiguration("GetTransact"), 0));
            if (response.CodeError == 200)
            {
                transaction_Types = JsonConvert.DeserializeObject<List<Transaction_Type>>(response.Data.ToString());
            }

            return View(transaction_Types);
        }

        public ActionResult CreateTransact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTransact(Transaction_Type transaction_Type)
        {
            if (!ModelState.IsValid)
            {
                return View(transaction_Type);
            }

            var response = await apiService.InsertPost(transaction_Type, "CreateTransact");
            if (response.CodeError != 200)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View(transaction_Type);
            }

            var usercurrent = apiService.ValidateUser(this,User.Identity.Name);
            var url = Request.Url.AbsolutePath.Split('/')[1];
            await NotifyHelper.SaveLog(usercurrent, string.Concat("Se creó el trámite ", transaction_Type.DESCRIPTION), url);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> EditTransact(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            Transaction_Type transaction_Type = new Transaction_Type();
            var response = await apiService.GetDataV2(this,string.Concat(Utilities.GetConfiguration("GetTransact"), id));
            if (response.CodeError == 200)
            {
                var transaction_Types = JsonConvert.DeserializeObject<List<Transaction_Type>>(response.Data.ToString());
                transaction_Type = transaction_Types.FirstOrDefault();
            }

            return View(transaction_Type);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditTransact(Transaction_Type transaction_Type)
        {
            if (!ModelState.IsValid)
            {
                return View(transaction_Type);
            }

            var response = await apiService.InsertPost(transaction_Type, "UpdateTransact");
            if (response.CodeError != 200)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View(transaction_Type);
            }

            var usercurrent = apiService.ValidateUser(this,User.Identity.Name);
            var url = Request.Url.AbsolutePath.Split('/')[1];
            await NotifyHelper.SaveLog(usercurrent, string.Concat("Se editó el trámite ", transaction_Type.DESCRIPTION), url);
            return RedirectToAction("Index");
        }
    }
}