namespace PortfolioAppBackend.DTO
{
    public class VideoDTO
    {
        public int IdVideo { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Url { get; set; }
        public IFormFile VideoFile { get; set; }
    }
}
