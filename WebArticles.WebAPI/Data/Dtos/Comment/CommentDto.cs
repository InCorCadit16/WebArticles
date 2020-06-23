using System;

namespace WebArticles.WebAPI.Data.Dtos
{
    public class CommentDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int Rating { get; set; }
        public string UserName { get; set; }

        public long ArticleId { get; set; }

        public string ArticleTitle { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime? LastEditDate { get; set; }
    }
}
