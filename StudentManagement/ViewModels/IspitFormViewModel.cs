using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.ViewModels
{
    public class IspitFormViewModel
    {
        public IEnumerable<Predmet> predmeti { get; set; }

        public int? id { get; set; }
        [Required]
        [Display(Name = "Datum Ispita")]
        public DateTime? datumIspita { get; set; }
        [Required]
        [Display(Name = "Naziv predmeta")]
        public int? predmetId { get; set; }


        public string Title {
            get
            {
                //if (ispit != null && ispit.id != 0)
                //{
                //    return "Izmeni ispit";
                //}
                return id != 0 ? "Izmeni ispit" : "Novi ispit";
            }
        }

        public IspitFormViewModel()
        {
            id = 0;
        }

        public IspitFormViewModel(Ispit ispit)
        {
            id = ispit.id;
            datumIspita = ispit.datumIspita;
            predmetId = ispit.predmetId;
        }

    }
}