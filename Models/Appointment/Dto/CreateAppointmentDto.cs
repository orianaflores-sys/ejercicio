using System.ComponentModel.DataAnnotations;

namespace ProyectoTecWeb.Models.DTO
{
    public record CreateAppointmentDto
    {
        [Required]
        public Guid DoctorId { get; set; }

        [Required]
        public Guid PatientId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        [Required]
        public string Reason { get; set; } = string.Empty;
    }
}
