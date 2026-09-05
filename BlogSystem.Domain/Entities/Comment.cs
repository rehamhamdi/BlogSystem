using System;
using System.Collections.Generic;
using System.Text;

namespace BlogSystem.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public string AuthorName { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //Navigation
        public Post? Post { get; set; }

    }
}
