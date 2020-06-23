using System;
using System.Collections.Generic;
using System.Text;

namespace WebArticles.DataModel.Entities
{
    public class UserArticleMark
    {
        public long UserId { get; set; }

        public long ArticleId { get; set; }

        public bool Mark { get; set; }

        public User User { get; set; }

        public Article Article { get; set; }        
    }
}
