using System;
using System.Web;

namespace PayPadAdministrator.Helpers
{
    public class CookiesHelper
    {
        public static HttpCookie CreateTokenCookie(string token)
        {
            HttpCookie httpCookie = new HttpCookie("TokenDashboard");
            httpCookie.Value = token;
            httpCookie.Expires = DateTime.Now.AddHours(1);            
            return httpCookie;
        }

        //public string GetToken()
        //{
        //    if (Request.Cookies["key"] != null)
        //    {
        //        var value = Request.Cookies["key"].Value;
        //    }
        //}
    }
}