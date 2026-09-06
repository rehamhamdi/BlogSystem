using System.Threading.Channels;
using BlogSystem.Application.BackgroundJobs;
using BlogSystem.Application.Interfaces;

namespace BlogSystem.Infrastructure.BackgroundJobs;

public class CommentEmailQueue : ICommentEmailQueue
{
    private readonly Channel<CommentEmailJob> _channel;

    public CommentEmailQueue()
    {
        _channel = Channel.CreateBounded<CommentEmailJob>(
            new BoundedChannelOptions(100)
            {
                FullMode = BoundedChannelFullMode.Wait //producer wait untill channel has free space 
            });
    }

    public async ValueTask EnqueueAsync(
        CommentEmailJob job,
        CancellationToken ct)
    {
        await _channel.Writer.WriteAsync(job, ct);
    }

    public async ValueTask<CommentEmailJob> DequeueAsync(
        CancellationToken ct)
    {
        return await _channel.Reader.ReadAsync(ct);
    }
}