using PortfolioAppBackend.DTO;

namespace PortfolioAppBackend.Services.Interfaces
{
    public interface IPostService
    {
        Task<List<PostDTO>> Lista();
        Task<PostDTO> GetById(int id);
        Task<PostDTO> Create(PostDTO postDto);
        Task<bool> Update(PostDTO postDto);
        Task<bool> Delete(int id);
    }
}
