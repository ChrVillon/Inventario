using Microsoft.EntityFrameworkCore;
using Inventario.Api.Data;

namespace Inventario.Api.Services;

public class AutenticationService
{
    private readonly InventarioContexto _context;

    public AutenticationService(InventarioContexto context)
    {
        _context = context;
    }

    public async Task<bool> ValidarCredenciales(string correo, string clave)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);

        if (usuario == null)
        {
            return false;
        }

        return BCrypt.Net.BCrypt.Verify(clave, usuario.Clave);
    }
}