using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WebArticles.DataModel.Entities;

namespace DataModel.Data.DbConfig
{
    public class ArticleConfig : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasOne(a => a.Writer).WithMany(w => w.Articles).OnDelete(DeleteBehavior.Cascade);

            builder.Property(a => a.PublishDate);

            builder.Property(a => a.LastEditDate);
        }
    }
}
