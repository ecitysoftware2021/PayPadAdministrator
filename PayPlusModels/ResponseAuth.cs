﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPlusModels
{
    public class ResponseAuth
    {
        public int CodeError { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public object Session { get; set; }
        public object User { get; set; }
    }
}