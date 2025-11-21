namespace ProyectoTecWeb.Models.DTO
{
    public class DoctorResponse {
        public Guid DoctorId {get; init;}
        public string Name {get; init; } = string.Empty; 
        public string Phone {get; init; } = string.Empty; 
        public string Specialty {get; init; } = string.Empty;
    }
}