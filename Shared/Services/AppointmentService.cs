using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CS212FinalProject.Data;
using CS212FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace Shared.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IDbContextFactory<CS212FinalProjectContext> _contextFactory;

        public AppointmentService(IDbContextFactory<CS212FinalProjectContext> contextFactory)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        public async Task<OperationResult> ValidateAndCreateAsync(Appointment appointment)
        {
            if (appointment == null)
                return OperationResult.Fail("Appointment is required.");

            // Validation errors collected here
            var errors = new List<string>();

            await using var db = await _contextFactory.CreateDbContextAsync();

            // Date/time sanity
            if (appointment.DateAndTime == default)
            {
                errors.Add("Appointment start time must be provided.");
            }
            else if (appointment.DateAndTime < DateTime.Now.AddMinutes(-1))
            {
                errors.Add("Appointment start time cannot be in the past.");
            }

            // Customer must exist and be a Customer
            var customer = await db.Users.FindAsync(appointment.CustomerId);
            if (customer == null)
            {
                errors.Add("Selected customer does not exist.");
            }
            else if (customer.Role != RoleType.Customer)
            {
                errors.Add("Selected customer must have role 'Customer'.");
            }

            // Optional: Service provider must exist and be a ServiceProvider
            if (appointment.ServiceProviderId.HasValue)
            {
                var sp = await db.Users.FindAsync(appointment.ServiceProviderId.Value);
                if (sp == null)
                {
                    errors.Add("Selected service provider does not exist.");
                }
                else if (sp.Role != RoleType.ServiceProvider)
                {
                    errors.Add("Selected service provider must have role 'ServiceProvider'.");
                }
            }

            // Service must exist (optionally require IsAvailable)
            var service = await db.Services.FindAsync(appointment.ServiceId);
            if (service == null)
            {
                errors.Add("Selected service does not exist.");
            }
            else if (!service.IsAvailable)
            {
                errors.Add("Selected service is not available.");
            }

            if (errors.Count > 0)
                return OperationResult.Fail(errors.ToArray());

            // Example extra server-side safety checks:
            // Prevent exact-duplicate appointment
            var existsSame = await db.Appointments.AnyAsync(a =>
                a.ServiceProviderId == appointment.ServiceProviderId &&
                a.DateAndTime == appointment.DateAndTime &&
                a.ServiceId == appointment.ServiceId &&
                a.CustomerId == appointment.CustomerId);

            if (existsSame)
                return OperationResult.Fail("An identical appointment already exists.");

            // Persist
            db.Appointments.Add(appointment);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return OperationResult.Fail("Database update failed when saving the appointment.", ex.Message);
            }

            return OperationResult.Ok();
        }
    }
}