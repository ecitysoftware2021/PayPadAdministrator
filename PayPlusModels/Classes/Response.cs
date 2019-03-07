using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPlusModels.Classes
{
    public class Response
    {
        public int CodeError { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
