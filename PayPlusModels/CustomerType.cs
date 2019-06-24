using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayPlusModels
{
    public class CustomerType
    {
        [Display(Name ="ID")]
        public int CUSTOMER_TYPE_ID { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(30, ErrorMessage =
            "El campo {0} puede contener un máximo de {1} y un mínimo de {2} caracteres",
            MinimumLength = 3)]
        [Required(ErrorMessage = "Debes ingresar una {0}")]
        public string DESCRIPTION { get; set; }

        [Display(Name = "Estado")]
        public bool STATE { get; set; }
    }
}