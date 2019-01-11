using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class UserSession
    {
        public int USER_ID { get; set; }

        public int CUSTOMER_ID { get; set; }

        public string IDENTIFICATION { get; set; }

        public string USERNAME { get; set; }

        public string PASSWORD { get; set; }

        public string EMAIL { get; set; }

        public string PHONE { get; set; }

        public bool STATE { get; set; }

        public List<Role> Roles { get; set; }

        public byte[] IMAGE { get; set; }

        public string NAME { get; set; }

    }
}