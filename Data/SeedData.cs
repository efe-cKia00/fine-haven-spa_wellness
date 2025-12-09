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

        // Check if any of the database context variable(s) are null. They shouldn't be.
        if (context == null || context.Users == null || context.Services == null || context.Appointments == null)
        {
            throw new NullReferenceException(
                "Null CS212FinalProjectContext or Users DbSet");
        }

        // Check if the database is already seeded
        if (context.Users.Any() && context.Services.Any() && context.Appointments.Any())
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
            }
        );

        context.Services.AddRange(
            new Service
            {
                Name = "Massage",
                Description = "Lorem ipsum dolor, sit amet consectetur adipisicing elit. Assumenda enim rerum laboriosam. Culpa, iusto? Accusamus quasi corporis facilis fugit similique, dolorum recusandae cupiditate nobis mollitia, deleniti aspernatur, quo veritatis rem? Sed quaerat, placeat eaque at voluptate itaque odit sint libero modi, maiores delectus qui magni.",
                Price = 20,
                IsAvailable = true,
            },
            new Service
            {
                Name = "Beard Trim",
                Description = "Lorem ipsum dolor, sit amet consectetur adipisicing elit. Assumenda enim rerum laboriosam. Culpa, iusto? Accusamus quasi corporis facilis fugit similique, dolorum recusandae cupiditate nobis mollitia, deleniti aspernatur, quo veritatis rem? Sed quaerat, placeat eaque at voluptate itaque odit sint libero modi, maiores delectus qui magni.",
                Price = 15,
                IsAvailable = true,
            },
            new Service
            {
                Name = "Deep Tissue Massage",
                Description = "Lorem ipsum dolor, sit amet consectetur adipisicing elit. Assumenda enim rerum laboriosam. Culpa, iusto? Accusamus quasi corporis facilis fugit similique, dolorum recusandae cupiditate nobis mollitia, deleniti aspernatur, quo veritatis rem? Sed quaerat, placeat eaque at voluptate itaque odit sint libero modi, maiores delectus qui magni.",
                Price = 10,
                IsAvailable = false,
            },
            new Service
            {
                Name = "Facial",
                Description = "Lorem ipsum dolor, sit amet consectetur adipisicing elit. Assumenda enim rerum laboriosam. Culpa, iusto? Accusamus quasi corporis facilis fugit similique, dolorum recusandae cupiditate nobis mollitia, deleniti aspernatur, quo veritatis rem? Sed quaerat, placeat eaque at voluptate itaque odit sint libero modi, maiores delectus qui magni.",
                Price = 25,
                IsAvailable = true,
            }
        );

        context.Appointments.AddRange(
            new Appointment
            {
                CustomerId = 1,
                ServiceProviderId = 3,
                ServiceId = 1,
                DateAndTime = DateTime.Now,
                Status = StatusType.Pending,
            },
            new Appointment
            {
                CustomerId = 1,
                ServiceProviderId = 3,
                ServiceId = 2,
                DateAndTime = DateTime.Now,
                Status = StatusType.Pending,
            },
            new Appointment
            {
                CustomerId = 1,
                ServiceProviderId = 3,
                ServiceId = 1,
                DateAndTime = DateTime.Now,
                Status = StatusType.NoShow,
            },
            new Appointment
            {
                CustomerId = 1,
                ServiceProviderId = 3,
                ServiceId = 2,
                DateAndTime = DateTime.Now,
                Status = StatusType.Completed,
            }
        );

        context.SaveChanges();
    }
}
