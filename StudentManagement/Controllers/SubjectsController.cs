using StudentManagement.Models;
using StudentManagement.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace StudentManagement.Controllers
{
    public class SubjectsController : Controller
    {
        private ApplicationDbContext _context;
        private string getRole;
        public SubjectsController()
        {
            _context = new ApplicationDbContext();
            getRole = _context.Roles.Where(r => r.Name == "Profesor").Select(m => m.Id).SingleOrDefault();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Subject
        [Authorize(Roles = "Profesor, Administrator")]
        public ViewResult Index()
        {
            var subjects = _context.Subjects.Include(u => u.User).ToList();
            return View(subjects);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult NewSubject()
        {
            var users = _context.Users.Where(u => u.Roles.Any(r => r.RoleId == getRole)).ToList();
            
            var viewModel = new SubjectFormViewModel
            {
                ApplicationUsers = users
            };
            return View(viewModel);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Subject subject)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new SubjectFormViewModel(subject)
                {
                    ApplicationUsers = _context.Users.ToList()
                };
                return View("NewSubject", viewModel);
            }

            if (subject.Id == 0)
            {
                _context.Subjects.Add(subject);
            }
            else
            {
                var existingExam = _context.Subjects.Single(i => i.Id == subject.Id);
                existingExam.Name = subject.Name;
                existingExam.Espb = subject.Espb;
                existingExam.UserId = subject.UserId;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Subjects");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            var subject = _context.Subjects.SingleOrDefault(p => p.Id == id);

            var users = _context.Users.Where(u => u.Roles.Any(r => r.RoleId == getRole)).ToList();

            if (subject == null)
            {
                return HttpNotFound();
            }
            var viewModel = new SubjectFormViewModel(subject)
            {
                ApplicationUsers = users
            };
            return View("NewSubject", viewModel);
        }

    }
}