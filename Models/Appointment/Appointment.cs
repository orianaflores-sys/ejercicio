namespace ProyectoTecWeb.Models
{
    public class Appointment
    {
        public Guid AppointmentId { get; set; }

        // Doctor FK (solo ID por ahora)
        public Guid DoctorId { get; set; }
        // public Doctor? Doctor { get; set; }

        // Patient FK (igual, solo ID por ahora)
        public Guid PatientId { get; set; }

        // Datos de la cita
        public DateTime Date { get; set; }      // Fecha
        public TimeSpan Time { get; set; }      // Hora exacta

        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// 0 = Pending
        /// 1 = Confirmed
        /// 2 = Cancelled
        /// </summary>
        public int Status { get; set; } = 0;

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Doctor? doctor {get; set;}
    }
}
