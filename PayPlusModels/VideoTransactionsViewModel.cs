using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPlusModels
{
    public class VideoTransactionsViewModel
    {
        public string FileName { get; set; }

        public byte[] Video { get; set; }

        public string TransactionId { get; set; }

        public string TypeVideo { get; set; }

        public string NameVideo { get; set; }

        public DateTime Date { get; set; }
    }
}