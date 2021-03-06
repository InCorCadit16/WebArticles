﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebArticles.DataModel.Entities;

namespace DataModel.Data.DbConfig
{
    public class WriterConfig : IEntityTypeConfiguration<Writer>
    {
        public void Configure(EntityTypeBuilder<Writer> builder)
        {
            builder.HasOne(w => w.User)
                .WithOne(u => u.Writer).OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(w => w.Articles)
                .WithOne(u => u.Writer).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
