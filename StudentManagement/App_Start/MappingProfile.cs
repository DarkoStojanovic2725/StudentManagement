using AutoMapper;
using StudentManagement.Dtos;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagement.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Ispit, IspitDto>();
            Mapper.CreateMap<Predmet, PredmetDto>();
            




            Mapper.CreateMap<IspitDto, Ispit>().ForMember(m => m.id, opt => opt.Ignore());
            Mapper.CreateMap<PredmetDto, Predmet>().ForMember(m => m.id, opt => opt.Ignore());
           
        }
    }
}