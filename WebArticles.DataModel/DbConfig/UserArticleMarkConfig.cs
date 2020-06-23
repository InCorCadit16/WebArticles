using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.InteropServices;
using WebArticles.DataModel.Entities;

namespace WebArticles.DataModel.DbConfig
{
    class UserArticleMarkConfig : IEntityTypeConfiguration<UserArticleMark>
    {
        public void Configure(EntityTypeBuilder<UserArticleMark> builder)
        {
            builder.HasKey(wam => new { wam.UserId, wam.ArticleId });

            builder.HasOne(wam => wam.Article)
                .WithMany(a => a.UserArticleMarks).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(wam => wam.User)
                .WithMany(u => u.UserArticleMarks).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
