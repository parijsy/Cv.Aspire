var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Cv_Aspire_ApiService>("apiservice");

builder.AddProject<Projects.Cv_Aspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
