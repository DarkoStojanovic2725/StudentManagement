using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.ViewModels
{
    public class IspitFormViewModel
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

        public IspitFormViewModel()
        {
            Id = 0;
        }

        public IspitFormViewModel(Exam ispit)
        {
            Id = ispit.Id;
            DateOfExam = ispit.DateOfExam;
            SubjectId = ispit.Subjectid;
        }

    }
}