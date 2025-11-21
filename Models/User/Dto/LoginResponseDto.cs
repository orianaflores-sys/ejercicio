namespace ProyectoTecWeb.Models.DTO
{
    public class LoginResponseDto
    {
        public required UserResponseDto user { get; set; }
        public required TokenPresenterDto Token { get; set;  }    
        public required RefreshRequestDto refreshToken  { get; set; }    
    }


}