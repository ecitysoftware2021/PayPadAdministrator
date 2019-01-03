using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Classes
{
    public class Utilities
    {
        public static string GetConfiguration(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

    }
}