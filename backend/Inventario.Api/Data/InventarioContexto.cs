using Microsoft.EntityFrameworkCore;

namespace Inventario.Api.Data;

public class InventarioContexto : DbContext
{
    public InventarioContexto(DbContextOptions<InventarioContexto> options) : base(options)
    {
    }

    public DbSet<Models.Producto> Productos { get; set; }
    public DbSet<Models.Proveedor> Proveedores { get; set; }
    public DbSet<Models.ExistenciaProducto> ExistenciaProductos { get; set; }
    public DbSet<Models.Usuario> Usuarios { get; set; }
}