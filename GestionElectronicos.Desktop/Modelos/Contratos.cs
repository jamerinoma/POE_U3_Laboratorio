namespace GestionElectronicos.Desktop.Modelos;

public sealed class LoginRespuesta
{
    public string Token { get; set; } = "";
    public int IdEmpleado { get; set; }
    public string Nombre { get; set; } = "";
    public string NombreUsuario { get; set; } = "";
}
public sealed class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public string Etiqueta => $"{Nombre} — ${Precio:N2} (Stock: {Stock})";
}
public sealed class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public string Telefono { get; set; } = "";
    public string Email { get; set; } = "";
    public string Etiqueta => $"{Nombre} {Apellido}";
}
public record ProductoGuardar(string Nombre, string Descripcion, decimal Precio, int Stock);
public record ClienteGuardar(string Nombre, string Apellido, string Telefono, string Email);
public record DetalleVentaEntrada(int IdProducto, int Cantidad);
public record VentaGuardar(int IdCliente, List<DetalleVentaEntrada> Detalles);
public sealed class VentaListado
{
    public int IdVenta { get; set; }
    public DateTime Fecha { get; set; }
    public string Cliente { get; set; } = "";
    public string Empleado { get; set; } = "";
    public int Unidades { get; set; }
    public decimal Total { get; set; }
}
public sealed class VentasPorProducto
{
    public int IdProducto { get; set; }
    public string Producto { get; set; } = "";
    public int Unidades { get; set; }
    public decimal Monto { get; set; }
}
public sealed class StockReporte
{
    public int IdProducto { get; set; }
    public string Producto { get; set; } = "";
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public string Estado { get; set; } = "";
}
public sealed class CarritoItem
{
    public int IdProducto { get; set; }
    public string Producto { get; set; } = "";
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal Subtotal => Cantidad * Precio;
}
