namespace MonoGameWepAPI.Database.GameDbMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MonoGameWepAPI.Models.GameContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Database\GameDbMigrations";
        }

        protected override void Seed(MonoGameWepAPI.Models.GameContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}
            if (context.Games.Count() == 0)
            {
                context.Games.Add(new DbGame
                {
                    GameTitle = "Shooter",
                    GameDescription = "Simple shooter"
                });
                context.Games.Add(new DbGame
                {
                    GameTitle = "Platformer",
                    GameDescription = "Simple Platformer"
                });
            }
            context.SaveChanges();
            DbPlayer p = new DbPlayer
            {
                Id= Guid.NewGuid().ToString(),
                FaceBookID = "10205230159335993",
                GamerTag = "Legend",
            };
            try
            {
                if (context.Players.Count() == 0)
                {
                    context.Players.Add(p);
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            context.SaveChanges();
            //DbGameAchievement[] achievements = new DbGameAchievement[] {
            //        new DbGameAchievement { Description= "Starter", AchivementImageUrl = "Badges_0"},
            //        new DbGameAchievement { Description = "Finisher", AchivementImageUrl="Badge_1"}};
            
            foreach (DbGame g in context.Games)
            {
                if (g.GameAchievements == null)
                {
                    g.GameAchievements = new DbGameAchievement[] {new DbGameAchievement { Description = "Starter", AchivementImageUrl = "Badges_0" },
                    new DbGameAchievement { Description = "Finisher", AchivementImageUrl = "Badges_1" }};
                    
                }
            }
            context.SaveChanges();
            if (context.Players.First().PlayerAchievements == null )
            {
                context.Players.First().PlayerAchievements = new InGameAchievement[] {
                    new InGameAchievement
                    {
                        DateAchieved = DateTime.Now.AddDays(-2),
                        Achievement = context.Games.First().GameAchievements.First()
                    }};
            }
            context.SaveChanges();
        }
    }
}
