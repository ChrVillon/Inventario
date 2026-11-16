using Microsoft.AspNetCore.Mvc;
using Inventario.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PruebaController : ControllerBase
{
    private readonly InventarioContexto _contexto;

    public PruebaController(InventarioContexto contexto)
    {
        _contexto = contexto;
    }

    [HttpGet]
    public async Task<IActionResult> ProbarConexion()
    {
        var productos = await _contexto.Productos.ToListAsync();

        return Ok(productos);
    }
}