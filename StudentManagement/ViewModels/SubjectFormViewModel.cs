using StudentManagement.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.ViewModels
{
    public class SubjectFormViewModel
    {

        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Naziv predmeta")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Broj ESPB bodova")]
        public int Espb { get; set; }

        //public ApplicationUser user { get; set; }
        [Display(Name = "Profesor")]
        [Required]
        public string UserId { get; set; }


        public string Title
        {
            get
            {
               
                return Id != 0 ? "Izmeni predmet" : "Novi predmet";
            }
        }

        public SubjectFormViewModel()
        {
            Id = 0;
        }

        public SubjectFormViewModel(Subject subject)
        {
            Id = subject.Id;
            Name = subject.Name;
            Espb = subject.Espb;
            UserId = subject.UserId;
        }
    }
}