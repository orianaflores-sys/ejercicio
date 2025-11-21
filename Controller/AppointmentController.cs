using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoTecWeb.Models.DTO;
using ProyectoTecWeb.Services;

namespace ProyectoTecWeb.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _service;

        public AppointmentController(IAppointmentService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var data = await _service.GetOne(id);
            if (data is null) return NotFound();
            return Ok(data);
        }

        [Authorize]
        [HttpGet("doctor/{doctorId:guid}")]
        public async Task<IActionResult> GetByDoctor(Guid doctorId)
        {
            return Ok(await _service.GetByDoctor(doctorId));
        }

        [Authorize]
        [HttpGet("patient/{patientId:guid}")]
        public async Task<IActionResult> GetByPatient(Guid patientId)
        {
            return Ok(await _service.GetByPatient(patientId));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var created = await _service.CreateAppointment(dto);
            return CreatedAtAction(nameof(GetOne), new { id = created.AppointmentId }, created);
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAppointmentDto dto)
        {
            var updated = await _service.Update(id, dto);
            if (updated is null) return NotFound();

            return CreatedAtAction(nameof(GetOne), new { id = updated.AppointmentId }, updated);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}
