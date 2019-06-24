using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPlusModels
{
    public class Action
    {
        [Display(Name ="ID")]
        public int ACTION_ERROR_ID { get; set; }

        [Display(Name = "Actión")]
        public int ACTION_ID { get; set; }

        [Display(Name = "Error ID")]
        public int ERROR_ID { get; set; }

        [Display(Name = "Estado")]
        public bool STATE { get; set; }

        [Display(Name = "Descripción")]
        public string DESCRIPTION { get; set; }

        [Display(Name = "Código de Acción")]
        public string CODE_ACTION { get; set; }
    }
}
