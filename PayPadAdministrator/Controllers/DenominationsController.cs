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
    public class DenominationsController : Controller
    {
        static ApiService apiService = new ApiService();

        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> Currencies()
        {
            List<Currency> currencies = new List<Currency>();
            var response = await apiService.GetData("GetCurrencies");
            if (response.CodeError == 200)
            {
                currencies = JsonConvert.DeserializeObject<List<Currency>>(response.Data.ToString());
            }

            return View(currencies);
        }

        public ActionResult CreateCurrency()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCurrency(Currency currency)
        {
            if (!ModelState.IsValid)
            {
                return View(currency);
            }

            var response = await apiService.InsertPost(currency, "CreateCurrency");
            if (response.CodeError != 200)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View(currency);
            }

            var usercurrent = apiService.ValidateUser(User.Identity.Name);
            var url = Request.Url.AbsolutePath.Split('/')[1];
            await NotifyHelper.SaveLog(usercurrent, string.Concat("Se creó la moneda ", currency.DESCRIPTION), url);
            return RedirectToAction("Currencies");
        }

        public async Task<ActionResult> ShowDenominationForCurrency(int id)
        {
            CurrencyDenominationViewModel currency = new CurrencyDenominationViewModel();
            var response = await apiService.GetDataV2(string.Concat(Utilities.GetConfiguration("GetDenominationsForCurrency"), id));
            if (response.CodeError == 200)
            {
                currency = JsonConvert.DeserializeObject<CurrencyDenominationViewModel>(response.Data.ToString());
            }

            return View(currency);
        }

        public ActionResult CreateDenominationForCurrency(int id)
        {
            var Currency = new Currency_Denomination
            {
                CURRENCY_ID = id
            };

            return View(Currency);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateDenominationForCurrency(Currency_Denomination currency_Denomination)
        {
            if (!ModelState.IsValid)
            {
                return View(currency_Denomination);
            }

            if (currency_Denomination.ImagePathFile == null)
            {
                ModelState.AddModelError(string.Empty, "Debe ingresar el logo");
                return View(currency_Denomination);
            }

            currency_Denomination.IMAGE = Utilities.GenerateByteArray(currency_Denomination.ImagePathFile.InputStream);
            currency_Denomination.ImagePathFile = null;
            var response = await apiService.InsertPost(currency_Denomination, "CreateDenominationForCurrency");
            if (response.CodeError != 200)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View(currency_Denomination);
            }

            var usercurrent = apiService.ValidateUser(User.Identity.Name);
            var url = Request.Url.AbsolutePath.Split('/')[1];
            await NotifyHelper.SaveLog(usercurrent, string.Concat("Se creó la denominación ", currency_Denomination.VALUE), url);
            return RedirectToAction("ShowDenominationForCurrency", new { id = currency_Denomination.CURRENCY_ID });
        }


        public async Task<ActionResult> EditDenominationForCurrency(int id)
        {
            Currency_Denomination currency_Denomination = new Currency_Denomination();
            var url = string.Concat(Utilities.GetConfiguration("GetCurrency_DenominationForId"), id);
            var response = await apiService.GetDataV2(url);
            if (response.CodeError == 200)
            {
                currency_Denomination = JsonConvert.DeserializeObject<Currency_Denomination>(response.Data.ToString());
            }

            return View(currency_Denomination);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditDenominationForCurrency(Currency_Denomination currency_Denomination)
        {
            if (!ModelState.IsValid)
            {
                return View(currency_Denomination);
            }

            if (currency_Denomination.ImagePathFile == null)
            {
                ModelState.AddModelError(string.Empty, "Debe ingresar el logo");
                return View(currency_Denomination);
            }

            currency_Denomination.IMAGE = Utilities.GenerateByteArray(currency_Denomination.ImagePathFile.InputStream);
            currency_Denomination.ImagePathFile = null;
            var response = await apiService.InsertPost(currency_Denomination, "UpdateDenominationForCurrency");
            if (response.CodeError != 200)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View(currency_Denomination);
            }

            var usercurrent = apiService.ValidateUser(User.Identity.Name);
            var url = Request.Url.AbsolutePath.Split('/')[1];
            await NotifyHelper.SaveLog(usercurrent, string.Concat("Se editó la denominación ", currency_Denomination.VALUE), url);
            return RedirectToAction("ShowDenominationForCurrency", new { id = currency_Denomination.CURRENCY_ID });
        }
    }
}