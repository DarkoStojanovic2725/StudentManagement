 using StudentManagement.Models;
using System;
using System.Data.Entity;
using System.Linq;
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

        // GET: ExamResult
        [Authorize(Roles = "Profesor, Administrator")]
        public ViewResult Index()
        {

            var examResults = _context.ExamResults
                .Include(s => s.Student)
                .Include(p => p.Exam)
                .Include(c => c.Exam.Subject)
                .ToList();

            return View(examResults);
        }

        [Authorize(Roles = "Profesor, Administrator")]
        public ActionResult StudentExams(string studentId) {
            if (studentId == null)
                return HttpNotFound();

            var ispiti = _context.ExamResults
                .Include(s => s.Student)
                .Include(p => p.Exam)
                .Include(c => c.Exam.Subject)
                .Where(p => p.StudentId == studentId).ToList();

            var brojPolozenihIspita = _context.ExamResults
                .Include(s => s.Student)
                .Where(s => s.StudentId == studentId && s.Grade > 5).Count();

            ViewBag.BrojPolozenih = brojPolozenihIspita;

            return View(ispiti);
        }

        [Authorize(Roles = "Student")]
        public ActionResult NewExamResult() {

            var user = HttpContext.User.Identity.Name;
            var students = _context.Users
                .Where(u => u.Roles.Any(r => r.RoleId == getRole) && u.Email == user)
                .ToList();
            var exams = _context.Exams
                .Include(p => p.Subject)
                .ToList();

            var viewModel = new PolaganjeFormViewModel {
                Students = students,
                Exams = exams
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Profesor, Administrator")]
        public ActionResult PassedExamsByProfessor(string idProfesora) {
            if (idProfesora == null)
                return HttpNotFound();
            var exams = _context.ExamResults
                .Include(s => s.Student)
                .Include(p => p.Exam)
                .Include(c => c.Exam.Subject)
                .Where(p => p.Grade > 5 && p.Exam.Subject.UserId == idProfesora)
                .ToList();

            var profesor = _context.Users.SingleOrDefault(p => p.Id == idProfesora)?.Email;

            ViewBag.Profesor = profesor;
            return View(exams);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ExamResult polaganje) {
            if (!ModelState.IsValid)
            {
                var viewModel = new PolaganjeFormViewModel(polaganje)
                {
                    //ispit = ispit,
                    Students = _context.Users.ToList(),
                    Exams = _context.Exams.ToList()
                };
                return View("NovoPolaganje", viewModel);
            }
            if (polaganje.Id == 0)
            {
                _context.ExamResults.Add(polaganje);
            }
            else
            {
                var polaganjeUBazi = _context.ExamResults.Single(p => p.Id == polaganje.Id);
                polaganjeUBazi.Grade = polaganje.Grade;
                polaganjeUBazi.Score = polaganje.Score;
                polaganjeUBazi.NumberOfAttempts = polaganje.NumberOfAttempts;
                polaganjeUBazi.Passed = polaganje.Grade > 5 ? true : false;

                polaganjeUBazi.StudentId = polaganje.StudentId;
                polaganjeUBazi.ExamId = polaganje.ExamId;
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
                return RedirectToAction("Index", "ExamResult");

            return RedirectToAction("Index", "Home");

        }
        [Authorize(Roles = "Profesor, Administrator")]
        public ActionResult Edit(int id)
        {
            var polaganje = _context.ExamResults.SingleOrDefault(p => p.Id == id);

            if (polaganje == null)
            {
                return HttpNotFound();
            }
            var viewModel = new PolaganjeFormViewModel(polaganje)
            {
                Students = _context.Users.ToList(),
                Exams = _context.Exams.Include(p => p.Subject).ToList()
            };
            return View("IzmeniPolaganje", viewModel);
        }

        [Authorize(Roles = "Profesor, Administrator")]
        public ActionResult Delete(int id)
        {

            var polaganje = _context.ExamResults.SingleOrDefault(p => p.Id == id);

            if (polaganje == null)
                return HttpNotFound();

            _context.ExamResults.Remove(polaganje);
            _context.SaveChanges();

            return RedirectToAction("Index", "ExamResult");
        }
    }
}