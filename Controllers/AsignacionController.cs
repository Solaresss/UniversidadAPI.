using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversidadAPI.Data;
using UniversidadAPI.Models;

namespace UniversidadAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AsignacionController : ControllerBase
{
    private readonly AppDbContext _context;

    public AsignacionController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Asignacion>>> GetAsignaciones() =>
        await _context.Asignaciones.AsNoTracking().ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Asignacion>> GetAsignacion(int id)
    {
        var asignacion = await _context.Asignaciones.FindAsync(id);
        return asignacion is null ? NotFound() : Ok(asignacion);
    }

    [HttpPost]
    public async Task<ActionResult<Asignacion>> CrearAsignacion(Asignacion asignacion)
    {
        var referenciasValidas =
            await _context.Estudiantes.AnyAsync(e => e.EstudianteId == asignacion.EstudianteId) &&
            await _context.Cursos.AnyAsync(c => c.CursoId == asignacion.CursoId) &&
            await _context.Docentes.AnyAsync(d => d.DocenteId == asignacion.DocenteId);

        if (!referenciasValidas)
            return BadRequest("Debe indicar un EstudianteId, CursoId y DocenteId existentes.");

        _context.Asignaciones.Add(asignacion);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAsignacion), new { id = asignacion.AsignacionId }, asignacion);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> ActualizarAsignacion(int id, Asignacion asignacion)
    {
        if (id != asignacion.AsignacionId) return BadRequest("El id de la URL no coincide con AsignacionId.");
        if (!await _context.Asignaciones.AnyAsync(a => a.AsignacionId == id)) return NotFound();

        var referenciasValidas =
            await _context.Estudiantes.AnyAsync(e => e.EstudianteId == asignacion.EstudianteId) &&
            await _context.Cursos.AnyAsync(c => c.CursoId == asignacion.CursoId) &&
            await _context.Docentes.AnyAsync(d => d.DocenteId == asignacion.DocenteId);

        if (!referenciasValidas)
            return BadRequest("Debe indicar un EstudianteId, CursoId y DocenteId existentes.");

        _context.Entry(asignacion).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> EliminarAsignacion(int id)
    {
        var asignacion = await _context.Asignaciones.FindAsync(id);
        if (asignacion is null) return NotFound();

        _context.Asignaciones.Remove(asignacion);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
