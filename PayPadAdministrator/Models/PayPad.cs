using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class PayPad
    {
        [Display(Name = "ID")]
        public int PAYPAD_ID { get; set; }

        [Display(Name = "Oficina")]
        public int OFFICE_ID { get; set; }

        [Display(Name = "Modena")]
        public int CURRENCY_ID { get; set; }

        [Display(Name = "Nombre")]
        public string NAME { get; set; }

        [Display(Name = "Imagen")]
        public byte[] IMAGE { get; set; }

        [Display(Name = "Descripción")]
        public string DESCRIPTION { get; set; }

        [Display(Name = "Longitud")]
        public string LONGITUDE { get; set; }

        [Display(Name = "Latitud")]
        public string LATITUDE { get; set; }

        [Display(Name = "Estado")]
        public bool STATE { get; set; }

        [Display(Name = "Oficina")]
        public string OFFICE_NAME { get; set; }

        [Display(Name = "Dirección")]
        public string OFFICE_ADDRESS { get; set; }

        [Display(Name = "Moneda")]
        public string CURRENCY_DESC { get; set; }

        [Display(Name = "Simbolo")]
        public string CURRENCY_SYMBOL { get; set; }

        [Display(Name = "Cliente")]
        public string CUSTOMER_NAME { get; set; }

        [Display(Name = "Cliente")]
        public int CUSTOMER_ID { get; set; }

        [NotMapped]
        [Display(Name = "Imagen")]
        public HttpPostedFileBase ImagePathFile { get; set; }
    }
}