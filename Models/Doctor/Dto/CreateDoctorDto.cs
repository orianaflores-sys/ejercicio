using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace ProyectoTecWeb.Models.DTO
{
    public record CreateDoctorDto
    {
        public Guid UserId {get; set; } //fk user

        [Required, StringLength(50)]
        public string Name {get; set; } = string.Empty; 
        [Required, MinLength(8), MaxLength(8)]
        public string Phone {get; set; } = string.Empty; 
        [Required,StringLength(100)]
        public string Specialty {get; set; } = string.Empty;
    }
}