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
            var exams = _context.Exams.Include(i => i.Subject).ToList();
            return View(exams);
        }
        [Authorize(Roles = "Profesor, Administrator")]
        public ActionResult NewExam()
        {
            var subjects = _context.Subjects.ToList();
            var viewModel = new ExamFormViewModel {
                  //ispit = new Ispit(),
                  Subjects = subjects
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Exam exam)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new ExamFormViewModel(exam)
                {
                    Subjects = _context.Subjects.ToList()
                };
                return View("NewExam", viewModel);
            }
            if (exam.Id == 0)
            {
                _context.Exams.Add(exam);
            }
            else
            {
                var existingExam = _context.Exams.Single(i => i.Id == exam.Id);
                existingExam.DateOfExam = exam.DateOfExam;
                existingExam.Subjectid = exam.Subjectid;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Exam");
        }

        [Authorize(Roles = "Profesor, Administrator")]
        public ActionResult Edit(int id)
        {
            var exam = _context.Exams.SingleOrDefault(i => i.Id == id);

            if (exam == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ExamFormViewModel(exam) {
                //ispit = ispit,
                Subjects = _context.Subjects.ToList()
            };
            return View("NewExam", viewModel);
        }
    }
}