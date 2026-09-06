public interface IEmailNotifier
{
    Task NotifyNewCommentAsync(
        string authorEmail,
        string postTitle,
        string commentText,
        CancellationToken ct);
}