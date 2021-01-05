using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Blazored.LocalStorage;
/// <summary>
/// https://docs.microsoft.com/en-us/aspnet/core/blazor/project-structure?view=aspnetcore-5.0
/// </summary>
namespace PTIWebAppBlazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            //Services are added and configured 
            //builder.Services.AddSingleton<IMyDependency, MyDependency>()
            ///lOCAL STORAGE
            builder.Services.AddTransient(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            })
                .AddBlazoredLocalStorage();
            //await addCustomConfigAsync(builder);
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            //LogLevel.Debug require browser set Default level to Verbose
            //set minimum Info better
            builder.Logging.SetMinimumLevel(LogLevel.Information);
            //END
            await builder.Build().RunAsync();

        }

        //public static async Task addCustomConfigAsync(WebAssemblyHostBuilder builder)
        //{
        //    var http = new HttpClient()
        //    {
        //        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
        //    };
        //    builder.Services.AddScoped(sp => http);
        //    not work if use < base > tag in wwwroot / index.html
        //    so don; t use
        //    using var response = await http.GetAsync("/config/insurance.json");
        //    using var stream = await response.Content.ReadAsStreamAsync();
        //    builder.Configuration.AddJsonStream(stream);
        //}
    }
}
