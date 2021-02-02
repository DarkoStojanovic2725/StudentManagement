using StudentManagement.Models;
using StudentManagement.ViewModels;
using System.Data.Entity;
using System.Linq;
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
        // GET: Subject
        [Authorize(Roles = "Profesor, Administrator")]
        public ViewResult Index()
        {
            var Subjects = _context.Subjects.Include(u => u.User).ToList();
            return View(Subjects);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult NewSubject(Subject predmet)
        {
            var users = _context.Users.Where(u => u.Roles.Any(r => r.RoleId == getRole)).ToList();
            
            var viewModel = new PredmetFormViewModel
            {
                ApplicationUsers = users
            };
            return View(viewModel);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Subject predmet)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new PredmetFormViewModel(predmet)
                {
                    ApplicationUsers = _context.Users.ToList()
                };
                return View("NoviPredmet", viewModel);
            }

            if (predmet.Id == 0)
            {
                _context.Subjects.Add(predmet);
            }
            else
            {
                var ispitUBazi = _context.Subjects.Single(i => i.Id == predmet.Id);
                ispitUBazi.Name = predmet.Name;
                ispitUBazi.Espb = predmet.Espb;
                ispitUBazi.UserId = predmet.UserId;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Predmeti");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            var predmet = _context.Subjects.SingleOrDefault(p => p.Id == id);

            var users = _context.Users.Where(u => u.Roles.Any(r => r.RoleId == getRole)).ToList();

            if (predmet == null)
            {
                return HttpNotFound();
            }
            var viewModel = new PredmetFormViewModel(predmet)
            {
                ApplicationUsers = users
            };
            return View("NoviPredmet", viewModel);
        }

    }
}