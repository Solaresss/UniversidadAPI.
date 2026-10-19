using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversidadAPI.Data;
using UniversidadAPI.Models;

namespace UniversidadAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CursoController : ControllerBase
{
    private readonly AppDbContext _context;

    public CursoController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Curso>>> GetCursos() =>
        await _context.Cursos.AsNoTracking().ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Curso>> GetCurso(int id)
    {
        var curso = await _context.Cursos.FindAsync(id);
        return curso is null ? NotFound() : Ok(curso);
    }

    [HttpPost]
    public async Task<ActionResult<Curso>> CrearCurso(Curso curso)
    {
        _context.Cursos.Add(curso);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCurso), new { id = curso.CursoId }, curso);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> ActualizarCurso(int id, Curso curso)
    {
        if (id != curso.CursoId) return BadRequest("El id de la URL no coincide con CursoId.");
        if (!await _context.Cursos.AnyAsync(c => c.CursoId == id)) return NotFound();

        _context.Entry(curso).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> EliminarCurso(int id)
    {
        var curso = await _context.Cursos.FindAsync(id);
        if (curso is null) return NotFound();

        if (await _context.Asignaciones.AnyAsync(a => a.CursoId == id))
            return Conflict("No se puede eliminar el curso porque tiene asignaciones.");

        _context.Cursos.Remove(curso);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
