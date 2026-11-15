namespace Iventario.Api.Models;

public class ExistenciaProducto
{
    public int IdExistencia { get; set; }
    public int IdProducto { get; set; }
    public int IdProveedor { get; set; }
    public string? NumeroLorte { get; set; }
    public  decimal Precio { get; set; }
    public decimal Cantidad { get; set; }
}