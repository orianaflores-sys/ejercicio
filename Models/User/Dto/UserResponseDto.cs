namespace ProyectoTecWeb.Models.DTO
{
    public class UserResponseDto
    {
        public Guid id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
    }
}