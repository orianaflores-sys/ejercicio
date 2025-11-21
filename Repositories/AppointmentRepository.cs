using Microsoft.EntityFrameworkCore;
using ProyectoTecWeb.Data;
using ProyectoTecWeb.Models;

namespace ProyectoTecWeb.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _ctx;
        public AppointmentRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddAsync(Appointment appointment)
        {
            await _ctx.appointments.AddAsync(appointment);
        }

        public async Task<Appointment?> GetOneAsync(Guid id)
        {
            return await _ctx.appointments.FirstOrDefaultAsync(a => a.AppointmentId == id);
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _ctx.appointments
                .OrderBy(a => a.Date)
                .ThenBy(a => a.Time)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByDoctorAsync(Guid doctorId)
        {
            return await _ctx.appointments
                .Where(a => a.DoctorId == doctorId)
                .OrderBy(a => a.Date)
                .ThenBy(a => a.Time)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByPatientAsync(Guid patientId)
        {
            return await _ctx.appointments
                .Where(a => a.PatientId == patientId)
                .OrderBy(a => a.Date)
                .ThenBy(a => a.Time)
                .ToListAsync();
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _ctx.appointments.Update(appointment);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Appointment appointment)
        {
            _ctx.appointments.Remove(appointment);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
