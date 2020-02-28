using AppCore.Services;
using AppCore.Services.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WissAppEF.Contexts;
using WissAppEntities.Entities;
using WissAppMvc.Models;
using WissAppMvc.Models.ViewModels;
using WissAppMvc.Utils;

namespace WissAppMvc.Controllers
{
    public class HomeController : Controller
    {
        DbContext db = new WissAppContext();
        ServiceBase<Users> userService;
        ServiceBase<UsersMessages> userMessagesService;

        public HomeController()
        {
            userService = new Service<Users>(db);
            userMessagesService = new Service<UsersMessages>(db);
        }

        [Authorize]
        public ActionResult Index()
        {
            var userMessages = userMessagesService.GetEntities(e => e.Senders.UserName == User.Identity.Name || e.Receivers.UserName == User.Identity.Name || e.Receivers == null);

            var users = userMessages.Select(e => e.Receivers == null ? new UsersModel
            {
                UserId = e.SenderId,
                UserName = e.Senders.UserName,
                Broadcast = true,
            } : (e.Receivers.UserName == User.Identity.Name ? new UsersModel
            {
                UserId = e.SenderId,
                UserName = e.Senders.UserName

            } : new UsersModel
            {
                UserId = e.ReceiverId ?? 0,
                UserName = e.Receivers.UserName,
            })).ToList();
            //users = users.Where(e => e.Broadcast == false).Distinct(new UsersModelComparer()).ToList();//utils comparer class'ı
            users = users.Where(e => e.Broadcast == false).GroupBy(e => new
            {
                e.UserId,
                e.UserName
            }).Select(e => new UsersModel
            {
                UserId = e.Key.UserId,
                UserName = e.Key.UserName,
                MessageCount = e.Count()
            }).ToList();

            var model = new HomeIndexViewModel()
            {
                Users = users,
            };
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [HandleError]
        public ActionResult Login(UsersLoginModel usersLoginModel)
        {
            
            if (ModelState.IsValid)
            {
                if(userService.EntityExists(e => e.UserName == usersLoginModel.UserName && e.Password == usersLoginModel.Password && !e.IsDeleted && e.IsActive))
                {
                    FormsAuthentication.SetAuthCookie(usersLoginModel.UserName, usersLoginModel.RememberMe);
                    return RedirectToAction("Index");
                }
                ViewBag.Message = "User Name or Password is incorrect!";
                return View(usersLoginModel);
            }
            ViewBag.Message = "User Name or Password is invalid!";
            return View(usersLoginModel);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                userService?.Dispose();
                userMessagesService?.Dispose();
                db?.Dispose();
            }
        }
    }
}