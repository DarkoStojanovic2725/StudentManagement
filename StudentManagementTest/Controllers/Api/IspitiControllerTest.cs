using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentManagement.Controllers.Api;
using StudentManagement.Dtos;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace StudentManagementTest.Controllers.Api
{
    [TestClass]
    public class IspitiControllerTest
    {
        [TestMethod]
        public void GetIspit()
        {

            Mapper.CreateMap<Ispit, IspitDto>();
            Mapper.CreateMap<Predmet, PredmetDto>();

            var controller = new IspitiController();

            var result = controller.getIspit(20) as OkNegotiatedContentResult<IspitDto>;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Content.predmet.naziv, "CS225");
        }
        [TestMethod]
        public void IspitVratiNotFound() {
            Mapper.CreateMap<Ispit, IspitDto>();
            Mapper.CreateMap<Predmet, PredmetDto>();

            var controller = new IspitiController();

            var result = controller.getIspit(40);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
