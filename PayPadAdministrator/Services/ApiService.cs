using Newtonsoft.Json;
using PayPadAdministrator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PayPadAdministrator.Services
{
    public class ApiService
    {
        string userAPi = string.Empty;

        string passwordAPi = string.Empty;

        public ApiService()
        {
            userAPi = "dfg";
            passwordAPi = "34";
        }

        public async Task<UserSession> Login(string username, string password)
        {
            try
            {
                var reques = new RequestAuthentication
                {
                    Password = password,
                    Type = 2,
                    UserName = username
                };

                var json = JsonConvert.SerializeObject(reques);
                var content = new StringContent(json, Encoding.UTF8, "Application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("");
                var url = "Authentication/AuthenticateRepo";

                var authentication = Encoding.ASCII.GetBytes(userAPi + ":" + passwordAPi);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authentication));
                var response = await client.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();
                var requestresponse = JsonConvert.DeserializeObject<UserSession>(result);
                return requestresponse;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}