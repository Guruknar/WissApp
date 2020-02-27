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

namespace WissAppMvc.Controllers
{
    public class HomeController : Controller
    {
        DbContext db = new WissAppContext();
        ServiceBase<Users> userService;

        public HomeController()
        {
            userService = new Service<Users>(db);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [HandleError]
        public ActionResult Login(UsersModel usersModel)
        {
            
            if (ModelState.IsValid)
            {
                if(userService.EntityExists(e => e.UserName == usersModel.UserName && e.Password == usersModel.Password && !e.IsDeleted && e.IsActive))
                {
                    FormsAuthentication.SetAuthCookie(usersModel.UserName, usersModel.RememberMe);
                    return RedirectToAction("Index");
                }
                ViewBag.Message = "User Name or Password is incorrect!";
                return View(usersModel);
            }
            ViewBag.Message = "User Name or Password is invalid!";
            return View(usersModel);
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
                db?.Dispose();
            }
        }
    }
}