using Newtonsoft.Json;
using PayPadAdministrator.Models;
using PayPadAdministrator.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PayPadAdministrator.Helpers
{
    public class ComboHelper
    {
        static ApiService apiService = new ApiService();

        public static async Task<List<CustomerType>> GetTypeCustomers()
        {
            List<CustomerType> customerTypes = new List<CustomerType>();
            var request = new GetRequest
            {
                Parameter = string.Empty,
                Type = 1
            };

            var response = await apiService.InsertPost(request, "CustomerGetType");
            if (response.CodeError == 200)
            {
                customerTypes = JsonConvert.DeserializeObject<List<CustomerType>>(response.Data.ToString());
            }

            return customerTypes;
        }

        public static async Task<List<Location>> GetLocations()
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

            return locations;
        }
    }
}