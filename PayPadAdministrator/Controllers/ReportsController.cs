﻿using Newtonsoft.Json;
using PayPadAdministrator.Classes;
using PayPadAdministrator.Helpers;
using PayPlusModels;
using PayPlusModels.CustomAuthentication;
using PayPadAdministrator.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using PayPlusModels.Classes;
using System.Linq;

namespace PayPadAdministrator.Controllers
{
    [CustomAuthorize]
    public class ReportsController : Controller
    {
        static ApiService apiService = new ApiService();
        public ReportsController()
        {
            ComboHelper.Controller = this;
        }
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetTransactionForTransact()
        {
            var userCurrent = apiService.ValidateUser(this, User.Identity.Name);
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
            var userCurrent = apiService.ValidateUser(this, User.Identity.Name);
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
            var response = await apiService.GetDataV2(this, url);
            if (response.CodeError != 200)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            var transactions = JsonConvert.DeserializeObject<TransactionViewModel>(response.Data.ToString());
            List<TransactionDetail> group = new List<TransactionDetail>();
            foreach (var item in transactions.TransactionDetails)
            {
                //var dato = group.Where(g => g.OPERATION == item.OPERATION && g.CURRENCY_VALUE == item.CURRENCY_VALUE);
                if (!group.Exists(gr => gr.OPERATION == item.OPERATION && gr.CURRENCY_VALUE == item.CURRENCY_VALUE))
                {
                    item.QUANTITY = 1;
                    group.Add(item);
                }
                else
                {
                    group.Find(gr => gr.OPERATION == item.OPERATION && gr.CURRENCY_VALUE == item.CURRENCY_VALUE).QUANTITY++;
                }
            }
            transactions.TransactionDetails = group;


            return PartialView(transactions);
        }

        public async Task<ActionResult> GetVideo(int transactionId)
        {
            List<VideoTransactionsViewModel> videos = new List<VideoTransactionsViewModel>();
            var url = string.Concat(Utilities.GetConfiguration("GetVideoForTransaction"), transactionId);
            var response = await apiService.GetDataV2(this, url);
            ViewBag.Title = "Video por transacción";
            if (response.CodeError != 200)
            {
                ViewBag.Title = response.Message;
            }
            else
            {
                videos = JsonConvert.DeserializeObject<List<VideoTransactionsViewModel>>(response.Data.ToString());
            }

            return PartialView(videos);
        }

        //public ActionResult PostReportPartial(ReportVM model)
        //{

        //    // Validate the Model is correct and contains valid data
        //    // Generate your report output based on the model parameters
        //    // This can be an Excel, PDF, Word file - whatever you need.

        //    // As an example lets assume we've generated an EPPlus ExcelPackage

        //    ExcelPackage workbook = new ExcelPackage();
        //    // Do something to populate your workbook

        //    // Generate a new unique identifier against which the file can be stored
        //    string handle = Guid.NewGuid().ToString();

        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        workbook.SaveAs(memoryStream);
        //        memoryStream.Position = 0;
        //        TempData[handle] = memoryStream.ToArray();
        //    }

        //    // Note we are returning a filename as well as the handle
        //    return new JsonResult()
        //    {
        //        Data = new { FileGuid = handle, FileName = "TestReportOutput.xlsx" }
        //    };

        //}
    }
}