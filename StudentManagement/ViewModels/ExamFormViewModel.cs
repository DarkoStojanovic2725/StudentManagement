using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.ViewModels
{
    public class ExamFormViewModel
    {
        public IEnumerable<Subject> Subjects { get; set; }

        public int? Id { get; set; }
        [Required]
        [Display(Name = "Datum Ispita")]
        public DateTime? DateOfExam { get; set; }
        [Required]
        [Display(Name = "Naziv predmeta")]
        public int? SubjectId { get; set; }


        public string Title {
            get
            {
                return Id != 0 ? "Izmeni ispit" : "Novi ispit";
            }
        }

        public ExamFormViewModel()
        {
            Id = 0;
        }

        public ExamFormViewModel(Exam exam)
        {
            Id = exam.Id;
            DateOfExam = exam.DateOfExam;
            SubjectId = exam.Subjectid;
        }

    }
}