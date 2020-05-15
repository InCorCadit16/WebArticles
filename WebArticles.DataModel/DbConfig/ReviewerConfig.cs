using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel.Data.Entities;

namespace DataModel.Data.DbConfig
{
    public class ReviewerConfig : IEntityTypeConfiguration<Reviewer>
    {
        public void Configure(EntityTypeBuilder<Reviewer> builder)
        {
            builder.HasOne(r => r.User)
               .WithOne(u => u.Reviewer).OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(r => r.Comments)
                .WithOne(c => c.Reviewer).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
