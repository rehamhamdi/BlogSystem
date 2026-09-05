using BlogSystem.Application.Interfaces;
using BlogSystem.Domain.Entities;
using MediatR;

namespace BlogSystem.Application.Comments.Commands.CreateComment;

public class CreateCommentHandler
    : IRequestHandler<CreateCommentCommand, Guid>
{
    private readonly ICommentRepository _repository;

    public CreateCommentHandler(ICommentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(
        CreateCommentCommand request,
        CancellationToken ct)
    {
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            PostId = request.PostId,
            AuthorName = request.AuthorName,
            Text = request.Text
        };

        await _repository.AddAsync(comment, ct);
        await _repository.SaveChangesAsync(ct);

        return comment.Id;
    }
}