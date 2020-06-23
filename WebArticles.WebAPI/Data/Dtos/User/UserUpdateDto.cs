using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Data.Dtos
{
    public class UserUpdateDto
    {
        public long Id { get; set; }

        public string ProfilePickLink { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }

        public int WriterRating { get; set; }
        public string WriterDescription { get; set; }
        public TopicDto[] WriterTopics { get; set; }

        public int ReviewerRating { get; set; }
        public string ReviewerDescription { get; set; }
        public TopicDto[] ReviewerTopics { get; set; }
    }
}
