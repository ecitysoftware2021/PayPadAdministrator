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
using PayPlusModels.Classes;

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

            var userCurrent = apiService.ValidateUser(this, User.Identity.Name);
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
            var response = await apiService.GetData(this, "GetAllPayPads");
            if (response.CodeError == 200 && !string.IsNullOrEmpty(response.Data.ToString()))
            {
                payPads = JsonConvert.DeserializeObject<List<PayPad>>(response.Data.ToString());
            }

            return payPads;
        }

        private async Task<List<PayPad>> GetAllsPaypadsForCustomer(int customerId)
        {
            List<PayPad> payPads = new List<PayPad>();
            var response = await apiService.GetDataV2(this, string.Concat(Utilities.GetConfiguration("GetAllPayPadsForCustomer"), customerId));
            if (response.CodeError == 200 && !string.IsNullOrEmpty(response.Data.ToString()))
            {
                payPads = JsonConvert.DeserializeObject<List<PayPad>>(response.Data.ToString());
            }

            return payPads;
        }

        private async Task<List<PayPad>> GetAllsPaypadsForSponsor(int customerId)
        {
            List<PayPad> payPads = new List<PayPad>();
            var response = await apiService.GetDataV2(this, string.Concat(Utilities.GetConfiguration("GetAllPayPadsForSponsor"), customerId));
            if (response.CodeError == 200 && !string.IsNullOrEmpty(response.Data.ToString()))
            {
                payPads = JsonConvert.DeserializeObject<List<PayPad>>(response.Data.ToString());
            }

            return payPads;
        }

        private async Task<List<PayPad>> GetAllsPaypadsForUser(int userId)
        {
            List<PayPad> payPads = new List<PayPad>();
            var response = await apiService.GetDataV2(this, string.Concat(Utilities.GetConfiguration("GetAllPayPadsForUserOffice"), userId));
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

            var usercurrent = apiService.ValidateUser(this, User.Identity.Name);
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
            var response = await apiService.GetDataV2(this, url);
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

            var usercurrent = apiService.ValidateUser(this, User.Identity.Name);
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

            var responsePaypad = await apiService.GetDataV2(this, string.Concat(Utilities.GetConfiguration("GetPayPadForId"), id));
            if (responsePaypad.CodeError == 200)
            {
                viewModel.PayPad = JsonConvert.DeserializeObject<PayPad>(responsePaypad.Data.ToString());
            }

            List<Device> devices = new List<Device>();
            var response = await apiService.GetDataV2(this, string.Concat(Utilities.GetConfiguration("GetDenominationsForCurrency"), id));
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
            var response = await apiService.GetDataV2(this, string.Concat(Utilities.GetConfiguration("GetTransactsForPaypad"), id));
            if (response.CodeError == 200)
            {
                transacts = JsonConvert.DeserializeObject<List<TransactPaypadViewModel>>(response.Data.ToString());
            }

            return PartialView(transacts);
        }

        /// <summary>
        /// Obtiene la información actual de los dispositivos dispensadores y habilita para editar los valores
        /// </summary>
        /// <param name="paypadId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetChargeData(int paypadId, int customerId, bool extraCharge)
        {
            var users = await ComboHelper.GetAllsUsersForCustomer(customerId);
            users = users.Where(u => u.USERNAME == User.Identity.Name).ToList();
            ViewBag.UserId = new SelectList(users, nameof(PayPlusModels.User.USER_ID), nameof(PayPlusModels.User.NAME), 0);
            ViewBag.PayPadId = paypadId;
            ViewBag.Extra = extraCharge;
            List<SP_GET_CHARGE_DATA_Result> viewModel = new List<SP_GET_CHARGE_DATA_Result>();

            var responsePaypad = await apiService.GetDataV2(this, string.Concat(Utilities.GetConfiguration("GetChargeData"), paypadId));
            if (responsePaypad.CodeError == 200)
            {
                viewModel = JsonConvert.DeserializeObject<List<SP_GET_CHARGE_DATA_Result>>(responsePaypad.Data.ToString());

            }

            return View(viewModel);
        }

        /// <summary>
        /// actualiza la información de los dispositivos dispensadores y registra un cargue por el valor ingresado
        /// </summary>
        /// <param name="payPad"></param>
        /// <returns></returns>
        public async Task<JsonResult> SetChargeData(UpdateData payPad)
        {
            EncryptionHelper encryptionHelper = new EncryptionHelper();
            foreach (var item in payPad.DataDenominations)
            {
                item.CURRENCY_DENOMINATION_ID = encryptionHelper.DecryptString(item.CURRENCY_DENOMINATION_ID);
                item.vALUE = encryptionHelper.DecryptString(item.vALUE);
                item.DEVICE_PAYPAD_DETAIL_ID = encryptionHelper.DecryptString(item.DEVICE_PAYPAD_DETAIL_ID);
                item.DEVICE_PAYPAD_ID = encryptionHelper.DecryptString(item.DEVICE_PAYPAD_ID);
            }
            var url = string.Concat(Utilities.GetConfiguration("GetChargeState"), payPad.pAYPAD_ID, "&operacion=", 2);
            var stateArching = await apiService.GetDataV2(this, url);
            Response response;
            if (stateArching.CodeError == 200 || payPad.ExtraCharge)
            {
                response = await apiService.InsertPost(payPad, "SetUpdateCharge");
            }
            else
            {
                response = stateArching;
            }



            return Json(response);
        }

        /// <summary>
        /// Obtiene la información actual de los dispositivos aceptadores
        /// </summary>
        /// <param name="paypadId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetArchingData(int paypadId, int customerId)
        {
            var users = await ComboHelper.GetAllsUsersForCustomer(customerId);
            users = users.Where(u => u.USERNAME == User.Identity.Name).ToList();
            ViewBag.UserId = new SelectList(users, nameof(PayPlusModels.User.USER_ID), nameof(PayPlusModels.User.NAME), 0);

            ViewBag.PayPadId = paypadId;
            List<SP_GET_ARCHING_DATA_Result> viewModel = new List<SP_GET_ARCHING_DATA_Result>();

            var responsePaypad = await apiService.GetDataV2(this, string.Concat(Utilities.GetConfiguration("GetArchingData"), paypadId));
            if (responsePaypad.CodeError == 200)
            {
                decimal totalAceptadores = 0;
                decimal totalDispensadores = 0;
                decimal granTotal = 0;
                viewModel = JsonConvert.DeserializeObject<List<SP_GET_ARCHING_DATA_Result>>(responsePaypad.Data.ToString());
                foreach (var item in viewModel)
                {
                    granTotal += (item.STACKER_QUANTITY * item.VALUE) + (item.CASHBOX_QUANTITY * item.VALUE);
                    if (item.NAME.Contains("Aceptador"))
                    {
                        totalAceptadores += (item.CASHBOX_QUANTITY * item.VALUE);
                    }
                    else
                    {
                        totalDispensadores += (item.STACKER_QUANTITY * item.VALUE);
                    }

                }
                ViewBag.TotalAceptadores = totalAceptadores.ToString("$ #,##0");
                ViewBag.TotalDispensadores = totalDispensadores.ToString("$ #,##0");
                ViewBag.GranTotal = granTotal.ToString("$ #,##0");
            }


            return View(viewModel);
        }

        /// <summary>
        /// Obtiene la información actual de los dispositivos aceptadores
        /// </summary>
        /// <param name="paypadId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetInvoiceData(int customerID)
        {
            ViewBag.CustomerID = customerID;
            SP_GET_INVOICE_DATA_Result viewModel = new SP_GET_INVOICE_DATA_Result();

            var responsePaypad = await apiService.GetDataV2(this, string.Concat(Utilities.GetConfiguration("GetInvoiceData"), customerID));
            if (responsePaypad.CodeError == 200)
            {
                viewModel = JsonConvert.DeserializeObject<SP_GET_INVOICE_DATA_Result>(responsePaypad.Data.ToString());
            }


            return View(viewModel);
        }

        /// <summary>
        /// actualiza la información para las facturas de los cinemas
        /// </summary>
        /// <param name="payPad"></param>
        /// <returns></returns>
        public async Task<JsonResult> UpdateInvoiceData(SP_GET_INVOICE_DATA_Result data)
        {

            var response = await apiService.InsertPost(data, "UpdateInvoiceDataByCustomer");

            return Json(response);
        }

        /// <summary>
        /// Pone en ceros los valores de los aceptadores y registra un arqueo con los valores actuales
        /// </summary>
        /// <param name="payPad"></param>
        /// <returns></returns>
        public async Task<JsonResult> SetArchingData(UpdateData payPad)
        {

            EncryptionHelper encryptionHelper = new EncryptionHelper();
            foreach (var item in payPad.DataDenominations)
            {
                item.CURRENCY_DENOMINATION_ID = encryptionHelper.DecryptString(item.CURRENCY_DENOMINATION_ID);
                item.vALUE = encryptionHelper.DecryptString(item.vALUE);
                item.DEVICE_PAYPAD_DETAIL_ID = encryptionHelper.DecryptString(item.DEVICE_PAYPAD_DETAIL_ID);
                item.DEVICE_PAYPAD_ID = encryptionHelper.DecryptString(item.DEVICE_PAYPAD_ID);
            }
            var url = string.Concat(Utilities.GetConfiguration("GetChargeState"), payPad.pAYPAD_ID, "&operacion=", 1);
            var stateArching = await apiService.GetDataV2(this, url);
            Response response;
            if (stateArching.CodeError == 200)
            {

                response = await apiService.InsertPost(payPad, "SetUpdateArching");
            }
            else
            {
                response = stateArching;
            }

            return Json(response);
        }

        /// <summary>
        /// Obtiene todos los arquos programados
        /// </summary>
        /// <param name="paypadId"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetBalances(int paypadId)
        {
            if (paypadId == null)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            List<BalancingPayplusResultViewModel> viewModel = new List<BalancingPayplusResultViewModel>();

            var responsePaypad = await apiService.GetDataV2(this, string.Concat(Utilities.GetConfiguration("GetPayPlusBalancing"), paypadId));
            if (responsePaypad.CodeError == 200)
            {
                viewModel = JsonConvert.DeserializeObject<List<BalancingPayplusResultViewModel>>(responsePaypad.Data.ToString());
            }

            return View(viewModel);
        }

        /// <summary>
        /// Obtiene el detalle de cada arqueo programado
        /// </summary>
        /// <param name="balancing_detail_id"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetBalanceDetail(int balancing_detail_id)
        {
            if (balancing_detail_id == null)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            List<BalancingDetailResultViewModel> viewModel = new List<BalancingDetailResultViewModel>();

            var responsePaypad = await apiService.GetDataV2(this, string.Concat(Utilities.GetConfiguration("GetPayPlusBalancingDetail"), balancing_detail_id));
            if (responsePaypad.CodeError == 200)
            {
                viewModel = JsonConvert.DeserializeObject<List<BalancingDetailResultViewModel>>(responsePaypad.Data.ToString());
            }

            return View(viewModel);
        }

        /// <summary>
        /// Obtiene todos los cargues programados
        /// </summary>
        /// <param name="paypadId"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetUploads(int paypadId)
        {
            if (paypadId == null)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            List<SP_GET_UPLOAD_PAYPAD_Result> viewModel = new List<SP_GET_UPLOAD_PAYPAD_Result>();

            var responsePaypad = await apiService.GetDataV2(this, string.Concat(Utilities.GetConfiguration("GetPayPlusUploads"), paypadId));
            if (responsePaypad.CodeError == 200)
            {
                viewModel = JsonConvert.DeserializeObject<List<SP_GET_UPLOAD_PAYPAD_Result>>(responsePaypad.Data.ToString());
            }

            return View(viewModel);
        }

        /// <summary>
        /// Obtiene todos el detalle de los cargues programados
        /// </summary>
        /// <param name="upload_detail_id"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetUploadsDetail(int upload_detail_id)
        {
            if (upload_detail_id == null)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            List<SP_GET_UPLOAD_DETAILS_Result> viewModel = new List<SP_GET_UPLOAD_DETAILS_Result>();

            var responsePaypad = await apiService.GetDataV2(this, string.Concat(Utilities.GetConfiguration("GetPayPlusUploadsDetail"), upload_detail_id));
            if (responsePaypad.CodeError == 200)
            {
                viewModel = JsonConvert.DeserializeObject<List<SP_GET_UPLOAD_DETAILS_Result>>(responsePaypad.Data.ToString());
            }

            return View(viewModel);
        }

        public async Task<ActionResult> AssingTransact(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            List<Transaction_Type> transaction_Types = new List<Transaction_Type>();
            var response = await apiService.GetDataV2(this, string.Concat(Utilities.GetConfiguration("GetAllsTransactForPayPad"), id));
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
            var response = await apiService.GetDataV2(this, string.Concat(Utilities.GetConfiguration("GetAllsDevicesForPayPad"), id));
            if (response.CodeError == 200)
            {
                devices = JsonConvert.DeserializeObject<List<Device>>(response.Data.ToString());
            }

            ViewBag.PayPadId = id;
            if (User.IsInRole("SuperAdmin"))
            {
                return View(devices.OrderByDescending(d => d.STATE).ToList());
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
                var response = await apiService.GetDataV2(this, url);
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
                var response = await apiService.GetDataV2(this, url);
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

            if (response.CodeError == 200)
            {
                try
                {
                    var usercurrent = apiService.ValidateUser(this, User.Identity.Name);
                    var url = Request.Url.AbsolutePath.Split('/')[1];
                    var message = string.Concat("se actualizó la denominación con la cantidad disponible en ", device.STACKER_QUANTITY,
                        ", la cantidad del baúl en ", device.CASHBOX_QUANTITY);
                    await NotifyHelper.SaveLog(usercurrent, message, url);
                }
                catch (Exception)
                {
                }
            }

            return Json(response);
        }
    }
}