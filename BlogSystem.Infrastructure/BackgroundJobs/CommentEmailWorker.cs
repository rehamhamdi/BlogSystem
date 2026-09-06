using BlogSystem.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BlogSystem.Infrastructure.BackgroundJobs;

public class CommentEmailWorker : BackgroundService
{
    private readonly ICommentEmailQueue _queue;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<CommentEmailWorker> _logger;

    public CommentEmailWorker(
        ICommentEmailQueue queue,
        IServiceScopeFactory scopeFactory,
        ILogger<CommentEmailWorker> logger)
    {
        _queue = queue;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "Comment Email Worker started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            var job = await _queue.DequeueAsync(
                stoppingToken);

            using var scope = _scopeFactory.CreateScope();

            var repository =
                scope.ServiceProvider
                    .GetRequiredService<ICommentRepository>();

            var notifier =
                scope.ServiceProvider
                    .GetRequiredService<IEmailNotifier>();

            var comment = await repository.GetByIdAsync(
                job.CommentId,
                stoppingToken);

            if (comment is null)
            {
                _logger.LogWarning(
                    "Comment {CommentId} was not found.",
                    job.CommentId);

                continue;
            }

            if (comment.Post is null)
            {
                _logger.LogWarning(
                    "Post for Comment {CommentId} was not found.",
                    job.CommentId);

                continue;
            }

            await notifier.NotifyNewCommentAsync(
                comment.Post.AuthorEmail,
                comment.Post.Title,
                comment.Text,
                stoppingToken);

            _logger.LogInformation(
                "Email job completed for Comment {CommentId}.",
                job.CommentId);
        }
    }
}