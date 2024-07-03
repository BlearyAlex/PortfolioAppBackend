using Microsoft.EntityFrameworkCore;
using PortfolioAppBackend.Models;

namespace PortfolioAppBackend.Data.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
