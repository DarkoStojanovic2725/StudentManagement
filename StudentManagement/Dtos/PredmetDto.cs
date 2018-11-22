using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.Dtos
{
    public class PredmetDto
    {
        public int id { get; set; }
        [Required]
        public string naziv { get; set; }
        [Required]
        public int espb { get; set; }
    }
}