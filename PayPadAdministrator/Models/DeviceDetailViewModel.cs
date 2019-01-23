﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class DeviceDetailViewModel
    {
        public Device_PayPad Device { get; set; }

        public List<Device_PayPad_Detail_ViewModel> DevicePayPadDetails { get; set; }
    }

    public class Device_PayPad_Detail_ViewModel
    {
        public int DEVICE_PAYPAD_DETAIL_ID { get; set; }
        public int DEVICE_PAYPAD_ID { get; set; }
        public int CURRENCY_DENOMINATION_ID { get; set; }
        public int CASHBOX_QUANTITY { get; set; }
        public int STACKER_QUANTITY { get; set; }
        public int MIN_STACKER_QUANTITY { get; set; }
        public bool STATE { get; set; }
        public byte[] CURRENCY_IMAGE { get; set; }
        public int CURRENCY_VALUE { get; set; }
    }
}