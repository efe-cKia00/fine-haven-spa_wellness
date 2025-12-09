using Microsoft.EntityFrameworkCore;
using CS212FinalProject.Models;
namespace CS212FinalProject.Data;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new CS212FinalProjectContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<CS212FinalProjectContext>>());

        if (context == null || context.Users == null)
        {
            throw new NullReferenceException(
                "Null CS212FinalProjectContext or Users DbSet");
        }

        if (context.Users.Any())
        {
            return;
        }

        context.Users.AddRange(
            new User
            {
                FirstName = "John",
                LastName = "Denton",
                Email = "customer@example.com",
                PhoneNumber = "123-456-7890",
                Role = RoleType.Customer,
                PasswordHash = PasswordHasher.Hash("Pcustomer"),
            },
           new User
           {
               FirstName = "Sarah",
               LastName = "Keys",
               Email = "receptionist@example.com",
               PhoneNumber = "123-456-7890",
               Role = RoleType.Receptionist,
               PasswordHash = PasswordHasher.Hash("Preceptionist"),
           },
            new User
            {
                FirstName = "Nicole",
                LastName = "Weyner",
                Email = "sp@example.com",
                PhoneNumber = "123-456-7890",
                Role = RoleType.ServiceProvider,
                PasswordHash = PasswordHasher.Hash("Pserviceprovider"),
            },
             new User
             {
                 FirstName = "Dan",
                 LastName = "Fisher",
                 Email = "manager@example.com",
                 PhoneNumber = "123-456-7890",
                 Role = RoleType.Manager,
                 PasswordHash = PasswordHasher.Hash("Pmanager"),
             });

        context.SaveChanges();
    }
}
