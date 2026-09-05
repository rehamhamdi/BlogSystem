using MediatR;

namespace BlogSystem.Application.Comments.Commands.CreateComment;

public record CreateCommentCommand(
    Guid PostId,
    string AuthorName,
    string Text
) : IRequest<Guid>;