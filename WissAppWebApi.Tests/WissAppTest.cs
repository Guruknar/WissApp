using System;
using System.Data.Entity;
using AppCore.Services;
using AppCore.Services.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WissAppEF.Contexts;
using WissAppEntities.Entities;

namespace WissAppWebApi.Tests
{
    [TestClass]
    public class WissAppTest
    {
        DbContext db = new WissAppContext();
        [TestMethod]
        public void ShouldGet4users()
        {
            using (var service = new Service<Users>(db))
            {
                var entities = service.GetEntities();
                Assert.AreEqual(4, entities.Count);
            }
        }
    }
}
