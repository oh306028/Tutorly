using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Tutorly.Domain.Models;

namespace Tutorly.WebAPI.Services
{
    public interface IUserContextService
    {
        int? GetUserId { get; }
        ClaimsPrincipal User { get; }
    }

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _context;

        public UserContextService(IHttpContextAccessor context)
        {
            _context = context;
        }
        public int? GetUserId => User is null ? null : int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public ClaimsPrincipal User => _context.HttpContext?.User;
    }
}
