namespace MarketplaceV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IGuserBcmeBlogger : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DataTime", c => c.DateTime(nullable: true));
            AddColumn("dbo.AspNetUsers", "IsBlogger", c => c.Boolean(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsBlogger");
            DropColumn("dbo.AspNetUsers", "DataTime");
        }
    }
}
