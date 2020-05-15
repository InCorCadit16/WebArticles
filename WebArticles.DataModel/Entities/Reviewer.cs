using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DataModel.Data.Entities
{
    public class Reviewer : Entity
    {
        [NotMapped]
        public int ReviewerRating { 
            get
            {
                if (Comments == null)
                    return 0;
                else
                    return Comments.Sum(c => c.Rating);
            }
        }

        [MaxLength(2000)]
        public string ReviewerDescription { get; set; }

        public virtual ICollection<ReviewerTopic> TopicsLink { get; set; }

        public long? UserId { get; set; }

        public User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
