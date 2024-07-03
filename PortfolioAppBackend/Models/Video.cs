using System.ComponentModel.DataAnnotations;

namespace PortfolioAppBackend.Models
{
    public class Video
    {
        [Key]
        public int IdVideo { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Url { get; set; } = null!;
        public DateTime? FechaRegistro { get; set; }
    }
}
