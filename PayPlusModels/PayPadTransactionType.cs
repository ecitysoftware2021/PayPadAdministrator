using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPlusModels
{
    public class PayPadTransactionType
    {
        public int PAYPAD_TRANSACTION_TYPE_ID { get; set; }

        public int PAYPAD_ID { get; set; }

        public int TRANSACTION_TYPE_ID { get; set; }

        public bool STATE { get; set; }
    }
}