using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class RequestAuthentication
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public int Type { get; set; }
    }
}