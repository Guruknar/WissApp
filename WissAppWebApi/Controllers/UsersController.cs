using AppCore.Services;
using AppCore.Services.Base;
using AutoMapper.QueryableExtensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using WissAppEF.Contexts;
using WissAppEntities.Entities;
using WissAppWebApi.Attributes;
using WissAppWebApi.Configs;
using WissAppWebApi.Models;

namespace WissAppWebApi.Controllers
{
    [RoutePrefix("api/Users")]
    //[ClaimsAuthorize(ClaimType = "role", ClaimValue = "Admin,User")]
    [ClaimsAuthorize(ClaimType = "role", ClaimValue = "Admin")]
    public class UsersController : ApiController
    {
        DbContext db;
        ServiceBase<Users> userService;

        public UsersController()
        {
            db = new WissAppContext();
            userService = new Service<Users>(db);
        }

        //[AllowAnonymous]
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

        public IHttpActionResult Get(int id)
        {
            try
            {
                var entity = userService.GetEntity(id);
                var model = Mapping.mapper.Map<UsersModel>(entity);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();

            }
        }

        public IHttpActionResult Post(UsersModel usersModel)
        {
            try
            {
                var entity = Mapping.mapper.Map<Users>(usersModel);
                userService.AddEntity(entity);
                var model = Mapping.mapper.Map<UsersModel>(entity);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(); 
            }
        }

        public IHttpActionResult Put(UsersModel usersModel)
        {
            try
            {
                var entity = userService.GetEntity(usersModel.Id);
                entity.BirthDate = usersModel.BirthDate;
                entity.E_Mail = usersModel.E_Mail;
                entity.Gender = usersModel.Gender;
                entity.IsActive = usersModel.IsActive;
                entity.Location = usersModel.Location;
                entity.Password = usersModel.Password;
                entity.RoleId = usersModel.RoleId;
                entity.School = usersModel.School;
                entity.UserName = usersModel.UserName;

                userService.UpdateEntity(entity);
                var model = Mapping.mapper.Map<UsersModel>(entity);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                var entity = userService.GetEntity(id);
                userService.DeleteEntity(id);
                var model = Mapping.mapper.Map<UsersModel>(entity);
                return Ok(model);
            }
            catch (Exception exc)
            {

                return BadRequest();
            }
        }
        
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            try
            {
                var entities = userService.GetEntities();
                var resultEntities = JsonConvert.SerializeObject(entities, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });// ****-******_*****
                return Ok(JsonConvert.DeserializeObject(resultEntities));
            }
            catch (Exception exc)
            {

                return BadRequest();
            }
        }

        [Route("Logout")]
        [HttpGet]
        public IHttpActionResult Logout()
        {
            var principal = RequestContext.Principal as ClaimsPrincipal;
            if(principal.Identity.IsAuthenticated)
            {
                UserConfig.AddLoggedOutUser(principal.FindFirst(e => e.Type == "user").Value);
                return Ok("User logged out.");
            }
            return BadRequest("User didn't login.");
            
        }

        [Route("LoggedoutUsers")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult LoggedoutUsers()
        {
            return Ok(UserConfig.GetLoggedOutUsers());
        }
    }
}
