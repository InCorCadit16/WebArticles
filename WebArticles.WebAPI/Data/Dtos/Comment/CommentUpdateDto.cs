using System;

namespace WebArticles.WebAPI.Data.Dtos
{
    public class CommentUpdateDto
    {
        public long Id { get; set; }

        public string NewContent { get; set; }

        public DateTime LastEditDate { get; set; }
    }
}
