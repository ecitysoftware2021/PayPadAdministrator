using Newtonsoft.Json;
using PayPadAdministrator.Classes;
using PayPadAdministrator.Helpers;
using PayPlusModels;
using PayPlusModels.Classes;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PayPadAdministrator.Services
{
    public class ApiService
    {

        string urlApi = string.Empty;

        string userAPi = string.Empty;

        string passwordAPi = string.Empty;


        public ApiService()
        {
            urlApi = Utilities.GetConfiguration("UrlApi");
            ReadKeys();
        }

        public async Task<UserSession> Login(string username, string password, string token)
        {
            try
            {
                var reques = new RequestAuthentication
                {
                    Password = password,
                    UserName = username
                };

                ServicePointManager.Expect100Continue = false;
                var json = JsonConvert.SerializeObject(reques);
                var content = new StringContent(json, Encoding.UTF8, "Application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlApi);
                var url = Utilities.GetConfiguration("LoginDash");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();
                var responseApi = JsonConvert.DeserializeObject<Response>(result);
                if (responseApi.CodeError != 200)
                {
                    return null;
                }

                var user = JsonConvert.DeserializeObject<UserViewModel>(responseApi.Data.ToString());
                var userSession = new UserSession
                {
                    CUSTOMER_ID = user.CUSTOMER_ID,
                    EMAIL = user.EMAIL,
                    IDENTIFICATION = user.IDENTIFICATION,
                    IMAGE = user.IMAGE,
                    NAME = user.NAME,
                    PASSWORD = user.PASSWORD,
                    PHONE = user.PHONE,
                    STATE = user.STATE,
                    USERNAME = user.USERNAME,
                    USER_ID = user.USER_ID,
                    Roles = new List<Role>()
                    {
                        new Role
                        {
                            DESCRIPTION = user.ROL_NAME,
                            ROLE_ID = user.ROLE_ID
                        }
                    },

                };

                return userSession;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Response> InsertPost<T>(T model, string controller)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "Application/json");
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(urlApi);
                var url = Utilities.GetConfiguration(controller);
                string token = string.Empty;
                if (ComboHelper.Controller.Request.Cookies["TokenDashboard"] != null)
                {
                    token = ComboHelper.Controller.Request.Cookies["TokenDashboard"].Value;
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        CodeError = 100,
                        Message = response.ReasonPhrase,
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var responseApi = JsonConvert.DeserializeObject<Response>(result);
                return responseApi;
            }
            catch (Exception ex)
            {
                return new Response
                {
                    CodeError = 300,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetData(Controller controllerP, string controller)
        {
            try
            {
                ServicePointManager.Expect100Continue = false;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(urlApi);
                var url = Utilities.GetConfiguration(controller);
                string token = string.Empty;
                if (ComboHelper.Controller.Request.Cookies["TokenDashboard"] != null)
                {
                    token = ComboHelper.Controller.Request.Cookies["TokenDashboard"].Value;
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        CodeError = 100,
                        Message = response.ReasonPhrase,
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var responseApi = JsonConvert.DeserializeObject<Response>(result);
                return responseApi;
            }
            catch (Exception ex)
            {
                return new Response
                {
                    CodeError = 300,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetDataV2(Controller controller, string url)
        {
            try
            {
                ServicePointManager.Expect100Continue = false;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(urlApi);
                string token = string.Empty;
                if (controller.Request.Cookies["TokenDashboard"] != null)
                {
                    token = controller.Request.Cookies["TokenDashboard"].Value;
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        CodeError = 100,
                        Message = response.ReasonPhrase,
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var responseApi = JsonConvert.DeserializeObject<Response>(result);
                return responseApi;
            }
            catch (Exception ex)
            {
                return new Response
                {
                    CodeError = 300,
                    Message = ex.Message,
                };
            }
        }


        public UserViewModel ValidateUser(Controller controller, string name)
        {
            try
            {
                
                var url = string.Concat(urlApi,Utilities.GetConfiguration("GetUserForUserName") , name);
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                string token = string.Empty;
                if (controller.Request.Cookies["TokenDashboard"] != null)
                {
                    token = controller.Request.Cookies["TokenDashboard"].Value;
                }

                request.AddHeader("Authorization", string.Format("Bearer {0}", token));
                IRestResponse iResponse = client.Execute(request);
                var responseApi = JsonConvert.DeserializeObject<Response>(iResponse.Content.ToString());
                if (responseApi.CodeError != 200)
                {
                    return null;
                }

                var userCurrent = JsonConvert.DeserializeObject<UserViewModel>(responseApi.Data.ToString());
                return userCurrent;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Response GetDataRest(Controller controllerP, string controller)
        {
            try
            {
                var url = string.Concat(urlApi, controller);
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                string token = string.Empty;
                if (controllerP.Request.Cookies["TokenDashboard"] != null)
                {
                    token = controllerP.Request.Cookies["TokenDashboard"].Value;
                }

                request.AddHeader("Authorization", string.Format("Bearer {0}", token));
                IRestResponse iResponse = client.Execute(request);
                var response = JsonConvert.DeserializeObject<Response>(iResponse.Content.ToString());
                return response;
            }
            catch (Exception ex)
            {
                return new Response
                {
                    CodeError = 300,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseAuth> SecurityToken(RequestAuth requestAuth)
        {
            try
            {
                ServicePointManager.Expect100Continue = false;
                var request = JsonConvert.SerializeObject(requestAuth);
                var content = new StringContent(request, Encoding.UTF8, "Application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlApi);
                var url = Utilities.GetConfiguration("GetToken");
                var authentication = Encoding.ASCII.GetBytes(userAPi + ":" + passwordAPi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(authentication));
                var response = await client.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();
                if (result != null)
                {
                    var requestresponse = JsonConvert.DeserializeObject<ResponseAuth>(result);
                    if (requestresponse != null)
                    {
                        if (requestresponse.CodeError == 200)
                        {
                            return requestresponse;
                        }

                        return null;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void ReadKeys()
        {
            string path = HttpContext.Current.Server.MapPath(string.Concat("~/App_Data/keys.txt"));
            string[] text = File.ReadAllLines(path);
            if (text.Length > 0)
            {
                string[] line1 = text[0].Split(';');
                userAPi = line1[0].Split(':')[1];
                passwordAPi = line1[1].Split(':')[1];
            }
        }
    }
}