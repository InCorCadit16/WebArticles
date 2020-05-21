using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Data.Dtos
{
    public class CommentUpdateDto
    {
        public long Id { get; set; }

        public string NewContent { get; set; }
    }
}
