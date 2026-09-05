using MediatR;

namespace BlogSystem.Application.Posts.Commands.CreatePost;

public record CreatePostCommand(
    string Title,
    string Body,
    string AuthorEmail
) : IRequest<Guid>;