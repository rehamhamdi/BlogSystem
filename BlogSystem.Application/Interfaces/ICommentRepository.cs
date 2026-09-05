using BlogSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogSystem.Application.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment?> GetByIdAsync(Guid id, CancellationToken ct);
        Task AddAsync(Comment comment, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);

    }
}
