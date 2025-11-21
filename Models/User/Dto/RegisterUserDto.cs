
using System.ComponentModel.DataAnnotations;

namespace ProyectoTecWeb.Models.DTO
{
    public record RegisterUserDto
    {
        [Required, MaxLength(25)]
        public required string Username {get; init; }
        [Required, DataType(DataType.EmailAddress), EmailAddress]
        ///[RegularExpression( "^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$" , ErrorMessage = "Invalid email format." )] 
        public required string Email { get; init; }
        [Required, MaxLength(25), MinLength(8)]
        //[RegularExpression("^[aA-zZ]")]
        public required string Password { get; init;  }
        public required string phone {get; init; }
        [Required]
        public string Role { get; set; } = "User";
    }
}