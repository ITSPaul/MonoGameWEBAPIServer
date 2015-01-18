using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MonoGameWepAPI.Models
{

    public class GameContext : DbContext
    {
        
        public DbSet<DbPlayer> Players { get; set; }
        public DbSet<DbGame> Games { get; set; }
        public GameContext()
            : base("name=DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    }

    public class DbGame
    {
        [Key]
        public int Id { get; set; }
        public string GameTitle { get; set; }
        public string GameDescription { get; set; }
        public virtual ICollection<DbPlayer> Played { get; set; }
        public virtual ICollection<DbGameAchievement> GameAchievements { get; set; }
    }

    public class DbPlayer
    {
        [Key]
        public string Id { get; set; }
        public string GamerTag { get; set; }
        public string FaceBookID { get; set; }
        public virtual ICollection<InGameAchievement> PlayerAchievements { get; set; }
    }
    
    public class DbGameAchievement
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string AchivementImageUrl { get; set; }
    
    }

    public class InGameAchievement
    {
        [Key]
        public int Id { get; set; }
        public virtual DbGameAchievement Achievement { get; set; }
        public DateTime DateAchieved { get; set; }
    }


}