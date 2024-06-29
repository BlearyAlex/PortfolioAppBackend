using System.ComponentModel.DataAnnotations;

namespace PortfolioAppBackend.Models
{
    public class Usuario
    {
        [Key]
        public int idUsuario { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
