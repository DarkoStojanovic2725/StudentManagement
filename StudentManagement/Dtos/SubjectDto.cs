using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.Dtos
{
    public class SubjectDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Espb { get; set; }
    }
}