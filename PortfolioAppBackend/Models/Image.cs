using System.ComponentModel.DataAnnotations;

namespace PortfolioAppBackend.Models
{
    public class Image
    {
        [Key]
        public int IdImage { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public string Type { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }
    }
}
