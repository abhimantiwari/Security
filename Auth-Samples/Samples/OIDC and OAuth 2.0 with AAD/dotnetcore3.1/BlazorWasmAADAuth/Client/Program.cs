using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlazorWasmAADAuth.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("BlazorWasmAADAuth.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorWasmAADAuth.ServerAPI"));

            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add("api://b96150f4-f22f-4e73-8976-ce0cabf26392/API.Access");
            });


            #region
            //        builder.Services.AddHttpClient("BlazorWasmAADMsal.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
            //.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            //        // Supply HttpClient instances that include access tokens when making requests to the server project
            //        builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorWasmAADMsal.ServerAPI"));

            //        builder.Services.AddMsalAuthentication(options =>
            //        {
            //            builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
            //            options.ProviderOptions.DefaultAccessTokenScopes.Add("api://71a076df-5293-4ebb-9fd8-b686833b5e01/API.Access");
            //        });
            #endregion
            await builder.Build().RunAsync();
        }
    }
}
