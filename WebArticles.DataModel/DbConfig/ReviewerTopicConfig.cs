using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel.Data.Entities;

namespace DataModel.Data.DbConfig
{
    public class ReviewerTopicConfig : IEntityTypeConfiguration<ReviewerTopic>
    {
        public void Configure(EntityTypeBuilder<ReviewerTopic> builder)
        {
           builder.HasKey(rt => new { rt.ReviewerId, rt.TopicId });
        }
    }
}
