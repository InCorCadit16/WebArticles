using AutoMapper;
using DataModel.Data.Entities;
using WebArticles.WebAPI.Data.Dtos;

namespace WebArticles.WebAPI.Data.Profiles
{
    public class ArticleUpdateProfile : Profile
    {
        public ArticleUpdateProfile()
        {
            this.CreateMap<ArticleUpdateDto, Article>()
                .ForMember(a => a.Rating, mapper => mapper.Ignore())
                .ForMember(a => a.Overview, mapper => mapper.Ignore())
                .ForMember(a => a.Writer, mapper => mapper.Ignore())
                .ForMember(a => a.PublishDate, mapper => mapper.Ignore())
                .ForMember(a => a.Comments, mapper => mapper.Ignore())
                .ForMember(a => a.Tags, mapper => mapper.MapFrom(am => "#" + string.Join('#', am.Tags)));
        }
    }
}
