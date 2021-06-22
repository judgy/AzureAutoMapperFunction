using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AzureAutoMapperFunction.Interfaces;
using AzureAutoMapperFunction.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureAutoMapperFunction
{
    public class LegacySystemFunction
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ITaskHelper _taskHelper;

        public LegacySystemFunction(IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ITaskHelper taskHelper)
        {
            _mapper = mapper;
            _configuration = configuration;
            _taskHelper = taskHelper;
            _client = httpClientFactory.CreateClient();
        }

        [FunctionName("LegacySystemFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<FunctionRequest>(requestBody);
            var legacySystemRequest = _mapper.Map<LegacySystemRequest>(data);
            var serializedDataToSend = _taskHelper.Serialize(legacySystemRequest);

            var url = _configuration["url"];
            var username = _configuration["username"];
            var password = _configuration["password"];

            var userNamePassword = _taskHelper.GetBase64EncodedString($"{username}:{password}");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", userNamePassword);
            var requestContent = new StringContent(serializedDataToSend, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, requestContent);
            response.EnsureSuccessStatusCode();

            var readAsStringAsync = await response.Content.ReadAsStringAsync();
            var legacySystemResponse = _taskHelper.Deserialize<LegacySystemResponse>(readAsStringAsync);

            var functionRequest = _mapper.Map<FunctionResponse>(legacySystemResponse);

            return new OkObjectResult(functionRequest);
        }
    }
}