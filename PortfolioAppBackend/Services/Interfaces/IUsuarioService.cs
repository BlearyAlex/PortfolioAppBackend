namespace PortfolioAppBackend.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<bool> UsuarioExiste(string email);
    }
}
