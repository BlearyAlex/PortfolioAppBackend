using PortfolioAppBackend.DTO;

namespace PortfolioAppBackend.Services.Interfaces
{
    public interface IVideoService
    {
        Task<List<VideoDTO>> Lista();
        Task<VideoDTO> ObtenerById(int id);
        Task<VideoDTO> Crear(VideoDTO videoDto);
        Task<bool> Editar(VideoDTO videoDto);
        Task<bool> Eliminar(int id);
    }
}
