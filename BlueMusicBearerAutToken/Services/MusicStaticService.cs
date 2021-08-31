using BlueMusicBearerAutToken.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BlueMusicBearerAutToken.Services
{
    public class MusicStaticService : IMusicService
    {
        public List<Music> All()
        {
            List<Music> listaMusic = new();

            string[] Names = new string[] { "Transparent Soul”", "Best Friend", "Yonaguni", "Miénteme", "Higher Power", "Astronaut In The Ocean", "Leave Before You Love Me", "Count On Me", "Your Power", "Solar Power" };
            string[] Authors = new string[] { "Willow e Travis Barker", "Saweetie e Doja Cat", "Bad Bunny", "TINI e Maria Becerra", " Coldplay", "Masked Wolf", "Marshmello e Jonas Brothers", "Brockhampton", "Billie Eilish", "Lorde" };
            int[] Durations = new int[] { 160, 180, 240, 300, 360, 420, 500, 620, 120, 280 };

            Random rand = new();
            var Name = $"{Names[rand.Next(0, Names.Length)]}";
            var Author = $"{Authors[rand.Next(0, Authors.Length)]}";
            var Duration = $"{Durations[rand.Next(0, Durations.Length)]}";

            for (int i = 0; i < 10; i++)
            {
                listaMusic.Add(new Music { Name = Name, Author = Author, Duration = Convert.ToInt32(Duration), Link = "" });
            }

            return listaMusic;
        }

        public bool Create(Music m, string user)
        {
            try
            {
                List<Music> musicList = All();
                m.Id = musicList.Count() + 1;
                musicList.Add(m);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Music Get(int? id)
        {
            return All().FirstOrDefault(m => m.Id == id);
        }

        public bool Update(Music m, string user)
        {
            try
            {
                List<Music> musicList = All();
                Music updatedMusic = musicList.Find(music => music.Id == m.Id);
                m = updatedMusic;
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
                List<Music> musicList = All();
                Music deletedMusic = musicList.FirstOrDefault(m => m.Id == id);
                musicList.Remove(deletedMusic);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Create(Music m)
        {
            throw new NotImplementedException();
        }

        public bool Update(Music m)
        {
            throw new NotImplementedException();
        }

        public List<Music> MusicsByUserRole(string getRole)
        {
            throw new NotImplementedException();
        }
    }
}
