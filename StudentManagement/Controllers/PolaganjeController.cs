 using StudentManagement.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentManagement.ViewModels;
using System.Data.Entity.Validation;

namespace StudentManagement.Controllers
{
    public class PolaganjeController : Controller
    {

        private ApplicationDbContext _context;
        private string getRole;
        public PolaganjeController()
        {
            _context = new ApplicationDbContext();
            getRole = _context.Roles.Where(r => r.Name == "Student").Select(m => m.Id).SingleOrDefault();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();  
        }


        // GET: Polaganje
        [Authorize(Roles = "Profesor, Administrator")]
        public ViewResult Index()
        {

            var polaganja = _context.polaganja
                .Include(s => s.student)
                .Include(p => p.ispit)
                .Include(c => c.ispit.predmet)
                .ToList();

            return View(polaganja);
        }
        [Authorize(Roles = "Profesor, Administrator")]
        public ActionResult IspitiStudenta(string idStudenta) {
            if (idStudenta == null)
                return HttpNotFound();

            var ispiti = _context.polaganja
                .Include(s => s.student)
                .Include(p => p.ispit)
                .Include(c => c.ispit.predmet)
                .Where(p => p.studentId == idStudenta).ToList();

            var brojPolozenihIspita = _context.polaganja
                .Include(s => s.student)
                .Where(s => s.studentId == idStudenta && s.ocena > 5).Count();

            ViewBag.BrojPolozenih = brojPolozenihIspita;

            return View(ispiti);
        }
        [Authorize(Roles = "Student")]
        public ActionResult NovoPolaganje() {

            var user = HttpContext.User.Identity.Name;
            var studenti = _context.Users
                .Where(u => u.Roles.Any(r => r.RoleId == getRole) && u.Email == user)
                .ToList();
            var ispiti = _context.ispiti
                .Include(p => p.predmet)
                .ToList();

            var viewModel = new PolaganjeFormViewModel {
                studenti = studenti,
                ispiti = ispiti
            };

            return View("NovoPolaganje", viewModel);
        }
        [Authorize(Roles = "Profesor, Administrator")]
        public ActionResult PolozeniIspitiKodProfesora(string idProfesora) {
            if (idProfesora == null)
                return HttpNotFound();
            var ispiti = _context.polaganja
                .Include(s => s.student)
                .Include(p => p.ispit)
                .Include(c => c.ispit.predmet)
                .Where(p => p.ocena > 5 && p.ispit.predmet.userId == idProfesora)
                .ToList();

            var profesor = _context.Users.SingleOrDefault(p => p.Id == idProfesora).Email;

            ViewBag.Profesor = profesor;
            return View(ispiti);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Sacuvaj(Polaganje polaganje) {
            if (!ModelState.IsValid)
            {
                var viewModel = new PolaganjeFormViewModel(polaganje)
                {
                    //ispit = ispit,
                    studenti = _context.Users.ToList(),
                    ispiti = _context.ispiti.ToList()
                };
                return View("NovoPolaganje", viewModel);
            }
            if (polaganje.id == 0)
            {
                _context.polaganja.Add(polaganje);
            }
            else
            {
                var polaganjeUBazi = _context.polaganja.Single(p => p.id == polaganje.id);
                polaganjeUBazi.ocena = polaganje.ocena;
                polaganjeUBazi.brojBodova = polaganje.brojBodova;
                polaganjeUBazi.brojPokusaja = polaganje.brojPokusaja;
                polaganjeUBazi.polozio = polaganje.ocena > 5 ? true : false;

                polaganjeUBazi.studentId = polaganje.studentId;
                polaganjeUBazi.ispitId = polaganje.ispitId;
            }
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }

            if(User.IsInRole("Profesor"))
                return RedirectToAction("Index", "Polaganje");

            return RedirectToAction("Index", "Home");

        }
        [Authorize(Roles = "Profesor, Administrator")]
        public ActionResult Izmeni(int id)
        {
            var polaganje = _context.polaganja.SingleOrDefault(p => p.id == id);

            if (polaganje == null)
            {
                return HttpNotFound();
            }
            var viewModel = new PolaganjeFormViewModel(polaganje)
            {
                studenti = _context.Users.ToList(),
                ispiti = _context.ispiti.Include(p => p.predmet).ToList()
            };
            return View("IzmeniPolaganje", viewModel);
        }
        [Authorize(Roles = "Profesor, Administrator")]
        public ActionResult Obrisi(int id)
        {

            var polaganje = _context.polaganja.SingleOrDefault(p => p.id == id);

            if (polaganje == null)
                return HttpNotFound();

            _context.polaganja.Remove(polaganje);
            _context.SaveChanges();

            return RedirectToAction("Index", "Polaganje");
        }


    }
}