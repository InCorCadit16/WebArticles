using AutoMapper;
using DataModel.Data.Entities;
using System;
using WebArticles.WebAPI.Data.Dtos;

namespace WebArticles.WebAPI.Data.Profiles
{
    public class ArticleCreateProfile: Profile
    {
        public ArticleCreateProfile()
        {
            this.CreateMap<ArticleCreateDto, Article>()
                .ForMember(a => a.Rating, mapper => mapper.MapFrom(ac => 0))
                .ForMember(a => a.PublishDate, mapper => mapper.MapFrom(ac => DateTime.Now))
                .ForMember(a => a.Tags, mapper => mapper.MapFrom(ac => "#" + string.Join('#', ac.Tags)));
        }
    }
}
