using System.ComponentModel.DataAnnotations;

namespace Inventario.Api.Models;

public class Producto
{
    [Key]
    public int IdProducto { get; set; }
    
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = string.Empty;
}