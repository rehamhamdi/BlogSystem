using Microsoft.Extensions.Logging;

namespace BlogSystem.Infrastructure.BackgroundJobs.HangfireJobs;

public class TestRetryJob
{
    private readonly ILogger<TestRetryJob> _logger;

    public TestRetryJob(
        ILogger<TestRetryJob> logger)
    {
        _logger = logger;
    }

    public void Execute()
    {
        _logger.LogInformation(
            "Hangfire retry job is executing...");

        throw new Exception(
            "Simulated failure!");
    }
}