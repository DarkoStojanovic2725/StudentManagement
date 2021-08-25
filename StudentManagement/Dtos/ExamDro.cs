using System;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Dtos
{
    public class ExamDto
    {
        public int Id { get; set; }
        [Required]
        public DateTime DateOfExam { get; set; }
        [Required]
        public int SubjectId { get; set; }

        public SubjectDto Subject { get; set; }
    }
}