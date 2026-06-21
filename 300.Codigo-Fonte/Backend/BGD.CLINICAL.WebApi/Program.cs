using BGD.CLINICAL.Application;
using BGD.CLINICAL.Infra.Data;
using BGD.CLINICAL.Infra.ExternalApis;
using BGD.CLINICAL.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfraData(builder.Configuration);
builder.Services.AddExternalApis();

builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddHealthChecks();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();
app.UseApiExceptionHandling();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
