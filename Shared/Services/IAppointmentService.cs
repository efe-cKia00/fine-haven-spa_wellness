using System.Threading.Tasks;
using CS212FinalProject.Models;

namespace Shared.Services
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