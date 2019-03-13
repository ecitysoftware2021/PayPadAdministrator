using Newtonsoft.Json;
using PaypadConsoleLog.Helpers;
using PaypadConsoleLog.Services;
using PayPlusModels;
using PayPlusModels.CustomAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PaypadConsoleLog.Controllers
{
    public class AccountController : Controller
    {
        static ApiService apiService = new ApiService();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string ReturnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }

            ViewBag.ReturnUrl = ReturnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            //TODO: para produccion
            #region Metodo api
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var data = new RequestAuth
            {
                Password = model.Password,
                Type = 2,
                UserName = model.UserName
            };

            var request = await apiService.SecurityToken(data);
            if (request == null)
            {
                ModelState.AddModelError(string.Empty, "¡Usuario y contraseña incorrectas!");
                return View(model);
            }

            var cookie = CookiesHelper.CreateTokenCookie(request.Token,"TokenDashboard");
            Response.Cookies.Add(cookie);

            var response = await apiService.Login(model.UserName, model.Password, request.Token);
            if (response == null)
            {
                ModelState.AddModelError(string.Empty, "¡Usuario y contraseña incorrectas!");
                return View(model);
            }

            var user = new CustomMembershipUser(response);
            if (user != null)
            {
                CustomSerializeModel userModel = new CustomSerializeModel()
                {
                    UserId = user.UserId,
                    User_Name = user.User_Name,
                    CustomerId = user.CustomerId,
                    Email = user.Email,
                    Name = user.Name,
                    Roles = user.Roles.Select(r => r.DESCRIPTION).ToList()
                };

                string userData = JsonConvert.SerializeObject(userModel);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                    (
                    1, model.UserName, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
                    );

                string enTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(CookiesHelper.GetNameCookie(), enTicket);
                Response.Cookies.Add(faCookie);
            }

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            #endregion
        }

        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie(CookiesHelper.GetNameCookie(), "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }

        [CustomAuthorize]
        [ChildActionOnly]
        public ActionResult ProfileUser()
        {
            var user = apiService.ValidateUser(this, User.Identity.Name);

            return PartialView(user);
        }
    }
}