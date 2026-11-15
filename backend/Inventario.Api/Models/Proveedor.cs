namespace Iventario.Api.Models;

public class Proveedores
{
    public int IdProveedor { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Correo { get; set; }
}