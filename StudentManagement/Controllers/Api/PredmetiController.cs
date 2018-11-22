using AutoMapper;
using StudentManagement.Dtos;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentManagement.Controllers.Api
{
    public class PredmetiController : ApiController
    {

        private ApplicationDbContext _context;

        public PredmetiController()
        {
            _context = new ApplicationDbContext();
        }


        //GET api/predmeti
        public IHttpActionResult getPredmeti()
        {
            var predmeti = _context.predmeti.ToList().Select(Mapper.Map<Predmet, PredmetDto>);

            return Ok(predmeti);
        }

        //Get /api/predmeti/{id}
        public IHttpActionResult getPredmet(int id)
        {
            var predmet = _context.predmeti.SingleOrDefault(p => p.id == id);

            if (predmet == null)
                return NotFound();

            return Ok(Mapper.Map<Predmet, PredmetDto>(predmet));
        }

        //POST /api/predmeti
        //[HttpPost]
        //public IHttpActionResult noviPredmet(PredmetDto predmetDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        //throw new HttpResponseException(HttpStatusCode.BadRequest);
        //        return BadRequest();
        //    }

        //    var predmet = Mapper.Map<PredmetDto, Predmet>(predmetDto);
        //    _context.predmeti.Add(predmet);
        //    _context.SaveChanges();

        //    predmetDto.id = predmet.id;

        //    return Created(new Uri(Request.RequestUri + "/" + predmet.id), predmetDto);
        //}

        //PUT api/predmeti/{id}
        //[HttpPut]
        //public IHttpActionResult updatePredmet(int id, PredmetDto predmetDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        //throw new HttpResponseException(HttpStatusCode.BadRequest);
        //        return BadRequest();
        //    }
        //    var predmetUBazi = _context.predmeti.SingleOrDefault(p => p.id == id);

        //    if (predmetUBazi == null)
        //    {
        //        //throw new HttpResponseException(HttpStatusCode.NotFound);
        //        return NotFound();
        //    }

        //    Mapper.Map<PredmetDto, Predmet>(predmetDto, predmetUBazi);

        //    _context.SaveChanges();

        //    return Ok();
        //}

        [HttpDelete]
        public IHttpActionResult obrisiPredmet(int id)
        {
            var predmetUBazi = _context.predmeti.SingleOrDefault(i => i.id == id);

            if (predmetUBazi == null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }

            _context.predmeti.Remove(predmetUBazi);
            _context.SaveChanges();

            return Ok();
        }

    }
}
