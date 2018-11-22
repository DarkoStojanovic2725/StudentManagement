using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    public class Ispit
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Datum Ispita")]
        public DateTime datumIspita { get; set; }
        [Required]
        [Display(Name = "Naziv predmeta")]
        public int predmetId { get; set; }
        public Predmet predmet { get; set; }

        public string nazivDatum {
            get {
                return predmet.naziv + " " + datumIspita; 
            }
        }
        
    }
}