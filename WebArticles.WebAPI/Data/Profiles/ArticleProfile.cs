using AutoMapper;
using DataModel.Data.Entities;
using WebArticles.WebAPI.Data.Dtos;

namespace WebArticles.WebAPI.Data.Profiles
{
    public class ArticleProfile: Profile
    {
        public ArticleProfile()
        {
            this.CreateMap<Article, ArticleDto>()
                .ForMember(am => am.UserId, mapper => mapper.MapFrom(a => a.Writer.UserId))
                .ForMember(am => am.UserName, mapper => mapper.MapFrom(a => a.Writer.User.UserName))
                .ForMember(am => am.Tags, mapper => mapper.MapFrom(a => a.Tags.Substring(1).Split("#", System.StringSplitOptions.None)));
        }
    }
}
