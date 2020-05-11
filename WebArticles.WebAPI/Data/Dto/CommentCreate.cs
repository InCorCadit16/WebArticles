using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Data.Dto
{
    public class CommentCreate
    {
        public long UserId { get; set; }

        public long ReviewerId { get; set; }

        public long ArticleId { get; set; }

        public string Content { get; set; }
    }
}
