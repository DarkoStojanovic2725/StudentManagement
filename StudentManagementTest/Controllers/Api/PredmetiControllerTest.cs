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
    public class PredmetiControllerTest
    {
        [TestMethod]
        public void GetIspit()
        {

           
            Mapper.CreateMap<Predmet, PredmetDto>();

            var controller = new PredmetiController();

            var result = controller.getPredmet(23) as OkNegotiatedContentResult<PredmetDto>;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Content.naziv, "CS225");
        }

        [TestMethod]
        public void PredmetVratiNotFound()
        {
           
            Mapper.CreateMap<Predmet, PredmetDto>();

            var controller = new PredmetiController();

            var result = controller.getPredmet(45);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
