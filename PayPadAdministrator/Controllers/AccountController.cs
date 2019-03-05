using Newtonsoft.Json;
using PayPadAdministrator.Classes;
using PayPadAdministrator.CustomAuthentication;
using PayPadAdministrator.Helpers;
using PayPadAdministrator.Services;
using PayPlusModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PayPadAdministrator.Controllers
{
    public class AccountController : Controller
    {

        ApiService apiService = new ApiService();

        public AccountController()
        {
            ComboHelper.Controller = this;
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

            var cookie = CookiesHelper.CreateTokenCookie(request.Token);
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
                HttpCookie faCookie = new HttpCookie(Utilities.GetNameCookie(), enTicket);
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

            //TODO: para desarrollo quemado
            //usuario: branmous
            //contraseña: 123

            #region Metodo inicial
            //if (Membership.ValidateUser(model.UserName, model.Password))
            //{
            //    var user = (CustomMembershipUser)Membership.GetUser(model.UserName, false);
            //    if (user != null)
            //    {
            //        CustomSerializeModel userModel = new Models.CustomSerializeModel()
            //        {
            //            UserId = user.UserId,
            //            User_Name = user.User_Name,
            //            CustomerId = user.CustomerId,
            //            Email = user.Email,
            //            Name = user.Name,
            //            Roles = user.Roles.Select(r => r.DESCRIPTION).ToList()
            //        };

            //        string userData = JsonConvert.SerializeObject(userModel);
            //        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
            //            (
            //            1, model.UserName, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
            //            );

            //        string enTicket = FormsAuthentication.Encrypt(authTicket);
            //        HttpCookie faCookie = new HttpCookie(Utilities.GetNameCookie(), enTicket);
            //        Response.Cookies.Add(faCookie);
            //    }

            //    if (Url.IsLocalUrl(returnUrl))
            //    {
            //        return Redirect(returnUrl);
            //    }
            //    else
            //    {
            //        return RedirectToAction("Index", "Home");
            //    }
            //}

            //ModelState.AddModelError(string.Empty, "Something Wrong : Username or Password invalid ^_^ ");
            //return View(model);
            #endregion
        }

        public ActionResult ForgotPassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = apiService.ValidateUser(this, model.UserName);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, string.Concat("¡El usuario ", model.UserName, " no existe, Verifique nuevamente la información!"));
                return View(model);
            }


            string body = string.Format(EmailHelper.BodyCreateUser("BodyForgotPassword"), Utilities.GetConfiguration("UrlPageWeb"), user.USERNAME, user.PASSWORD);
            var email = new Email
            {
                Body = body,
                To = user.EMAIL,
                Subject = "Credenciales para el ingreso al Dashboard"
            };


            var response = await apiService.InsertPost(email, "SendEmail");
            if (response.CodeError != 200)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View(model);
            }

            return RedirectToAction("AfterForgotPassword");
        }

        public ActionResult AfterForgotPassword()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie(Utilities.GetNameCookie(), "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }

        [CustomAuthorize]
        public async Task<ActionResult> Locations()
        {
            List<Location> locations = new List<Location>();
            var request = new GetRequest
            {
                Parameter = string.Empty,
                Type = 1
            };
            var response = await apiService.InsertPost(request, "GetLocations");
            if (response.CodeError == 200)
            {
                locations = JsonConvert.DeserializeObject<List<Location>>(response.Data.ToString());
            }

            return View(locations);
        }

        [CustomAuthorize]
        public ActionResult ProfileUser()
        {
            var user = apiService.ValidateUser(this, User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            ViewBag.Title = string.Concat("Perfil de ", user.USERNAME);
            return View(user);
        }

        [ChildActionOnly]        
        public ActionResult GetModule()
        {
            List<ModuleViewModel> modules = new List<ModuleViewModel>();
            var userCurrent = apiService.ValidateUser(this, User.Identity.Name);
            if (userCurrent == null)
            {
                return PartialView(modules);
            }

            var response = apiService.GetDataRest(this, string.Concat(Utilities.GetConfiguration("GetModuleForUser"), userCurrent.USER_ID));
            if (response.CodeError == 200)
            {
                modules = JsonConvert.DeserializeObject<List<ModuleViewModel>>(response.Data.ToString());
            }

            return PartialView(modules);
        }

        [ChildActionOnly]
        public ActionResult GetPhotoProfile()
        {
            var userCurrent = apiService.ValidateUser(this, User.Identity.Name);
            return PartialView(userCurrent);
        }

        [ChildActionOnly]
        public ActionResult GetDataLogoCustomer()
        {
            var userCurrent = apiService.ValidateUser(this, User.Identity.Name);
            return PartialView(userCurrent);
        }

    }
}
