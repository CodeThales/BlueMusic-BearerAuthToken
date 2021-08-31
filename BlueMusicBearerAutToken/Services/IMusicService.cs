using BlueMusicBearerAutToken.Models;
using System.Collections.Generic;


namespace BlueMusicBearerAutToken.Services
{
    public interface IMusicService
    {
        List<Music> All();
        Music Get(int? id);
        bool Create(Music m);
        bool Update(Music m);
        bool Delete(int? id);
        List<Music> MusicsByUserRole(string getRole);
    }
}
