using System;

namespace AzureAutoMapperFunction.Models
{
    public class LegacySystemResponse
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}