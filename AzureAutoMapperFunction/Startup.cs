using AzureAutoMapperFunction;
using AzureAutoMapperFunction.Helper;
using AzureAutoMapperFunction.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace AzureAutoMapperFunction
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddAutoMapper(typeof(Startup));
            builder.Services.AddSingleton<ITaskHelper, TaskHelper>();
        }
    }
}