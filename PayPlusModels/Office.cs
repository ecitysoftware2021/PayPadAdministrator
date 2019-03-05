using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayPlusModels
{
    public class Office
    {
        [Display(Name ="ID")]
        public int OFFICE_ID { get; set; }

        [Display(Name = "Cliente ID")]
        public int CUSTOMER_ID { get; set; }

        [Display(Name = "Código")]
        public string CODE { get; set; }

        [Display(Name = "Nombre")]
        public string NAME { get; set; }

        [Display(Name = "Dirección")]
        public string ADDRESS { get; set; }

        [Display(Name = "Estado")]
        public bool STATE { get; set; }
    }
}