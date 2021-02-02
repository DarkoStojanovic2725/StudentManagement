using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    public class Subject
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Naziv predmeta")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Broj ESPB bodova")]
        public int Espb { get; set; }

        [Display(Name = "Profesor")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

    }
}