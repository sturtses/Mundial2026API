using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mundial2026API.Data;
using Mundial2026API.Models;

namespace Mundial2026API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeleccionesController : ControllerBase
{
    private readonly MundialContext _context;

    public SeleccionesController(MundialContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Seleccion>>> Get()
    {
        return await _context.Selecciones.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Seleccion>> Get(int id)
    {
        var seleccion = await _context.Selecciones.FindAsync(id);

        if (seleccion == null)
            return NotFound();

        return seleccion;
    }

    [HttpPost]
    public async Task<ActionResult<Seleccion>> Post(Seleccion seleccion)
    {
        _context.Selecciones.Add(seleccion);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = seleccion.Id }, seleccion);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Seleccion seleccion)
    {
        if (id != seleccion.Id)
            return BadRequest();

        _context.Entry(seleccion).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var seleccion = await _context.Selecciones.FindAsync(id);

        if (seleccion == null)
            return NotFound();

        _context.Selecciones.Remove(seleccion);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}