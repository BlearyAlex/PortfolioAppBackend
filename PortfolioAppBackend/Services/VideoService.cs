using AutoMapper;
using PortfolioAppBackend.Data.DBContext;
using PortfolioAppBackend.DTO;
using PortfolioAppBackend.Models;
using PortfolioAppBackend.Repositories.Interfaces;
using PortfolioAppBackend.Services.Interfaces;

namespace PortfolioAppBackend.Services
{
    public class VideoService : IVideoService
    {
        private readonly IGenericRepository<Video> _videoRepository;
        private readonly IMapper _mapper;

        public VideoService(IGenericRepository<Video> videoRepository, IMapper mapper)
        {
            _videoRepository = videoRepository;
            _mapper = mapper;
        }

        public async Task<List<VideoDTO>> Lista()
        {
            try
            {
                var queryVideo = await _videoRepository.Lista();
                var queryVideoDto = _mapper.Map<List<VideoDTO>>(queryVideo);
                return queryVideoDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<VideoDTO> ObtenerById(int id)
        {
            try
            {
                var queryVideo = await _videoRepository.Obtener(v => v.IdVideo == id);
                var queryVideoDto = _mapper.Map<VideoDTO>(queryVideo);
                return queryVideoDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Private Methods
        private async Task<string> SaveVideoAndGetUrl(IFormFile videoFile)
        {
            try
            {
                var fileName = $"{Guid.NewGuid()}_{Path.GetExtension(videoFile.FileName)}";
                var filePath = Path.Combine("wwwroot/videos", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await videoFile.CopyToAsync(stream);
                }

                return $"/videos/{fileName}";
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool EsVideo(IFormFile file)
        {
            var formatosPermitidos = new[] { ".mp4", ".avi", ".mov", ".wmv", ".flv", ".mkv" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return formatosPermitidos.Contains(extension);
        }
        #endregion Private Methods

        public async Task<VideoDTO> Crear(VideoDTO videoDto)
        {
            try
            {
                if (videoDto.VideoFile == null || videoDto.VideoFile.Length == 0)
                    throw new Exception("No se ha proporcionado ninguna imagen");

                if (!EsVideo(videoDto.VideoFile))
                    throw new Exception("El archivo proporcionado no es una imagen");

                // Guardar la imagen en el servidor y obtener la URL
                var videoUrl = await SaveVideoAndGetUrl(videoDto.VideoFile);

                // Asignar la URL de la imagen al DTO
                videoDto.Url = videoUrl;

                // Mapear el DTO a la entidad y guardarla en la base de datos
                var videoEntity = _mapper.Map<Video>(videoDto);
                var imageCreated = await _videoRepository.Crear(videoEntity);

                // Asignar el ID de la imagen creada en la base de datos al DTO
                videoDto.IdVideo = imageCreated.IdVideo;

                return videoDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(VideoDTO videoDto)
        {
            try
            {
                if (videoDto.VideoFile != null && videoDto.VideoFile.Length > 0)
                {
                    if (!EsVideo(videoDto.VideoFile))
                        throw new Exception("El archivo no es un video");


                    var videoUrl = await SaveVideoAndGetUrl(videoDto.VideoFile);
                    videoDto.Url = videoUrl;
                }

                var videoExistente = await _videoRepository.ObtenerById(videoDto.IdVideo);
                if(videoExistente == null)
                    throw new Exception("El video no existe");

                videoExistente.Name = videoDto.Name;
                videoExistente.Description = videoDto.Description;
                videoExistente.Url = videoDto.Url;

                var videoActualizado = await _videoRepository.Editar(videoExistente);

                return videoActualizado;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var videoQuery = await _videoRepository.Obtener(v => v.IdVideo == id);
                if (videoQuery == null)
                    throw new Exception("El video no existe");

                var videoEliminado = await _videoRepository.Eliminar(videoQuery);

                return videoEliminado;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
