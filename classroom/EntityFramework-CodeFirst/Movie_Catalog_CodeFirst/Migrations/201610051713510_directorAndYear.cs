namespace Movie_Catalog_CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class directorAndYear : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        DirectorId = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.DirectorId);
            
            AddColumn("dbo.Movies", "ReleaseDate", c => c.DateTime());
            AddColumn("dbo.Movies", "DirectorId", c => c.Int());
            CreateIndex("dbo.Movies", "DirectorId");
            AddForeignKey("dbo.Movies", "DirectorId", "dbo.Directors", "DirectorId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "DirectorId", "dbo.Directors");
            DropIndex("dbo.Movies", new[] { "DirectorId" });
            DropColumn("dbo.Movies", "DirectorId");
            DropColumn("dbo.Movies", "ReleaseDate");
            DropTable("dbo.Directors");
        }
    }
}
