using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversidadAPI.Data;
using UniversidadAPI.Models;

namespace UniversidadAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocenteController : ControllerBase
{
    private readonly AppDbContext _context;

    public DocenteController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Docente>>> GetDocentes() =>
        await _context.Docentes.AsNoTracking().ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Docente>> GetDocente(int id)
    {
        var docente = await _context.Docentes.FindAsync(id);
        return docente is null ? NotFound() : Ok(docente);
    }

    [HttpPost]
    public async Task<ActionResult<Docente>> CrearDocente(Docente docente)
    {
        _context.Docentes.Add(docente);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDocente), new { id = docente.DocenteId }, docente);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> ActualizarDocente(int id, Docente docente)
    {
        if (id != docente.DocenteId) return BadRequest("El id de la URL no coincide con DocenteId.");
        if (!await _context.Docentes.AnyAsync(d => d.DocenteId == id)) return NotFound();

        _context.Entry(docente).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> EliminarDocente(int id)
    {
        var docente = await _context.Docentes.FindAsync(id);
        if (docente is null) return NotFound();

        if (await _context.Asignaciones.AnyAsync(a => a.DocenteId == id))
            return Conflict("No se puede eliminar el docente porque tiene asignaciones.");

        _context.Docentes.Remove(docente);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
