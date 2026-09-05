using System;
using System.Collections.Generic;
using System.Text;

namespace BlogSystem.Domain.Entities
{
    public class Post
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public string AuthorEmail { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<Comment> Comments { get; set; } = new();
    }
}
