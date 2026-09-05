using BlogSystem.Application.Interfaces;
using BlogSystem.Domain.Entities;
using BlogSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly BlogDbContext _db;

    public PostRepository(BlogDbContext db)
    {
        _db = db;
    }

    public Task<Post?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return _db.Posts
            .FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public async Task AddAsync(Post post, CancellationToken ct)
    {
        await _db.Posts.AddAsync(post, ct);
    }

    public Task SaveChangesAsync(CancellationToken ct)
    {
        return _db.SaveChangesAsync(ct);
    }
}