using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class SponsorToClientViewModel
    {
        public Customer Sponsor { get; set; }

        public List<Customer> Clients { get; set; }
    }

    public class SponsorToClientViewModelV2
    {
        public Customer Sponsor { get; set; }

        public List<Customer> Clients { get; set; }//Faltantes

        public List<Customer> ClientsAssined { get; set; }//Asignados
    }
}