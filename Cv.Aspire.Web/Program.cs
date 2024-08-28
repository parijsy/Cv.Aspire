using Azure.Core.Diagnostics;
using Azure.Identity;
using Cv.Aspire.Web.Code.Clients.Api;
using Cv.Aspire.Web.Code.SemanticKernel.Plugins;
using Cv.Aspire.Web.Components;
using Microsoft.SemanticKernel;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var kernel = builder.Services.AddKernel();
kernel.Plugins.AddFromType<TimePlugin>();
kernel.Plugins.AddFromType<WeatherPlugin>();
builder.Services.AddScoped(sp => KernelPluginFactory.CreateFromType<ThemePlugin>(serviceProvider: sp));

using AzureEventSourceListener listener = AzureEventSourceListener.CreateConsoleLogger();

var credentials = new DefaultAzureCredential(new DefaultAzureCredentialOptions
{
    ExcludeVisualStudioCredential = true
});

builder.Services.AddAzureOpenAIChatCompletion(
    builder.Configuration.GetValue<string>("AzureOpenAI:ChatDeploymentName")!,
    builder.Configuration.GetValue<string>("AzureOpenAI:Endpoint")!,
    credentials);


builder.Services.AddOutputCache();

builder.Services.AddHttpClient<ApiClient>(client =>
    {
        // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
        // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
        client.BaseAddress = new("https+http://apiservice");
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();