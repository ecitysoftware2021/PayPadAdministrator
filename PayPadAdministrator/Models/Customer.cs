using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class Customer
    {
        [Display(Name = "ID")]
        public int CUSTOMER_ID { get; set; }

        [Display(Name = "Tipo de Cliente ID")]
        public int TYPE_CUSTOMER_ID { get; set; }

        [Display(Name = "Tipo de Cliente")]
        public string CT_DESCRIPTION { get; set; }

        [Display(Name = "Region ID")]
        public int LOCATION_ID { get; set; }

        [Display(Name = "Region")]
        public string L_NAME { get; set; }

        [Display(Name = "Nit")]
        public string NIT { get; set; }

        [Display(Name = "Nombre")]
        public string NAME { get; set; }

        [Display(Name = "ICono")]
        public byte[] ICON { get; set; }
        
        [Display(Name = "Logo")]
        public string ImagePath { get; set; }

        [NotMapped]
        [Display(Name = "Logo de la entidad")]
        public HttpPostedFileBase ImagePathFile { get; set; }

        [Display(Name = "Correo")]
        public string EMAIL { get; set; }

        [Display(Name = "Télefono")]
        public decimal PHONE { get; set; }

        [Display(Name = "Estado")]
        public bool STATE { get; set; }

    }
}