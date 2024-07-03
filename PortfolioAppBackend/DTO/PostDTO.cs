namespace PortfolioAppBackend.DTO
{
    public class PostDTO
    {
        public int IdPost { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? UrlImage { get; set; }
        public IFormFile PostFile { get; set; }
    }
}
