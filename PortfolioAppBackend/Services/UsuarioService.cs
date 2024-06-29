using Microsoft.EntityFrameworkCore;
using PortfolioAppBackend.Data.DBContext;
using PortfolioAppBackend.Services.Interfaces;

namespace PortfolioAppBackend.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDbContext _context;

        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UsuarioExiste(string email)
        {
            return await _context.Usuarios.AnyAsync(x => x.Email == email.ToLower());
        }
    }
}
