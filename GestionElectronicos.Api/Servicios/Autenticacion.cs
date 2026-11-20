using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using GestionElectronicos.Api.Modelos;
using GestionElectronicos.Api.Repositorios;

namespace GestionElectronicos.Api.Servicios;

public sealed record SesionInterna(int IdEmpleado, string Nombre, string Usuario, DateTime VenceUtc);

public sealed class Sesiones
{
    private readonly ConcurrentDictionary<string, SesionInterna> _activas = new();
    public string Crear(int empleado, string nombre, string usuario)
    {
        string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
        _activas[token] = new(empleado, nombre, usuario, DateTime.UtcNow.AddHours(8));
        return token;
    }
    public SesionInterna? Obtener(HttpContext ctx)
    {
        var encabezado = ctx.Request.Headers.Authorization.ToString();
        if (!encabezado.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)) return null;
        var token = encabezado[7..].Trim();
        return _activas.TryGetValue(token, out var sesion) && sesion.VenceUtc > DateTime.UtcNow ? sesion : null;
    }
    public void Cerrar(HttpContext ctx)
    {
        var encabezado = ctx.Request.Headers.Authorization.ToString();
        if (encabezado.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            _activas.TryRemove(encabezado[7..].Trim(), out _);
    }
}

public sealed class Autenticacion(IRepository<UsuarioAcceso> usuarios, IRepository<Empleados> empleados, Sesiones sesiones)
{
    public SesionDto? Ingresar(Credenciales credenciales)
    {
        var registro = usuarios.Listar().FirstOrDefault(x => x.activo &&
             string.Equals(x.nombre_usuario, credenciales.NombreUsuario?.Trim(), StringComparison.OrdinalIgnoreCase));
        if (registro is null || registro.clave_salt.Length != 16 || registro.clave_hash.Length != 32) return null;
        // SHA-256 + sal por usuario; comparación resistente a diferencias de tiempo.
        byte[] clave = Encoding.UTF8.GetBytes(credenciales.Clave ?? "");
        byte[] datos = new byte[registro.clave_salt.Length + clave.Length];
        registro.clave_salt.CopyTo(datos, 0);
        clave.CopyTo(datos, registro.clave_salt.Length);
        byte[] hash = SHA256.HashData(datos);
        if (!CryptographicOperations.FixedTimeEquals(hash, registro.clave_hash)) return null;
        var empleado = empleados.Obtener(registro.id_empleado);
        if (empleado is null) return null;
        string nombre = $"{empleado.nombre} {empleado.apellido}".Trim();
        return new SesionDto(sesiones.Crear(empleado.id_empleado, nombre, registro.nombre_usuario),
            empleado.id_empleado, nombre, registro.nombre_usuario);
    }
}
