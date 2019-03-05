using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPlusModels
{
    public class CustomSerializeModel
    {
        public int UserId { get; set; }

        public string User_Name { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public int CustomerId { get; set; }

        public string Phone { get; set; }

        public byte[] Image { get; set; }

        public List<string> Roles { get; set; }

    }
}