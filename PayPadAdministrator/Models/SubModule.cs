using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class SubModule
    {
        public int SUBMODULE_ID { get; set; }

        public int MODULE_ID { get; set; }

        public string DESCRIPTION { get; set; }

        public string URL { get; set; }

        public string ICON { get; set; }

        public bool STATE { get; set; }
    }
}