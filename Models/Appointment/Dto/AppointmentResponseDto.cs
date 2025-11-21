namespace ProyectoTecWeb.Models.DTO
{
    public class AppointmentResponseDto
    {
        public Guid AppointmentId { get; set; }

        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public string Reason { get; set; } = string.Empty;
        public int Status { get; set; }
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
