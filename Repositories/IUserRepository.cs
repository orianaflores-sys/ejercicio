using System.Reflection;
using ProyectoTecWeb.Models;

namespace ProyectoTecWeb.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAddress(string email); 
        Task AddAsync(User user); 
        Task UpdateAsync(User user); 

        Task<User?> GetByRefreshToken(string refreshToken); 
    }
}