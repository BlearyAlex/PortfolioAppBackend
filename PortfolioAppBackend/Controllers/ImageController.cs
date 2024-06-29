using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioAppBackend.DTO;
using PortfolioAppBackend.Services.Interfaces;
using System.Security.Claims;

namespace PortfolioAppBackend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<ActionResult<List<ImageDTO>>> GetImages()
        {
            var images = await _imageService.Lista();
            return Ok(images);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        public async Task<ActionResult<ImageDTO>> GetImage(int id)
        {
            try
            {
                var image = await _imageService.GetById(id);
                if (image != null)
                {
                    return Ok(image);
                }

                return NotFound("No se encontró la imagen");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("Crear")]
        public async Task<ActionResult> CreateImage([FromForm] ImageDTO imageDto)
        {
            try
            {
                var createdImage = await _imageService.Create(imageDto);

                if (createdImage != null)
                {
                    // Asegúrate de que createdImage.IdImage contenga el ID asignado correctamente
                    return CreatedAtAction(nameof(GetImage), new { id = createdImage.IdImage }, createdImage);
                }

                return BadRequest("Error al crear la imagen.");

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Editar/{id:int}")]
        public async Task<ActionResult> UpdateImage([FromForm] ImageDTO imageDto, int id)
        {
            try
            {
                // Setear el Id de la imagen en el DTO
                imageDto.IdImage = id;

                var editSuccess = await _imageService.Update(imageDto);

                if (editSuccess)
                {
                    return Ok("Imagen editada correctamente");
                }

                return BadRequest("Error al editar la imagen.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<ActionResult> DeleteImage(int id)
        {
            var result = await _imageService.Delete(id);
            if (result)
            {
                return NoContent();
            }

            return NotFound("No se encontró la imagen");
        }
    }
}
