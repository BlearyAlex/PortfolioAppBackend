using PortfolioAppBackend.Models;

namespace PortfolioAppBackend.Services.Interfaces
{
    public interface ITokenServicio
    {
        string CrearToken(Usuario usuario);
    }
}
