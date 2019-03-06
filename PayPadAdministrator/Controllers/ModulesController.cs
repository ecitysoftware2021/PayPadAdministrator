using Newtonsoft.Json;
using PayPadAdministrator.Classes;
using PayPadAdministrator.Helpers;
using PayPlusModels;
using PayPlusModels.CustomAuthentication;
using PayPadAdministrator.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PayPadAdministrator.Controllers
{
    [CustomAuthorize]
    public class ModulesController : Controller
    {
        static ApiService apiService = new ApiService();

        public ModulesController()
        {
            ComboHelper.Controller = this;
        }
        // GET: Modules

        public async Task<ActionResult> Index()
        {
            List<ModuleViewModel> moduleViewModels = new List<ModuleViewModel>();
            var response = await apiService.GetData(this,"GetModules");
            if (response.CodeError == 200)
            {
                var modules = JsonConvert.DeserializeObject<List<Module>>(response.Data.ToString());
                foreach (var module in modules)
                {
                    ModuleViewModel moduleViewModel = new ModuleViewModel();
                    List<SubModule> subModules = new List<SubModule>();
                    var responseSM = await apiService.GetDataV2(this,string.Concat(Utilities.GetConfiguration("GetSubModules"), module.MODULE_ID));
                    if (responseSM.CodeError == 200)
                    {
                        subModules = JsonConvert.DeserializeObject<List<SubModule>>(responseSM.Data.ToString());
                    }

                    moduleViewModel.Module = module;
                    moduleViewModel.SubModules = subModules;
                    moduleViewModels.Add(moduleViewModel);
                }
            }

            return View(moduleViewModels);
        }

        public ActionResult CreateModule()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateModule(Module module)
        {
            if (!ModelState.IsValid)
            {
                return View(module);
            }

            var response = await apiService.InsertPost(module, "CreateModule");
            if (response.CodeError != 200)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View(module);
            }

            var usercurrent = apiService.ValidateUser(this,User.Identity.Name);
            var url = Request.Url.AbsolutePath.Split('/')[1];
            await NotifyHelper.SaveLog(usercurrent, string.Concat("Se creó el modulo ", module.DESCRIPTION), url);
            return RedirectToAction("Index");
        }

        public ActionResult CreateSubModule(int id)
        {
            var subModule = new SubModule
            {
                MODULE_ID = id
            };

            return PartialView(subModule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateSubModule(SubModule module)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(module);
            }

            var response = await apiService.InsertPost(module, "CreateSubModule");
            if (response.CodeError != 200)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return PartialView(module);
            }

            var usercurrent = apiService.ValidateUser(this,User.Identity.Name);
            var url = Request.Url.AbsolutePath.Split('/')[1];
            await NotifyHelper.SaveLog(usercurrent, string.Concat("Se creó el submodulo ", module.DESCRIPTION), url);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> AssingModuleToUser(int? userId)
        {
            if (userId == null)
            {
                return RedirectToAction("AccessDenied", "Errors");
            }

            List<ModuleViewModel> moduleViewModels = new List<ModuleViewModel>();
            if (User.IsInRole("SuperAdmin"))
            {
                moduleViewModels = await ModuleAllUsers(userId.Value);
            }
            else
            {
                var usercurrent = apiService.ValidateUser(this,User.Identity.Name);
                moduleViewModels = await ModuleUserResponsible(userId.Value, usercurrent.CUSTOMER_ID);
            }

            ViewBag.UserId = userId;
            return View(moduleViewModels);
        }

        private async Task<List<ModuleViewModel>> ModuleUserResponsible(int userId, int customerId)
        {
            List<ModuleViewModel> moduleViewModels = new List<ModuleViewModel>();
            var response = await apiService.GetDataV2(this,
                string.Concat(
                    Utilities.GetConfiguration("GetValidateModulesAssingForUserResponsible"),
                    "?userId=", userId,
                    "&customerId=", customerId));
            if (response.CodeError == 200)
            {
                moduleViewModels = JsonConvert.DeserializeObject<List<ModuleViewModel>>(response.Data.ToString());
            }

            return moduleViewModels;
        }

        private async Task<List<ModuleViewModel>> ModuleAllUsers(int userId)
        {
            List<ModuleViewModel> moduleViewModels = new List<ModuleViewModel>();
            var response = await apiService.GetDataV2(this,string.Concat(Utilities.GetConfiguration("GetValidateModulesAssingForUser"), userId));
            if (response.CodeError == 200)
            {
                moduleViewModels = JsonConvert.DeserializeObject<List<ModuleViewModel>>(response.Data.ToString());
            }

            return moduleViewModels;
        }

        public async Task<ActionResult> AssingModuleToCustomer(int id)
        {
            List<ModuleViewModel> moduleViewModels = new List<ModuleViewModel>();
            var response = await apiService.GetDataV2(this,string.Concat(Utilities.GetConfiguration("GetValidateModulesAssingForCustomer"), id));
            if (response.CodeError == 200)
            {
                moduleViewModels = JsonConvert.DeserializeObject<List<ModuleViewModel>>(response.Data.ToString());
            }

            ViewBag.CustomerId = id;
            return View(moduleViewModels);
        }
    }
}