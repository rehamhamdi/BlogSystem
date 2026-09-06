using BlogSystem.Application.Interfaces;
using BlogSystem.Domain.Entities;
using BlogSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly BlogDbContext _db;

    public CommentRepository(BlogDbContext db)
    {
        _db = db;
    }

    public async Task<Comment?> GetByIdAsync(
     Guid id,
     CancellationToken ct)
    {
        return await _db.Comments
            .Include(c => c.Post)
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task AddAsync(Comment comment, CancellationToken ct)
    {
        await _db.Comments.AddAsync(comment, ct);
    }

    public Task SaveChangesAsync(CancellationToken ct)
    {
        return _db.SaveChangesAsync(ct);
    }
}