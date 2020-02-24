﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WissAppEntities.Entities;
using WissAppWebApi.Models;

namespace WissAppWebApi // WissAppWebApi.Configs
{
    public class MappingConfig
    {
        public static readonly MapperConfiguration mapperConfiguration; // Mapping işlemleri

        static MappingConfig()
        {
            mapperConfiguration = new MapperConfiguration( c =>
            {
                c.AddProfile<UsersProfile>();
            });
        }

    }

    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<Users, UsersModel>().ReverseMap();
        }
    }

}