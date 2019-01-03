using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class CustomerType
    {
        [Display(Name ="ID")]
        public int CUSTOMER_TYPE_ID { get; set; }

        [Display(Name = "Descripción")]
        public string DESCRIPTION { get; set; }

        [Display(Name = "Estado")]
        public bool STATE { get; set; }
    }
}