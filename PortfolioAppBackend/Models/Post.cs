using System.ComponentModel.DataAnnotations;

namespace PortfolioAppBackend.Models
{
    public class Post
    {
        [Key]
        public int IdPost { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string UrlImage { get; set; } = null!;
        public DateTime? FechaRegistro { get; set; }
    }
}
