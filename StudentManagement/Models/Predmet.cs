using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    public class Predmet
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Naziv predmeta")]
        public string naziv { get; set; }
        [Required]
        [Display(Name = "Broj ESPB bodova")]
        public int espb { get; set; }

        [Display(Name = "Profesor")]
        public string userId { get; set; }

        public ApplicationUser user { get; set; }

    }
}