using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class UserSession
    {
        public int User_ID { get; set; }

        public int Customer_ID { get; set; }

        public string Identification { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int StateId { get; set; }

        public List<Role> Roles { get; set; }

    }
}