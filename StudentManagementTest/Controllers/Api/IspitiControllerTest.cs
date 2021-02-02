using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentManagement.Dtos;
using StudentManagement.Models;
using System.Web.Http.Results;

namespace StudentManagementTest.Controllers.Api
{
    [TestClass]
    public class IspitiControllerTest
    {
        [TestMethod]
        public void GetIspit()
        {

            Mapper.CreateMap<Exam, ExamDto>();
            Mapper.CreateMap<Subject, SubjectDto>();

            var controller = new StudentManagement.Controllers.Api.ExamController();

            var result = controller.GetExam(20) as OkNegotiatedContentResult<ExamDto>;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Content.Subject.Name, "CS225");
        }
        [TestMethod]
        public void IspitVratiNotFound() {
            Mapper.CreateMap<Exam, ExamDto>();
            Mapper.CreateMap<Subject, SubjectDto>();

            var controller = new StudentManagement.Controllers.Api.ExamController();

            var result = controller.GetExam(40);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
