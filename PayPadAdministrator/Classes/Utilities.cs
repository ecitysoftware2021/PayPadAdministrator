using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace PayPadAdministrator.Classes
{
    public class Utilities
    {
        public static string GetConfiguration(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static byte[] GenerateByteArray(Stream file)
        {
            byte[] data;
            using (Stream inputStream = file)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }

            return data;
        }

        public static string RemoveAccentsWithNormalization(string inputString)
        {
            string normalizedString = inputString.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < normalizedString.Length; i++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(normalizedString[i]);
                }
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        public static string RemoveSpecialCharacters(string str)
        {
            try
            {
                return Regex.Replace(str, @"[^\w\.@-]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            catch (Exception)
            {
                return str;
            }
        }

    }
}