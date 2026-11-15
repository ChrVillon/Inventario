using Microsoft.EntityFrameworkCore;

namespace Iventario.Api.Data;

public class InventarioContexto : DbContext
{
    public InventarioContexto(DbContextOptions<InventarioContexto> options) : base(options)
    {
    }

    public DbSet<Models.Producto> Productos { get; set; }
    public DbSet<Models.Proveedor> Proveedores { get; set; }
    public DbSet<Models.ExistenciaProducto> ExistenciaProductos { get; set; }
}