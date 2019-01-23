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
    public class PayPadsController : Controller
    {
        static ApiService apiService = new ApiService();
        // GET: PayPads
        public async Task<ActionResult> Index()
        {
            List<PayPad> payPads = new List<PayPad>();

            if (User.IsInRole("SuperAdmin"))
            {
                payPads = await GetAllsPaypads();
                return View(payPads);
            }

            var userCurrent = apiService.ValidateUser(User.Identity.Name);
            if (User.IsInRole("Admin"))
            {
                payPads = await GetAllsPaypadsForCustomer(userCurrent.CUSTOMER_ID);
                return View(payPads);
            }

            payPads = await GetAllsPaypadsForUser(userCurrent.USER_ID);
            return View(payPads.Where(p => p.CUSTOMER_ID == userCurrent.CUSTOMER_ID).ToList());
        }

        private async Task<List<PayPad>> GetAllsPaypads()
        {
            List<PayPad> payPads = new List<PayPad>();
            var response = await apiService.GetData("GetAllPayPads");
            if (response.CodeError == 200 && !string.IsNullOrEmpty(response.Data.ToString()))
            {
                payPads = JsonConvert.DeserializeObject<List<PayPad>>(response.Data.ToString());
            }

            return payPads;
        }

        private async Task<List<PayPad>> GetAllsPaypadsForCustomer(int customerId)
        {
            List<PayPad> payPads = new List<PayPad>();
            var response = await apiService.GetDataV2(string.Concat(Utilities.GetConfiguration("GetAllPayPadsForCustomer"), customerId));
            if (response.CodeError == 200 && !string.IsNullOrEmpty(response.Data.ToString()))
            {
                payPads = JsonConvert.DeserializeObject<List<PayPad>>(response.Data.ToString());
            }

            return payPads;
        }


        private async Task<List<PayPad>> GetAllsPaypadsForUser(int userId)
        {
            List<PayPad> payPads = new List<PayPad>();
            var response = await apiService.GetDataV2(string.Concat(Utilities.GetConfiguration("GetAllPayPadsForUserOffice"), userId));
            if (response.CodeError == 200 && !string.IsNullOrEmpty(response.Data.ToString()))
            {
                payPads = JsonConvert.DeserializeObject<List<PayPad>>(response.Data.ToString());
            }

            return payPads;
        }

        public async Task<ActionResult> CreatePayPad()
        {
            ViewBag.CUSTOMER_ID = new SelectList(await ComboHelper.GetCustomers(),
                                                    nameof(Customer.CUSTOMER_ID),
                                                    nameof(Customer.NAME), 0);

            ViewBag.OFFICE_ID = new SelectList(await ComboHelper.GetOffices(0),
                                                    nameof(Office.OFFICE_ID),
                                                    nameof(Office.NAME), 0);

            ViewBag.CURRENCY_ID = new SelectList(await ComboHelper.GetCurrencies(),
                                                    nameof(Currency.CURRENCY_ID),
                                                    nameof(Currency.DESCRIPTION), 0);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePayPad(PayPad payPad)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CUSTOMER_ID = new SelectList(await ComboHelper.GetCustomers(),
                                                   nameof(Customer.CUSTOMER_ID),
                                                   nameof(Customer.NAME), payPad.CUSTOMER_ID);

                ViewBag.OFFICE_ID = new SelectList(await ComboHelper.GetOffices(payPad.CUSTOMER_ID),
                                                        nameof(Office.OFFICE_ID),
                                                        nameof(Office.NAME), payPad.OFFICE_ID);

                ViewBag.CURRENCY_ID = new SelectList(await ComboHelper.GetCurrencies(),
                                                        nameof(Currency.CURRENCY_ID),
                                                        nameof(Currency.DESCRIPTION), payPad.CURRENCY_ID);
                return View(payPad);
            }

            if (payPad.ImagePathFile == null)
            {
                ViewBag.CUSTOMER_ID = new SelectList(await ComboHelper.GetCustomers(),
                                   nameof(Customer.CUSTOMER_ID),
                                   nameof(Customer.NAME), payPad.CUSTOMER_ID);

                ViewBag.OFFICE_ID = new SelectList(await ComboHelper.GetOffices(payPad.CUSTOMER_ID),
                                                        nameof(Office.OFFICE_ID),
                                                        nameof(Office.NAME), payPad.OFFICE_ID);

                ViewBag.CURRENCY_ID = new SelectList(await ComboHelper.GetCurrencies(),
                                                        nameof(Currency.CURRENCY_ID),
                                                        nameof(Currency.DESCRIPTION), payPad.CURRENCY_ID);

                ModelState.AddModelError(string.Empty, "Debe imgresar la foto del Pay+");
                return View(payPad);
            }

            payPad.IMAGE = Utilities.GenerateByteArray(payPad.ImagePathFile.InputStream);
            payPad.ImagePathFile = null;
            var response = await apiService.InsertPost(payPad, "CreatePayPad");
            if (response.CodeError != 200)
            {
                ViewBag.CUSTOMER_ID = new SelectList(await ComboHelper.GetCustomers(),
                   nameof(Customer.CUSTOMER_ID),
                   nameof(Customer.NAME), payPad.CUSTOMER_ID);

                ViewBag.OFFICE_ID = new SelectList(await ComboHelper.GetOffices(payPad.CUSTOMER_ID),
                                                        nameof(Office.OFFICE_ID),
                                                        nameof(Office.NAME), payPad.OFFICE_ID);

                ViewBag.CURRENCY_ID = new SelectList(await ComboHelper.GetCurrencies(),
                                                        nameof(Currency.CURRENCY_ID),
                                                        nameof(Currency.DESCRIPTION), payPad.CURRENCY_ID);

                ModelState.AddModelError(string.Empty, response.Message);
                return View(payPad);
            }

            return RedirectToAction("Index", new { Message = "Se creó correctamente" });
        }

        public async Task<ActionResult> GetDeviceForPayPad(int id)
        {
            DevicesForPayPadViewModel viewModel = new DevicesForPayPadViewModel();

            var responsePaypad = await apiService.GetDataV2(string.Concat(Utilities.GetConfiguration("GetPayPadForId"), id));
            if (responsePaypad.CodeError == 200)
            {
                viewModel.PayPad = JsonConvert.DeserializeObject<PayPad>(responsePaypad.Data.ToString());
            }

            List<Device> devices = new List<Device>();
            var response = await apiService.GetDataV2(string.Concat(Utilities.GetConfiguration("GetDenominationsForCurrency"), id));
            if (response.CodeError == 200)
            {
                devices = JsonConvert.DeserializeObject<List<Device>>(response.Data.ToString());
            }

            viewModel.Devices = devices;
            return View(viewModel);
        }

        public async Task<ActionResult> GetTransactsPaypad(int id)
        {
            List<TransactPaypadViewModel> transacts = new List<TransactPaypadViewModel>();
            var response = await apiService.GetDataV2(string.Concat(Utilities.GetConfiguration("GetTransactsForPaypad"), id));
            if (response.CodeError == 200)
            {
                transacts = JsonConvert.DeserializeObject<List<TransactPaypadViewModel>>(response.Data.ToString());
            }

            return PartialView(transacts);
        }

        public async Task<ActionResult> AssingTransact(int id)
        {
            List<Transaction_Type> transaction_Types = new List<Transaction_Type>();
            var response = await apiService.GetDataV2(string.Concat(Utilities.GetConfiguration("GetAllsTransactForPayPad"), id));
            if (response.CodeError == 200)
            {
                transaction_Types = JsonConvert.DeserializeObject<List<Transaction_Type>>(response.Data.ToString());
            }

            ViewBag.PayPadId = id;
            return View(transaction_Types);
        }

        public async Task<ActionResult> AssingDevice(int id)
        {
            List<Device> devices = new List<Device>();
            var response = await apiService.GetDataV2(string.Concat(Utilities.GetConfiguration("GetAllsDevicesForPayPad"), id));
            if (response.CodeError == 200)
            {
                devices = JsonConvert.DeserializeObject<List<Device>>(response.Data.ToString());
            }

            ViewBag.PayPadId = id;
            if (User.IsInRole("SuperAdmin"))
            {
                return View(devices);
            }

            return View(devices.Where(d=>d.STATE == true).ToList());
        }

        public async Task<ActionResult> ViewDetailsDevice()
        {
            DeviceDetailViewModel device = new DeviceDetailViewModel();
            var url = string.Concat(Utilities.GetConfiguration("GetDetailsDevicesForPayPad"), "payPad_Id=",1, "&deviceId=",2);
            var response = await apiService.GetDataV2(url);
            if (response.CodeError == 200)
            {
                device = JsonConvert.DeserializeObject<DeviceDetailViewModel>(response.Data.ToString());
            }

            return View(device);
        }
    }
}