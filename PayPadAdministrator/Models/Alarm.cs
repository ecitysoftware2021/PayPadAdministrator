using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class Alarm
    {
        [Display(Name = "ID")]
        public int ALARM_ID { get; set; }

        [Display(Name = "PayPad")]
        public int PAYPAD_ID { get; set; }

        [Display(Name = "Usuario")]
        public string USERNAME { get; set; }

        [Display(Name = "Contraseña")]
        public string PASSWORD { get; set; }

        [Display(Name = "Código Activación")]
        public int ACTIVATE_CODE { get; set; }

        [Display(Name = "Código Desactivación")]
        public int DESACTIVATE_CODE { get; set; }

        [Display(Name = "Wifi")]
        public string WIFI { get; set; }

        [Display(Name = "Contraseña del Wifi")]
        public string PASSWORD_WIFI { get; set; }

        [Display(Name = "Estado")]
        public bool STATE { get; set; }

        [Display(Name = "PayPad")]
        public string PAYPAD_NAME { get; set; }

    }
}