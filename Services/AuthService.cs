using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using ProyectoTecWeb.Models;
using ProyectoTecWeb.Models.DTO;
using ProyectoTecWeb.Repository;
using Sprache;
namespace ProyectoTecWeb.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _users; 
        private readonly IConfiguration _confi; 
        public AuthService(IUserRepository users, IConfiguration confi)
        {
            _users = users; 
            _confi = confi; 
        }
        public async Task<(bool Ok, LoginResponseDto? response)> LoginAsync(LoginUserDto dto)
        {
            var user = await _users.GetByEmailAddress(dto.Email); 
            if(user is null) throw new ArgumentException("Email doesnt exist"); 

            var ok = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password); 
            if(!ok) throw new ArgumentException("Incorrec Password"); 

            var (accessToken, expiresIn, jti) = GenerateJwtToken(user); 
            var refreshToken = GenerateSecureRefreshToken();
            var refreshDays = int.Parse(_confi["Jwt:RefreshDays"] ?? "14");

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(refreshDays);
            user.RefreshTokenRevokedAt = null;
            user.CurrentJwtId = jti;
            await _users.UpdateAsync(user);

            var resp = new LoginResponseDto
            {
                user = new UserResponseDto {id = user.Id, Username = user.UserName,Email = user.Email }, 
                Token = new TokenPresenterDto{ AcessToken = accessToken, TokenType = "Bearer", ExpireIn = expiresIn},
                refreshToken = new RefreshRequestDto{RefreshToken = refreshToken}
            }; 

            return (true, resp); 
        }
        public async Task<string> RegisterAsync(RegisterUserDto dto)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password); 
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email, 
                Password = hashedPassword, 
                UserName = dto.Username, 
                Role = dto.Role,
                Phone = dto.phone
            }; 
            await _users.AddAsync(user); 
            return user.Id.ToString(); 
        }

        public async Task<(bool ok, LoginResponseDto? response)> RefreshAsync(RefreshRequestDto dto)
        {
            // Buscar usuario que tenga ese refresh token (simple)
            var user = await _users.GetByRefreshToken(dto.RefreshToken);
            if (user == null) throw new ArgumentException("User not found");  

            // Validaciones de refresh
            if (user.RefreshToken != dto.RefreshToken) throw new ArgumentException("invalid refresh token"); 
            if (user.RefreshTokenRevokedAt.HasValue) throw new ArgumentException("refresh token used"); 
            if (!user.RefreshTokenExpiresAt.HasValue || user.RefreshTokenExpiresAt.Value < DateTime.UtcNow) throw new ArgumentException("refresh token used"); 

            // Rotación: generar nuevo access + refresh y revocar el anterior
            var (accessToken, expiresIn, jti) = GenerateJwtToken(user);
            var newRefresh = GenerateSecureRefreshToken();
            var refreshDays = int.Parse(Environment.GetEnvironmentVariable("JWT_REFRESH") !);  

            user.RefreshToken = newRefresh;
            user.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(refreshDays);
            user.RefreshTokenRevokedAt = null; // seguimos activo
            user.CurrentJwtId = jti;
            await _users.UpdateAsync(user);

            var resp = new LoginResponseDto
            {
                user = new UserResponseDto {id = user.Id, Username = user.UserName,Email = user.Email }, 
                Token = new TokenPresenterDto{ AcessToken = accessToken, TokenType = "Bearer", ExpireIn = expiresIn},
                refreshToken = new RefreshRequestDto{RefreshToken = newRefresh}
            };

            return (true, resp);
        }


        private (string token, int expiresInSeconds, string jti) GenerateJwtToken(User user)
        {
            var jwtSection = _confi.GetSection("Jwt"); 
            var key = Environment.GetEnvironmentVariable("JWT_KEY")!; 
            var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"); 
            var expireMinutes = int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRES")!); 

            var jti = Guid.NewGuid().ToString(); 

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), 
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(ClaimTypes.Name, user.UserName), 
                new Claim(ClaimTypes.Role, user.Role), 
                new Claim(JwtRegisteredClaimNames.Jti, jti),
            }; 
            var keyBytes = Encoding.UTF8.GetBytes(key); 
            var creds = new SigningCredentials(new SymmetricSecurityKey(keyBytes),SecurityAlgorithms.HmacSha256); 

            var expires = DateTime.UtcNow.AddMinutes(expireMinutes); 

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience : audience, 
                claims : claims,
                expires : expires,
                signingCredentials: creds
            ); 

            var jwt = new JwtSecurityTokenHandler().WriteToken(token); 
            return (jwt, (int)TimeSpan.FromMinutes(expireMinutes).TotalSeconds, jti); 
        }

        private static string GenerateSecureRefreshToken()
        {
            // 64 bytes aleatorios en Base64Url
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Base64UrlEncoder.Encode(bytes);
        }

        public async Task<bool> LogoutAsync(string email)
        {
            var user = await _users.GetByEmailAddress(email);

            if (user is null) throw new ArgumentException("User not found");

            user.RefreshToken = null;
            user.RefreshTokenExpiresAt = null;
            user.RefreshTokenRevokedAt = DateTime.UtcNow;
            user.CurrentJwtId = null;

            await _users.UpdateAsync(user);
            return true;
}
    }
}