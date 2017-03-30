namespace HeroSaga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Battles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CharacterId = c.Int(nullable: false),
                        EnemyId = c.Int(nullable: false),
                        IsWon = c.Boolean(),
                        DateCreated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ExpGained = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Characters", t => t.CharacterId)
                .ForeignKey("dbo.Characters", t => t.EnemyId)
                .Index(t => t.CharacterId)
                .Index(t => t.EnemyId);
            
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Desc = c.String(maxLength: 280, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CharactersStats",
                c => new
                    {
                        CharacterId = c.Int(nullable: false),
                        StatId = c.Int(nullable: false),
                        Value = c.Int(),
                    })
                .PrimaryKey(t => new { t.CharacterId, t.StatId })
                .ForeignKey("dbo.Stats", t => t.StatId)
                .ForeignKey("dbo.Characters", t => t.CharacterId)
                .Index(t => t.CharacterId)
                .Index(t => t.StatId);
            
            CreateTable(
                "dbo.Stats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CharactersStats", "CharacterId", "dbo.Characters");
            DropForeignKey("dbo.CharactersStats", "StatId", "dbo.Stats");
            DropForeignKey("dbo.Battles", "EnemyId", "dbo.Characters");
            DropForeignKey("dbo.Battles", "CharacterId", "dbo.Characters");
            DropIndex("dbo.CharactersStats", new[] { "StatId" });
            DropIndex("dbo.CharactersStats", new[] { "CharacterId" });
            DropIndex("dbo.Battles", new[] { "EnemyId" });
            DropIndex("dbo.Battles", new[] { "CharacterId" });
            DropTable("dbo.Stats");
            DropTable("dbo.CharactersStats");
            DropTable("dbo.Characters");
            DropTable("dbo.Battles");
        }
    }
}
