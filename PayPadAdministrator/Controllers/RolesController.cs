using Newtonsoft.Json;
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
    public class RolesController : Controller
    {
        static ApiService apiService = new ApiService();
        // GET: Roles
        public async Task<ActionResult> Index()
        {
            List<Role> roles = new List<Role>();
            var request = new GetRequest
            {
                Parameter = string.Empty,
                Type = 1
            };

            var response = await apiService.InsertPost(request, "GetRoles");
            if (response.CodeError == 200)
            {
                roles = JsonConvert.DeserializeObject<List<Role>>(response.Data.ToString());
            }
            
            return View(roles);
        }

        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRole(Role role)
        {
            if (!ModelState.IsValid)
            {
                return View(role);
            }

            var response = await apiService.InsertPost(role, "CreateRole");
            if (response.CodeError !=200)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View(role);
            }

            var usercurrent = apiService.ValidateUser(User.Identity.Name);
            var url = Request.Url.AbsolutePath.Split('/')[1];
            await NotifyHelper.SaveLog(usercurrent, string.Concat("Se creó el rol ", role.DESCRIPTION), url);
            return RedirectToAction("Index");
        }
    }
}