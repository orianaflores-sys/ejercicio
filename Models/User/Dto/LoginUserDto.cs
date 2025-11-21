using System.ComponentModel.DataAnnotations;

namespace ProyectoTecWeb.Models.DTO
{
    public record LoginUserDto
    {
        [DataType(DataType.EmailAddress), EmailAddress]
        public required string Email { get; init; }
        [MaxLength(25), MinLength(8)]
        public required string Password { get; init; }
    }
}