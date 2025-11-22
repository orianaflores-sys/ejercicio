using ProyectoTecWeb.Models;
using ProyectoTecWeb.Models.DTOS.Animals;
using ProyectoTecWeb.Repository;

namespace ProyectoTecWeb.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _repo;

        public AnimalService(IAnimalRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Animal>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Animal?> GetOne(Guid id)
        {
            return await _repo.GetOne(id);
        }

        public async Task<Animal> CreateAnimal(CreateAnimalDto dto)
        {
            var animal = new Animal
            {
                AnimalId = Guid.NewGuid(),
                Name = dto.Name,
                Species = dto.Species,
                Age = dto.Age
            };

            await _repo.Add(animal);
            return animal;
        }

        public async Task<Animal> UpdateAnimal(UpdateAnimalDto dto, Guid id)
        {
            var animal = await _repo.GetOne(id);
            if (animal == null) throw new Exception("Animal not found");

            animal.Name = dto.Name;
            animal.Species = dto.Species;
            animal.Age = dto.Age;

            await _repo.Update(animal);
            return animal;
        }

        public async Task DeleteAnimal(Guid id)
        {
            var animal = await _repo.GetOne(id);
            if (animal == null) return;

            await _repo.Delete(animal);
        }
    }
}
