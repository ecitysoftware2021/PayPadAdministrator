using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayPlusModels
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
        [Display(Name ="ID")]
        public int DEVICE_LOG_ID { get; set; }

        [Display(Name = "ID")]
        public int DEVICE_PAYPAD_ID { get; set; }

        [Display(Name = "Transacción")]
        public int? TRANSACTION_ID { get; set; }

        [Display(Name = "Descripción")]
        public string DESCRIPTION { get; set; }

        [Display(Name = "Fecha/Hora")]
        public DateTime DATETIME { get; set; }

        [Display(Name = "Fecha/Hora de Inserción")]
        public DateTime DATETIME_INSERT { get; set; }

        [Display(Name = "Descripción del dispositivo")]
        public string DEVICE_DESCRIPTION { get; set; }

        [Display(Name = "Dispositivo")]
        public string DEVICE_NAME { get; set; }

        [Display(Name = "Pay +")]
        public string PAYPAD_NAME { get; set; }
    }
}