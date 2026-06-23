using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BGD.CLINICAL.Infra.Jobs.Jobs;

public sealed class JobWorker : BackgroundService
{
    private readonly ILogger<JobWorker> _logger;

    public JobWorker(ILogger<JobWorker> logger)
    {
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Job host started. Register scheduled or background jobs in Infra.Jobs.");

        return Task.CompletedTask;
    }
}
