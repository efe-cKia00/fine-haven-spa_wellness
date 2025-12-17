using System.Threading.Tasks;

public interface IAuthService
{
    // include lastName and phoneNumber so creation persists all user fields
    Task<bool> CreateAccountAsync(string firstName, string lastName, string email, string phoneNumber, string password);
    Task<bool> SignInAsync(string email, string password);
    Task SignOutAsync();
}