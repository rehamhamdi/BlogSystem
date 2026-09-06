using BlogSystem.Application.BackgroundJobs;

namespace BlogSystem.Application.Interfaces;

public interface ICommentEmailQueue
{
    ValueTask EnqueueAsync(
        CommentEmailJob job,
        CancellationToken ct);

    ValueTask<CommentEmailJob> DequeueAsync(
        CancellationToken ct);
}