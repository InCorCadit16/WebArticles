using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebArticles.DataModel.Entities;

namespace WebArticles.DataModel.DbConfig
{
    class UserCommentMarkConfig : IEntityTypeConfiguration<UserCommentMark>
    {
        public void Configure(EntityTypeBuilder<UserCommentMark> builder)
        {
            builder.HasKey(wcm => new { wcm.UserId, wcm.CommentId });

            builder.HasOne(wcm => wcm.Comment)
                .WithMany(c => c.UserCommentMarks).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(wcm => wcm.User)
                .WithMany(u => u.UserCommentMarks).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
