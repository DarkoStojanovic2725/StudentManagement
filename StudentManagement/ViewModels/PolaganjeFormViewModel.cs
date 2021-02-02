using StudentManagement.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.ViewModels
{
    public class PolaganjeFormViewModel
    {

        [Display(Name = "Studenti")]
        public IEnumerable<ApplicationUser> Students { get; set; }
        [Display(Name = "Ispiti")]
        public IEnumerable<Exam> Exams { get; set; }
        public int Id { get; set; }
        [Display(Name = "Ocena")]
        [Range(5, 10)]
        public int? Grade { get; set; }
        [Display(Name = "Broj bodova")]
        public int? Score { get; set; }
        [Display(Name = "Broj pokusaja")]
        public int? NumberOfAttampts { get; set; }
        [Display(Name = "Polozio")]
        public bool? Passed { get; set; }
        [Required]
        [Display(Name = "Email studenta")]
        public string StudentId { get; set; }
        [Required]
        [Display(Name = "Ispit")]
        public int? ExamId { get; set; }

        public PolaganjeFormViewModel()
        {
            Id = 0;
        }

        public PolaganjeFormViewModel(ExamResult polaganje)
        {
            Id = polaganje.Id;
            Grade = polaganje.Grade;
            Score = polaganje.Score;
            NumberOfAttampts = polaganje.NumberOfAttempts;
            Passed = polaganje.Passed;
            StudentId = polaganje.StudentId;
            ExamId = polaganje.ExamId;
        }

        public string Title
        {
            get
            {
                return Id != 0 ? "Izmeni polaganje" : "Novo polaganje";
            }
        }
    }
}