using AutoMapper;
using Tutorly.Application.Commands;
using Tutorly.Application.Dtos.CreateDtos;
using Tutorly.Application.Dtos.DisplayDtos;
using Tutorly.Domain.Models;

namespace Tutorly.WebAPI
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<RegisterUserDto, RegisterUser>()
                .ForMember(r => r.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(g => g.Grade, opt => opt.MapFrom(src => src.Grade));

            CreateMap<LoginUserDto, LoginUser>();

            CreateMap<Post, PostWithTutorDto>()
              .ForMember(dest => dest.CurrentStudentAmount, opt => opt.MapFrom(src => src.CurrentStudentAmount))
              .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students))
              .ForMember(dest => dest.IsAtStudentPlace, opt => opt.MapFrom(src => src.IsAtStudentPlace));

          
            CreateMap<Tutor, TutorDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Post, PostDto>();

            CreateMap<Tutor, TutorWithPostsDto>();
        }


    }
}
