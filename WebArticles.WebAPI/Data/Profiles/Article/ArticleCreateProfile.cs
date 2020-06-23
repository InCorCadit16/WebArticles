using AutoMapper;
using WebArticles.DataModel.Entities;
using System;
using WebArticles.WebAPI.Data.Dtos;

namespace WebArticles.WebAPI.Data.Profiles
{
    public class ArticleCreateProfile: Profile
    {
        public ArticleCreateProfile()
        {
            this.CreateMap<ArticleCreateDto, Article>()
                .ForMember(a => a.Tags, mapper => mapper.MapFrom(ac => "#" + string.Join('#', ac.Tags)));
        }
    }
}
