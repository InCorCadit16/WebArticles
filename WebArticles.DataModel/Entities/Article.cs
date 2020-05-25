using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WebArticles.DataModel.Entities
{
    public class Article : Entity
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [NotMapped]
        public int Rating { 
            get {
                if (UserArticleMarks != null)
                    return UserArticleMarks.Select(uam => uam.Mark ? 1 : -1).Sum();
                else
                    return 0;
            }
        }

        [MaxLength(1000)]
        public string Overview { get; set; }

        [Required]
        public string Content { get; set; }

        [MaxLength(100)]
        public string Tags { get; set; }

        public long TopicId { get; set; }

        public long WriterId { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime? LastEditDate { get; set; }
        
        public Writer Writer { get; set; }

        public Topic Topic { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<UserArticleMark> UserArticleMarks { get; set; }
       
    }
}
