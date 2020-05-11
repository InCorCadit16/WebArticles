using AutoMapper;
using DataModel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dto;

namespace WebArticles.WebAPI.Data.Profiles
{
    public class CommentCreateProfile: Profile
    {
        public CommentCreateProfile()
        {
            this.CreateMap<CommentCreate, Comment>()
                .ForMember(c => c.Rating, mapper => mapper.MapFrom(cm => 0))
                .ForMember(c => c.PublichDate, mapper => mapper.MapFrom(cm => DateTime.Now));
        }
    }
}
