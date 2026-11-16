using System.ComponentModel.DataAnnotations;

namespace Inventario.Api.Models;

public class Usuario
{
    [Key]
    public int IdUsuario { get; set; }
    
    public string Nombre { get; set; } = null!;
    public string Correo { get; set; } = null!;
    public string Clave { get; set; } = null!;
}