using GestionElectronicos.Api.Modelos;
using GestionElectronicos.Api.Repositorios;

namespace GestionElectronicos.Api.Servicios;

public sealed class CatalogosServicio(IRepository<Productos> productos, IRepository<Clientes> clientes)
{
    public IEnumerable<ProductoDto> ListarProductos() => productos.Listar()
        .OrderBy(p => p.nombre).Select(p => new ProductoDto(p.id_producto, p.nombre,
            p.descripcion ?? "", p.precio, p.stock));
    public IEnumerable<ClienteDto> ListarClientes() => clientes.Listar()
        .OrderBy(p => p.apellido).ThenBy(p => p.nombre)
        .Select(p => new ClienteDto(p.id_cliente, p.nombre, p.apellido, p.telefono ?? "", p.email ?? ""));

    public (bool Exito, string Mensaje) GuardarProducto(int id, ProductoEntrada dato)
    {
        string nombre = (dato.Nombre ?? "").Trim();
        string descripcion = (dato.Descripcion ?? "").Trim();
        if (nombre.Length is < 2 or > 100) return (false, "Nombre entre 2 y 100 caracteres.");
        if (descripcion.Length > 255) return (false, "Descripción máxima: 255 caracteres.");
        if (dato.Precio <= 0 || dato.Precio > 99999999.99m) return (false, "Precio positivo y válido.");
        if (dato.Stock < 0) return (false, "No se admite stock negativo.");
        var e = new Productos { id_producto = id, nombre = nombre,
            descripcion = descripcion, precio = dato.Precio, stock = dato.Stock };
        if (id == 0) productos.Insertar(e);
        else if (productos.Actualizar(e) == 0) return (false, "El producto ya no existe.");
        return (true, "Producto guardado correctamente.");
    }

    public (bool Exito, string Mensaje) GuardarCliente(int id, ClienteEntrada dato)
    {
        string nombre = (dato.Nombre ?? "").Trim(), apellido = (dato.Apellido ?? "").Trim();
        string telefono = (dato.Telefono ?? "").Trim(), email = (dato.Email ?? "").Trim();
        if (nombre.Length is < 2 or > 100 || apellido.Length is < 2 or > 100)
            return (false, "Nombre y apellido: entre 2 y 100 caracteres.");
        if (telefono.Length > 20 || email.Length > 100) return (false, "Teléfono o correo muy largos.");
        if (email.Length > 0 && (!email.Contains('@') || !email.Contains('.')))
            return (false, "El correo indicado no es válido.");
        var e = new Clientes { id_cliente = id, nombre = nombre, apellido = apellido,
            telefono = telefono.Length == 0 ? null : telefono,
            email = email.Length == 0 ? null : email };
        if (id == 0) clientes.Insertar(e);
        else if (clientes.Actualizar(e) == 0) return (false, "El cliente ya no existe.");
        return (true, "Cliente guardado correctamente.");
    }
}
