using System;
using System.Data.Entity;
using AppCore.Services;
using AppCore.Services.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using WissAppEF.Contexts;
using WissAppEntities.Entities;

namespace WissAppWebApi.Tests
{
    [TestClass]
    public class WissAppTest
    {
        DbContext db = new WissAppContext();
        [TestMethod]
        public void ShouldGetusers()
        {
            using (var service = new Service<Users>(db))
            {
                var entities = service.GetEntities();
                //Assert.AreEqual(4, entities.Count); //normal yolu// 1 yerine
                //Assert.AreNotEqual(0, entities.Count);// 2 yerine
                //entities.Count.ShouldBe(4);// shouldy kütüphanesi// bu 1
                entities.Count.ShouldBeGreaterThan(0);// shouldy kütüphanesi// 2 bu

            }
        }

        [TestMethod]
        public void ShouldAddUser()
        {
            var entity = new Users()
            {
                Id = 0,
                UserName = "test user name",
                Password = "test pass",
                School = "test school",
                Location = "test location",
                BirthDate = DateTime.Now,
                E_Mail = "test e-mail",
                Gender = "f",
                IsActive = true,
                RoleId = 2
            };
            using (var service = new Service<Users>(db))
            {
                service.AddEntity(entity);
            }
            entity.Id.ShouldNotBe(0); //eklendiyse id si 0 olmuyacak.

        }

        [TestMethod]
        public void ShouldGetUser()
        {
            using (var service = new Service<Users>(db))
            {
                var entity = service.GetEntity(e => e.UserName == "leo");
                entity.ShouldNotBeNull();
            }
            
        }

        [TestMethod]
        public void ShouldUpdateUser()
        {
            using (var service = new Service<Users>(db))
            {
                var entity = service.GetEntity(e => e.UserName == "leo");
                entity.UserName.ShouldBe("leo");
                entity.Password = "oel";
                service.UpdateEntity(entity);
                entity = service.GetEntity(e => e.UserName == "leo");
                entity.Password.ShouldBe("oel");
            }
        }

        [TestMethod]
        public void ShouldDeleteUser()
        {
            using (var service = new Service<Users>(db))
            {
                service.DeleteEntity(2);
                var entity = service.GetEntity(e => e.Id == 2 && e.IsDeleted == false);
                entity.ShouldBeNull();
            }
        }

    }
}
