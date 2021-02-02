using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    public class ExamResult
    {
        public int Id { get; set; }
        [Display(Name = "Ocena")]
        [Range(5, 10)]
        public int? Grade { get; set; }
        [Display(Name = "Broj bodova")]
        public int? Score { get; set; }
        [Display(Name = "Broj pokusaja")]
        public int? NumberOfAttempts { get; set; }
        [Display(Name = "Polozio")]
        public bool? Passed { get; set; }

        public ApplicationUser Student { get; set; }
        [Required]
        [Display(Name = "Email studenta")]
        public string StudentId { get; set; }

        public Exam Exam { get; set; }
        [Required]
        [Display(Name = "Ispit")]
        public int ExamId { get; set; }
    }
}