using StudentManagement.Models;
using StudentManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagement.Controllers
{
    public class PredmetiController : Controller
    {
        private ApplicationDbContext _context;
        private string getRole;
        public PredmetiController()
        {
            _context = new ApplicationDbContext();
            getRole = _context.Roles.Where(r => r.Name == "Profesor").Select(m => m.Id).SingleOrDefault();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Predmet
        [Authorize(Roles = "Profesor, Administrator")]
        public ViewResult Index()
        {
            var predmeti = _context.predmeti.Include(u => u.user).ToList();
            return View(predmeti);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult NoviPredmet(Predmet predmet)
        {

            //var getRole = _context.Roles.Where(r => r.Name == "Profesor").Select(m => m.Id).SingleOrDefault();

            var users = _context.Users.Where(u => u.Roles.Any(r => r.RoleId == getRole)).ToList();

            //var predmeti = _context.predmeti.Include(u => users).ToList();

           // var users = _context.Users.ToList();
            
            var viewModel = new PredmetFormViewModel
            {
                //ispit = new Ispit(),
                applicationUsers = users
            };
            return View("NoviPredmet", viewModel);


        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sacuvaj(Predmet predmet)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new PredmetFormViewModel(predmet)
                {
                    applicationUsers = _context.Users.ToList()
                };
                return View("NoviPredmet", viewModel);
            }

            if (predmet.id == 0)
            {
                _context.predmeti.Add(predmet);
            }
            else
            {
                var ispitUBazi = _context.predmeti.Single(i => i.id == predmet.id);
                ispitUBazi.naziv = predmet.naziv;
                ispitUBazi.espb = predmet.espb;
                ispitUBazi.userId = predmet.userId;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Predmeti");
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult Izmeni(int id)
        {
            var predmet = _context.predmeti.SingleOrDefault(p => p.id == id);


            //var getRole = _context.Roles.Where(r => r.Name == "Profesor").Select(m => m.Id).SingleOrDefault();

            var users = _context.Users.Where(u => u.Roles.Any(r => r.RoleId == getRole)).ToList();

            if (predmet == null)
            {
                return HttpNotFound();
            }
            var viewModel = new PredmetFormViewModel(predmet)
            {
                //ispit = ispit,
                //applicationUsers = _context.Users.ToList()
                applicationUsers = users
            };
            return View("NoviPredmet", viewModel);
        }

        //public ActionResult Obrisi(int id)
        //{
        //    var predmet = _context.predmeti.SingleOrDefault(p => p.id == id);

        //    if (predmet == null)
        //        return HttpNotFound();

        //    _context.predmeti.Remove(predmet);
        //    _context.SaveChanges();

        //    return RedirectToAction("Index", "Predmeti");
        //}

    }
}