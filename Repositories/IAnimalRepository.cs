using ProyectoTecWeb.Models;

namespace ProyectoTecWeb.Repository
{
    public interface IAnimalRepository
    {
        Task<IEnumerable<Animal>> GetAll();
        Task<Animal?> GetOne(Guid id);
        Task Add(Animal animal);
        Task Update(Animal animal);
        Task Delete(Animal animal);
    }
}
