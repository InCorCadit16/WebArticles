﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WebArticles.DataModel.Entities
{
    public class Writer : Entity
    {
        [NotMapped]
        public int WriterRating { 
            get {
                if (Articles == null)
                    return 0;
                else
                    return Articles.Sum(a => a.Rating);
            } 
        }

        [MaxLength(2000)]
        public string WriterDescription { get; set; }

        public virtual ICollection<WriterTopic> TopicsLink { get; set; }

        public long? UserId { get; set; }

        public User User { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
