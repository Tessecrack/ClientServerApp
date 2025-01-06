using ClientServerApp.BlazorUI.Components;
using ClientServerApp.Client;

var builder = WebApplication.CreateBuilder(args);

var clientConnectionString = builder.Environment.IsDevelopment() 
    ? builder.Configuration.GetSection("webapi_address").Value
    : Environment.GetEnvironmentVariable("WEB_API_ADDRESS");

builder.Services.AddSingleton(new ApplicationClient(clientConnectionString!));

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

app.Logger.LogInformation($"Client: {clientConnectionString}");

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
