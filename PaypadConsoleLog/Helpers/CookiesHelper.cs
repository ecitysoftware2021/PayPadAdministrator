﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaypadConsoleLog.Helpers
{
    public class CookiesHelper
    {
        public static HttpCookie CreateTokenCookie(string value,string name)
        {
            HttpCookie httpCookie = new HttpCookie(name);
            httpCookie.Value = value;
            httpCookie.Expires = DateTime.Now.AddHours(1);
            return httpCookie;
        }



        public static string GetNameCookie()
        {
            return string.Concat("PaypadConsoleLog", DateTime.Now.ToString("yyyyMMdd"));
        }
    }
}