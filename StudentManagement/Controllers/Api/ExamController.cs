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
            var examDtos = _context.Exams.Include(i => i.Subject)
                .ToList()
                .Select(Mapper.Map<Exam, ExamDto>);
            return Ok(examDtos);
        }

        public IHttpActionResult GetExam(int id)
        {
            var exam = _context.Exams.Include(i => i.Subject).FirstOrDefault(i => i.Id == id);

            if (exam == null)
                return NotFound();

            return Ok(Mapper.Map<Exam, ExamDto>(exam));
        }

        //POST /api/ispiti
        [HttpPost]
        public IHttpActionResult NewExam(ExamDto ispitDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var exam = Mapper.Map<ExamDto, Exam>(ispitDto);
            _context.Exams.Add(exam);
            _context.SaveChanges();

            ispitDto.Id = exam.Id;

            return Created(new Uri(Request.RequestUri + "/" + exam.Id), ispitDto);
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
            var existingExam = _context.Exams.FirstOrDefault(i => i.Id == id);

            if (existingExam == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

            _context.Exams.Remove(existingExam);
            _context.SaveChanges();

            return Ok();
        }
    }
}
