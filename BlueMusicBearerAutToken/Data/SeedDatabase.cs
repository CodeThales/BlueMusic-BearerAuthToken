using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;


namespace BlueMusicBearerAutToken.Data
{
    public static class SeedDatabase
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new BlueMusicContext(
                serviceProvider.GetRequiredService<DbContextOptions<BlueMusicContext>>()))
            {
                if (context.Music.Any())
                {
                    return;
                }

                //if (!context.Music.Any())
                //{
                //    context.Music.Add(new Music { Name = "Aways", Author = "Bon Jovi", Duration = 180, Link = "", CreatedBy = "System" });
                //    context.Music.Add(new Music { Name = "Hotel California", Author = "The Eagles", Duration = 360, Link = "", CreatedBy = "System" });
                //    context.Music.Add(new Music { Name = "Smells Like Teen Spirity", Author = "Nirvana", Duration = 240, Link = "", CreatedBy = "System" });
                //    context.Music.Add(new Music { Name = "Sultans Of Swings", Author = "Dire Straits", Duration = 210, Link = "", CreatedBy = "System" });
                //    context.Music.Add(new Music { Name = "Nothing Else Matter", Author = "Metalica", Duration = 0, Link = "", CreatedBy = "System" });
                //}

                context.SaveChanges();
            }
        }
    }
}
