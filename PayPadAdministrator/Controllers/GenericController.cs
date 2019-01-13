﻿using PayPadAdministrator.Classes;
using PayPadAdministrator.Helpers;
using PayPadAdministrator.Models;
using PayPadAdministrator.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        
        public async Task<JsonResult> GetTransactionForTransact(RequestReport model)
        {
            model.StartDate = DateTime.ParseExact(model.DateStartString, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            model.FinishDate = DateTime.ParseExact(model.DateFinishString, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var response = await apiService.InsertPost(model, "GetTransactionForTransact");
            return Json(response);
        }

        public async Task<JsonResult> GetTransactionCM(RequestReport model)
        {
            model.StartDate = DateTime.ParseExact(model.DateStartString, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            model.FinishDate = DateTime.ParseExact(model.DateFinishString, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var response = await apiService.InsertPost(model, "GetTransactionCM");
            return Json(response);
        }
    }
}