using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Data.Dtos
{
    public class ArticlePreviewDto
    {
        public long Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        public int Rating { get; set; }

        public DateTime PublishDate { get; set; }

        [MaxLength(1000)]
        public string Overview { get; set; }

        [MaxLength(100)]
        public string Tags { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }

        public string TopicName { get; set; }
    }
}
