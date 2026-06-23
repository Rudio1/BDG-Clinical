using BGD.CLINICAL.Application;
using BGD.CLINICAL.Infra.Data;
using BGD.CLINICAL.Infra.Jobs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfraData(builder.Configuration);
builder.Services.AddInfraJobs();

builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");

app.Run();
