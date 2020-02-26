using AutoMapper;
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
                c.AddProfile<UsersModelProfile>();
                c.AddProfile<RolesProfile>();
            });
        }

    }

    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<Users, UsersModel>()
                .ForMember(d => d.Password, o => o.Ignore())
                .ForMember(d => d.Role, o => o.MapFrom(s => s.Roles.Name));
        }
    }

    public class UsersModelProfile : Profile
    {
        public UsersModelProfile()
        {
            CreateMap<UsersModel, Users>();
        }
    }

    public class RolesProfile :Profile
    {
        public RolesProfile()
        {
            CreateMap<Roles, RolesModel>().ReverseMap();
        }
    }

}