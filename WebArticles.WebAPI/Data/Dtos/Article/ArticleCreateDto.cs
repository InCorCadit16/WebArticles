using WebArticles.DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Data.Dtos
{
    public class ArticleCreateDto
    {
        public string Title { get; set; }

        public long TopicId { get; set; }

        public string Overview { get; set; }

        public string Content { get; set; }

        public string[] Tags { get; set; }

        public long UserId { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
