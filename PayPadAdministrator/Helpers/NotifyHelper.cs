using PayPadAdministrator.Classes;
using PayPadAdministrator.Models;
using PayPadAdministrator.Services;
using PayPadAdministrator.CustomAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace PayPadAdministrator.Helpers
{
    public class NotifyHelper
    {
        static ApiService apiService = new ApiService();

        public static async Task SaveLog(UserViewModel user, string description, string url)
        {
            try
            {
                var moduleId = GetModule(user.USER_ID, url);
                DashboardLog log = new DashboardLog
                {
                    DATE = DateTime.Now,
                    MODULE_ID = moduleId,
                    DESCRIPTION = description,
                    USER_ID = user.USER_ID
                };

                var response = await apiService.InsertPost(log, "InsertLogDashboard");
            }
            catch (Exception ex)
            {

            }
        }

        public static int GetModule(int userId, string url)
        {
            List<ModuleViewModel> modules = new List<ModuleViewModel>();            
            var response = apiService.GetDataRest(ComboHelper.Controller, string.Concat(Utilities.GetConfiguration("GetModuleForUser"), userId));
            if (response.CodeError == 200)
            {
                modules = JsonConvert.DeserializeObject<List<ModuleViewModel>>(response.Data.ToString());
            }

            int moduleId = 0;
            foreach (var module in modules)
            {
                foreach (var item in module.SubModules)
                {
                    if (item.URL.Contains(url))
                    {
                        moduleId = module.Module.MODULE_ID;
                    }
                }
            }

            return moduleId;
        }
    }
}