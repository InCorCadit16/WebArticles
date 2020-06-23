using AutoMapper;
using WebArticles.DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dtos;
using WebArticles.WebAPI.Data.Services;

namespace WebArticles.WebAPI.Data.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            this.CreateMap<User, UserDto>()
                .ForMember(um => um.WriterRating, mapper => mapper.MapFrom(u => u.Writer.WriterRating))
                .ForMember(um => um.WriterDescription, mapper => mapper.MapFrom(u => u.Writer.WriterDescription))
                .ForMember(um => um.WriterTopics, mapper => mapper.MapFrom(u => u.Writer.TopicsLink.Select(tl => new Topic { TopicName = tl.Topic.TopicName, Id = tl.TopicId }).ToArray()))
                .ForMember(um => um.ReviewerRating, mapper => mapper.MapFrom(u => u.Reviewer.ReviewerRating))
                .ForMember(um => um.ReviewerDescription, mapper => mapper.MapFrom(u => u.Reviewer.ReviewerDescription))
                .ForMember(um => um.ReviewerTopics, mapper => mapper.MapFrom(u => u.Reviewer.TopicsLink.Select(tl =>new Topic { TopicName = tl.Topic.TopicName, Id = tl.TopicId }).ToArray()));
        }
    }
}
