using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAppBackend.Data.DBContext;
using PortfolioAppBackend.DTO;
using PortfolioAppBackend.Models;
using PortfolioAppBackend.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace PortfolioAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenServicio _tokenServicio;

        public UsuarioController(ApplicationDbContext context, IUsuarioService usuarioService, ITokenServicio tokenServicio)
        {
            _context = context;
            _usuarioService = usuarioService;
            _tokenServicio = tokenServicio;
        }

        [HttpPost]
        [Route("Registrar")]
        public async Task<ActionResult<UsuarioDto>> Registrar(RegistroDTO registroDto)
        {
            if (await _usuarioService.UsuarioExiste(registroDto.Email)) return BadRequest("El email ya esta registrado");

            using var hmac = new HMACSHA512();
            var usuario = new Usuario
            {
                Name = registroDto.Name.ToLower(),
                Email = registroDto.Email.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registroDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return new UsuarioDto
            {
                Email = usuario.Email,
                Token = _tokenServicio.CrearToken(usuario)
            };
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<UsuarioDto>> Login(LoginDTO loginDto)
        {
            var usuario = await _context.Usuarios.SingleOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());
            if(usuario == null) return Unauthorized("Email invalido");

            using var hmac = new HMACSHA512(usuario.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != usuario.PasswordHash[i]) return Unauthorized("Password no válido");
            }
            return new UsuarioDto
            {
                Email = usuario.Email,
                Token = _tokenServicio.CrearToken(usuario)
            };
        }
    }
}
