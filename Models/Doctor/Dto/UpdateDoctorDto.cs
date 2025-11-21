using System.ComponentModel.DataAnnotations;

namespace ProyectoTecWeb.Models.DTO
{
    public record UpdateDoctorDto
    {
        public required string Name {get; init; } 
        [Required, MinLength(8), MaxLength(8)]

        public required string Phone {get; init; }  
        [Required,StringLength(100)]

        public required string Specialty {get; init; }
    }
}