using AutoMapper;
using StudentManagement.Dtos;
using StudentManagement.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace StudentManagement.Controllers.Api
{
    public class ExamController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public ExamController()
        {
            _context = new ApplicationDbContext();
        }

        //GET api/GetExams
        public IHttpActionResult GetExams()
        {
            var ispitiDtos = _context.Exams.Include(i => i.Subject)
                .ToList()
                .Select(Mapper.Map<Exam, ExamDto>);
            return Ok(ispitiDtos);
        }

        public IHttpActionResult GetExam(int id)
        {
            var ispit = _context.Exams.Include(i => i.Subject).FirstOrDefault(i => i.Id == id);

            if (ispit == null)
                return NotFound();

            return Ok(Mapper.Map<Exam, ExamDto>(ispit));
        }

        //POST /api/ispiti
        [HttpPost]
        public IHttpActionResult NewExam(ExamDto ispitDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var ispit = Mapper.Map<ExamDto, Exam>(ispitDto);
            _context.Exams.Add(ispit);
            _context.SaveChanges();

            ispitDto.Id = ispit.Id;

            return Created(new Uri(Request.RequestUri + "/" + ispit.Id), ispitDto);
        }

        [HttpPut]
        public IHttpActionResult UpdateExam(ExamDto examDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);

            var examInDb = _context.Exams.FirstOrDefault(i => i.Id == examDto.Id);

            if (examInDb == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

            Mapper.Map(examDto, examInDb);

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteExam(int id)
        {
            var ispitUBazi = _context.Exams.FirstOrDefault(i => i.Id == id);

            if (ispitUBazi == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

            _context.Exams.Remove(ispitUBazi);
            _context.SaveChanges();

            return Ok();
        }
    }
}
