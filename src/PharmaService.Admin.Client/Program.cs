using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PharmaService.Admin.Client;
using MudBlazor.Services;
using PharmaService.IntegrationClient.Http;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddRefitClient<IPharmaServiceClient>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Integrations:Admin:Address")!);
    });

builder.Services.AddMudServices();

await builder.Build().RunAsync();