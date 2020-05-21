using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataModel.Data.Entities
{
    public class Comment : Entity
    {
        public int Rating { get; set; }

        [Required]
        public string Content { get; set; }

        public long ArticleId { get; set; }

        public long ReviewerId { get; set; }

        public long? AnsweredCommentId { get; set; }

        public DateTime PublishDate { get; set; }

        public Reviewer Reviewer { get; set; }

        public Article Article { get; set; }

        public Comment AnsweredComment { get; set; } 
    }
}
