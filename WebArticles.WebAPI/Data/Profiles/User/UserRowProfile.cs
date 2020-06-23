using AutoMapper;
using WebArticles.DataModel.Entities;
using System.Linq;
using WebArticles.WebAPI.Data.Dtos;

namespace WebArticles.WebAPI.Data.Profiles
{
    public class UserRowProfile : Profile
    {
        public UserRowProfile()
        {
            this.CreateMap<User, UserRowDto>()
                .ForMember(ur => ur.Articles, mapper => mapper.MapFrom(u => u.Writer.Articles.Count()))
                .ForMember(ur => ur.WriterRating, mapper => mapper.MapFrom(u => u.Writer.WriterRating))
                .ForMember(ur => ur.Reviewes, mapper => mapper.MapFrom(u => u.Reviewer.Comments.Count()))
                .ForMember(ur => ur.ReviewerRating, mapper => mapper.MapFrom(u => u.Reviewer.ReviewerRating));
        }
    }
}
