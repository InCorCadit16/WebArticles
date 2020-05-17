using AutoMapper;
using DataModel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dto;

namespace WebArticles.WebAPI.Data.Profiles
{
    public class ExternalSignInProfile: Profile
    {
        public ExternalSignInProfile()
        {
            this.CreateMap<ExternalSignInQuery, User>();
        }
    }
}
