using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class Location
    {
        [Display(Name ="ID")]
        public int LOCATION_ID { get; set; }

        [Display(Name = "País")]
        public string NAME { get; set; }

        [Display(Name = "Descripción")]
        public string DESCRIPTION { get; set; }

        [Display(Name = "Longitud")]
        public decimal LONGITUDE { get; set; }

        [Display(Name = "latitud")]
        public decimal LATITUDE { get; set; }

        [Display(Name = "Estado")]
        public bool STATE { get; set; }
    }
}