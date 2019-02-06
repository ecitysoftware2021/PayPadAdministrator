using PayPadAdministrator.Classes;
using PayPadAdministrator.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PayPadAdministrator.Helpers
{
    public class EmailHelper
    {

        static ApiService apiService = new ApiService();

        public async static Task<Response> SendEmail(Email email)
        {
            var responseEmail = await apiService.InsertPost(email, "SendEmail");
            return responseEmail;
        }

        public static string BodyCreateUser(string fileName)
        {            
            string startupPath = Environment.CurrentDirectory;
            var fileContents = File.ReadAllText(HttpContext.Current.Server.MapPath(string.Concat("~/App_Data/", fileName,".txt")));
            return fileContents;
        }
    }
}