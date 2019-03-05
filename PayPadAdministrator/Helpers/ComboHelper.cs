using Newtonsoft.Json;
using PayPadAdministrator.Classes;
using PayPlusModels;
using PayPadAdministrator.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PayPadAdministrator.Helpers
{
    public class ComboHelper
    {
        static ApiService apiService = new ApiService();

        public static Controller Controller;

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

        public static async Task<List<DeviceType>> GetDevicesType()
        {
            List<DeviceType> deviceTypes = new List<DeviceType>();            
            var response = await apiService.GetData(Controller, "GetDeviceTypes");
            if (response.CodeError == 200)
            {
                deviceTypes = JsonConvert.DeserializeObject<List<DeviceType>>(response.Data.ToString());
            }

            deviceTypes.Add(new DeviceType
            {
                APOSTROPHE = "Seleccione un tipo de dispositivo",
                DEVICE_TYPE_ID = 0
            });

            return deviceTypes;
        }

        public static async Task<List<OperationType>> GetOperations()
        {
            List<OperationType> operations = new List<OperationType>();
            var response = await apiService.GetData(Controller, "GetOperations");
            if (response.CodeError == 200)
            {
                operations = JsonConvert.DeserializeObject<List<OperationType>>(response.Data.ToString());
            }

            operations.Add(new OperationType
            {
                DESCRIPTION = "Seleccione una operación",
                TYPE_OPERATION_ID = 0
            });

            return operations;
        }

        public static async Task<List<PayPad>> GetAllsPaypads()
        {
            List<PayPad> payPads = new List<PayPad>();            
            var response = await apiService.GetData(Controller, "GetAllPayPads");
            if (response.CodeError == 200 && !string.IsNullOrEmpty(response.Data.ToString()))
            {
                payPads = JsonConvert.DeserializeObject<List<PayPad>>(response.Data.ToString());
            }

            payPads.Add(new PayPad
            {
                PAYPAD_ID = 0,
                NAME = "Seleccione un Pay +"
            });
            return payPads;
        }

        public static async Task<List<TransactPaypadViewModel>> GetTransact(int payPadId)
        {
            List<TransactPaypadViewModel> transacts = new List<TransactPaypadViewModel>();            
            var response = await apiService.GetDataV2(Controller, string.Concat(Utilities.GetConfiguration("GetTransactsForPaypad"), payPadId));
            if (response.CodeError == 200)
            {
                transacts = JsonConvert.DeserializeObject<List<TransactPaypadViewModel>>(response.Data.ToString());
            }

            transacts.Add(new TransactPaypadViewModel
            {
                TRANSACTION_TYPE_ID = 0,
                DESCRIPTION = "Seleccione un Trámite"
            });
            return transacts;
        }

        public static async Task<List<PayPad>> GetAllsPaypadsForCustomer(int customerId)
        {
            List<PayPad> payPads = new List<PayPad>();            
            var response = await apiService.GetDataV2(Controller, string.Concat(Utilities.GetConfiguration("GetAllPayPadsForCustomer"), customerId));
            if (response.CodeError == 200 && !string.IsNullOrEmpty(response.Data.ToString()))
            {
                payPads = JsonConvert.DeserializeObject<List<PayPad>>(response.Data.ToString());
            }

            payPads.Add(new PayPad
            {
                PAYPAD_ID = 0,
                NAME = "Seleccione un Pay +"
            });
            return payPads;
        }

        public static async Task<List<PayPad>> GetAllsPaypadsForUser(int userId)
        {
            List<PayPad> payPads = new List<PayPad>();            
            var response = await apiService.GetDataV2(Controller, string.Concat(Utilities.GetConfiguration("GetAllPayPadsForUserOffice"), userId));
            if (response.CodeError == 200 && !string.IsNullOrEmpty(response.Data.ToString()))
            {
                payPads = JsonConvert.DeserializeObject<List<PayPad>>(response.Data.ToString());
            }

            payPads.Add(new PayPad
            {
                PAYPAD_ID = 0,
                NAME = "Seleccione un Pay +"
            });
            return payPads;
        }

        public static async Task<List<PayPad>> GetAllsPaypadsForSponsor(int customerId)
        {
            List<PayPad> payPads = new List<PayPad>();            
            var response = await apiService.GetDataV2(Controller, string.Concat(Utilities.GetConfiguration("GetAllPayPadsForSponsor"), customerId));
            if (response.CodeError == 200 && !string.IsNullOrEmpty(response.Data.ToString()))
            {
                payPads = JsonConvert.DeserializeObject<List<PayPad>>(response.Data.ToString());
            }

            payPads.Add(new PayPad
            {
                PAYPAD_ID = 0,
                NAME = "Seleccione un Pay +"
            });

            return payPads;
        }

        public static async Task<List<Customer>> GetAllCustomers()
        {
            List<Customer> clients = new List<Customer>();
            var request = new GetRequest
            {
                Parameter = string.Empty,
                Type = 1
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

        public static async Task<List<Role>> GetRoles()
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

            roles.Add(new Role
            {
                ROLE_ID = 0,
                DESCRIPTION = "Seleccione una opción"
            });

            return roles;
        }

        public static async Task<List<Currency>> GetCurrencies()
        {
            List<Currency> currencies = new List<Currency>();            
            var response = await apiService.GetData(Controller, "GetCurrencies");
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
                if (!string.IsNullOrEmpty(response.Data.ToString()))
                {
                    offices = JsonConvert.DeserializeObject<List<Office>>(response.Data.ToString());
                }
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

        public static async Task<List<Alarm>> GetAlarms(int customerId)
        {
            List<Alarm> alarms = new List<Alarm>();
            var url = string.Concat(Utilities.GetConfiguration("GetAlarmsForCustomer"), customerId);            
            var response = await apiService.GetDataV2(Controller, url);
            if (response.CodeError == 200)
            {
                alarms = JsonConvert.DeserializeObject<List<Alarm>>(response.Data.ToString());
            }

            alarms.Add(new Alarm
            {
                ALARM_ID = 0,
                USERNAME = "Seleccione una alarma"
            });

            return alarms.OrderBy(a=>a.ALARM_ID).ToList();
        }
    }
}