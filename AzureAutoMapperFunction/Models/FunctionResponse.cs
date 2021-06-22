using System;

namespace AzureAutoMapperFunction.Models
{
    public class FunctionResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}