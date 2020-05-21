using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Data.Dtos
{
    public class CommentCreateDto
    {
        public long UserId { get; set; }

        public long ReviewerId { get; set; }

        public long ArticleId { get; set; }

        public string Content { get; set; }
    }
}
