using Newtonsoft.Json;
using PayPadAdministrator.Classes;
using PayPadAdministrator.CustomAuthentication;
using PayPadAdministrator.Helpers;
using PayPlusModels;
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
    public class UsersController : Controller
    {
        static ApiService apiService = new ApiService();

        public UsersController()
        {
            ComboHelper.Controller = this;
        }
        // GET: Users
        public async Task<ActionResult> Index()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            var response = await apiService.GetData(this,"GetAllUsers");
            if (response.CodeError == 200)
            {
                users = JsonConvert.DeserializeObject<List<UserViewModel>>(response.Data.ToString());
            }

            return View(users.OrderBy(u => u.ROLE_ID).ToList());
        }

        public ActionResult UserDetails(string userName)
        {
            var user = apiService.ValidateUser(this, userName);
            if (user == null)
            {
                return RedirectToAction("Error500", "Errors");
            }

            return PartialView(user);
        }

        public async Task<ActionResult> SuperAdministrators()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            var response = await apiService.GetData(this,"GetAllUsers");
            if (response.CodeError == 200)
            {
                users = JsonConvert.DeserializeObject<List<UserViewModel>>(response.Data.ToString());
            }

            return View(users.Where(u => u.ROLE_ID == 1).ToList());
        }

        public async Task<ActionResult> Administrators()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            var response = await apiService.GetData(this,"GetAllUsers");
            if (response.CodeError == 200)
            {
                users = JsonConvert.DeserializeObject<List<UserViewModel>>(response.Data.ToString());
            }

            return View(users.Where(u => u.ROLE_ID == 2).ToList());
        }

        public async Task<ActionResult> Responsible()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            var userCurrent = apiService.ValidateUser(this, User.Identity.Name);
            var response = await apiService.GetData(this,"GetAllUsers");
            if (response.CodeError == 200)
            {
                users = JsonConvert.DeserializeObject<List<UserViewModel>>(response.Data.ToString());
            }

            if (User.IsInRole("SuperAdmin"))
            {
                return View(users.Where(u => u.ROLE_ID == 3).ToList());
            }

            return View(users.Where(u => u.ROLE_ID == 3 && u.CUSTOMER_ID == userCurrent.CUSTOMER_ID && u.STATE == true).ToList());
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

            var usercurrent = apiService.ValidateUser(this, User.Identity.Name);
            var url = Request.Url.AbsolutePath.Split('/')[1];
            await NotifyHelper.SaveLog(usercurrent, string.Concat("Se creó el usuario ", user.USERNAME), url);

            string body = string.Format(EmailHelper.BodyCreateUser("BodyCreateUser"), Utilities.GetConfiguration("UrlPageWeb"), user.USERNAME, user.PASSWORD);
            var email = new Email
            {
                Body = body,
                To = user.EMAIL,
                Subject = "Credenciales Dashboard E-City Software"
            };

            var responseEmail = await EmailHelper.SendEmail(email);

            return RedirectToAction("Index");
        }

        public ActionResult CreateUserResponsible()
        {
            var userCurrent = apiService.ValidateUser(this, User.Identity.Name);
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

            var usercurrent = apiService.ValidateUser(this, User.Identity.Name);
            var url = Request.Url.AbsolutePath.Split('/')[1];
            await NotifyHelper.SaveLog(usercurrent, string.Concat("Se creó el usuario ", user.USERNAME), url);
            return RedirectToAction("Responsible");
        }

        private async Task<string> GeneratePassword(int customerId)
        {
            Random random = new Random();
            string character = Dictionaries.SpecialCharacters[random.Next(1, Dictionaries.SpecialCharacters.Count)];
            string password = string.Empty;
            var response = await apiService.GetDataV2(this,string.Concat(Utilities.GetConfiguration("GetNameCustomerForId"), customerId));
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

        public async Task<ActionResult> EditUser(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data))
                {
                    return RedirectToAction("AccessDenied", "Errors");
                }

                EncryptionHelper encryptionHelper = new EncryptionHelper();
                var username = encryptionHelper.DecryptString(data);

                var user = apiService.ValidateUser(this, username);
                if (user == null)
                {
                    return RedirectToAction("AccessDenied", "Errors");
                }

                ViewBag.ROLE_ID = new SelectList(await ComboHelper.GetRoles(), nameof(Role.ROLE_ID), nameof(Role.DESCRIPTION), user.ROLE_ID);
                ViewBag.CUSTOMER_ID = new SelectList(await ComboHelper.GetAllCustomers(), nameof(Customer.CUSTOMER_ID), nameof(Customer.NAME), user.CUSTOMER_ID);
                return View(user);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Errors");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ROLE_ID = new SelectList(await ComboHelper.GetRoles(), nameof(Role.ROLE_ID), nameof(Role.DESCRIPTION), user.ROLE_ID);
                ViewBag.CUSTOMER_ID = new SelectList(await ComboHelper.GetAllCustomers(), nameof(Customer.CUSTOMER_ID), nameof(Customer.NAME), user.CUSTOMER_ID);
                return View(user);
            }

            if (user.ImagePathFile != null)
            {
                user.IMAGE = Utilities.GenerateByteArray(user.ImagePathFile.InputStream);
                user.ImagePathFile = null;
            }

            var response = await apiService.InsertPost(user, "UpdateUser");
            if (response.CodeError != 200)
            {
                ViewBag.ROLE_ID = new SelectList(await ComboHelper.GetRoles(), nameof(Role.ROLE_ID), nameof(Role.DESCRIPTION), user.ROLE_ID);
                ViewBag.CUSTOMER_ID = new SelectList(await ComboHelper.GetAllCustomers(), nameof(Customer.CUSTOMER_ID), nameof(Customer.NAME), user.CUSTOMER_ID);
                ModelState.AddModelError(string.Empty, response.Message);
                return View(user);
            }

            var usercurrent = apiService.ValidateUser(this, User.Identity.Name);
            var url = Request.Url.AbsolutePath.Split('/')[1];
            await NotifyHelper.SaveLog(usercurrent, string.Concat("Se actualizó el usuario ", user.USERNAME), url);
            if (User.IsInRole("SuperAdmin"))
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Responsible");
        }

        public async Task<ActionResult> EditUserResponsible(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data))
                {
                    return RedirectToAction("AccessDenied", "Errors");
                }

                EncryptionHelper encryptionHelper = new EncryptionHelper();
                var username = encryptionHelper.DecryptString(data);

                var user = apiService.ValidateUser(this, username);
                if (user == null)
                {
                    return RedirectToAction("AccessDenied", "Errors");
                }

                ViewBag.ROLE_ID = new SelectList(await ComboHelper.GetRoles(), nameof(Role.ROLE_ID), nameof(Role.DESCRIPTION), user.ROLE_ID);
                ViewBag.CUSTOMER_ID = new SelectList(await ComboHelper.GetAllCustomers(), nameof(Customer.CUSTOMER_ID), nameof(Customer.NAME), user.CUSTOMER_ID);
                return View(user);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error500", "Errors");
            }
        }


        public async Task<ActionResult> GetUserAlarms()
        {
            var userCurrent = apiService.ValidateUser(this, User.Identity.Name);
            if (userCurrent == null)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            var url = string.Concat(Utilities.GetConfiguration("GetUserAlarmForCustomer"), userCurrent.CUSTOMER_ID);
            var response = await apiService.GetDataV2(this,url);
            if (response.CodeError != 200)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            var users = JsonConvert.DeserializeObject<List<UserAlarm>>(response.Data.ToString());
            return View(users);
        }

        public async Task<ActionResult> CreateUserAlarms()
        {
            var userCurrent = apiService.ValidateUser(this, User.Identity.Name);
            if (userCurrent == null)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            var userAlarm = new UserAlarm
            {
                CUSTOMER_ID = userCurrent.CUSTOMER_ID,
            };

            ViewBag.ALARM_ID = new SelectList(await ComboHelper.GetAlarms(userCurrent.CUSTOMER_ID), nameof(Alarm.ALARM_ID), nameof(Alarm.USERNAME), 0);
            return View(userAlarm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUserAlarms(UserAlarm userAlarm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ALARM_ID = new SelectList(await ComboHelper.GetAlarms(userAlarm.CUSTOMER_ID.Value), nameof(Alarm.ALARM_ID), nameof(Alarm.USERNAME), userAlarm.ALARM_ID);
                return View(userAlarm);
            }

            var response = await apiService.InsertPost(userAlarm, "CreateUserAlarm");
            if (response.CodeError != 200)
            {
                ViewBag.ALARM_ID = new SelectList(await ComboHelper.GetAlarms(userAlarm.CUSTOMER_ID.Value), nameof(Alarm.ALARM_ID), nameof(Alarm.USERNAME), userAlarm.ALARM_ID);
                ModelState.AddModelError(string.Empty, response.Message);
                return View(userAlarm);
            }

            return RedirectToAction("GetUserAlarms");
        }
    }
}