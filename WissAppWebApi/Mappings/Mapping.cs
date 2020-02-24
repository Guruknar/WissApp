using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WissAppWebApi // WissAppWebApi.Mappings
{
    public class Mapping
    {
        public static readonly Mapper mapper;

        static Mapping()
        {
            MappingConfig mappingConfig = new MappingConfig();
            mapper = new Mapper(MappingConfig.mapperConfiguration);
        }

    }
}