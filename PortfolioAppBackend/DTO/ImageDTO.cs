namespace PortfolioAppBackend.DTO
{
    public class ImageDTO
    {
        public int IdImage { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public string Type { get; set; } = null!;
        public IFormFile ImageFile { get; set; }
    }
}
