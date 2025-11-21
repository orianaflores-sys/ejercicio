namespace ProyectoTecWeb.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; 
        public string UserName { get; set; } = string.Empty;
        public required string Phone { get; set; }

        ///
        public string Role { get; set; } = "User"; 

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiresAt { get; set; }
        public DateTime? RefreshTokenRevokedAt { get; set; }
        public string? CurrentJwtId { get; set; }

        //Relaciones
        //public Doctor? doctor {get; set ;}

    }
}