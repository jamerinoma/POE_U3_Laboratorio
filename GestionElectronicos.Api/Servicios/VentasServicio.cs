using System.Data;
using GestionElectronicos.Api.Modelos;
using Microsoft.Data.SqlClient;
using RepoDb;

namespace GestionElectronicos.Api.Servicios;

public sealed class VentasServicio(IConfiguration configuracion)
{
    private readonly string _cadena = configuracion.GetConnectionString("SqlServer")
        ?? throw new InvalidOperationException("Falta ConnectionStrings:SqlServer.");

    public int Registrar(NuevaVenta request, int empleadoId)
    {
        if (request.IdCliente < 1 || request.Detalles is null || request.Detalles.Count == 0)
            throw new ArgumentException("Seleccione un cliente y al menos un producto.");
        if (request.Detalles.Any(x => x.IdProducto < 1 || x.Cantidad < 1 || x.Cantidad > 10000))
            throw new ArgumentException("Los productos y cantidades deben ser válidos.");
        var detalles = request.Detalles.GroupBy(x => x.IdProducto)
            .Select(g => new LineaVenta(g.Key, checked(g.Sum(i => i.Cantidad)))).ToList();
        using var cn = new SqlConnection(_cadena);
        cn.Open();
        using var transaccion = cn.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            bool existeCliente = cn.ExecuteQuery<int>(
                "SELECT COUNT(1) FROM dbo.Clientes WHERE id_cliente=@Id",
                new { Id = request.IdCliente }, transaction: transaccion).First() == 1;
            if (!existeCliente) throw new ArgumentException("Cliente no encontrado.");
            var venta = new Ventas { fecha = DateTime.Today, id_cliente = request.IdCliente,
                id_empleado = empleadoId };
            int idVenta = Convert.ToInt32(cn.Insert(venta, transaction: transaccion));
            foreach (var detalle in detalles)
            {
                var producto = cn.ExecuteQuery<Productos>(
                    "SELECT id_producto,nombre,descripcion,precio,stock FROM dbo.Productos WITH (UPDLOCK, HOLDLOCK) WHERE id_producto=@Id",
                    new { Id = detalle.IdProducto }, transaction: transaccion).SingleOrDefault();
                if (producto is null) throw new ArgumentException($"No existe el producto {detalle.IdProducto}.");
                if (producto.stock < detalle.Cantidad)
                    throw new ArgumentException($"Stock insuficiente para {producto.nombre}. Disponible: {producto.stock}.");
                int afectadas = cn.ExecuteNonQuery(
                    "UPDATE dbo.Productos SET stock=stock-@Cantidad WHERE id_producto=@Id AND stock>=@Cantidad",
                    new { Id = detalle.IdProducto, Cantidad = detalle.Cantidad }, transaction: transaccion);
                if (afectadas != 1) throw new ArgumentException("El inventario cambió; vuelva a consultar existencias.");
                cn.Insert(new DetalleVenta { id_venta = idVenta, id_producto = producto.id_producto,
                    cantidad = detalle.Cantidad, precio_unitario = producto.precio }, transaction: transaccion);
            }
            transaccion.Commit();
            return idVenta;
        }
        catch
        {
            transaccion.Rollback();
            throw;
        }
    }

    public List<VentaDto> Historial()
    {
        const string sql = """
            SELECT v.id_venta AS IdVenta, v.fecha AS Fecha,
                   CONCAT(c.nombre, ' ', c.apellido) AS Cliente,
                   CONCAT(e.nombre, ' ', e.apellido) AS Empleado,
                   SUM(d.cantidad) AS Unidades,
                   CAST(SUM(d.cantidad*d.precio_unitario) AS DECIMAL(16,2)) AS Total
            FROM dbo.Ventas v
            JOIN dbo.Clientes c ON c.id_cliente=v.id_cliente
            JOIN dbo.Empleados e ON e.id_empleado=v.id_empleado
            JOIN dbo.DetalleVenta d ON d.id_venta=v.id_venta
            GROUP BY v.id_venta,v.fecha,c.nombre,c.apellido,e.nombre,e.apellido
            ORDER BY v.id_venta DESC
            """;
        using var cn = new SqlConnection(_cadena);
        return cn.ExecuteQuery<VentaDto>(sql).ToList();
    }

    public List<VentaProductoDto> ProductosVendidos(int? productoId)
    {
        const string sql = """
            SELECT p.id_producto AS IdProducto, p.nombre AS Producto,
                   SUM(d.cantidad) AS Unidades,
                   CAST(SUM(d.cantidad*d.precio_unitario) AS DECIMAL(16,2)) AS Monto
            FROM dbo.DetalleVenta d
            JOIN dbo.Productos p ON p.id_producto=d.id_producto
            WHERE (@Id IS NULL OR p.id_producto=@Id)
            GROUP BY p.id_producto,p.nombre
            ORDER BY Monto DESC
            """;
        using var cn = new SqlConnection(_cadena);
        return cn.ExecuteQuery<VentaProductoDto>(sql, new { Id = productoId }).ToList();
    }

    public List<StockDto> Inventario(int? productoId)
    {
        const string sql = """
            SELECT p.id_producto AS IdProducto, p.nombre AS Producto,
                   p.precio AS Precio, p.stock AS Stock,
                   CASE WHEN p.stock <= 5 THEN N'CRÍTICO'
                        WHEN p.stock <= 10 THEN N'PRECAUCIÓN' ELSE N'NORMAL' END AS Estado
            FROM dbo.Productos p
            WHERE (@Id IS NULL OR p.id_producto=@Id)
            ORDER BY p.nombre
            """;
        using var cn = new SqlConnection(_cadena);
        return cn.ExecuteQuery<StockDto>(sql, new { Id = productoId }).ToList();
    }
}
