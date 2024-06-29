using System.ComponentModel.DataAnnotations;

namespace PortfolioAppBackend.DTO
{
    public class RegistroDTO
    {
        [Required(ErrorMessage ="Name es requerido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password es requerido")]
        public string Password { get; set; }
    }
}
