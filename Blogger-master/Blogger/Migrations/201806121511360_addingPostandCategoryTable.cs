namespace Blogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingPostandCategoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.PostDetails",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Post_Content = c.String(nullable: false),
                        Create_time = c.DateTime(nullable: false),
                        Tages = c.String(nullable: false),
                        FeaturedImage = c.String(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.CategoryId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostDetails", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostDetails", "CategoryId", "dbo.Categories");
            DropIndex("dbo.PostDetails", new[] { "UserId" });
            DropIndex("dbo.PostDetails", new[] { "CategoryId" });
            DropTable("dbo.PostDetails");
            DropTable("dbo.Categories");
        }
    }
}
