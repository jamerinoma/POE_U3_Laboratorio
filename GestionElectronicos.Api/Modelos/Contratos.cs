namespace GestionElectronicos.Api.Modelos;

public record Credenciales(string NombreUsuario, string Clave);
public record SesionDto(string Token, int IdEmpleado, string Nombre, string NombreUsuario);
public record ProductoDto(int Id, string Nombre, string Descripcion, decimal Precio, int Stock);
public record ClienteDto(int Id, string Nombre, string Apellido, string Telefono, string Email);
public record ProductoEntrada(string Nombre, string? Descripcion, decimal Precio, int Stock);
public record ClienteEntrada(string Nombre, string Apellido, string? Telefono, string? Email);
public record LineaVenta(int IdProducto, int Cantidad);
public record NuevaVenta(int IdCliente, List<LineaVenta> Detalles);
public class VentaDto
{
    public int IdVenta { get; set; }
    public DateTime Fecha { get; set; }
    public string Cliente { get; set; } = "";
    public string Empleado { get; set; } = "";
    public int Unidades { get; set; }
    public decimal Total { get; set; }
}
public class VentaProductoDto
{
    public int IdProducto { get; set; }
    public string Producto { get; set; } = "";
    public int Unidades { get; set; }
    public decimal Monto { get; set; }
}
public class StockDto
{
    public int IdProducto { get; set; }
    public string Producto { get; set; } = "";
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public string Estado { get; set; } = "";
}
public record ErrorDto(string Mensaje);
