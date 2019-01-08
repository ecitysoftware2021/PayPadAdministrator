using Newtonsoft.Json;
using PayPadAdministrator.Classes;
using PayPadAdministrator.CustomAuthentication;
using PayPadAdministrator.Helpers;
using PayPadAdministrator.Models;
using PayPadAdministrator.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PayPadAdministrator.Controllers
{
    [CustomAuthorize]
    public class CustomersController : Controller
    {
        ApiService apiService = new ApiService();
        // GET: Customers
        public async Task<ActionResult> Index()
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

            return View(clients);
        }

        public async Task<ActionResult> SponsorCustomers()
        {
            List<Customer> clients = new List<Customer>();
            var request = new GetRequest
            {
                Parameter = "1",
                Type = 4
            };

            var response = await apiService.InsertPost(request, "GetCustomers");
            if (response.CodeError == 200)
            {
                clients = JsonConvert.DeserializeObject<List<Customer>>(response.Data.ToString());
            }

            return View(clients);
        }

        public async Task<ActionResult> SponsorToClients(int id)
        {
            SponsorToClientViewModel sponsor = new SponsorToClientViewModel();
            var request = new GetRequest
            {
                Parameter = id.ToString(),
                Type = 2
            };

            var responseSponsor = await apiService.InsertPost(request, "GetCustomers");
            if (responseSponsor.CodeError == 200)
            {
                var dd = JsonConvert.DeserializeObject<List<Customer>>(responseSponsor.Data.ToString());
                sponsor.Sponsor = dd[0];
                var data = new Sponsor
                {
                    SPONSOR_CUSTOMER_ID = id
                };

                var responseClient = await apiService.InsertPost(data, "GetClientsFromSponsor");
                if (responseClient.CodeError == 200)
                {
                    sponsor.Clients = JsonConvert.DeserializeObject<List<Customer>>(responseClient.Data.ToString());
                }
            }

            List<SponsorToClientViewModel> sponsorToClientViews = new List<SponsorToClientViewModel>();
            sponsorToClientViews.Add(sponsor);
            return View(sponsorToClientViews);
        }

        public async Task<ActionResult> AssingClient(int id)
        {
            SponsorToClientViewModelV2 sponsor = new SponsorToClientViewModelV2();
            var request = new GetRequest
            {
                Parameter = id.ToString(),
                Type = 2
            };

            var responseSponsor = await apiService.InsertPost(request, "GetCustomers");
            if (responseSponsor.CodeError == 200)
            {
                var dd = JsonConvert.DeserializeObject<List<Customer>>(responseSponsor.Data.ToString());
                sponsor.Sponsor = dd[0];
                var data = new Sponsor
                {
                    SPONSOR_CUSTOMER_ID = id
                };

                var responseClient = await apiService.InsertPost(data, "GetClientsFromSponsor");

                if (responseClient.CodeError == 200)
                {
                    sponsor.ClientsAssined = JsonConvert.DeserializeObject<List<Customer>>(responseClient.Data.ToString());
                }

                var requestClients = new GetRequest
                {
                    Parameter = "2",
                    Type = 4
                };

                var responseSponso = await apiService.InsertPost(requestClients, "GetCustomers");
                if (responseSponso.CodeError == 200)
                {
                    sponsor.Clients = new List<Customer>();
                    var clients = JsonConvert.DeserializeObject<List<Customer>>(responseSponso.Data.ToString());
                    foreach (var item in clients)
                    {
                        var client = sponsor.ClientsAssined.Where(c => c.CUSTOMER_ID == item.CUSTOMER_ID).FirstOrDefault();
                        if (client == null)
                        {
                            sponsor.Clients.Add(item);
                        }
                    }
                }
            }

            List<SponsorToClientViewModelV2> sponsorToClientViews = new List<SponsorToClientViewModelV2>();
            sponsorToClientViews.Add(sponsor);
            return View(sponsorToClientViews);
        }

        public async Task<ActionResult> ShowOfficeForCustomer(int id)
        {
            var customer = new Customer
            {
                CUSTOMER_ID = id
            };

            List<Office> offices = new List<Office>();
            var response = await apiService.InsertPost(customer, "GetOfficesForClient");
            if (response.CodeError == 200)
            {
                offices = JsonConvert.DeserializeObject<List<Office>>(response.Data.ToString());
            }

            ViewBag.CustomerId = id;
            return View(offices);
        }


        public ActionResult CreateOffice(int id)
        {
            var office = new Office
            {
                CUSTOMER_ID = id
            };

            return View(office);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateOffice(Office office)
        {
            if (!ModelState.IsValid)
            {
                return View(office);
            }

            var response = await apiService.InsertPost(office, "CreateOfficeForClient");
            if (response.CodeError != 200)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View(office);
            }

            return RedirectToAction("ShowOfficeForCustomer", new { id = office.CUSTOMER_ID, Message = "Se creó el cliente correctamente" });
        }

        public async Task<ActionResult> CreateCustomer()
        {
            ViewBag.TYPE_CUSTOMER_ID = new SelectList(await ComboHelper.GetTypeCustomers(),
                                                    nameof(CustomerType.CUSTOMER_TYPE_ID),
                                                    nameof(CustomerType.DESCRIPTION), 0);
            ViewBag.LOCATION_ID = new SelectList(await ComboHelper.GetLocations(),
                                                    nameof(Location.LOCATION_ID),
                                                    nameof(Location.NAME), 0);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TYPE_CUSTOMER_ID = new SelectList(await ComboHelper.GetTypeCustomers(),
                        nameof(CustomerType.CUSTOMER_TYPE_ID),
                        nameof(CustomerType.DESCRIPTION), customer.TYPE_CUSTOMER_ID);
                ViewBag.LOCATION_ID = new SelectList(await ComboHelper.GetLocations(),
                                                        nameof(Location.LOCATION_ID),
                                                        nameof(Location.NAME), customer.LOCATION_ID);
                return View(customer);
            }

            if (customer.ImagePathFile == null)
            {
                ViewBag.TYPE_CUSTOMER_ID = new SelectList(await ComboHelper.GetTypeCustomers(),
                        nameof(CustomerType.CUSTOMER_TYPE_ID),
                        nameof(CustomerType.DESCRIPTION), customer.TYPE_CUSTOMER_ID);
                ViewBag.LOCATION_ID = new SelectList(await ComboHelper.GetLocations(),
                                                        nameof(Location.LOCATION_ID),
                                                        nameof(Location.NAME), customer.LOCATION_ID);
                ModelState.AddModelError(string.Empty, "Debe ingresar un logo");
                return View(customer);
            }

            customer.ICON = Utilities.GenerateByteArray(customer.ImagePathFile.InputStream);
            customer.ImagePathFile = null;
            var response = await apiService.InsertPost(customer, "CreateCustomer");
            if (response.CodeError != 200)
            {
                ViewBag.TYPE_CUSTOMER_ID = new SelectList(await ComboHelper.GetTypeCustomers(),
                                                    nameof(CustomerType.CUSTOMER_TYPE_ID),
                                                    nameof(CustomerType.DESCRIPTION), customer.TYPE_CUSTOMER_ID);
                ViewBag.LOCATION_ID = new SelectList(await ComboHelper.GetLocations(),
                                                        nameof(Location.LOCATION_ID),
                                                        nameof(Location.NAME), customer.LOCATION_ID);
                ModelState.AddModelError(string.Empty, response.Message);
                return View(customer);
            }

            return RedirectToAction("Index", new { Message = "Se creó el cliente correctamente" });
        }

        public async Task<ActionResult> TypeCustomers()
        {
            List<CustomerType> customerTypes = new List<CustomerType>();
            var request = new GetRequest
            {
                Parameter = "",
                Type = 1
            };

            var response = await apiService.InsertPost(request, "CustomerGetType");
            if (response.CodeError == 200)
            {
                customerTypes = JsonConvert.DeserializeObject<List<CustomerType>>(response.Data.ToString());
            }

            return View(customerTypes);
        }

        public ActionResult CreateTypeCustomers()
        {
            var custom = new CustomerType
            {
                STATE = true
            };

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTypeCustomers(CustomerType customerType)
        {
            if (ModelState.IsValid)
            {
                var response = await apiService.InsertPost(customerType, "CreateTypeCustomer");
                if (response.CodeError == 200)
                {
                    return RedirectToAction("TypeCustomers", new { Message = "Se creo correctamente" });
                }

                ModelState.AddModelError(string.Empty, response.Message);
                return View(customerType);
            }

            return View(customerType);
        }

    }
}
