using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class OperationType
    {
        public int TYPE_OPERATION_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public bool STATE { get; set; }
    }
}