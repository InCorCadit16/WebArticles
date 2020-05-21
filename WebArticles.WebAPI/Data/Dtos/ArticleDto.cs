using DataModel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Data.Dtos
{
    public class ArticleDto
    {

        public long Id { get; set; }
        public string Title { get; set; }

        public int Rating { get; set; }

        public string Content { get; set; }

        public string[] Tags { get; set; }

        public DateTime PublishDate { get; set; }

        public long UserId { get; set; }

        public string UserName { get; set; }

        public TopicDto Topic { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
