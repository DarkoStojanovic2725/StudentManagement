using AutoMapper;
using StudentManagement.Dtos;
using StudentManagement.Models;

namespace StudentManagement.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Exam, ExamDto>().ReverseMap();
            Mapper.CreateMap<Subject, SubjectDto>().ReverseMap();
        }
    }
}