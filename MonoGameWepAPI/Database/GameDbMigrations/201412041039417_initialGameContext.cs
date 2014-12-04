namespace MonoGameWepAPI.Database.GameDbMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialGameContext : DbMigration
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
                        AchievmentId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        AchivementImageUrl = c.String(),
                        DbGame_Id = c.Int(),
                        DbPlayer_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AchievmentId)
                .ForeignKey("dbo.DbGames", t => t.DbGame_Id)
                .ForeignKey("dbo.DbPlayers", t => t.DbPlayer_Id)
                .Index(t => t.DbGame_Id)
                .Index(t => t.DbPlayer_Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DbPlayers", "DbGame_Id", "dbo.DbGames");
            DropForeignKey("dbo.DbGameAchievements", "DbPlayer_Id", "dbo.DbPlayers");
            DropForeignKey("dbo.DbGameAchievements", "DbGame_Id", "dbo.DbGames");
            DropIndex("dbo.DbPlayers", new[] { "DbGame_Id" });
            DropIndex("dbo.DbGameAchievements", new[] { "DbPlayer_Id" });
            DropIndex("dbo.DbGameAchievements", new[] { "DbGame_Id" });
            DropTable("dbo.DbPlayers");
            DropTable("dbo.DbGameAchievements");
            DropTable("dbo.DbGames");
        }
    }
}
