using Newtonsoft.Json;
using PayPadAdministrator.Classes;
using PayPadAdministrator.Helpers;
using PayPlusModels;
using PayPadAdministrator.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PayPlusModels.Classes;
using PayPadAdministrator.Models;

namespace PayPadAdministrator.Controllers
{
    public class GenericController : Controller
    {
        static ApiService apiService = new ApiService();

        public GenericController()
        {
            ComboHelper.Controller = this;
        }
        // GET: Generic
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> AssingCustomerToSponsor(int sponsor, int client, bool state)
        {
            var request = new Sponsor
            {
                CUSTOMER_ID = client,
                SPONSOR_CUSTOMER_ID = sponsor,
                STATE = state
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

        public async Task<JsonResult> GetTransacts(int payPadId)
        {
            try
            {
                var transacts = await ComboHelper.GetTransact(payPadId);
                return Json(new Response
                {
                    CodeError = 200,
                    Data = transacts
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

        public async Task<JsonResult> UpdateModuleToUser(int moduleId, int userId, bool state)
        {
            var userModule = new UserModule
            {
                MODULE_ID = moduleId,
                USER_ID = userId,
                STATE = state
            };

            var response = await apiService.InsertPost(userModule, "AssingModuleToUser");
            return Json(response);
        }

        public async Task<JsonResult> GetBalance(int userId, int paypadId, DateTime date)
        {
            var balancingModule = new BalancingModel
            {
                PAYPAD_ID = paypadId,
                USER_ID = userId,
                DATE = date
            };

            var response = await apiService.InsertPost(balancingModule, "GetBalance");
            return Json(response);
        }

        public async Task<JsonResult> UpdateModuleToCustomer(int moduleId, int customerId, bool state)
        {
            var moduleCustomer = new ModuleCustomer
            {
                MODULE_ID = moduleId,
                CUSTOMER_ID = customerId,
                STATE = state
            };

            var response = await apiService.InsertPost(moduleCustomer, "AssingModuleToCustomer");
            return Json(response);
        }

        public async Task<JsonResult> AssingUserToOffice(int officeId, int userId, bool state)
        {
            var request = new UserOffice
            {
                OFFICE_ID = officeId,
                STATE = state,
                USER_ID = userId
            };

            var response = await apiService.InsertPost(request, "AssingUserToOffice");
            return Json(response);
        }

        public async Task<JsonResult> AssingTransactToPayPad(int transactId, int payPadId, bool state)
        {
            var payPadTransaction = new PayPadTransactionType
            {
                PAYPAD_ID = payPadId,
                TRANSACTION_TYPE_ID = transactId,
                STATE = state
            };

            var response = await apiService.InsertPost(payPadTransaction, "AssingTransactForPaypad");
            return Json(response);
        }

        public async Task<JsonResult> AssingDeviceForPayPad(int payPadId, int deviceId, bool state)
        {
            var request = new Device_PayPad
            {
                PAYPAD_ID = payPadId,
                DEVICE_ID = deviceId,
                CASHBOX_AMOUNT = 0,
                CASHBOX_QUANTITY = 0,
                MAX_CASHBOX_QUANTITY = 0,
                MAX_STACKER_QUANTITY = 0,
                STACKER_AMOUNT = 0,
                STACKER_QUANTITY = 0,
                STATE = state
            };

            var response = await apiService.InsertPost(request, "AssingDeviceForPayPad");
            return Json(response);
        }

        public async Task<JsonResult> GetTransactionHome(RequestReport model)
        {
            model.StartDate = DateTime.ParseExact(model.DateStartString, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            model.FinishDate = DateTime.ParseExact(model.DateFinishString, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            List<TransactionHomeViewModel> transactions = new List<TransactionHomeViewModel>();
            var response = await apiService.InsertPost(model, "GetTransactionHome");
            if (response.CodeError == 200)
            {
                transactions = JsonConvert.DeserializeObject<List<TransactionHomeViewModel>>(response.Data.ToString());
            }
            List<TransactionHomeViewModel> transactionsHome = new List<TransactionHomeViewModel>();
            foreach (var item in transactions)
            {
                if (transactionsHome.Where(t => t.TRANSACTION_ID == item.TRANSACTION_ID).Count() < 1)
                {
                    transactionsHome.Add(item);
                }
            }
            response.Data = transactionsHome.OrderByDescending(t => t.DATE_BEGIN);
            return Json(response);
        }

        public async Task<JsonResult> GetTransactionForTransact(RequestReport model)
        {
            model.StartDate = DateTime.ParseExact(model.DateStartString, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            model.FinishDate = DateTime.ParseExact(model.DateFinishString, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            List<TransactionForTransactViewModel> transactions = new List<TransactionForTransactViewModel>();
            var response = await apiService.InsertPost(model, "GetTransactionForTransact");
            if (response.CodeError == 200)
            {
                transactions = JsonConvert.DeserializeObject<List<TransactionForTransactViewModel>>(response.Data.ToString());
            }

            List<TransactionForTransactViewModel> transactionsR = new List<TransactionForTransactViewModel>();
            foreach (var item in transactions)
            {
                if (transactionsR.Where(t => t.TRANSACTION_ID == item.TRANSACTION_ID).Count() < 1)
                {
                    transactionsR.Add(item);
                }
            }
            response.Data = transactionsR.OrderByDescending(t => t.DATE_BEGIN);

            //response.Data = transactions.OrderByDescending(t => t.DATE_BEGIN).ToList();
            return Json(response);
        }

        //public async Task<JsonResult> GetTransactionCamara(RequestReport model)
        //{
        //    model.StartDate = DateTime.ParseExact(model.DateStartString, "MM/dd/yyyy", CultureInfo.InvariantCulture);
        //    model.FinishDate = DateTime.ParseExact(model.DateFinishString, "MM/dd/yyyy", CultureInfo.InvariantCulture);
        //    model.StateId = 2;
        //    List<ResponseData> transactions = new List<ResponseData>();
        //    var response = await apiService.InsertPost(model, "GetTransactionsCM");
        //    if (response.CodeError == 200)
        //    {
        //        transactions = JsonConvert.DeserializeObject<List<ResponseData>>(response.Data.ToString());
        //    }


        //    response.Data = transactions.OrderByDescending(t => t.Fecha);
            
        //    return Json(response);
        //}

        public async Task<JsonResult> GetTransactionCM(RequestReport model)
        {
            model.StartDate = DateTime.ParseExact(model.DateStartString, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            model.FinishDate = DateTime.ParseExact(model.DateFinishString, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            model.StateId = 2;
            var response = await apiService.InsertPost(model, "GetTransactionCM");
            return Json(response);
        }

        public async Task<JsonResult> GetDashboardLog(int userId)
        {
            var url = string.Concat(Utilities.GetConfiguration("GetLogDashboardForUser"), "?userId=", userId);
            var response = await apiService.GetDataV2(this, url);
            List<DasboardLogViewModel> dasboardLogs = new List<DasboardLogViewModel>();
            if (response.CodeError == 200)
            {
                dasboardLogs = JsonConvert.DeserializeObject<List<DasboardLogViewModel>>(response.Data.ToString());
            }

            response.Data = dasboardLogs.OrderByDescending(d => d.DATE).ToList();
            return Json(response);
        }

        public async Task<JsonResult> AssingDetailDeviceForPayPad(int Device_Paypad_ID, int denominationId, bool state)
        {
            var resquest = new Device_PayPad_Detail_ViewModel
            {
                DEVICE_PAYPAD_ID = Device_Paypad_ID,
                CURRENCY_DENOMINATION_ID = denominationId,
                STATE = state
            };

            var response = await apiService.InsertPost(resquest, "AssingDevicePayPadDetail");
            return Json(response);
        }

        public async Task<JsonResult> DeleteUser(string data)
        {
            var user = new UserViewModel
            {
                USERNAME = data,
                STATE = false
            };

            var response = await apiService.InsertPost(user, "DeleteUser");
            return Json(response);
        }
    }
}