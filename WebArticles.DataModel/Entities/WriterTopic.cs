using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataModel.Data.Entities
{
    public class WriterTopic
    {
        public long WriterId { get; set; }
        
        public long TopicId { get; set; }

        public Writer Writer { get; set; }

        public virtual Topic Topic { get; set; }
    }
}
