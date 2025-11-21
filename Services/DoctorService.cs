using System.Data.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProyectoTecWeb.Models;
using ProyectoTecWeb.Models.DTO;
using ProyectoTecWeb.Repository;

namespace ProyectoTecWeb.Services
{
    public class DoctorService: IDoctorService
    {
        private readonly IDoctorRepository _repo; 
        public DoctorService(IDoctorRepository repo) => _repo = repo; 

        public async Task<DoctorResponse> GetOneDoctor(Guid id)
        {
            var doc = await _repo.GetDoctor(id); 
            if(doc is null) throw new ArgumentException("Doctor not found"); 
            var response = new DoctorResponse
            {
                DoctorId = doc.DoctorId,
                Name = doc.Name,
                Phone = doc.Phone,
                Specialty = doc.Specialty
            }; 
            return response; 
        } 
        public async Task<IEnumerable<DoctorResponse>> GetAllDoctors()
        {
            var items = await _repo.GetAllDoctorsAsync(); 
            var response = items.Select(doc => new DoctorResponse
            {
                DoctorId = doc.DoctorId,
                Name = doc.Name,
                Phone = doc.Phone,
                Specialty = doc.Specialty
            }); 
            return response; 
        } 

        public async Task<Doctor> CreateDoctor(CreateDoctorDto dto)
        {
            
            var created = new Doctor
            {
                DoctorId = Guid.NewGuid(),
                UserId = dto.UserId,
                Name = dto.Name,
                Phone = dto.Phone,
                Specialty = dto.Specialty
            }; 
            await _repo.AddAsync(created); 
            await _repo.SaveChangesAsync(); 
            return created; 
        } 

        public async Task<Doctor> UpdateDoctor( UpdateDoctorDto dto, Guid id)
        {
            var doctor = await _repo.GetDoctor(id);
            if(doctor is null) throw new ArgumentException("Doctor not found");  
            doctor.Name = dto.Name;
            doctor.Phone = dto.Phone; 
            doctor.Specialty = dto.Specialty; 
            await _repo.UpadteAsync(doctor); 
            return doctor; 
        }

        public async Task Delete(Guid id)
        {
            var deleted = await _repo.GetDoctor(id); 
            if(deleted is null) throw new ArgumentException("Doctor not found"); 
            await _repo.DeleteAsync(deleted); 
        }
    }
}