using Newtonsoft.Json;
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
using System.Web.Security;

namespace PayPadAdministrator.Controllers
{
    public class AccountController : Controller
    {

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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //var response = await apiService.Login(model.UserName, model.Password);
            //if (response == null)
            //{
            //    ModelState.AddModelError(string.Empty, "Something Wrong : Username or Password invalid ^_^ ");
            //    return View(model);
            //}

            //var user = new CustomMembershipUser(response);
            //if (user != null)
            //{
            //    CustomSerializeModel userModel = new Models.CustomSerializeModel()
            //    {
            //        UserId = user.UserId,
            //        User_Name = user.User_Name,
            //        CustomerId = user.CustomerId,
            //        Email = user.Email,
            //        Image = user.Image,
            //        Name = user.Name,
            //        Roles = user.Roles.Select(r => r.DESCRIPTION).ToList()
            //    };

            //    string userData = JsonConvert.SerializeObject(userModel);
            //    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, model.UserName,
            //                                            DateTime.Now, DateTime.Now.AddMinutes(15),
            //                                            false,
            //                                            userData);

            //    string enTicket = FormsAuthentication.Encrypt(authTicket);
            //    HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
            //    Response.Cookies.Add(faCookie);
            //    if (Url.IsLocalUrl(returnUrl))
            //    {
            //        return Redirect(returnUrl);
            //    }
            //    else
            //    {
            //        return RedirectToAction("Index", "Home");
            //    }
            //}

            if (Membership.ValidateUser(model.UserName, model.Password))
            {
                var user = (CustomMembershipUser)Membership.GetUser(model.UserName, false);
                if (user != null)
                {
                    CustomSerializeModel userModel = new Models.CustomSerializeModel()
                    {
                        UserId = user.UserId,
                        User_Name = user.User_Name,
                        CustomerId = user.CustomerId,
                        Email = user.Email,
                        Image = user.Image,
                        Name = user.Name,                        
                        Roles = user.Roles.Select(r => r.DESCRIPTION).ToList()
                    };

                    string userData = JsonConvert.SerializeObject(userModel);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                        (
                        1, model.UserName, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
                        );

                    string enTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
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
            }

            ModelState.AddModelError(string.Empty, "Something Wrong : Username or Password invalid ^_^ ");
            return View(model);

        }

        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("Cookie1", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }

        public ActionResult Locations()
        {
            return View();
        }
    }
}
