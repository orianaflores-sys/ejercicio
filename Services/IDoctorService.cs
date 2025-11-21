using Microsoft.AspNetCore.Http.HttpResults;
using ProyectoTecWeb.Models;
using ProyectoTecWeb.Models.DTO;

namespace ProyectoTecWeb.Services
{
    public interface IDoctorService
    {
        Task<DoctorResponse> GetOneDoctor(Guid id); 
        Task<IEnumerable<DoctorResponse>> GetAllDoctors(); 

        Task<Doctor> CreateDoctor(CreateDoctorDto dto); 

        Task<Doctor> UpdateDoctor(UpdateDoctorDto dto, Guid id); 

        Task Delete (Guid id); 

    }
}