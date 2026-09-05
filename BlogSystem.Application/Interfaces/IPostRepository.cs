using BlogSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogSystem.Application.Interfaces
{
    public interface IPostRepository
    {
        Task<Post?> GetByIdAsync(Guid id, CancellationToken ct);
        Task AddAsync(Post post, CancellationToken ct);
        Task SaveChangesAsync(CancellationToken ct);

    }
}
