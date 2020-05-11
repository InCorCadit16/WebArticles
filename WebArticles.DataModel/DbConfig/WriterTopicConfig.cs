using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel.Data.Entities;

namespace DataModel.Data.DbConfig
{
    public class WriterTopicConfig : IEntityTypeConfiguration<WriterTopic>
    {
        public void Configure(EntityTypeBuilder<WriterTopic> builder)
        {
            builder.HasKey(wt => new { wt.WriterId, wt.TopicId });
        }
    }
}
