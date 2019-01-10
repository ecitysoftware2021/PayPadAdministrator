using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class UserModule
    {
        public int USER_MODULE_ID { get; set; }

        public int USER_ID { get; set; }

        public int MODULE_ID { get; set; }

        public bool STATE { get; set; }
    }
}