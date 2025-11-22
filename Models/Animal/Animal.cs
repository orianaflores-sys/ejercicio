namespace ProyectoTecWeb.Models
{
    public class Animal
    {
        public Guid AnimalId { get; set; }
        public string Name { get; set; } = null!;
        public string Species { get; set; } = null!;
        public int Age { get; set; }
    }
}
