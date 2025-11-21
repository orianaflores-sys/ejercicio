using ProyectoTecWeb.Models;

namespace ProyectoTecWeb.Repository
{
    public interface IAppointmentRepository
    {
        Task AddAsync(Appointment appointment);
        Task<Appointment?> GetOneAsync(Guid id);
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<IEnumerable<Appointment>> GetByDoctorAsync(Guid doctorId);
        Task<IEnumerable<Appointment>> GetByPatientAsync(Guid patientId);

        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(Appointment appointment);

        Task SaveChangesAsync();
    }
}
