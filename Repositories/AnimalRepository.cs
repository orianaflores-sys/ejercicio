using Microsoft.EntityFrameworkCore;
using ProyectoTecWeb.Data;
using ProyectoTecWeb.Models;

namespace ProyectoTecWeb.Repository
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly AppDbContext _db;

        public AnimalRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Animal>> GetAll()
        {
            return await _db.Animals.ToListAsync();
        }

        public async Task<Animal?> GetOne(Guid id)
        {
            return await _db.Animals.FirstOrDefaultAsync(a => a.AnimalId == id);
        }

        public async Task Add(Animal animal)
        {
            await _db.Animals.AddAsync(animal);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Animal animal)
        {
            _db.Animals.Update(animal);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Animal animal)
        {
            _db.Animals.Remove(animal);
            await _db.SaveChangesAsync();
        }
    }
}
