using BlogSystem.Application.Interfaces;
using BlogSystem.Domain.Entities;
using MediatR;

namespace BlogSystem.Application.Posts.Commands.CreatePost;

public class CreatePostHandler : IRequestHandler<CreatePostCommand, Guid>
{
    private readonly IPostRepository _repository;

    public CreatePostHandler(IPostRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(
        CreatePostCommand request,
        CancellationToken ct)
    {
        var post = new Post
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Body = request.Body,
            AuthorEmail = request.AuthorEmail
        };

        await _repository.AddAsync(post, ct);

        await _repository.SaveChangesAsync(ct);

        return post.Id;
    }
}