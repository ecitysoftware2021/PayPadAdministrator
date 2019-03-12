using System;
using System.ComponentModel.DataAnnotations;

namespace PayPlusModels
{
    public class ConsoleErrorLog
    {
        [Display(Name ="ID")]
        public int PAYPAD_CONSOLE_ERROR_ID { get; set; }

        [Display(Name = "Pay +")]
        public int PAYPAD_ID { get; set; }

        [Display(Name = "Error")]
        public int ERROR_ID { get; set; }

        [Display(Name = "Nivel de Error")]
        public int ERROR_LEVEL_ID { get; set; }

        [Display(Name = "Dispositivo")]
        public int? DEVICE_PAYPAD_ID { get; set; }

        [Display(Name = "Descripción")]
        public string DESCRIPTION { get; set; }

        [Display(Name = "Fecha")]
        public DateTime DATE { get; set; }

        [Display(Name = "Observación")]
        public string OBSERVATION { get; set; }

        [Display(Name = "Estado")]
        public bool STATE { get; set; }

        [Display(Name = "Pay +")]
        public string PAYPAD_NAME { get; set; }

        [Display(Name = "Error")]
        public string ERROR_DESCRIPTION { get; set; }

        [Display(Name = "Código Error")]
        public int CODE_ERROR { get; set; }

        [Display(Name = "Nivel de Error")]
        public string ERROR_LEVEL_DESCRIPTION { get; set; }

        [Display(Name = "Dispositivo")]
        public string DEVICE_NAME { get; set; }
    }
}
