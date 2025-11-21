using Microsoft.EntityFrameworkCore;
using ProyectoTecWeb.Data;
using ProyectoTecWeb.Models;

namespace ProyectoTecWeb.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db; 
        public UserRepository(AppDbContext db) => _db = db; 
        public Task<User?> GetByEmailAddress(string email) => _db.users.FirstOrDefaultAsync(u => u.Email == email); 
        
        public async Task AddAsync(User user)
        {
            _db.users.Add(user); 
            await _db.SaveChangesAsync(); 
        } 
        public async Task UpdateAsync(User user)
        {
            _db.users.Update(user); 
            await _db.SaveChangesAsync(); 
        } 

        public Task<User?> GetByRefreshToken(string refreshToken) =>
            _db.users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }
}