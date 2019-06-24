using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPlusModels
{
    public class Device_PayPad
    {
        public int DEVICE_PAYPAD_ID { get; set; }
        public int DEVICE_ID { get; set; }
        public int PAYPAD_ID { get; set; }
        public int CASHBOX_QUANTITY { get; set; }
        public int STACKER_QUANTITY { get; set; }
        public decimal CASHBOX_AMOUNT { get; set; }
        public decimal STACKER_AMOUNT { get; set; }
        public int MAX_CASHBOX_QUANTITY { get; set; }
        public int MAX_STACKER_QUANTITY { get; set; }
        public bool STATE { get; set; }
    }
}