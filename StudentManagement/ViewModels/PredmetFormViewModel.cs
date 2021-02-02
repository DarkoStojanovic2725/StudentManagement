using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.ViewModels
{
    public class PredmetFormViewModel
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

        public PredmetFormViewModel()
        {
            Id = 0;
        }

        public PredmetFormViewModel(Subject predmet)
        {
            Id = predmet.Id;
            Name = predmet.Name;
            Espb = predmet.Espb;
            UserId = predmet.UserId;
        }
    }
}