using StudentManagement.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using StudentManagement.ViewModels;

namespace StudentManagement.Controllers
{
    public class ExamController : Controller
    {
        private ApplicationDbContext _context;

        public ExamController()
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
            var ispiti = _context.Exams.Include(i => i.Subject).ToList();
            return View(ispiti);
        }
        [Authorize(Roles = "Profesor, Administrator")]
        public ActionResult NewExam()
        {
            var Subjects = _context.Subjects.ToList();
            var viewModel = new IspitFormViewModel {
                  //ispit = new Ispit(),
                  Subjects = Subjects
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Exam ispit)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new IspitFormViewModel(ispit)
                {
                    Subjects = _context.Subjects.ToList()
                };
                return View("IspitiForm", viewModel);
            }
            if (ispit.Id == 0)
            {
                _context.Exams.Add(ispit);
            }
            else
            {
                var ispitUBazi = _context.Exams.Single(i => i.Id == ispit.Id);
                ispitUBazi.DateOfExam = ispit.DateOfExam;
                ispitUBazi.Subjectid = ispit.Subjectid;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Ispiti");
        }

        [Authorize(Roles = "Profesor, Administrator")]
        public ActionResult Edit(int id)
        {
            var ispit = _context.Exams.SingleOrDefault(i => i.Id == id);

            if (ispit == null)
            {
                return HttpNotFound();
            }
            var viewModel = new IspitFormViewModel(ispit) {
                //ispit = ispit,
                Subjects = _context.Subjects.ToList()
            };
            return View("IspitiForm", viewModel);
        }
    }
}