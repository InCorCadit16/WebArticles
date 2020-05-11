using DataModel.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Data.DbConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.UserName).IsUnique();

            builder.HasOne(u => u.Writer)
                .WithOne(w => w.User).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.Reviewer)
                .WithOne(r => r.User).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
