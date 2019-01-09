using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class Transaction_Type
    {
        public int TRANSACTION_TYPE_ID { get; set; }

        public string DESCRIPTION { get; set; }

        public bool STATE { get; set; }
    }
}