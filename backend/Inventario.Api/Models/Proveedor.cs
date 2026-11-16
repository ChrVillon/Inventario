namespace Inventario.Api.Models;

public class Proveedor
{
    public int IdProveedor { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Correo { get; set; }
}