using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventario.Api.Data;
using Inventario.Api.Models;

namespace Inventario.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly InventarioContexto _contexto;

    public UsuarioController(InventarioContexto contexto)
    {
        _contexto = contexto;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerUsuarios()
    {
        var usuarios = await _contexto.Usuarios.ToListAsync();
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerUsuario(int id)
    {
        var usuario = await _contexto.Usuarios.FindAsync(id);
        if (usuario == null)
        {
            return NotFound(new { mensaje = "Usuario no encontrado." });
        }

        return Ok(usuario);
    }  

    [HttpPost]
    public async Task<IActionResult> CrearUsuario(Usuario usuario)
    {
        _contexto.Usuarios.Add(usuario);
        await _contexto.SaveChangesAsync();
        return CreatedAtAction(nameof(ObtenerUsuario), new { id = usuario.IdUsuario }, usuario);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarUsuario(int id, Usuario usuario)
    {
        if (id != usuario.IdUsuario)
        {
            return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del usuario." });
        }

        var usuarioExistente = await _contexto.Usuarios.FindAsync(id);

        if (usuarioExistente == null)
        {
            return NotFound(new { mensaje = "Usuario no encontrado." });
        }

        usuarioExistente.Nombre = usuario.Nombre;
        usuarioExistente.Correo = usuario.Correo;
        usuarioExistente.Clave = usuario.Clave;

        await _contexto.SaveChangesAsync();

        return Ok(usuarioExistente);
    }
}