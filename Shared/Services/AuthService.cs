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
        private readonly IDbContextFactory<CS212FinalProjectContext> _dbFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IDbContextFactory<CS212FinalProjectContext> dbFactory, IHttpContextAccessor httpContextAccessor)
        {
            _dbFactory = dbFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateAccountAsync(string firstName, string lastName, string email, string phoneNumber, string password)
        {
            email = (email ?? string.Empty).Trim().ToLowerInvariant();

            using var db = _dbFactory.CreateDbContext();
            if (await db.Users.AnyAsync(u => u.Email == email))
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

            db.Users.Add(user);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SignInAsync(string email, string password)
        {
            email = (email ?? string.Empty).Trim().ToLowerInvariant();

            using var db = _dbFactory.CreateDbContext();
            var user = await db.Users.SingleOrDefaultAsync(u => u.Email == email);
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
                await httpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
            // If HttpContext is null (e.g., called from a Blazor Server circuit),
            // this method will be a no-op; use a real HTTP endpoint to ensure sign-out.
        }
    }
}