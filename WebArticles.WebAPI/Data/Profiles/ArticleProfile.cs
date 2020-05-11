using AutoMapper;
using DataModel.Data.Entities;
using WebArticles.WebAPI.Data.Models;

namespace WebArticles.WebAPI.Data.Profiles
{
    public class ArticleProfile: Profile
    {

        public ArticleProfile()
        {
            this.CreateMap<Article, ArticleModel>()
                .ForMember(am => am.UserId, mapper => mapper.MapFrom(a => a.Writer.UserId))
                .ForMember(am => am.UserName, mapper => mapper.MapFrom(a => a.Writer.User.UserName))
                .ForMember(am => am.Tags, mapper => mapper.MapFrom(a => a.Tags.Substring(1).Split("#", System.StringSplitOptions.None)))
                .ReverseMap()
                .ForMember(a => a.Rating, mapper => mapper.Ignore())
                .ForMember(a => a.Overview, mapper => mapper.Ignore())
                .ForMember(a => a.Writer, mapper => mapper.Ignore())
                .ForMember(a => a.PublichDate, mapper => mapper.Ignore())
                .ForMember(a => a.Comments, mapper => mapper.Ignore())
                .ForMember(a => a.Tags, mapper => mapper.MapFrom(am => "#" + string.Join('#',am.Tags)));

        }
    }
}
