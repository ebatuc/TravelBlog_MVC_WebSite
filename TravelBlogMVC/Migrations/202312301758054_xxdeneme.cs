namespace TravelBlogMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xxdeneme : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Content = c.String(nullable: false),
                        ResimURL = c.String(),
                        DatePosted = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 100),
                        Role = c.String(nullable: false, maxLength: 50),
                        RegistrationDate = c.DateTime(nullable: false),
                        ImgUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogPosts", "UserId", "dbo.User");
            DropForeignKey("dbo.BlogPosts", "CityId", "dbo.Cities");
            DropIndex("dbo.BlogPosts", new[] { "CityId" });
            DropIndex("dbo.BlogPosts", new[] { "UserId" });
            DropTable("dbo.User");
            DropTable("dbo.Cities");
            DropTable("dbo.BlogPosts");
        }
    }
}
