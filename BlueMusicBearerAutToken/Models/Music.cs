using Microsoft.AspNetCore.Identity;
using System;


namespace BlueMusicBearerAutToken.Models
{
    public class Music
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Duration { get; set; }
        public string Link { get; set; }

        
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedById { get; set; }
        public IdentityUser UpdatedBy { get; set; }
        public string CreatedById { get; set; }
        public IdentityUser CreatedBy { get; set; }

    }
}
