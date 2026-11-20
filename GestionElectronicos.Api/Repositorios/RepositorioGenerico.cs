using Microsoft.Data.SqlClient;
using RepoDb;

namespace GestionElectronicos.Api.Repositorios;

public sealed class RepositorioGenerico<T>(IConfiguration configuracion) : IRepository<T> where T : class
{
    private readonly string _conexion = configuracion.GetConnectionString("SqlServer")
        ?? throw new InvalidOperationException("No existe ConnectionStrings:SqlServer.");

    public IReadOnlyList<T> Listar()
    {
        using var cn = new SqlConnection(_conexion);
        return cn.QueryAll<T>().ToList();
    }

    public T? Obtener(int id)
    {
        using var cn = new SqlConnection(_conexion);
        return cn.Query<T>(id).FirstOrDefault();
    }

    public long Insertar(T entidad)
    {
        using var cn = new SqlConnection(_conexion);
        var identidad = cn.Insert(entidad);
        return Convert.ToInt64(identidad);
    }

    public int Actualizar(T entidad)
    {
        using var cn = new SqlConnection(_conexion);
        return cn.Update(entidad);
    }

    public int Eliminar(int id)
    {
        using var cn = new SqlConnection(_conexion);
        return cn.Delete<T>(id);
    }
}
