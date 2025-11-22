using System.ComponentModel.DataAnnotations;

namespace ProyectoTecWeb.Models.DTOS.Animals
{
    public class CreateAnimalDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Species { get; set; } = null!;

        public int Age { get; set; }
    }
}
