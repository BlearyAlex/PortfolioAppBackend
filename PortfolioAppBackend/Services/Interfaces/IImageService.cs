using PortfolioAppBackend.DTO;

namespace PortfolioAppBackend.Services.Interfaces
{
    public interface IImageService
    {
        Task<List<ImageDTO>> Lista();
        Task<ImageDTO> GetById(int id);
        Task<ImageDTO> Create(ImageDTO imageDto);
        Task<bool> Update(ImageDTO imageDto);
        Task<bool> Delete(int id);
    }
}
