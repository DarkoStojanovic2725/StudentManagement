using System;
using System.ComponentModel.DataAnnotations;
namespace StudentManagement.Models
{
    public class Exam
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Datum Ispita")]
        public DateTime DateOfExam { get; set; }
        [Required]
        [Display(Name = "Naziv predmeta")]
        public int Subjectid { get; set; }
        public Subject Subject { get; set; }

        public string SubjectNameExamDate {
            get {
                return Subject.Name + " " + DateOfExam; 
            }
        }
        
    }
}