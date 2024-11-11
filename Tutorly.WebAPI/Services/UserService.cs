using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tutorly.Application.Commands;
using Tutorly.Application.Dtos;
using Tutorly.Application.Handlers;
using Tutorly.Domain.Models;
using Tutorly.WebAPI.Authentication;

namespace Tutorly.WebAPI.Services
{
    public class UserService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IHandler<RegisterUser> _registerUserHandler;
        private readonly IMapper _mapper;

        public UserService(JwtOptions jwtOptions, IHandler<RegisterUser> registerUserHandler, IMapper mapper)
        {
            _jwtOptions = jwtOptions;
            _registerUserHandler = registerUserHandler;
            _mapper = mapper;
        }

        public void RegisterUser(RegisterUserDto dto)  
        {
            var command = _mapper.Map<RegisterUser>(dto);   

            _registerUserHandler.Handle(command);
        }


        public void LoginUser()
        {

        }


        private string GenerateToken(User user) 
        {
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Key);
            var securityKey = new SymmetricSecurityKey(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())

            }),
                Expires = DateTime.UtcNow.AddMinutes(120),
                Issuer = _jwtOptions.Issuer,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}
