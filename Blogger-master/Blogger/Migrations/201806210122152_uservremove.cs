namespace Blogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uservremove : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostDetails", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.PostDetails", new[] { "UserId" });
            DropColumn("dbo.PostDetails", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostDetails", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.PostDetails", "UserId");
            AddForeignKey("dbo.PostDetails", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
