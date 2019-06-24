using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPlusModels
{
    public partial class TransactionType
    {
        public int OFFICE_ID { get; set; }
        public int CUSTOMER_ID { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string ADDRESS { get; set; }
        public bool STATE { get; set; }
    }
}
