using BlogSystem.Application.Posts.Commands.CreatePost;
using BlogSystem.Infrastructure.BackgroundJobs.HangfireJobs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.API.Controllers;

[ApiController]
[Route("posts")]
public class PostsController : ControllerBase
{
    private readonly ISender _sender;

    public PostsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreatePostCommand command,
        CancellationToken ct)
    {
        var postId = await _sender.Send(command, ct);

        return Created(
            $"/posts/{postId}",
            new { Id = postId });
    }

    [HttpPost("test-hangfire")]
    public IActionResult TestHangfire()
    {
        BackgroundJob.Enqueue(() =>
            Console.WriteLine(
                $"Hangfire Job executed at {DateTime.UtcNow}"));

        return Ok("Job queued.");
    }

    [HttpPost("test-hangfire-retry")]
    public IActionResult TestHangfireRetry()
    {
        BackgroundJob.Enqueue<TestRetryJob>(
            job => job.Execute());

        return Ok("Retry job queued.");
    }
}