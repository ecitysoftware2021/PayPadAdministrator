using System;

namespace PayPlusModels
{
    public class ConsoleErrorLog
    {
        public int PAYPAD_CONSOLE_ERROR_ID { get; set; }
        public int PAYPAD_ID { get; set; }
        public int ERROR_ID { get; set; }
        public int ERROR_LEVEL_ID { get; set; }
        public int? DEVICE_PAYPAD_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public DateTime DATE { get; set; }
        public string OBSERVATION { get; set; }
        public bool STATE { get; set; }
        public string PAYPAD_NAME { get; set; }
        public string ERROR_DESCRIPTION { get; set; }
        public int CODE_ERROR { get; set; }
        public string ERROR_LEVEL_DESCRIPTION { get; set; }
        public string DEVICE_NAME { get; set; }
    }
}
