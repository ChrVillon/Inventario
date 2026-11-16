using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventario.Api.Data;
using Inventario.Api.Models;

namespace Inventario.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExistenciaProductoController : ControllerBase
{
    private readonly InventarioContexto _contexto;

    public ExistenciaProductoController(InventarioContexto contexto)
    {
        _contexto = contexto;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerExistencias()
    {
        var existencias = await _contexto.ExistenciaProductos.ToListAsync();
        return Ok(existencias);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerExistencia(int id)
    {
        var existencia = await _contexto.ExistenciaProductos.FindAsync(id);
        if (existencia == null)
        {
            return NotFound(new { mensaje = "Existencia de producto no encontrada." });
        }

        return Ok(existencia);
    }  

    [HttpPost]
    public async Task<IActionResult> CrearExistencia(ExistenciaProducto existencia)
    {
        _contexto.ExistenciaProductos.Add(existencia);
        await _contexto.SaveChangesAsync();
        return CreatedAtAction(nameof(ObtenerExistencia), new { id = existencia.IdExistencia }, existencia);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarExistencia(int id, ExistenciaProducto existencia)
    {
        if (id != existencia.IdExistencia)
        {
            return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID de la existencia de producto." });
        }

        var existenciaExistente = await _contexto.ExistenciaProductos.FindAsync(id);

        if (existenciaExistente == null)
        {
            return NotFound(new { mensaje = "Existencia de producto no encontrada." });
        }
        
        existenciaExistente.Cantidad = existencia.Cantidad;

        await _contexto.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarExistencia(int id)
    {
        var existencia = await _contexto.ExistenciaProductos.FindAsync(id);
        if (existencia == null)
        {
            return NotFound(new { mensaje = "Existencia de producto no encontrada." });
        }

        _contexto.ExistenciaProductos.Remove(existencia);
        await _contexto.SaveChangesAsync();

        return NoContent();
    }
}