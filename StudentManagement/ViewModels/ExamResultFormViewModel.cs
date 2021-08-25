using StudentManagement.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.ViewModels
{
    public class ExamResultFormViewModel
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

        public ExamResultFormViewModel()
        {
            Id = 0;
        }

        public ExamResultFormViewModel(ExamResult examResult)
        {
            Id = examResult.Id;
            Grade = examResult.Grade;
            Score = examResult.Score;
            NumberOfAttampts = examResult.NumberOfAttempts;
            Passed = examResult.Passed;
            StudentId = examResult.StudentId;
            ExamId = examResult.ExamId;
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