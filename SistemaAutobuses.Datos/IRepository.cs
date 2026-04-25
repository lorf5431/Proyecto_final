namespace SistemaAutobuses.Datos
{
    public interface IRepository<T>
    {
        List<T> ObtenerTodos();
        T? ObtenerPorId(int id);
        void Insertar(T entidad);
        void Actualizar(T entidad);
        void Eliminar(int id);
    }
}
