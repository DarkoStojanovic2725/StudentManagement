using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using StudentManagement.ViewModels;

namespace StudentManagement.Controllers
{
    public class IspitiController : Controller
    {
        private ApplicationDbContext _context;

        public IspitiController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Ispit
        [Authorize(Roles = "Profesor, Administrator")]
        public ViewResult Index()
        {
            var ispiti = _context.ispiti.Include(i => i.predmet).ToList();
            return View(ispiti);
        }
        [Authorize(Roles = "Profesor, Administrator")]
        public ActionResult NoviIspit()
        {
            var predmeti = _context.predmeti.ToList();
            var viewModel = new IspitFormViewModel {
                  //ispit = new Ispit(),
                  predmeti = predmeti
            };
            return View("IspitiForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sacuvaj(Ispit ispit)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new IspitFormViewModel(ispit)
                {
                    //ispit = ispit,
                    predmeti = _context.predmeti.ToList()
                };
                return View("IspitiForm", viewModel);
            }
            if (ispit.id == 0)
            {
                _context.ispiti.Add(ispit);
            }
            else
            {
                var ispitUBazi = _context.ispiti.Single(i => i.id == ispit.id);
                ispitUBazi.datumIspita = ispit.datumIspita;
                ispitUBazi.predmetId = ispit.predmetId;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Ispiti");
        }
        [Authorize(Roles = "Profesor, Administrator")]
        public ActionResult Izmeni(int id)
        {
            var ispit = _context.ispiti.SingleOrDefault(i => i.id == id);

            if (ispit == null)
            {
                return HttpNotFound();
            }
            var viewModel = new IspitFormViewModel(ispit) {
                //ispit = ispit,
                predmeti = _context.predmeti.ToList()
            };
            return View("IspitiForm", viewModel);
        }
    }
}