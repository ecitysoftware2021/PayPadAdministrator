using Newtonsoft.Json;
using PayPadAdministrator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Helpers
{
    public class SessionHelper
    {
        public static UserSession GetUser(string userjson)
        {
            var user = JsonConvert.DeserializeObject<UserSession>(userjson);
            return user;
        }

        public static string SaveUser(UserSession userSession)
        {
            var user = JsonConvert.SerializeObject(userSession);
            return user;
        }
    }
}