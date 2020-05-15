using AutoMapper;
using DataModel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Models;

namespace WebArticles.WebAPI.Data.Profiles
{
    public class UserRowProfile : Profile
    {
        public UserRowProfile()
        {
            this.CreateMap<User, UserRow>()
                .ForMember(ur => ur.Articles, mapper => mapper.MapFrom(u => u.Writer.Articles.Count()))
                .ForMember(ur => ur.WriterRating, mapper => mapper.MapFrom(u => u.Writer.WriterRating))
                .ForMember(ur => ur.Reviewes, mapper => mapper.MapFrom(u => u.Reviewer.Comments.Count()))
                .ForMember(ur => ur.ReviewerRating, mapper => mapper.MapFrom(u => u.Reviewer.ReviewerRating));
        }
    }
}
