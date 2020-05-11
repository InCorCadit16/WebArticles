using AutoMapper;
using DataModel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dto;

namespace WebArticles.WebAPI.Data.Profiles
{
    public class ArticleCreateProfile: Profile
    {
        public ArticleCreateProfile()
        {
            this.CreateMap<ArticleCreate, Article>()
                .ForMember(a => a.Rating, mapper => mapper.MapFrom(ac => 0))
                .ForMember(a => a.PublichDate, mapper => mapper.MapFrom(ac => DateTime.Now))
                .ForMember(a => a.WriterId, mapper => mapper.MapFrom(ac => ac.UserId))
                .ForMember(a => a.Tags, mapper => mapper.MapFrom(ac => "#" + string.Join('#', ac.Tags)));
        }
    }
}
