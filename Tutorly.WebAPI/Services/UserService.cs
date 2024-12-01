using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tutorly.Application.Commands;
using Tutorly.Application.Dtos.CreateDtos;
using Tutorly.Application.Dtos.DisplayDtos;
using Tutorly.Application.Handlers;
using Tutorly.Application.Interfaces;
using Tutorly.Application.Queries;
using Tutorly.Domain.Models;
using Tutorly.WebAPI.Authentication;

namespace Tutorly.WebAPI.Services
{
    public interface IUserService
    {
        Task RegisterUserAsync(RegisterUserDto dto);
        Task DeleteUser(int userId);

        Task<string> LoginUser(LoginUserDto dto);
        Task<UserDataDto> GetUserData(int userId);
        Task UpdateUser(UpdateUserDto dto, int userId);
    }

    public class UserService : IUserService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IHandler<RegisterUser> _registerUserHandler;

        private readonly IMapper _mapper;
        private readonly IHandler<DeleteUser> _deleteUserHandler;
        private readonly IHandler<LoginUser> _loginUserHandler;
        private readonly IQueryHandler<GetUserBy, User> _getUserByHandler;
        private readonly IQueryHandler<GetUserData, User> _getUserDataHandler;
        private readonly IHandler<UpdateUserData> _updateUserHandler;

        public UserService(JwtOptions jwtOptions, IHandler<RegisterUser> registerUserHandler,
            IMapper mapper, IHandler<DeleteUser> deleteUserHandler, IHandler<LoginUser> loginUserHandler,
            IQueryHandler<GetUserBy, User> getUserByHandler, IQueryHandler<GetUserData, User> getUserDataHandler, IHandler<UpdateUserData> updateUserHandler)    
        {
            _jwtOptions = jwtOptions;
            _registerUserHandler = registerUserHandler;
            _mapper = mapper;
            _deleteUserHandler = deleteUserHandler;
            _loginUserHandler = loginUserHandler;
            _getUserByHandler = getUserByHandler;
            _getUserDataHandler = getUserDataHandler;
            _updateUserHandler = updateUserHandler;
        }


        public async Task UpdateUser(UpdateUserDto dto, int userId)
        {
            var command = new UpdateUserData(userId, dto.FirstName, dto.LastName, dto.Email, dto.Experience, dto.Grade, dto.Description);

            await _updateUserHandler.Handle(command);
        }


        public async Task<UserDataDto> GetUserData(int userId)
        {
            var query = new GetUserData(userId);
            var userData = await _getUserDataHandler.HandleAsync(query);

            var mappedResult = _mapper.Map<UserDataDto>(userData);
            return mappedResult;

        }

        public async Task RegisterUserAsync(RegisterUserDto dto)    
        {
            var command = _mapper.Map<RegisterUser>(dto);

            await _registerUserHandler.Handle(command);
        }

        public async Task DeleteUser(int userId)
        {
            var command = new DeleteUser(userId);
            await _deleteUserHandler.Handle(command);
        }

        public async Task<string> LoginUser(LoginUserDto dto)
        {
            var command = _mapper.Map<LoginUser>(dto);
            await _loginUserHandler.Handle(command);

            var query = new GetUserBy(x => x.Email == dto.Email);   
            var user = await _getUserByHandler.HandleAsync(query);

            var token = GenerateToken(user);
            return token;

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
                new Claim(ClaimTypes.Role, Enum.GetName(typeof(Role), user.Role) ?? string.Empty)
               
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _jwtOptions.Issuer,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}
