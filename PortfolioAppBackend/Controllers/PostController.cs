using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioAppBackend.DTO;
using PortfolioAppBackend.Services.Interfaces;

namespace PortfolioAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<ActionResult<List<PostDTO>>> GetPosts()
        {
            var posts = await _postService.Lista();
            return Ok(posts);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        public async Task<ActionResult<PostDTO>> GetPost(int id)
        {
            try
            {
                var post = await _postService.GetById(id);
                if (post != null)
                {
                    return Ok(post);
                }

                return NotFound("No se encontró el post");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("Crear")]
        public async Task<ActionResult<PostDTO>> CreatePost([FromForm] PostDTO postDto)
        {
            try
            {
                var createsPost = await _postService.Create(postDto);

                if (postDto != null)
                {
                    return CreatedAtAction(nameof(GetPost), new { id = postDto.IdPost }, postDto);
                }

                return BadRequest("No se pudo crear el post");

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        [Route("Editar/{id:int}")]
        public async Task<ActionResult> UpdatePost([FromForm] PostDTO postDto, int id)
        {
            try
            {
                postDto.IdPost = id;

                var updatedPost = await _postService.Update(postDto);

                if (updatedPost)
                {
                    return Ok("Post actualizado");
                }

                return BadRequest("No se pudo actualizar el post");

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            try
            {
                var deletedPost = await _postService.Delete(id);

                if (deletedPost)
                {
                    return Ok("Post eliminado");
                }

                return BadRequest("No se pudo eliminar el post");

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
