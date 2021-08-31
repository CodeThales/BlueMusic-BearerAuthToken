using BlueMusicBearerAutToken.Data;
using BlueMusicBearerAutToken.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BlueMusicBearerAutToken.Services
{
    public class MusicService : IMusicService
    {
        BlueMusicContext _context;

        public MusicService(BlueMusicContext context)
        {
            _context = context;
        }

        public List<Music> All()
        {
            return _context.Music.ToList();
        }

        public Music Get(int? id)
        {
            return _context.Music.FirstOrDefault(m => m.Id == id);
        }

        public bool Create(Music m)
        {
            try
            {
                m.Created = DateTime.Now;
                _context.Add(m);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Music m)
        {
            try
            {
                if (!_context.Music.Any(music => music.Id == m.Id))
                    throw new Exception("A música solicitado não consta em nossa base de dados!");

                m.Updated = DateTime.Now;                
                _context.Update(m);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }

        public bool Delete(int? id)
        {
            try
            {
                _context.Remove(this.Get(id));
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Music> MusicsByUserRole(string getRole)
        {
            var query = from music in _context.Set<Music>()
                        join user in _context.Set<IdentityUser>()
                            on music.CreatedById equals user.Id
                        join userRoles in _context.Set<IdentityUserRole<string>>()
                            on user.Id equals userRoles.UserId
                        join role in _context.Set<IdentityRole>()
                            on userRoles.RoleId equals role.Id
                        where role.Name.ToUpper() == getRole
                        select new Music()
                        {
                            Id = music.Id,
                            Name = music.Name,
                            Author = music.Author,
                            Duration = music.Duration,
                            Link = music.Link,
                            Created = music.Created,
                            Updated = music.Updated,
                            CreatedById = music.CreatedById,
                            UpdatedById = music.UpdatedById
                        };

            return query.ToList();
        }        
    }
}
