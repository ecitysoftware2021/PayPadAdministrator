using Newtonsoft.Json;
using PayPadAdministrator.Classes;
using PayPadAdministrator.Helpers;
using PayPlusModels;
using PayPlusModels.CustomAuthentication;
using PayPadAdministrator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PayPadAdministrator.Controllers
{
    [CustomAuthorize]
    public class PayPadsController : Controller
    {
        static ApiService apiService = new ApiService();

        public PayPadsController()
        {
            ComboHelper.Controller = this;
        }
        // GET: PayPads
        public async Task<ActionResult> Index()
        {
            List<PayPad> payPads = new List<PayPad>();

            if (User.IsInRole("SuperAdmin"))
            {
                payPads = await GetAllsPaypads();
                return View(payPads);
            }

            var userCurrent = apiService.ValidateUser(this,User.Identity.Name);
            if (userCurrent == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (User.IsInRole("Admin"))
            {
                payPads = await GetAllsPaypadsForCustomer(userCurrent.CUSTOMER_ID);
                return View(payPads);
            }

            if (User.IsInRole("Director"))
            {
                payPads = await GetAllsPaypadsForSponsor(userCurrent.CUSTOMER_ID);
                return View(payPads);
            }

            payPads = await GetAllsPaypadsForUser(userCurrent.USER_ID);
            return View(payPads.Where(p => p.CUSTOMER_ID == userCurrent.CUSTOMER_ID).ToList());
        }

        private async Task<List<PayPad>> GetAllsPaypads()
        {
            List<PayPad> payPads = new List<PayPad>();
            var response = await apiService.GetData(this,"GetAllPayPads");
            if (response.CodeError == 200 && !string.IsNullOrEmpty(response.Data.ToString()))
            {
                payPads = JsonConvert.DeserializeObject<List<PayPad>>(response.Data.ToString());
            }

            return payPads;
        }

        private async Task<List<PayPad>> GetAllsPaypadsForCustomer(int customerId)
        {
            List<PayPad> payPads = new List<PayPad>();
            var response = await apiService.GetDataV2(this,string.Concat(Utilities.GetConfiguration("GetAllPayPadsForCustomer"), customerId));
            if (response.CodeError == 200 && !string.IsNullOrEmpty(response.Data.ToString()))
            {
                payPads = JsonConvert.DeserializeObject<List<PayPad>>(response.Data.ToString());
            }

            return payPads;
        }

        private async Task<List<PayPad>> GetAllsPaypadsForSponsor(int customerId)
        {
            List<PayPad> payPads = new List<PayPad>();
            var response = await apiService.GetDataV2(this,string.Concat(Utilities.GetConfiguration("GetAllPayPadsForSponsor"), customerId));
            if (response.CodeError == 200 && !string.IsNullOrEmpty(response.Data.ToString()))
            {
                payPads = JsonConvert.DeserializeObject<List<PayPad>>(response.Data.ToString());
            }

            return payPads;
        }

        private async Task<List<PayPad>> GetAllsPaypadsForUser(int userId)
        {
            List<PayPad> payPads = new List<PayPad>();
            var response = await apiService.GetDataV2(this,string.Concat(Utilities.GetConfiguration("GetAllPayPadsForUserOffice"), userId));
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

            var usercurrent = apiService.ValidateUser(this,User.Identity.Name);
            var url = Request.Url.AbsolutePath.Split('/')[1];
            await NotifyHelper.SaveLog(usercurrent, string.Concat("Se creó el Pay+ ", payPad.NAME), url);
            return RedirectToAction("Index", new { Message = "Se creó correctamente" });
        }

        public async Task<ActionResult> EditPayPad(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            PayPad payPad = new PayPad();
            var url = string.Concat(Utilities.GetConfiguration("GetPayPadForId"), id);
            var response = await apiService.GetDataV2(this,url);
            if (response.CodeError == 200)
            {
                payPad = JsonConvert.DeserializeObject<PayPad>(response.Data.ToString());
            }
            else
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            ViewBag.CUSTOMER_ID = new SelectList(await ComboHelper.GetCustomers(),
                                                    nameof(Customer.CUSTOMER_ID),
                                                    nameof(Customer.NAME), payPad.CUSTOMER_ID);

            ViewBag.OFFICE_ID = new SelectList(await ComboHelper.GetOffices(payPad.CUSTOMER_ID),
                                                    nameof(Office.OFFICE_ID),
                                                    nameof(Office.NAME), payPad.OFFICE_ID);

            ViewBag.CURRENCY_ID = new SelectList(await ComboHelper.GetCurrencies(),
                                                    nameof(Currency.CURRENCY_ID),
                                                    nameof(Currency.DESCRIPTION), payPad.CUSTOMER_ID);
            return View(payPad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPayPad(PayPad payPad)
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

            if (payPad.ImagePathFile != null)
            {
                payPad.IMAGE = Utilities.GenerateByteArray(payPad.ImagePathFile.InputStream);
                payPad.ImagePathFile = null;
            }


            var response = await apiService.InsertPost(payPad, "UpdatePayPad");
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

            var usercurrent = apiService.ValidateUser(this,User.Identity.Name);
            var url = Request.Url.AbsolutePath.Split('/')[1];
            await NotifyHelper.SaveLog(usercurrent, string.Concat("Se actualizó el Pay+ ", payPad.NAME), url);
            return RedirectToAction("Index", new { Message = "Se actualizó correctamente" });
        }

        public async Task<ActionResult> GetDeviceForPayPad(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            DevicesForPayPadViewModel viewModel = new DevicesForPayPadViewModel();

            var responsePaypad = await apiService.GetDataV2(this,string.Concat(Utilities.GetConfiguration("GetPayPadForId"), id));
            if (responsePaypad.CodeError == 200)
            {
                viewModel.PayPad = JsonConvert.DeserializeObject<PayPad>(responsePaypad.Data.ToString());
            }

            List<Device> devices = new List<Device>();
            var response = await apiService.GetDataV2(this,string.Concat(Utilities.GetConfiguration("GetDenominationsForCurrency"), id));
            if (response.CodeError == 200)
            {
                devices = JsonConvert.DeserializeObject<List<Device>>(response.Data.ToString());
            }

            viewModel.Devices = devices;
            return View(viewModel);
        }

        public async Task<ActionResult> GetTransactsPaypad(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            List<TransactPaypadViewModel> transacts = new List<TransactPaypadViewModel>();
            var response = await apiService.GetDataV2(this,string.Concat(Utilities.GetConfiguration("GetTransactsForPaypad"), id));
            if (response.CodeError == 200)
            {
                transacts = JsonConvert.DeserializeObject<List<TransactPaypadViewModel>>(response.Data.ToString());
            }

            return PartialView(transacts);
        }

        public async Task<ActionResult> AssingTransact(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            List<Transaction_Type> transaction_Types = new List<Transaction_Type>();
            var response = await apiService.GetDataV2(this,string.Concat(Utilities.GetConfiguration("GetAllsTransactForPayPad"), id));
            if (response.CodeError == 200)
            {
                transaction_Types = JsonConvert.DeserializeObject<List<Transaction_Type>>(response.Data.ToString());
            }

            ViewBag.PayPadId = id;
            return View(transaction_Types);
        }

        public async Task<ActionResult> AssingDevice(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            List<Device> devices = new List<Device>();
            var response = await apiService.GetDataV2(this,string.Concat(Utilities.GetConfiguration("GetAllsDevicesForPayPad"), id));
            if (response.CodeError == 200)
            {
                devices = JsonConvert.DeserializeObject<List<Device>>(response.Data.ToString());
            }

            ViewBag.PayPadId = id;
            if (User.IsInRole("SuperAdmin"))
            {
                return View(devices.OrderByDescending(d=>d.STATE).ToList());
            }

            return View(devices.Where(d => d.STATE == true).ToList());
        }

        public async Task<ActionResult> ViewDetailsDevice(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data))
                {
                    //TODO:Colocar la pagina no disponible
                    return RedirectToAction("AccessDenied", "Errors");
                }

                EncryptionHelper encryptionHelper = new EncryptionHelper();
                var text = encryptionHelper.DecryptString(data);
                string paypadId = text.Split(',')[0];
                string deviceId = text.Split(',')[1];
                DeviceDetailViewModel device = new DeviceDetailViewModel();
                var url = string.Concat(Utilities.GetConfiguration("GetDetailsDevicesForPayPad"), "payPad_Id=", paypadId, "&deviceId=", deviceId);
                var response = await apiService.GetDataV2(this,url);
                if (response.CodeError == 200)
                {
                    device = JsonConvert.DeserializeObject<DeviceDetailViewModel>(response.Data.ToString());
                }

                ViewBag.Title = device.Device.NAME;
                return View(device);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error500", "Errors");
            }
        }

        public async Task<ActionResult> AssingDetailsDevice(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data))
                {
                    return RedirectToAction("AccessDenied", "Errors");
                }

                EncryptionHelper encryptionHelper = new EncryptionHelper();
                var text = encryptionHelper.DecryptString(data);
                string paypadDeviceId = text.Split(',')[0];
                string payPadId = text.Split(',')[1];
                List<Currency_Denomination> currency_Denominations = new List<Currency_Denomination>();
                var url = string.Concat(Utilities.GetConfiguration("GetDenominationsForDevice"), "devicePaypad_Id=", paypadDeviceId, "&payPad_Id=", payPadId);
                var response = await apiService.GetDataV2(this,url);
                if (response.CodeError == 200)
                {
                    currency_Denominations = JsonConvert.DeserializeObject<List<Currency_Denomination>>(response.Data.ToString());
                }

                ViewBag.Title = "Asignar Denominaciones para " + text.Split(',')[2];
                ViewBag.DevicePayPadId = paypadDeviceId;
                return View(currency_Denominations);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Errors");
            }
        }

        public ActionResult EditQuantitiesDevicePayPad(Device_PayPad_Detail_ViewModel device)
        {
            return PartialView(device);
        }

        public async Task<JsonResult> EditQuantitiesDevicePayPadP(Device_PayPad_Detail_ViewModel device)
        {
            var response = await apiService.InsertPost(device, "UpdateDevicePayPadDetail");
            return Json(response);
        }
    }
}