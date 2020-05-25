using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WebArticles.DataModel.Entities
{
    public class Comment : Entity
    {
        [NotMapped]
        public int Rating
        {
            get
            {
                if (UserCommentMarks != null)
                    return UserCommentMarks.Select(uam => uam.Mark ? 1 : -1).Sum();
                else
                    return 0;
            }
        }

        [Required]
        public string Content { get; set; }

        public long ArticleId { get; set; }

        public long ReviewerId { get; set; }

        public long? AnsweredCommentId { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime? LastEditDate { get; set; }

        public Reviewer Reviewer { get; set; }

        public Article Article { get; set; }

        public Comment AnsweredComment { get; set; } 

        public ICollection<UserCommentMark> UserCommentMarks { get; set; }
    }
}
