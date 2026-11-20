namespace GestionElectronicos.Api.Repositorios;

public interface IRepository<T> where T : class
{
    IReadOnlyList<T> Listar();
    T? Obtener(int id);
    long Insertar(T entidad);
    int Actualizar(T entidad);
    int Eliminar(int id);
}
