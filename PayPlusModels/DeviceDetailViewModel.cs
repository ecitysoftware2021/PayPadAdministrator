using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayPlusModels
{
    public class DeviceDetailViewModel
    {
        public Device Device { get; set; }

        public Device_PayPad DevicePayPad { get; set; }

        public List<Device_PayPad_Detail_ViewModel> DevicePayPadDetails { get; set; }
    }

    public class Device_PayPad_Detail_ViewModel
    {
        public int DEVICE_PAYPAD_DETAIL_ID { get; set; }

        public int DEVICE_PAYPAD_ID { get; set; }

        public int CURRENCY_DENOMINATION_ID { get; set; }

        [Display(Name = "Cantidad Baúl")]
        public int CASHBOX_QUANTITY { get; set; }

        [Display(Name = "Total Baúl")]
        public string TOTAL_CASHBOX { get { return string.Format("{0:C0}", (CASHBOX_QUANTITY * CURRENCY_VALUE)); } }
        [Display(Name = "Cantidad Disponible")]
        [MaxLength(5000)]
        public string STACKER_QUANTITY { get; set; }

        [Display(Name = "Total Disponible")]
        public string TOTAL_STACKER { get { return string.Format("{0:C0}", (int.Parse(STACKER_QUANTITY) * CURRENCY_VALUE)); } }


        [Display(Name = "Mínimo Cantidad Disponible")]
        public int MIN_STACKER_QUANTITY { get; set; }

        [Display(Name = "Estado")]
        public bool STATE { get; set; }

        public byte[] CURRENCY_IMAGE { get; set; }

        public int CURRENCY_VALUE { get; set; }
    }
}