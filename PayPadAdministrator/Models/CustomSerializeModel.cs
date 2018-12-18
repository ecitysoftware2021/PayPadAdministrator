using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class CustomSerializeModel
    {
        public int UserId { get; set; }

        public string User_Name { get; set; }

        public string LastName { get; set; }

        public List<string> Roles { get; set; }

    }
}