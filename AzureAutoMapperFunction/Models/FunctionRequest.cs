namespace AzureAutoMapperFunction.Models
{
    public class FunctionRequest
    {
        public string CorrelationId { get; set; }
        public string Reference { get; set; }
    }
}