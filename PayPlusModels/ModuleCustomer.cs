using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPlusModels
{
    public class ModuleCustomer
    {
        public int MODULE_CUSTOMER_ID { get; set; }

        public int MODULE_ID { get; set; }

        public int CUSTOMER_ID { get; set; }

        public bool STATE { get; set; }
    }
}