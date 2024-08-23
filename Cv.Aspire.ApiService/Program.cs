using Cv.Aspire.ApiService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services.RegisterBundlesInAssembly<Cv.Aspire.ApiService.Domain.ServiceRegistrationBundle>(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();


app.UseRouting();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.MapDefaultEndpoints();

app.Run();