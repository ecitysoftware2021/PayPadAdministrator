using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class Module
    {
        public int MODULE_ID { get; set; }

        public string DESCRIPTION { get; set; }

        public string ICON { get; set; }

        public bool STATE { get; set; }
    }
}