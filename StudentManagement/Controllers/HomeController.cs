using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Metrics;
using App.Metrics.Core.Options;

namespace StudentManagement.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;
        private string getRole;
        //private readonly IMetrics _metrics;

        public HomeController(/*IMetrics metrics*/)
        {
            _context = new ApplicationDbContext();
            //_metrics = metrics;
            getRole = _context.Roles.Where(r => r.Name == "Student").Select(m => m.Id).SingleOrDefault();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            //_metrics.Measure.Counter.Increment(new CounterOptions
            //{
            //    Name = "HomeIndexCounter"
            //});

            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize(Roles = "Profesor, Administrator")]
        public ActionResult Studenti() {

            var studenti = _context
                .Users
                .Where(u => u.Roles.Any(r => r.RoleId == getRole))
                .ToList();

            return View(studenti);
        }

        [Authorize(Roles = "Administrator, Profesor")]
        public ActionResult Profesori()
        {
            var roleProf = _context.Roles.Where(r => r.Name == "Profesor").Select(m => m.Id).SingleOrDefault();

            var profesori = _context
                .Users
                .Where(u => u.Roles.Any(r => r.RoleId == roleProf))
                .ToList();

            return View(profesori);
        }
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult Obrisi(string id)
        {

            var student = _context.Users.SingleOrDefault(p => p.Id == id);

            if (student == null)
                return HttpNotFound();

            _context.Users.Remove(student);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}