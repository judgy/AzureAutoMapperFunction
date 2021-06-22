namespace AzureAutoMapperFunction.Interfaces
{
    public interface ITaskHelper
    {
        // Use this method to deserialize the input HTTP message payload string
        T Deserialize<T>(string json);
        // Use this method to serialize data to JSON
        string Serialize(object data);
        // Encode string to Base64
        string GetBase64EncodedString(string text);

    }
}