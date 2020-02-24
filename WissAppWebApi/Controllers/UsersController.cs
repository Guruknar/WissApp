using AppCore.Services;
using AppCore.Services.Base;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WissAppEF.Contexts;
using WissAppEntities.Entities;
using WissAppWebApi.Models;

namespace WissAppWebApi.Controllers
{
    public class UsersController : ApiController
    {
        DbContext db;
        ServiceBase<Users> userService;

        public UsersController()
        {
            db = new WissAppContext();
            userService = new Service<Users>(db);
        }

        public IHttpActionResult Get()
        {
            try
            {
                var entities = userService.GetEntities();
                //var model = entities.Select(e => new UsersModel()
                //{
                //    Id = e.Id,
                //    RoleId = e.RoleId,
                //    UserName = e.UserName,
                //    Password = e.Password,
                //    E_Mail = e.E_Mail,
                //    School = e.School,
                //    Location = e.Location,
                //    BirthDate =e.BirthDate,
                //    Gender = e.Gender,
                //    IsActive = e.IsActive
                //}).ToList();
                //var model = Mapping.mapper.Map<List<Users>, List<UsersModel>>(entities); //source-destination, neyi değiştirmek istiyorsan(Üstteki işlemi bu şekilde automapper sayesinde yapıyoruz) 1. Kullanımı bu
                //var model = Mapping.mapper.Map<List<UsersModel>>(entities);// başka bir kullanımı
                var model = userService.GetEntityQuery().ProjectTo<UsersModel>(MappingConfig.mapperConfiguration).ToList();//başkabir kullanım daha
                return Ok(model);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}
