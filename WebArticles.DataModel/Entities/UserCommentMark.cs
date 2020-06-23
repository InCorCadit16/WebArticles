using System;
using System.Collections.Generic;
using System.Text;

namespace WebArticles.DataModel.Entities
{
    public class UserCommentMark
    {
        public long UserId { get; set; }

        public long CommentId { get; set; }

        public bool Mark { get; set; }

        public User User { get; set; }

        public Comment Comment { get; set; }
    }
}
