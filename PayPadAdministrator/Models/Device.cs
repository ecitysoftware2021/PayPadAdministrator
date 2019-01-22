using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class Device
    {
        [Display(Name = "ID")]
        public int DEVICE_ID { get; set; }

        [Display(Name = "Serial")]
        public string SERIAL { get; set; }

        [Display(Name = "Nombre")]
        public string NAME { get; set; }

        [Display(Name = "Descripción")]
        public string DESCRIPTION { get; set; }

        [Display(Name = "Marca")]
        public string BRAND { get; set; }

        [Display(Name = "Estado")]
        public bool STATE { get; set; }

        [Display(Name = "Logo")]
        public byte[] IMAGE { get; set; }

        [Display(Name = "Tipo de dispositivo")]
        public int DEVICE_TYPE_ID { get; set; }

        [NotMapped]
        [Display(Name = "Logo")]
        public HttpPostedFileBase ImagePathFile { get; set; }
    }
}