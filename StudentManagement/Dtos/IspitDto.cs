using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.Dtos
{
    public class IspitDto
    {
        public int id { get; set; }
        [Required]
        public DateTime datumIspita { get; set; }
        [Required]
        public int predmetId { get; set; }

        public PredmetDto predmet { get; set; }
    }
}