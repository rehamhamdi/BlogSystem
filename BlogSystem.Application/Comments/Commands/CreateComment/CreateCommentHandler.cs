using BlogSystem.Application.BackgroundJobs;
using BlogSystem.Application.Interfaces;
using BlogSystem.Domain.Entities;
using MediatR;

namespace BlogSystem.Application.Comments.Commands.CreateComment;

public class CreateCommentHandler
    : IRequestHandler<CreateCommentCommand, Guid>
{
    private readonly ICommentRepository _repository;
    private readonly IPostRepository _postRepository;
    private readonly IEmailNotifier _notifier;
    private readonly ICommentEmailQueue _queue;

    public CreateCommentHandler(
        ICommentRepository repository,
        IPostRepository postRepository,
        IEmailNotifier notifier,
        ICommentEmailQueue queue)
    {
        _repository = repository;
        _postRepository = postRepository;
        _notifier = notifier;
        _queue = queue;
    }

    public async Task<Guid> Handle(
        CreateCommentCommand request,
        CancellationToken ct)
    {
        var post = await _postRepository.GetByIdAsync(
            request.PostId,
            ct);

        if (post is null)
            throw new KeyNotFoundException("Post not found.");

        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            PostId = request.PostId,
            AuthorName = request.AuthorName,
            Text = request.Text
        };

        await _repository.AddAsync(comment, ct);

        await _repository.SaveChangesAsync(ct);


        //await _notifier.NotifyNewCommentAsync(
        //    post.AuthorEmail,
        //    post.Title,
        //    comment.Text,
        //    ct);

        //// Now handler became as a producer
        await _queue.EnqueueAsync(
    new CommentEmailJob(comment.Id),
    ct);

        return comment.Id;
    }
}