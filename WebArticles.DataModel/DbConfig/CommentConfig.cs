using WebArticles.DataModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Data.DbConfig
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(c => c.Article).WithMany(a => a.Comments).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Reviewer).WithMany(r => r.Comments).OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.AnsweredCommentId).IsRequired(false);

            builder.Property(a => a.PublishDate);

            builder.Property(a => a.LastEditDate);
        }
    }
}
