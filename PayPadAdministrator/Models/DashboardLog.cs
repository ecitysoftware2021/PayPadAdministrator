using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class DashboardLog
    {
        public int DASHBOARD_LOG_ID { get; set; }

        public int USER_ID { get; set; }

        public int MODULE_ID { get; set; }

        public string DESCRIPTION { get; set; }

        public DateTime DATE { get; set; }

    }

    public class DasboardLogViewModel
    {
        public int DASHBOARD_LOG_ID { get; set; }
        public int USER_ID { get; set; }
        public int MODULE_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public System.DateTime DATE { get; set; }
        public string MODULE_DESCRIPTION { get; set; }
        public string COLOR { get; set; }
        public string ICON { get; set; }
    }
}