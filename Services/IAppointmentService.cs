using ProyectoTecWeb.Models.DTO;

namespace ProyectoTecWeb.Services
{
    public interface IAppointmentService
    {
        Task<AppointmentResponseDto?> GetOne(Guid id);
        Task<IEnumerable<AppointmentResponseDto>> GetAll();
        Task<IEnumerable<AppointmentResponseDto>> GetByDoctor(Guid doctorId);
        Task<IEnumerable<AppointmentResponseDto>> GetByPatient(Guid patientId);

        Task<AppointmentResponseDto> CreateAppointment(CreateAppointmentDto dto);
        Task<AppointmentResponseDto?> Update(Guid id, UpdateAppointmentDto dto);
        Task Delete(Guid id);
    }
}
