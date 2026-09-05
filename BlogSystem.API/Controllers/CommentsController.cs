using BlogSystem.Application.Comments.Commands.CreateComment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.API.Controllers;

[ApiController]
[Route("posts/{postId}/comments")]
public class CommentsController : ControllerBase
{
    private readonly ISender _sender;

    public CommentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        Guid postId,
        CreateCommentBody body,
        CancellationToken ct)
    {
        var command = new CreateCommentCommand(
            postId,
            body.AuthorName,
            body.Text);

        var commentId = await _sender.Send(command, ct);

        return Created(
            $"/posts/{postId}/comments/{commentId}",
            new { Id = commentId });
    }
}

public record CreateCommentBody(
    string AuthorName,
    string Text);