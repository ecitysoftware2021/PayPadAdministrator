using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class Currency_Denomination
    {
        [Display(Name = "ID")]
        public int CURRENCY_DENOMINATION_ID { get; set; }

        [Display(Name = "Moneda")]
        public int CURRENCY_ID { get; set; }

        [Display(Name = "Logo")]
        public byte[] IMAGE { get; set; }

        [Display(Name = "Valor")]
        [Required]
        public int VALUE { get; set; }

        [Display(Name = "Estado")]
        public bool STATE { get; set; }

        [Display(Name = "Moneda")]
        public string CURRENCY_DESCRIPTION { get; set; }

        [Display(Name = "Simbolo")]
        public string SYMBOL { get; set; }

        [NotMapped]
        [Display(Name = "Logo")]
        public HttpPostedFileBase ImagePathFile { get; set; }
    }
}