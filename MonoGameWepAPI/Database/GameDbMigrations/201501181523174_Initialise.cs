namespace MonoGameWepAPI.Database.GameDbMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialise : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DbGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameTitle = c.String(),
                        GameDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DbGameAchievements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        AchivementImageUrl = c.String(),
                        DbGame_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbGames", t => t.DbGame_Id)
                .Index(t => t.DbGame_Id);
            
            CreateTable(
                "dbo.DbPlayers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        GamerTag = c.String(),
                        FaceBookID = c.String(),
                        DbGame_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbGames", t => t.DbGame_Id)
                .Index(t => t.DbGame_Id);
            
            CreateTable(
                "dbo.InGameAchievements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAchieved = c.DateTime(nullable: false),
                        Achievement_Id = c.Int(),
                        DbPlayer_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbGameAchievements", t => t.Achievement_Id)
                .ForeignKey("dbo.DbPlayers", t => t.DbPlayer_Id)
                .Index(t => t.Achievement_Id)
                .Index(t => t.DbPlayer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DbPlayers", "DbGame_Id", "dbo.DbGames");
            DropForeignKey("dbo.InGameAchievements", "DbPlayer_Id", "dbo.DbPlayers");
            DropForeignKey("dbo.InGameAchievements", "Achievement_Id", "dbo.DbGameAchievements");
            DropForeignKey("dbo.DbGameAchievements", "DbGame_Id", "dbo.DbGames");
            DropIndex("dbo.InGameAchievements", new[] { "DbPlayer_Id" });
            DropIndex("dbo.InGameAchievements", new[] { "Achievement_Id" });
            DropIndex("dbo.DbPlayers", new[] { "DbGame_Id" });
            DropIndex("dbo.DbGameAchievements", new[] { "DbGame_Id" });
            DropTable("dbo.InGameAchievements");
            DropTable("dbo.DbPlayers");
            DropTable("dbo.DbGameAchievements");
            DropTable("dbo.DbGames");
        }
    }
}
