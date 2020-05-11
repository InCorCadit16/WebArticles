using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataModel.Data.Entities;

namespace DataModel.Data.DbConfig
{
    public class ArticleConfig : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasMany(a => a.Comments)
                   .WithOne(c => c.Article).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
