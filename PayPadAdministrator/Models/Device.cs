using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class Device
    {
        public int DEVICE_ID { get; set; }

        public string SERIAL { get; set; }

        public string NAME { get; set; }

        public string DESCRIPTION { get; set; }

        public string BRAND { get; set; }

        public bool STATE { get; set; }
    }
}