using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    public class Polaganje
    {
        public int id { get; set; }
        [Display(Name = "Ocena")]
        public int ocena { get; set; }
        [Display(Name = "Broj bodova")]
        public int brojBodova { get; set; }
        [Display(Name = "Broj pokusaja")]
        public int brojPokusaja { get; set; }
        [Display(Name = "Polozio")]
        public bool polozio { get; set; }

        public ApplicationUser student { get; set; }
        [Required]
        [Display(Name = "Email studenta")]
        public string studentId { get; set; }

        public Ispit ispit { get; set; }
        [Required]
        [Display(Name = "Ispit")]
        public int ispitId { get; set; }
    }
}