﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayPlusModels
{
    public class Transaction_Type
    {
        [Display(Name ="ID")]
        public int TRANSACTION_TYPE_ID { get; set; }

        [Display(Name = "Descripción")]
        public string DESCRIPTION { get; set; }

        [Display(Name = "Operación")]
        public string OPERATION_TYPE_ID { get; set; }

        [Display(Name = "Estado")]
        public bool STATE { get; set; }
    }
}