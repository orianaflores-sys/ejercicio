namespace ProyectoTecWeb.Models
{
    public class Doctor
    {
        public required Guid DoctorId {get; set; }
        public User? user {get; set; } 
        public Guid UserId {get; set; } //fk user
        public string Name {get; set; } = string.Empty; 
        public string Phone {get; set; } = string.Empty; 
        public string Specialty {get; set; } = string.Empty;

        public IEnumerable<Appointment> Appointments { get; set; } = new List<Appointment>(); 


    }
}