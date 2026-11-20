namespace GestionElectronicos.Api.Modelos;

// Nombres de clase y campos preservan el esquema SQL base de la actividad.
public class Clientes
{
    public int id_cliente { get; set; }
    public string nombre { get; set; } = "";
    public string apellido { get; set; } = "";
    public string? telefono { get; set; }
    public string? email { get; set; }
}
public class Empleados
{
    public int id_empleado { get; set; }
    public string nombre { get; set; } = "";
    public string apellido { get; set; } = "";
    public string cargo { get; set; } = "";
}
public class Productos
{
    public int id_producto { get; set; }
    public string nombre { get; set; } = "";
    public string? descripcion { get; set; }
    public decimal precio { get; set; }
    public int stock { get; set; }
}
public class Ventas
{
    public int id_venta { get; set; }
    public DateTime fecha { get; set; }
    public int id_cliente { get; set; }
    public int id_empleado { get; set; }
}
public class DetalleVenta
{
    public int id_detalle { get; set; }
    public int id_venta { get; set; }
    public int id_producto { get; set; }
    public int cantidad { get; set; }
    public decimal precio_unitario { get; set; }
}
public class UsuarioAcceso
{
    public int id_usuario { get; set; }
    public int id_empleado { get; set; }
    public string nombre_usuario { get; set; } = "";
    public byte[] clave_hash { get; set; } = [];
    public byte[] clave_salt { get; set; } = [];
    public bool activo { get; set; }
}
