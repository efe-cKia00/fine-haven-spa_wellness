using System.Threading.Tasks;
using CS212FinalProject.Models;
using Shared.Services;

namespace CS212FinalProject.Shared.Interfaces
{
    public interface IAppointmentService
    {
        /// <summary>
        /// Validates the appointment and, if valid, persists it to the database.
        /// Returns an OperationResult describing success or the validation errors.
        /// </summary>
        Task<OperationResult> ValidateAndCreateAsync(Appointment appointment);
    }
}