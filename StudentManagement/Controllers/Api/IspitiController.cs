using AutoMapper;
using StudentManagement.Dtos;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentManagement.Controllers.Api
{
    public class IspitiController : ApiController
    {
        private ApplicationDbContext _context;


        public IspitiController()
        {
            _context = new ApplicationDbContext();
        }

        //public IEnumerable<IspitDto> getIspiti() {
        //GET api/ispiti
        public IHttpActionResult getIspiti() { 

            //return _context.ispiti.ToList().Select(Mapper.Map<Ispit, IspitDto>);

            var ispitiDtos = _context.ispiti.Include(i => i.predmet).ToList().Select(Mapper.Map<Ispit, IspitDto>);

            return Ok(ispitiDtos);
        }

        //Get /api/ispiti/{id}
        //public IspitDto getIspit(int id) {
        //    var ispit = _context.ispiti.SingleOrDefault(i => i.id == id);

        //    if (ispit == null)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);

        //    return Mapper.Map<Ispit, IspitDto>(ispit);
        //}

        public IHttpActionResult getIspit(int id) {
            var ispit = _context.ispiti.Include(i => i.predmet).SingleOrDefault(i => i.id == id);

            if (ispit == null)
                return NotFound();

            return Ok(Mapper.Map<Ispit, IspitDto>(ispit));
        }


        //POST /api/ispiti
        [HttpPost]
        public IHttpActionResult noviIspit(IspitDto ispitDto) {
            if (!ModelState.IsValid) {
                return BadRequest();
            }

            var ispit = Mapper.Map<IspitDto, Ispit>(ispitDto);
            _context.ispiti.Add(ispit);
            _context.SaveChanges();

            ispitDto.id = ispit.id;

            return Created(new Uri(Request.RequestUri + "/" + ispit.id), ispitDto);
        }

        //PUT api/ispiti/{id}
        [HttpPut]
        public IHttpActionResult updateIspit(int id, IspitDto ispitDto) {
            if (!ModelState.IsValid)
            {
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest(); //postovanje konvencija za vracanje HttpRequestova
            }
            var ispitUBazi = _context.ispiti.SingleOrDefault(i => i.id == id);

            if (ispitUBazi == null) {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }

            Mapper.Map<IspitDto, Ispit>(ispitDto, ispitUBazi);

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult obrisiIspit(int id) {
            var ispitUBazi = _context.ispiti.SingleOrDefault(i => i.id == id);

            if (ispitUBazi == null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }

            _context.ispiti.Remove(ispitUBazi);
            _context.SaveChanges();

            return Ok();
        }
    }
}
