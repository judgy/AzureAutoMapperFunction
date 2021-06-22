using System;
using System.Collections.Generic;
using System.Text;
using AzureAutoMapperFunction.Interfaces;
using AzureAutoMapperFunction.Models;
using Newtonsoft.Json;

namespace AzureAutoMapperFunction.Helper
{
    public class TaskHelper : ITaskHelper
    {
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public string GetBase64EncodedString(string text)
        {
            var textArray = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textArray);

        }
    }
}
