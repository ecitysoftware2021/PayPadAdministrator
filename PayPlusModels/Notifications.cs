using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayPlusModels
{
    public class Notifications
    {
        [Display(Name ="ID")]
        public int NOTIFICATION_ID { get; set; }

        [Display(Name = "Tipo de notificación")]
        public int TYPE_NOTIFICATION_ID { get; set; }

        [Display(Name = "Medio de notificación")]
        public int MEANS_NOTIFICATION_ID { get; set; }

        [Display(Name = "Pay +")]
        public int PAYPAD_ID { get; set; }

        [Display(Name = "Pay +")]
        public string PAYPAD_NAME { get; set; }

        [Display(Name = "Mensaje")]
        public string MESSAGE { get; set; }

        [Display(Name = "Fecha")]
        public DateTime DATE { get; set; }

        public bool STATE { get; set; }

        [Display(Name = "Medio de notificación")]
        public string MEANS_DESCRIPTION { get; set; }

        [Display(Name = "Tipo de notificación")]
        public string TYPE_NOT_DESCRIPTION { get; set; }
    }
}