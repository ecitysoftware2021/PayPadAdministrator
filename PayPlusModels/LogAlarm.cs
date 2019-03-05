using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayPlusModels
{
    public class LogAlarm
    {
        [Display(Name = "ID")]
        public int ALARM_LOG_ID { get; set; }

        [Display(Name = "Alarma")]
        public int ALARM_ID { get; set; }

        [Display(Name = "Usuario Alarma")]
        public int USER_ALARM_ID { get; set; }

        [Display(Name = "ID")]
        public int USER_API_ID { get; set; }

        [Display(Name = "Descripción")]
        public string DESCRIPTION { get; set; }

        [Display(Name = "Fecha")]
        public DateTime DATE { get; set; }

        [Display(Name = "Estado Alarma")]
        public int STATE_ALARM { get; set; }

        [Display(Name = "Estado Alarma")]
        public string STATE_ALARM_NAME
        {
            get
            {
                if (STATE_ALARM == 0)
                {
                    return "Alarma Desarmada";
                }
                else if (STATE_ALARM == 1)
                {
                    return "Alarma Armada";
                }
                else
                {
                    return "Alarma Activada";
                }
            }
        }


        [Display(Name = "Estado")]
        public bool STATE { get; set; }

        [Display(Name = "Usuario Alarma")]
        public string USER_ALARMA { get; set; }

        [Display(Name = "Alarma")]
        public string ALARMA_NAME { get; set; }
    }
}