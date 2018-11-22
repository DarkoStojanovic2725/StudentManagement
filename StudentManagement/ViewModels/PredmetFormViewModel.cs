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

        public IEnumerable<ApplicationUser> applicationUsers { get; set; }

        public int id { get; set; }
        [Required]
        [Display(Name = "Naziv predmeta")]
        public string naziv { get; set; }
        [Required]
        [Display(Name = "Broj ESPB bodova")]
        public int espb { get; set; }

        //public ApplicationUser user { get; set; }
        [Display(Name = "Profesor")]
        [Required]
        public string userId { get; set; }


        public string Title
        {
            get
            {
               
                return id != 0 ? "Izmeni predmet" : "Novi predmet";
            }
        }

        public PredmetFormViewModel()
        {
            id = 0;
        }

        public PredmetFormViewModel(Predmet predmet)
        {
            id = predmet.id;
            naziv = predmet.naziv;
            espb = predmet.espb;
            userId = predmet.userId;
        }
    }
}