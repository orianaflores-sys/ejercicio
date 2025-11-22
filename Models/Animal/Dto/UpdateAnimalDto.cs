namespace ProyectoTecWeb.Models.DTOS.Animals
{
    public class UpdateAnimalDto
    {
        public string Name { get; set; } = null!;
        public string Species { get; set; } = null!;
        public int Age { get; set; }
    }
}
