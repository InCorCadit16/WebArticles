using AutoMapper;
using WebArticles.DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dtos;

namespace WebArticles.WebAPI.Data.Profiles
{
    public class UserUpdateProfile: Profile
    {
        public UserUpdateProfile()
        {
            this.CreateMap<UserUpdateDto, User>()
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
                .AfterMap((ud, u) =>
                {
                    u.Writer.WriterDescription = ud.WriterDescription;
                    u.Writer.TopicsLink = ud.WriterTopics.Select(t => new WriterTopic { WriterId = u.Writer.Id, TopicId = t.Id }).ToList();
                    u.Reviewer.ReviewerDescription = ud.ReviewerDescription;
                    u.Reviewer.TopicsLink = ud.ReviewerTopics.Select(t => new ReviewerTopic { ReviewerId = u.Reviewer.Id, TopicId = t.Id }).ToList();
                });
        }
    }
}
