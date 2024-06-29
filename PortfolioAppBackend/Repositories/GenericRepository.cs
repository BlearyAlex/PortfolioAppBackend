using Microsoft.EntityFrameworkCore;
using PortfolioAppBackend.Data.DBContext;
using PortfolioAppBackend.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PortfolioAppBackend.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TModel>> Lista()
        {
            return await _context.Set<TModel>().ToListAsync();
        }

        public async Task<List<TModel>> Lista(Expression<Func<TModel, bool>> filtro)
        {
            try
            {
                List<TModel> modelos = await _context.Set<TModel>().Where(filtro).ToListAsync();
                return modelos;
            }
            catch (Exception)
            {
                throw; // Re-lanza la excepción para que se maneje en un nivel superior
            }
        }

        public async Task<TModel> Crear(TModel modelo)
        {
            try
            {
                _context.Set<TModel>().Add(modelo);
                await _context.SaveChangesAsync();
                return modelo;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(TModel modelo)
        {
            try
            {
                // Actualiza el modelo en el contexto y guarda los cambios
                _context.Set<TModel>().Update(modelo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw; // Re-lanza la excepción para que se maneje en un nivel superior
            }
        }

        public async Task<bool> Eliminar(TModel modelo)
        {
            try
            {
                // Elimina el modelo del contexto y guarda los cambios
                _context.Set<TModel>().Remove(modelo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw; // Re-lanza la excepción para que se maneje en un nivel superior
            }
        }

        public async Task<TModel> Obtener(Expression<Func<TModel, bool>> filtro)
        {
            try
            {
                TModel model = await _context.Set<TModel>().FirstOrDefaultAsync(filtro);

                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TModel> ObtenerById(int id)
        {
            return await _context.Set<TModel>().FindAsync(id);
        }
    }
}