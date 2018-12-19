using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class Role
    {
        public int ROLE_ID { get; set; }

        public string DESCRIPTION { get; set; }

        public int STATE { get; set; }

        public virtual ICollection<UserSession> Users { get; set; }
    }
}