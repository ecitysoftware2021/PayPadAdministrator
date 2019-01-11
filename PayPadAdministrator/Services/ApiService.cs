using Newtonsoft.Json;
using PayPadAdministrator.Classes;
using PayPadAdministrator.Models;
using RestSharp;
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

        string urlApi = string.Empty;

        string passwordAPi = string.Empty;

        public ApiService()
        {
            userAPi = Utilities.GetConfiguration("UrlApi");
            urlApi = Utilities.GetConfiguration("UrlApi");
            passwordAPi = "34";
        }

        public async Task<UserSession> Login(string username, string password)
        {
            try
            {
                var reques = new RequestAuthentication
                {
                    Password = password,                    
                    UserName = username
                };

                var json = JsonConvert.SerializeObject(reques);
                var content = new StringContent(json, Encoding.UTF8, "Application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlApi);
                var url = "api/Users/Login";                
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

        public async Task<UserSession> LoginApi(string username, string password)
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
                client.BaseAddress = new Uri(urlApi);
                var url = "Authentication/AuthenticateRepo";

                //var authentication = Encoding.ASCII.GetBytes(userAPi + ":" + passwordAPi);
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authentication));
                var response = await client.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();
                var requestresponse = JsonConvert.DeserializeObject<UserSession>(result);
                return requestresponse;
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

        public async Task<Response> GetData(string controller)
        {
            try
            {                               
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(urlApi);
                var url = Utilities.GetConfiguration(controller);
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

        public async Task<Response> GetDataV2(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(urlApi);                
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

        public async Task<UserViewModel> ValidateUserAsync(string name)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(urlApi);
                var url = string.Concat("api/Users/GetUserForUserName?userName=", name);
                var response = await client.GetAsync(url);
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

                var userCurrent = JsonConvert.DeserializeObject<UserViewModel>(responseApi.Data.ToString());
                return userCurrent;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public UserViewModel ValidateUser(string name)
        {
            try
            {
                var url = string.Concat(urlApi, "api/Users/GetUserForUserName?userName=", name);
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
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

        public Response GetDataRest(string controller)
        {
            try
            {
                var url = string.Concat(urlApi, controller);
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
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
    }
}