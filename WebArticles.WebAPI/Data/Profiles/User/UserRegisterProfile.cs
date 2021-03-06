﻿using AutoMapper;
using WebArticles.DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dtos;

namespace WebArticles.WebAPI.Data.Profiles
{
    public class UserRegisterProfile: Profile
    {

        public UserRegisterProfile()
        {
            this.CreateMap<UserRegisterQueryDto, User>();
        }
    }
}
