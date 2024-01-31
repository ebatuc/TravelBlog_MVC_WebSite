namespace TravelBlogMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commtabler : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Comment = c.String(nullable: false, maxLength: 250),
                        BlogPostsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogPosts", t => t.BlogPostsId, cascadeDelete: true)
                .Index(t => t.BlogPostsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "BlogPostsId", "dbo.BlogPosts");
            DropIndex("dbo.Comments", new[] { "BlogPostsId" });
            DropTable("dbo.Comments");
        }
    }
}
