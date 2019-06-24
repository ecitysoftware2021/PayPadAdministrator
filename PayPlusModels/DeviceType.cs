using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayPlusModels
{
    public class DeviceType
    {
        [Display(Name = "ID")]
        public int DEVICE_TYPE_ID { get; set; }

        [Display(Name ="Apostrofe")]
        public string APOSTROPHE { get; set; }

        [Display(Name = "Descripción")]
        public string DESCRIPTION { get; set; }

        [Display(Name = "Estado")]
        public bool STATE { get; set; }
    }
}