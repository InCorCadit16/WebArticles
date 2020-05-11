using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataModel.Data.Entities
{
    public class Article : Entity
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public int Rating { get; set; }

        [MaxLength(1000)]
        public string Overview { get; set; }

        [Required]
        public string Content { get; set; }

        [MaxLength(100)]
        public string Tags { get; set; }

        public long TopicId { get; set; }

        public long WriterId { get; set; }

        public DateTime PublichDate { get; set; }
        
        public Writer Writer { get; set; }

        public Topic Topic { get; set; }

        public ICollection<Comment> Comments { get; set; }
       
    }
}
