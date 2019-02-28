using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPadAdministrator.Models
{
    public class TransactionViewModel
    {
        public TransactionHomeViewModel Transaction { get; set; }

        public List<TransactionDescription> TransactionDescriptions { get; set; }
        public List<TransactionDetail> TransactionDetails { get; set; }

        public List<LogDevice> LogDevices { get; set; }
    }

    public class TransactionDescription
    {
        public int TRANSACTION_DESCRIPTION_ID { get; set; }
        public int? TRANSACTION_ID { get; set; }
        public string REFERENCE { get; set; }
        public decimal? AMOUNT { get; set; }
        public string OBSERVATION { get; set; }
        public bool? STATE { get; set; }
    }

    public class TransactionDetail
    {
        public int TRANSACTION_DETAIL_ID { get; set; }
        public int TRANSACTION_ID { get; set; }
        public int DEVICE_PAYPAD_ID { get; set; }
        public int CURRENCY_DENOMINATION_ID { get; set; }
        public int TYPE_OPERATION_ID { get; set; }
        public string OPERATION { get; set; }
        public byte[] CURRENCY_IMAGE { get; set; }
        public int CURRENCY_VALUE { get; set; }
        public string DEVICE_NAME { get; set; }
    }

    public class LogDevice
    {
        public int DEVICE_LOG_ID { get; set; }
        public int DEVICE_PAYPAD_ID { get; set; }
        public int? TRANSACTION_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public DateTime DATETIME { get; set; }
        public DateTime DATETIME_INSERT { get; set; }
        public string DEVICE_DESCRIPTION { get; set; }
        public string DEVICE_NAME { get; set; }
        public string PAYPAD_NAME { get; set; }
    }
}