using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventario.Api.Data;
using Inventario.Api.Models;
using Microsoft.AspNetCore.Authorization;

namespace Inventario.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductoController : ControllerBase
{
    private readonly InventarioContexto _contexto;

    public ProductoController(InventarioContexto contexto)
    {
        _contexto = contexto;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerProductos()
    {
        var productos = await _contexto.Productos.ToListAsync();
        return Ok(productos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerProducto(int id)
    {
        var producto = await _contexto.Productos.FindAsync(id);
        if (producto == null)
        {
            return NotFound(new { mensaje = "Producto no encontrado." });
        }

        return Ok(producto);
    }

    [HttpPost]
    public async Task<IActionResult> CrearProducto(Producto producto)
    {
        _contexto.Productos.Add(producto);
        await _contexto.SaveChangesAsync();
        return CreatedAtAction(nameof(ObtenerProducto), new { id = producto.IdProducto }, producto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarProducto(int id, Producto producto)
    {
        if (id != producto.IdProducto)
        {
            return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del producto." });
        }

        var productoExistente = await _contexto.Productos.FindAsync(id);

        if (productoExistente == null)
        {
            return NotFound(new { mensaje = "Producto no encontrado." });
        }

        productoExistente.Nombre = producto.Nombre;
        productoExistente.Descripcion = producto.Descripcion;

        await _contexto.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarProducto(int id)
    {
        var producto = await _contexto.Productos.FindAsync(id);

        if (producto == null)
        {
            return NotFound(new { mensaje = "Producto no encontrado." });
        }

        _contexto.Productos.Remove(producto);
        await _contexto.SaveChangesAsync();
        return NoContent();
    }
}