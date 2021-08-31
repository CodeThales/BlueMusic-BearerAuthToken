using BlueMusicBearerAutToken.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BlueMusicBearerAutToken.Data
{
    public class BlueMusicContext : IdentityDbContext
    {
        public BlueMusicContext(DbContextOptions<BlueMusicContext> options) : base(options) { }
        public DbSet<Music> Music { get; set; }

    }
}
