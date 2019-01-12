using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class UserOffice
    {
        public int USER_OFFICE_ID { get; set; }

        public int USER_ID { get; set; }

        public int OFFICE_ID { get; set; }

        public bool STATE { get; set; }
    }
}