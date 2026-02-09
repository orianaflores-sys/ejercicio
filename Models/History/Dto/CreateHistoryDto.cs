using System.ComponentModel.DataAnnotations;

namespace ProyectoTecWeb.Models.DTO
{
    public class CreateHistoryDto
    {
        [Required]
        public Guid PatientId { get; set; }

        [Required, MaxLength(5), MinLength(4)]
        public string BloodType { get; set; } = string.Empty;

        [Required]
        public string Diagnoses { get; set; } = string.Empty;

        [Required]
        public string Medication { get; set; } = string.Empty;

        [Required]
        public string Allergies { get; set; } = string.Empty;
    }
}
