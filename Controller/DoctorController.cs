using System.Drawing;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoTecWeb.Models.DTO;
using ProyectoTecWeb.Services;

namespace ProyectoTecWeb.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doc; 

        public DoctorController(IDoctorService doc)
        {
            _doc = doc; 
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOneDoctor(Guid id)
        {
            var doctor = await _doc.GetOneDoctor(id); 
            return Ok(doctor); 
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorDto dto)
        { if(!ModelState.IsValid) return ValidationProblem(ModelState); 
            var created = await _doc.CreateDoctor(dto); 
            return CreatedAtAction(nameof(GetOneDoctor), new {id = created.DoctorId}, created); 
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<DoctorResponse> items =await _doc.GetAllDoctors(); 
            return Ok(items); 
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromBody] UpdateDoctorDto dto, Guid id)
        {
            if(!ModelState.IsValid) return ValidationProblem(ModelState); 
            var Update = await _doc.UpdateDoctor(dto, id); 
            return CreatedAtAction(nameof(GetOneDoctor), new {id = Update.DoctorId}, Update); 
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _doc.Delete(id); 
            return NoContent(); 
        }
    }
    
}