using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.ViewModels
{
    public class PolaganjeFormViewModel
    {

        [Display(Name = "Studenti")]
        public IEnumerable<ApplicationUser> studenti { get; set; }
        [Display(Name = "Ispiti")]
        public IEnumerable<Ispit> ispiti { get; set; }
        public int id { get; set; }
        [Display(Name = "Ocena")]
        [Range(5, 10)]
        public int? ocena { get; set; }
        [Display(Name = "Broj bodova")]
        public int? brojBodova { get; set; }
        [Display(Name = "Broj pokusaja")]
        public int? brojPokusaja { get; set; }
        [Display(Name = "Polozio")]
        public bool? polozio { get; set; }
        [Required]
        [Display(Name = "Email studenta")]
        public string studentId { get; set; }
        [Required]
        [Display(Name = "Ispit")]
        public int? ispitId { get; set; }


       

        public PolaganjeFormViewModel()
        {
            id = 0;
        }

        public PolaganjeFormViewModel(Polaganje polaganje)
        {
            id = polaganje.id;
            ocena = polaganje.ocena;
            brojBodova = polaganje.brojBodova;
            brojPokusaja = polaganje.brojPokusaja;
            polozio = polaganje.polozio;
            studentId = polaganje.studentId;
            ispitId = polaganje.ispitId;
        }

        public string Title
        {
            get
            {
                return id != 0 ? "Izmeni polaganje" : "Novo polaganje";
            }
        }
    }
}