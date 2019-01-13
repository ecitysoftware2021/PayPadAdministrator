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
    public class UsersController : Controller
    {
        static ApiService apiService = new ApiService();
        // GET: Users
        public async Task<ActionResult> Index()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            var response = await apiService.GetData("GetAllUsers");
            if (response.CodeError == 200)
            {
                users = JsonConvert.DeserializeObject<List<UserViewModel>>(response.Data.ToString());
            }

            return View(users.OrderBy(u => u.ROLE_ID).ToList());
        }

        public async Task<ActionResult> SuperAdministrators()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            var response = await apiService.GetData("GetAllUsers");
            if (response.CodeError == 200)
            {
                users = JsonConvert.DeserializeObject<List<UserViewModel>>(response.Data.ToString());
            }

            return View(users.Where(u => u.ROLE_ID == 1).ToList());
        }

        public async Task<ActionResult> Administrators()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            var response = await apiService.GetData("GetAllUsers");
            if (response.CodeError == 200)
            {
                users = JsonConvert.DeserializeObject<List<UserViewModel>>(response.Data.ToString());
            }

            return View(users.Where(u => u.ROLE_ID == 2).ToList());
        }

        public async Task<ActionResult> Responsible()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            var userCurrent = apiService.ValidateUser(User.Identity.Name);
            var response = await apiService.GetData("GetAllUsers");
            if (response.CodeError == 200)
            {
                users = JsonConvert.DeserializeObject<List<UserViewModel>>(response.Data.ToString());
            }

            if (userCurrent.ROLE_ID == 1)
            {
                return View(users.Where(u => u.ROLE_ID == 3).ToList());
            }

            return View(users.Where(u => u.ROLE_ID == 3 && u.CUSTOMER_ID == userCurrent.CUSTOMER_ID).ToList());
        }

        public async Task<ActionResult> CreateUser()
        {
            ViewBag.ROLE_ID = new SelectList(await ComboHelper.GetRoles(), nameof(Role.ROLE_ID), nameof(Role.DESCRIPTION), 0);
            ViewBag.CUSTOMER_ID = new SelectList(await ComboHelper.GetAllCustomers(), nameof(Customer.CUSTOMER_ID), nameof(Customer.NAME), 0);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUser(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ROLE_ID = new SelectList(await ComboHelper.GetRoles(), nameof(Role.ROLE_ID), nameof(Role.DESCRIPTION), user.ROLE_ID);
                ViewBag.CUSTOMER_ID = new SelectList(await ComboHelper.GetAllCustomers(), nameof(Customer.CUSTOMER_ID), nameof(Customer.NAME), user.CUSTOMER_ID);
                return View(user);
            }

            if (user.ImagePathFile == null)
            {
                ViewBag.ROLE_ID = new SelectList(await ComboHelper.GetRoles(), nameof(Role.ROLE_ID), nameof(Role.DESCRIPTION), user.ROLE_ID);
                ViewBag.CUSTOMER_ID = new SelectList(await ComboHelper.GetAllCustomers(), nameof(Customer.CUSTOMER_ID), nameof(Customer.NAME), user.CUSTOMER_ID);
                ModelState.AddModelError(string.Empty, "Debe ingresar una foto");
                return View(user);
            }

            user.IMAGE = Utilities.GenerateByteArray(user.ImagePathFile.InputStream);
            user.ImagePathFile = null;
            user.PASSWORD = await GeneratePassword(user.CUSTOMER_ID);
            var response = await apiService.InsertPost(user, "CreateUser");
            if (response.CodeError != 200)
            {
                ViewBag.ROLE_ID = new SelectList(await ComboHelper.GetRoles(), nameof(Role.ROLE_ID), nameof(Role.DESCRIPTION), user.ROLE_ID);
                ViewBag.CUSTOMER_ID = new SelectList(await ComboHelper.GetAllCustomers(), nameof(Customer.CUSTOMER_ID), nameof(Customer.NAME), user.CUSTOMER_ID);
                ModelState.AddModelError(string.Empty, response.Message);
                return View(user);
            }

            return RedirectToAction("Index");
        }

        public ActionResult CreateUserResponsible()
        {
            var userCurrent = apiService.ValidateUser(User.Identity.Name);
            if (userCurrent == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var userViewModel = new UserViewModel
            {
                ROLE_ID = 3,
                CUSTOMER_ID = userCurrent.CUSTOMER_ID,
            };

            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUserResponsible(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            if (user.ImagePathFile == null)
            {
                ViewBag.ROLE_ID = new SelectList(await ComboHelper.GetRoles(), nameof(Role.ROLE_ID), nameof(Role.DESCRIPTION), user.ROLE_ID);
                ViewBag.CUSTOMER_ID = new SelectList(await ComboHelper.GetAllCustomers(), nameof(Customer.CUSTOMER_ID), nameof(Customer.NAME), user.CUSTOMER_ID);
                ModelState.AddModelError(string.Empty, "Debe ingresar una foto");
                return View(user);
            }

            user.IMAGE = Utilities.GenerateByteArray(user.ImagePathFile.InputStream);
            user.ImagePathFile = null;
            user.PASSWORD = await GeneratePassword(user.CUSTOMER_ID);
            var response = await apiService.InsertPost(user, "CreateUser");
            if (response.CodeError != 200)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View(user);
            }

            return RedirectToAction("Responsible");
        }

        private async Task<string> GeneratePassword(int customerId)
        {
            Random random = new Random();
            string character = Dictionaries.SpecialCharacters[random.Next(1, Dictionaries.SpecialCharacters.Count)];
            string password = string.Empty;
            var response = await apiService.GetDataV2(string.Concat(Utilities.GetConfiguration("GetNameCustomerForId"), customerId));
            if (response.CodeError == 200)
            {
                password = string.Concat(Utilities.RemoveAccentsWithNormalization(response.Data.ToString()),
                    DateTime.Now.Year, character);
            }
            else
            {
                password = string.Concat("Ecity", DateTime.Now.Year, character);
            }

            return password.Replace(" ", string.Empty);
        }
    }
}