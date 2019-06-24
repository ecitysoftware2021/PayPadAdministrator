using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPlusModels
{
    public class User
    {
        [Display(Name = "USER_ID")]
        public int USER_ID { get; set; }

        public int CUSTOMER_ID { get; set; }
        public int ROLE_ID { get; set; }
        public string IDENTIFICATION { get; set; }

        [Display(Name = "Nombre")]
        public string NAME { get; set; }

        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string EMAIL { get; set; }
        public byte[] IMAGE { get; set; }
        public decimal PHONE { get; set; }
        public bool STATE { get; set; }

    }
}
