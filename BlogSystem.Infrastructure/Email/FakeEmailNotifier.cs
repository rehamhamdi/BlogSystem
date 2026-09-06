using BlogSystem.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace BlogSystem.Infrastructure.Email;

public class FakeEmailNotifier : IEmailNotifier
{
    private readonly ILogger<FakeEmailNotifier> _logger;

    public FakeEmailNotifier(
        ILogger<FakeEmailNotifier> logger)
    {
        _logger = logger;
    }

    public async Task NotifyNewCommentAsync(
        string authorEmail,
        string postTitle,
        string commentText,
        CancellationToken ct)
    {
        _logger.LogInformation(
            "Starting email sending to {Email}...",
            authorEmail);

        await Task.Delay(800, ct);

        _logger.LogInformation(
            "EMAIL sent to {Email}: New comment on '{Title}': \"{Comment}\"",
            authorEmail,
            postTitle,
            commentText);
    }
}