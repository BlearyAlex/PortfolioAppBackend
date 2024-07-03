using AutoMapper;
using PortfolioAppBackend.DTO;
using PortfolioAppBackend.Models;
using PortfolioAppBackend.Repositories.Interfaces;
using PortfolioAppBackend.Services.Interfaces;
using System.Formats.Tar;

namespace PortfolioAppBackend.Services
{
    public class PostService : IPostService
    {
        private readonly IGenericRepository<Post> _postRepository;
        private readonly IMapper _mapper;

        public PostService(IGenericRepository<Post> postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PostDTO> GetById(int id)
        {
            try
            {
                var postQuery = await _postRepository.Obtener(p => p.IdPost == id);
                var postDto = _mapper.Map<PostDTO>(postQuery);
                return postDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<PostDTO>> Lista()
        {
            try
            {
                var queryPost = await _postRepository.Lista();
                var postDto = _mapper.Map<List<PostDTO>>(queryPost);
                return postDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<string> SavePostAndGetUrl(IFormFile postfile) 
        {
            try
            {
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(postfile.FileName)}";
                var filePath = Path.Combine("wwwroot/posts", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await postfile.CopyToAsync(fileStream);
                }

                return $"posts/{fileName}";
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool IsPost(IFormFile file)
        {
            var formatosPermitidos = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return formatosPermitidos.Contains(extension);
        }

        public async Task<PostDTO> Create(PostDTO postDto)
        {
            try
            {
                if(postDto.PostFile == null || postDto.PostFile.Length == 0)
                    throw new Exception("La imagen es requerida");

                if(!IsPost(postDto.PostFile))
                    throw new Exception("Formato de imagen no permitido");

                var postUrl = await SavePostAndGetUrl(postDto.PostFile);
                postDto.UrlImage = postUrl;

                var postEntity = _mapper.Map<Post>(postDto);
                var queryPost = await _postRepository.Crear(postEntity);

                postDto.IdPost = queryPost.IdPost;

                return postDto;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.InnerException?.Message);
            }
        }

        public async Task<bool> Update(PostDTO postDto)
        {
            try
            {
                if (postDto.PostFile != null && postDto.PostFile.Length > 0)
                {
                    if (!IsPost(postDto.PostFile))
                        throw new Exception("Formato de imagen no permitido");

                    var postUrl = await SavePostAndGetUrl(postDto.PostFile);
                    postDto.UrlImage = postUrl;
                }

                var searchPost = await _postRepository.ObtenerById(postDto.IdPost);
                if (searchPost == null)
                    throw new Exception("Post no encontrado");

                searchPost.Title = postDto.Title;
                searchPost.Description = postDto.Description;
                searchPost.UrlImage = postDto.UrlImage;

                var successPost = await _postRepository.Editar(searchPost);

                return successPost;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var queryPost = await _postRepository.ObtenerById(id);
                if (queryPost == null)
                    throw new Exception("Post no encontrado");

                var successPost = await _postRepository.Eliminar(queryPost);
                return successPost;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
