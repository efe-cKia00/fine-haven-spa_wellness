using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using CS212FinalProject.Data;
using CS212FinalProject.Models;

namespace Shared.Services
{
    public class AuthService : IAuthService
    {
        private readonly CS212FinalProjectContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(CS212FinalProjectContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateAccountAsync(string firstName, string lastName, string email, string phoneNumber, string password)
        {
            email = (email ?? string.Empty).Trim().ToLowerInvariant();
            if (await _db.Users.AnyAsync(u => u.Email == email))
                return false;

            var user = new User
            {
                FirstName = firstName ?? string.Empty,
                LastName = lastName ?? string.Empty,
                Email = email,
                PhoneNumber = phoneNumber ?? string.Empty,
                // system-assigned default role
                Role = RoleType.Customer,
                PasswordHash = PasswordHasher.Hash(password ?? string.Empty)
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SignInAsync(string email, string password)
        {
            email = (email ?? string.Empty).Trim().ToLowerInvariant();
            var user = await _db.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null) return false;

            var valid = PasswordHasher.Verify(password ?? string.Empty, user.PasswordHash);
            if (!valid) return false;

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}".Trim()),
                    new Claim(ClaimTypes.Email, user.Email),
                    // roles are stored as enum -> string for claim
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }

            return true;
        }

        public async Task SignOutAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}