using System.ComponentModel.DataAnnotations;

namespace PortfolioAppBackend.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password es requerido")]
        public string Password { get; set; }
    }
}
