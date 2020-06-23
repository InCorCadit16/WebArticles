using AutoMapper;
using WebArticles.DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dtos;

namespace WebArticles.WebAPI.Data.Profiles
{
    public class TopicProfile: Profile
    {
        public TopicProfile()
        {
            this.CreateMap<Topic, TopicDto>()
                .ReverseMap()
                .ForMember(t => t.WritersLink, mapper => mapper.Ignore())
                .ForMember(t => t.ReviewersLink, mapper => mapper.Ignore());
        }
    }
}
