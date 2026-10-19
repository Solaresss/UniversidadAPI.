using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversidadAPI.Data;
using UniversidadAPI.Models;

namespace UniversidadAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EstudianteController : ControllerBase
{
    private readonly AppDbContext _context;

    public EstudianteController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Estudiante>>> GetEstudiantes() =>
        await _context.Estudiantes.AsNoTracking().ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Estudiante>> GetEstudiante(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);
        return estudiante is null ? NotFound() : Ok(estudiante);
    }

    [HttpPost]
    public async Task<ActionResult<Estudiante>> CrearEstudiante(Estudiante estudiante)
    {
        _context.Estudiantes.Add(estudiante);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetEstudiante), new { id = estudiante.EstudianteId }, estudiante);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> ActualizarEstudiante(int id, Estudiante estudiante)
    {
        if (id != estudiante.EstudianteId) return BadRequest("El id de la URL no coincide con EstudianteId.");
        if (!await _context.Estudiantes.AnyAsync(e => e.EstudianteId == id)) return NotFound();

        _context.Entry(estudiante).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> EliminarEstudiante(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);
        if (estudiante is null) return NotFound();

        if (await _context.Asignaciones.AnyAsync(a => a.EstudianteId == id))
            return Conflict("No se puede eliminar el estudiante porque tiene asignaciones.");

        _context.Estudiantes.Remove(estudiante);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
