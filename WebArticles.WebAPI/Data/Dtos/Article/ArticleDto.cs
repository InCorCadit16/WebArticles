using WebArticles.DataModel.Entities;
using System;
using System.Collections.Generic;

namespace WebArticles.WebAPI.Data.Dtos
{
    public class ArticleDto
    {

        public long Id { get; set; }
        public string Title { get; set; }

        public int Rating { get; set; }

        public string Content { get; set; }

        public string Overview { get; set; }

        public string[] Tags { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime? LastEditDate { get; set; }

        public long UserId { get; set; }

        public string UserName { get; set; }

        public TopicDto Topic { get; set; }

        public ICollection<CommentDto> Comments { get; set; }
    }
}
