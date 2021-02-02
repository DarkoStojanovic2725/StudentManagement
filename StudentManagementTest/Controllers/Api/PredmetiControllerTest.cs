using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentManagement.Controllers.Api;
using StudentManagement.Dtos;
using StudentManagement.Models;
using System.Web.Http.Results;

namespace StudentManagementTest.Controllers.Api
{
    [TestClass]
    public class PredmetiControllerTest
    {
        [TestMethod]
        public void GetIspit()
        {

           
            Mapper.CreateMap<Subject, SubjectDto>();

            var controller = new PredmetiController();

            var result = controller.GetSubject(23) as OkNegotiatedContentResult<SubjectDto>;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Content.Name, "CS225");
        }

        [TestMethod]
        public void PredmetVratiNotFound()
        {
           
            Mapper.CreateMap<Subject, SubjectDto>();

            var controller = new PredmetiController();

            var result = controller.GetSubject(45);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
