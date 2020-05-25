using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebArticles.DataModel.Entities
{
    public class User : IdentityUser<long>
    { 

        [MaxLength(500)]
        public string ProfilePickLink { get; set; }
        
        [MinLength(2)]
        [MaxLength(30)]
        public string FirstName { get; set; }
        
        [MinLength(2)]
        [MaxLength(30)]
        public string LastName { get; set; }
        
        [MinLength(4)]
        [MaxLength(30)]
        [Required]
        public override string UserName { get; set; }
        
        [MinLength(30)]
        [Required]
        [EmailAddress]
        public override string Email { get; set; }
        
        public DateTime? BirthDate { get; set; }

        public Writer Writer { get; set; }
        public Reviewer Reviewer { get; set; }

        public bool ExternalProvider { get; set; }

        public ICollection<UserArticleMark> UserArticleMarks { get; set; }
        public ICollection<UserCommentMark> UserCommentMarks { get; set; }
    }
}
