using AutoMapper;
using Tutorly.Application.Commands;
using Tutorly.Application.Dtos;

namespace Tutorly.WebAPI
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<RegisterUserDto, RegisterUser>()
                .ForMember(r => r.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(g => g.Grade, opt => opt.MapFrom(src => src.Grade));

        }


    }
}
