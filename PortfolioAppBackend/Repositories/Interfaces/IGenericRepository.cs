using System.Linq.Expressions;

namespace PortfolioAppBackend.Repositories.Interfaces
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<List<TModel>> Lista();
        Task<List<TModel>> Lista(Expression<Func<TModel, bool>> filtro);
        Task<TModel> Crear(TModel modelo);
        Task<bool> Editar(TModel modelo);
        Task<bool> Eliminar(TModel modelo);
        Task<TModel> Obtener(Expression<Func<TModel, bool>> filtro);
        Task<TModel> ObtenerById(int id);
    }
}
