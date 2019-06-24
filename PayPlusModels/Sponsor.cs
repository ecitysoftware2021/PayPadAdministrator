using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPlusModels
{
    public class Sponsor
    {
        public int SPONSOR_ID { get; set; }

        public int SPONSOR_CUSTOMER_ID { get; set; }

        public int CUSTOMER_ID { get; set; }

        public bool STATE { get; set; }
    }
}