using AppCore.Services;
using AppCore.Services.Base;
using AutoMapper.QueryableExtensions;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using WissAppEF.Contexts;
using WissAppEntities.Entities;
using WissAppWebApi.Models;

namespace WissAppWebApi.Controllers
{
    public class UsersMessagesController : ApiController
    {
        DbContext db = new WissAppContext();
        ServiceBase<UsersMessages> service;

        public UsersMessagesController()
        {
            service = new Service<UsersMessages>(db);
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                var query = service.GetEntityQuery(e => e.SenderId == id || e.ReceiverId == id).OrderBy(e => e.Messages.Date);

                return Ok(query.ProjectTo<UsersMessagesModel>(MappingConfig.mapperConfiguration).ToList());

            }
            catch (Exception exc)
            {

                return BadRequest();
            }
        }

    }
}