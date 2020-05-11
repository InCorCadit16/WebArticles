using AutoMapper;
using DataModel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Models;
using WebArticles.WebAPI.Data.Services;

namespace WebArticles.WebAPI.Data.Profiles
{
    public class UserProfile: Profile
    {

        public UserProfile()
        {
            this.CreateMap<User, UserModel>()
                .ForMember(um => um.WriterRating, mapper => mapper.MapFrom(u => u.Writer.WriterRating))
                .ForMember(um => um.WriterDescription, mapper => mapper.MapFrom(u => u.Writer.WriterDescription))
                .ForMember(um => um.WriterTopics, mapper => mapper.MapFrom(u => u.Writer.TopicsLink.Select(tl => new Topic { TopicName = tl.Topic.TopicName, Id = tl.TopicId }).ToArray()))
                .ForMember(um => um.ReviewerRating, mapper => mapper.MapFrom(u => u.Reviewer.ReviewerRating))
                .ForMember(um => um.ReviewerDescription, mapper => mapper.MapFrom(u => u.Reviewer.ReviewerDescription))
                .ForMember(um => um.ReviewerTopics, mapper => mapper.MapFrom(u => u.Reviewer.TopicsLink.Select(tl =>new Topic { TopicName = tl.Topic.TopicName, Id = tl.TopicId }).ToArray()))
                .ReverseMap()
                .ForMember(u => u.UserName, mapper => mapper.Ignore())
                .ForMember(u => u.LockoutEnd, mapper => mapper.Ignore())
                .ForMember(u => u.TwoFactorEnabled, mapper => mapper.Ignore())
                .ForMember(u => u.PhoneNumber, mapper => mapper.Ignore())
                .ForMember(u => u.PhoneNumberConfirmed, mapper => mapper.Ignore())
                .ForMember(u => u.ConcurrencyStamp, mapper => mapper.Ignore())
                .ForMember(u => u.SecurityStamp, mapper => mapper.Ignore())
                .ForMember(u => u.PasswordHash, mapper => mapper.Ignore())
                .ForMember(u => u.EmailConfirmed, mapper => mapper.Ignore())
                .ForMember(u => u.NormalizedEmail, mapper => mapper.Ignore())
                .ForMember(u => u.NormalizedUserName, mapper => mapper.Ignore())
                .ForMember(u => u.LockoutEnabled, mapper => mapper.Ignore())
                .ForMember(u => u.AccessFailedCount, mapper => mapper.Ignore())
                .ForMember(u => u.Writer, mapper => mapper.Ignore())
                .ForMember(u => u.Reviewer, mapper => mapper.Ignore())
                .AfterMap((um, u) =>
                {
                    u.Writer.WriterDescription = um.WriterDescription;
                    u.Writer.TopicsLink = um.WriterTopics.Select(t => new WriterTopic { WriterId = u.Writer.Id, TopicId = t.Id }).ToList();
                    u.Reviewer.ReviewerDescription = um.ReviewerDescription;
                    u.Reviewer.TopicsLink = um.ReviewerTopics.Select(t => new ReviewerTopic { ReviewerId = u.Reviewer.Id, TopicId = t.Id }).ToList();
                });
                //.ForMember(u => u.Writer.TopicsLink, mapper => mapper.MapFrom(um => um.WriterTopics.Select(n => topicService.GetTopicByName(n))))
                //.ForMember(u => u.Reviewer.TopicsLink, mapper => mapper.MapFrom(um => um.ReviewerTopics.Select(n => topicService.GetTopicByName(n))));
        }
    }
}
