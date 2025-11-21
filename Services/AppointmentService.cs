using ProyectoTecWeb.Models;
using ProyectoTecWeb.Models.DTO;
using ProyectoTecWeb.Repository;

namespace ProyectoTecWeb.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;

        public AppointmentService(IAppointmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<AppointmentResponseDto> CreateAppointment(CreateAppointmentDto dto)
        {
            var appointment = new Appointment
            {
                AppointmentId = Guid.NewGuid(),
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                Date = dto.Date.Date,
                Time = dto.Time,
                Reason = dto.Reason,
                Status = 0,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(appointment);
            await _repo.SaveChangesAsync();

            return Map(appointment);
        }

        public async Task<AppointmentResponseDto?> GetOne(Guid id)
        {
            var item = await _repo.GetOneAsync(id);
            return item == null ? null : Map(item);
        }

        public async Task<IEnumerable<AppointmentResponseDto>> GetAll()
        {
            var data = await _repo.GetAllAsync();
            return data.Select(Map);
        }

        public async Task<IEnumerable<AppointmentResponseDto>> GetByDoctor(Guid doctorId)
        {
            var data = await _repo.GetByDoctorAsync(doctorId);
            return data.Select(Map);
        }

        public async Task<IEnumerable<AppointmentResponseDto>> GetByPatient(Guid patientId)
        {
            var data = await _repo.GetByPatientAsync(patientId);
            return data.Select(Map);
        }

        public async Task<AppointmentResponseDto?> Update(Guid id, UpdateAppointmentDto dto)
        {
            var appointment = await _repo.GetOneAsync(id);
            if (appointment == null) return null;

            appointment.Date = dto.Date.Date;
            appointment.Time = dto.Time;
            appointment.Reason = dto.Reason;
            appointment.Status = dto.Status;
            appointment.Notes = dto.Notes;

            await _repo.UpdateAsync(appointment);
            await _repo.SaveChangesAsync();

            return Map(appointment);
        }

        public async Task Delete(Guid id)
        {
            var appointment = await _repo.GetOneAsync(id);
            if (appointment == null) return;

            await _repo.DeleteAsync(appointment);
            await _repo.SaveChangesAsync();
        }

        private static AppointmentResponseDto Map(Appointment a)
        {
            return new AppointmentResponseDto
            {
                AppointmentId = a.AppointmentId,
                DoctorId = a.DoctorId,
                PatientId = a.PatientId,
                Date = a.Date,
                Time = a.Time,
                Reason = a.Reason,
                Status = a.Status,
                Notes = a.Notes,
                CreatedAt = a.CreatedAt
            };
        }
    }
}
