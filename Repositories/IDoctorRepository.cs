using ProyectoTecWeb.Models;

namespace ProyectoTecWeb.Repository
{
    public interface IDoctorRepository
    {
        Task<Doctor?> GetDoctor(Guid id); 
        Task AddAsync(Doctor doctor); 
        Task UpadteAsync(Doctor doctor); 
        Task DeleteAsync(Doctor doctor); 
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync(); 

        Task SaveChangesAsync(); 
    }
}