using AutoMapper;
using StudentManagement.Dtos;
using StudentManagement.Models;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace StudentManagement.Controllers.Api
{
    public class PredmetiController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public PredmetiController()
        {
            _context = new ApplicationDbContext();
        }

        //GET api/Subjects
        public IHttpActionResult GetSubjects()
        {
            var subjects = _context.Subjects.ToList().Select(Mapper.Map<Subject, SubjectDto>);
            return Ok(subjects);
        }

        //Get /api/Subjects/{id}
        public IHttpActionResult GetSubject(int id)
        {
            var predmet = _context.Subjects.FirstOrDefault(p => p.Id == id);

            if (predmet == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(Mapper.Map<Subject, SubjectDto>(predmet));
        }

        [HttpDelete]
        public IHttpActionResult DeleteSubject(int id)
        {
            var dbEntry = _context.Subjects.FirstOrDefault(i => i.Id == id);

            if (dbEntry == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Subjects.Remove(dbEntry);
            _context.SaveChanges();

            return Ok();
        }
    }
}
