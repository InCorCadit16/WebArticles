using AutoMapper;
using DataModel.Data.Entities;
using WebArticles.WebAPI.Data.Models;

namespace WebArticles.WebAPI.Data.Profiles
{
    public class CommentProfile: Profile
    {
        public CommentProfile()
        {
            this.CreateMap<Comment, CommentModel>()
                .ForMember(cm => cm.UserId, mapper => mapper.MapFrom(c => c.Reviewer.UserId))
                .ForMember(cm => cm.ArticleId, mapper => mapper.MapFrom(c => c.ArticleId))
                .ForMember(cm => cm.ArticleTitle, mapper => mapper.MapFrom(c => c.Article.Title))
                .ForMember(cm => cm.UserName, mapper => mapper.MapFrom(c => c.Reviewer.User.UserName));
            
        }
    }
}
