using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.Controllers;
using System.Web.Mvc;
using WebApplication.Models;
using System.Collections.Generic;
namespace WebApplication.Tests.Controllers
{
    [TestClass]
    public class DanhSachSanPhamTest
    {
        [TestMethod]
        public void TestIndex()
        {
            var controller = new DanhSachSanPhamController();
            var result = controller.Index() as ViewResult;
            var db = new CS4PEntities();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<BangSanPham>));
            Assert.AreEqual(db.BangSanPhams.Count(), ((List<BangSanPham>)result.Model).Count);
        }
    }
}
