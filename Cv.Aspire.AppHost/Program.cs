var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Cv_Aspire_ApiService>("apiservice");


var chatDeploymentName = builder.AddParameter("chatDeploymentName", secret: true);
var chatEndpoint = builder.AddParameter("chatEndpoint", secret: true);

builder.AddProject<Projects.Cv_Aspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithEnvironment("AzureOpenAI__ChatDeploymentName", chatDeploymentName)
    .WithEnvironment("AzureOpenAI__Endpoint", chatEndpoint)
    .WithReference(apiService);

builder.Build().Run();