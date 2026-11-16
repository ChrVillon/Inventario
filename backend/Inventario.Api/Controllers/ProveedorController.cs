using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventario.Api.Data;
using Inventario.Api.Models;

namespace Inventario.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProveedorController : ControllerBase
{
    private readonly InventarioContexto _contexto;

    public ProveedorController(InventarioContexto contexto)
    {
        _contexto = contexto;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerProveedores()
    {
        var proveedores = await _contexto.Proveedores.ToListAsync();
        return Ok(proveedores);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerProveedor(int id)
    {
        var proveedor = await _contexto.Proveedores.FindAsync(id);
        if (proveedor == null)
        {
            return NotFound(new { mensaje = "Proveedor no encontrado." });
        }

        return Ok(proveedor);
    }  

    [HttpPost]
    public async Task<IActionResult> CrearProveedor(Proveedor proveedor)
    {
        _contexto.Proveedores.Add(proveedor);
        await _contexto.SaveChangesAsync();
        return CreatedAtAction(nameof(ObtenerProveedor), new { id = proveedor.IdProveedor }, proveedor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarProveedor(int id, Proveedor proveedor)
    {
        if (id != proveedor.IdProveedor)
        {
            return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del proveedor." });
        }

        var proveedorExistente = await _contexto.Proveedores.FindAsync(id);

        if (proveedorExistente == null)
        {
            return NotFound(new { mensaje = "Proveedor no encontrado." });
        }

        proveedorExistente.Nombre = proveedor.Nombre;
        proveedorExistente.Correo = proveedor.Correo;

        await _contexto.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarProveedor(int id)
    {
        var proveedor = await _contexto.Proveedores.FindAsync(id);
        if (proveedor == null)
        {
            return NotFound(new { mensaje = "Proveedor no encontrado." });
        }

        _contexto.Proveedores.Remove(proveedor);
        await _contexto.SaveChangesAsync();

        return NoContent();
    }
}