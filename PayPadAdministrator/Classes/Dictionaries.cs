using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Classes
{
    public class Dictionaries
    {
        public static Dictionary<int, string> SpecialCharacters = new Dictionary<int, string>()
        {
            { 1, "$"},
            { 2, "#"},
            { 3, "!"},
            { 4, "%"},
            { 5, "&"},
            { 6, "/"},
            { 7, "="},
            { 8, "*"},
            { 9, "-"},
            { 10, "_"},
        };
    }
}