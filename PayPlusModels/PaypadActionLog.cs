using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPlusModels
{
    public class PaypadActionLog
    {
        [Display(Name = "ID")]
        public int PAYPAD_ACTION_LOG_ID { get; set; }

        [Display(Name = "Pay +")]
        public int PAYPAD_ID { get; set; }

        [Display(Name = "Acción")]
        public int ACTION_ID { get; set; }

        [Display(Name = "Usuario")]
        public int USER_ID { get; set; }

        [Display(Name = "Dispositivo")]
        public int? DEVICE_PAYPAD_ID { get; set; }

        [Display(Name = "Descripción")]
        public string DESCRIPTION { get; set; }

        [Display(Name = "Fecha de Creación")]
        public DateTime DATE_CREATION { get; set; }

        [Display(Name = "Fecha de ejecución")]
        public DateTime? DATE_EXECUTE { get; set; }

        [Display(Name = "Fecha de Acción")]
        public DateTime DATE_ACTION { get; set; }

        [Display(Name = "Estado")]
        public int STATE { get; set; }

        [Display(Name = "Pay +")]
        public string PAYPAD_NAME { get; set; }

        [Display(Name = "Descripción de la acción")]
        public string DESCRIPTION_ACTION { get; set; }

        [Display(Name = "Código de la acción")]
        public string CODE_ACTION { get; set; }

        [Display(Name = "Usuario")]
        public string USERNAME { get; set; }

        [Display(Name = "Dispositivo")]
        public string DEVICE_NAME { get; set; }
    }
}
