using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPlusModels
{
    public class UserAlarm
    {
        public int USER_ALARM_ID { get; set; }
        public int? CUSTOMER_ID { get; set; }
        public int? ALARM_ID { get; set; }
        public string USER_NAME { get; set; }
        public string PASSWORD { get; set; }
        public string DESACTIVATE_CODE { get; set; }
        public bool? STATE { get; set; }
        public string NAME { get; set; }
        public string ALARM_NAME { get; set; }
    }
}