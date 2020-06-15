namespace MarketplaceV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class messageeseen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chats", "Seen", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Chats", "Seen");
        }
    }
}
