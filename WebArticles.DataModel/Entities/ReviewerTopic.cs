using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.Data.Entities
{
    public class ReviewerTopic
    {
        public long ReviewerId { get; set; }
        public long TopicId { get; set; }

        public Reviewer Reviewer { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
