using AutoMapper;
using DataModel.Data.Entities;
using WebArticles.WebAPI.Data.Dto;

namespace WebArticles.WebAPI.Data.Profiles
{
    public class ArticlePreviewProfile: Profile
    {
        public ArticlePreviewProfile()
        {
            this.CreateMap<Article, ArticlePreview>()
                .ForMember(ap => ap.UserName, mapper => mapper.MapFrom(a => a.Writer.User.UserName))
                .ForMember(ap => ap.UserId, mapper => mapper.MapFrom(a => a.Writer.User.Id))
                .ForMember(ap => ap.TopicName, mapper => mapper.MapFrom(a => a.Topic.TopicName));
        }
    }
}
