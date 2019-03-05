using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPlusModels
{
    public class Role
    {
        public int ROLE_ID { get; set; }

        public string DESCRIPTION { get; set; }

        public bool STATE { get; set; }

        public virtual ICollection<UserSession> Users { get; set; }
    }
}