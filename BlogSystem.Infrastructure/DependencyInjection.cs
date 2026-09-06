using BlogSystem.Application.Interfaces;
using BlogSystem.Infrastructure.BackgroundJobs;
using BlogSystem.Infrastructure.BackgroundJobs.HangfireJobs;
using BlogSystem.Infrastructure.Data;
using BlogSystem.Infrastructure.Email;
using BlogSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<BlogDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("Blog")));

        services.AddScoped<IPostRepository, PostRepository>();

        services.AddScoped<ICommentRepository, CommentRepository>();

        services.AddSingleton<IEmailNotifier, FakeEmailNotifier>();

        services.AddSingleton<ICommentEmailQueue, CommentEmailQueue>(); // singleton to make all the requests use the queue
        services.AddHostedService<CommentEmailWorker>();
        services.AddTransient<TestRetryJob>();
        return services;
    }
}