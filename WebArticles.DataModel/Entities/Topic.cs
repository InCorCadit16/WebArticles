using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebArticles.DataModel.Entities
{
    public class Topic : Entity
    { 
        [Required]
        [MaxLength(120)]
        public string TopicName { get; set; }

        public ICollection<ReviewerTopic> ReviewersLink { get; set; }
        
        public ICollection<WriterTopic> WritersLink { get; set; }
    }
}
