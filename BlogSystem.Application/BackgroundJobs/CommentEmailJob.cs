namespace BlogSystem.Application.BackgroundJobs;

public record CommentEmailJob(
    Guid CommentId);