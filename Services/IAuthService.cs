using ProyectoTecWeb.Models.DTO;

namespace ProyectoTecWeb.Services
{
    public interface IAuthService
    {
        Task<(bool Ok, LoginResponseDto? response)> LoginAsync(LoginUserDto dto); 
        Task<string> RegisterAsync(RegisterUserDto dto); 
        Task<(bool ok, LoginResponseDto? response)> RefreshAsync(RefreshRequestDto dto);

        Task<bool> LogoutAsync(string email);
    }
}