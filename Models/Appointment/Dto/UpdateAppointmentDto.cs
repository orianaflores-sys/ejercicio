using System.ComponentModel.DataAnnotations;

namespace ProyectoTecWeb.Models.DTO
{
    public record UpdateAppointmentDto
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        [Required]
        public string Reason { get; set; } = string.Empty;

        // 0 = Pending, 1 = Confirmed, 2 = Cancelled
        [Required]
        public int Status { get; set; }

        public string? Notes { get; set; }
    }
}
