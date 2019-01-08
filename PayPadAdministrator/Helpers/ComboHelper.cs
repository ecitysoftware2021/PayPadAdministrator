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

            customerTypes.Add(new CustomerType
            {
                CUSTOMER_TYPE_ID = 0,
                DESCRIPTION = "Seleccione un tipo de cliente"
            });

            return customerTypes;
        }

        public static async Task<List<Customer>> GetCustomers()
        {
            List<Customer> clients = new List<Customer>();
            var request = new GetRequest
            {
                Parameter = "2",
                Type = 4
            };

            var response = await apiService.InsertPost(request, "GetCustomers");
            if (response.CodeError == 200)
            {
                var data = response.Data.ToString();
                clients = JsonConvert.DeserializeObject<List<Customer>>(response.Data.ToString());
            }

            clients.Add(new Customer
            {
                CUSTOMER_ID = 0,
                NAME = "Seleccione un cliente"
            });

            return clients;
        }

        public static async Task<List<Currency>> GetCurrencies()
        {
            List<Currency> currencies = new List<Currency>();
            var response = await apiService.GetData("GetCurrencies");
            if (response.CodeError == 200)
            {
                currencies = JsonConvert.DeserializeObject<List<Currency>>(response.Data.ToString());
            }

            currencies.Add(new Currency
            {
                CURRENCY_ID = 0,
                DESCRIPTION = "Seleccione una moneda"
            });

            return currencies;
        }

        public static async Task<List<Office>> GetOffices(int customerId)
        {
            List<Office> offices = new List<Office>();
            var data = new Office
            {
                CUSTOMER_ID = customerId
            };

            var response = await apiService.InsertPost(data, "GetOfficesForClient");
            if (response.CodeError == 200)
            {
                offices = JsonConvert.DeserializeObject<List<Office>>(response.Data.ToString());
            }

            offices.Add(new Office
            {
                OFFICE_ID = 0,
                NAME = "Seleccione una sucursal"
            });
            return offices;            
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

            locations.Add(new Location
            {
                LOCATION_ID = 0,
                NAME = "Seleccione una región"
            });

            return locations;
        }
    }
}