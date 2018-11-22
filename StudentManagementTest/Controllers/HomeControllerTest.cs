using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web.Mvc;
using StudentManagement;
using StudentManagement.Controllers;
using StudentManagement.Models;

namespace StudentManagementTest.Controllers
{

    [TestClass]
    public class HomeControllerTest
    {
        //private ApplicationDbContext context;
        

        //public UnitTest1()
        //{
        //    context = new ApplicationDbContext();
        //    context.Dispose();
        //}

        
        [TestMethod]
        public void Index()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About() {
            HomeController controller = new HomeController();

            ViewResult result = controller.About() as ViewResult;

            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact() {
            HomeController controller = new HomeController();

            ViewResult result = controller.Contact() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Studenti() {
            HomeController controller = new HomeController();

            ViewResult result = controller.Contact() as ViewResult;

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void Profesori()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Profesori() as ViewResult;

            Assert.IsNotNull(result);

        }
        //[TestMethod]
        //public void Studenti_Return_List() {
        //    HomeController controller = new HomeController();

        //    var result = controller.Studenti() ;

        //    Assert.IsNotNull(result);
        //}
    }
}
