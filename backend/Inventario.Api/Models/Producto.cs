namespace Iventario.Api.Models;

public class Producto
{
    public int IdProducto { get; set; }
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = string.Empty;
}