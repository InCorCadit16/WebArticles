using DataModel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Data.Models
{
    public class ArticleModel
    {

        public long Id { get; set; }
        public string Title { get; set; }

        public int Rating { get; set; }

        public string Content { get; set; }

        public string[] Tags { get; set; }

        public DateTime PublichDate { get; set; }

        public long UserId { get; set; }

        public string UserName { get; set; }

        public Topic Topic { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
