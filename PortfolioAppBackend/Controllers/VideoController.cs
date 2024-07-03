using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioAppBackend.DTO;
using PortfolioAppBackend.Services.Interfaces;

namespace PortfolioAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;

        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<ActionResult<List<VideoDTO>>> GetVideos()
        {
            var video = await _videoService.Lista();
            return Ok(video);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        public async Task<ActionResult<VideoDTO>> GetVideoById(int id)
        {
            try
            {
                var video = await _videoService.ObtenerById(id);
                if (video != null)
                {
                    return Ok(video);
                }

                return BadRequest("No es encontro la imagen");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("Crear")]
        public async Task<ActionResult<VideoDTO>> CreateVideo([FromForm] VideoDTO videoDto)
        {
            try
            {
                var createdVideo = await _videoService.Crear(videoDto);
                if (createdVideo != null)
                {
                    return CreatedAtAction(nameof(GetVideoById), new { id = createdVideo.IdVideo }, createdVideo);
                }

                return BadRequest("Error al crear video");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        [Route("Editar/{id:int}")]
        public async Task<ActionResult<VideoDTO>> EditVideo([FromForm] VideoDTO videoDto, int id)
        {
            try
            {
                videoDto.IdVideo = id;

                var videoUpdated = await _videoService.Editar(videoDto);
                
                if(videoUpdated)
                {
                    return Ok("Video editado exitosamente");
                }

                return BadRequest("Error al editar el video");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<ActionResult<VideoDTO>> DeleteVideo(int id)
        {
            try
            {
                var videoDelete = await _videoService.Eliminar(id);
                if (videoDelete)
                {
                    return Ok("Video eliminado exitosamente");
                }

                return BadRequest("Errro al eliminar el video");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
